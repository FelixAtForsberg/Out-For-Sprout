using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    private class SpeedBuff
    {
        public float timer;
        public float totalTime;
        public float startSpeedBonus;

        public SpeedBuff(float totalTime, float startSpeedBonus)
        {
            this.totalTime = totalTime;
            this.startSpeedBonus = startSpeedBonus;
        }
    }
    private GameActions gameActions;
    private Rigidbody2D rigidbody;
    private Vector2 lastDirection;

    [SerializeField] private AnimationCurve speedCurveByPercentageOfMap;

    private List<SpeedBuff> activeBuffs = new List<SpeedBuff>();

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        rigidbody.velocity = Vector2.down * GetSpeedForProgress();
        lastDirection = rigidbody.velocity.normalized;
        GameManager.Instance.OnPlayerWin.AddListener(DisableMovement);
    }

    private float GetSpeedForProgress()
    {
        var progressPercentage = ProgressTracker.Instance.GetFullProgressPercentage();
        return speedCurveByPercentageOfMap.Evaluate(progressPercentage);
    }

    private void DisableMovement()
    {
        enabled = false;
        rigidbody.velocity = Vector2.zero;
        rigidbody.isKinematic = true;
    }

    private void OnEnable()
    {
        gameActions = new GameActions();
        gameActions.Enable();
    }

    private void OnDisable()
    {
        gameActions.Disable();
        gameActions.Dispose();
    }

    private void FixedUpdate()
    {
        UpdateBuffs();
        UpdateMovingDirection();
    }

    private void UpdateBuffs()
    {
        for (int i = 0; i < activeBuffs.Count; i++)
        {
            activeBuffs[i].timer += Time.deltaTime;
            if (activeBuffs[i].timer >= activeBuffs[i].totalTime)
            {
                activeBuffs.RemoveAt(i);
                i--;
            }
        }
    }

    private void UpdateMovingDirection()
    {
        var inputDirection = gameActions.Player.Move.ReadValue<Vector2>();
        var getSpeedBonus = GetSpeedBonusByBuffs();
        
        var newVelocity = GetSpeedForProgress() + getSpeedBonus;

        var finalDirection = lastDirection;
        if (inputDirection.magnitude > 0)
        {
            finalDirection = inputDirection.normalized;
        }
        
        rigidbody.velocity = newVelocity * finalDirection;
        lastDirection = rigidbody.velocity.normalized;
    }

    private float GetSpeedBonusByBuffs()
    {
        var totalSpeedBonus = 0f;
        foreach (var buff in activeBuffs)
        {
            var alpha = 1.0f - (buff.timer / buff.totalTime);
            totalSpeedBonus += buff.startSpeedBonus * alpha;
        }

        return totalSpeedBonus;
    }

    public void AddSpeedupBuff(float speedBonus, float fadeoutTime)
    {
        activeBuffs.Add(new SpeedBuff(fadeoutTime, speedBonus));
    }
}
