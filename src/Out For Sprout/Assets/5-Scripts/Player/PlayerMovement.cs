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
    private Camera camera;
    
    [SerializeField] private AnimationCurve speedCurveByPercentageOfMap;
    [SerializeField] private FMOD_Instantiator rootSound;
    [SerializeField] private float maxReferenceSpeedToSound;

    private List<SpeedBuff> activeBuffs = new List<SpeedBuff>();
    private List<SpeedBuff> activeDebuffs = new List<SpeedBuff>();

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        camera = Camera.main;
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
        UpdateSoundParams();
    }

    private void UpdateBuffs()
    {
        UpdateBuffTimers(activeBuffs);
        UpdateBuffTimers(activeDebuffs);
    }

    private void UpdateBuffTimers(List<SpeedBuff> buffs)
    {
        for (int i = 0; i < buffs.Count; i++)
        {
            buffs[i].timer += Time.deltaTime;
            if (buffs[i].timer >= buffs[i].totalTime)
            {
                buffs.RemoveAt(i);
                i--;
            }
        }
    }

    private void UpdateMovingDirection()
    {
        var inputDirection = gameActions.Player.Move.ReadValue<Vector2>();
        var getSpeedBonus = GetSpeedBonusByBuffs(activeBuffs);
        var getSpeedSLowdown = GetSpeedBonusByBuffs(activeDebuffs);
        
        var newVelocity = Mathf.Max(0, GetSpeedForProgress() + getSpeedBonus + getSpeedSLowdown);

        var finalDirection = lastDirection;
        if (inputDirection.magnitude > 0)
        {
            finalDirection = inputDirection.normalized;
        }
        
        rigidbody.velocity = newVelocity * finalDirection;
        lastDirection = rigidbody.velocity.normalized;
    }

    private float GetSpeedBonusByBuffs(List<SpeedBuff> buffs)
    {
        var totalSpeedBonus = 0f;
        foreach (var buff in buffs)
        {
            var alpha = 1.0f - (buff.timer / buff.totalTime);
            totalSpeedBonus += buff.startSpeedBonus * alpha;
        }

        return totalSpeedBonus;
    }
    
    private void UpdateSoundParams()
    {
        var viewHalfHeight = camera.orthographicSize;
        var viewHalfWidth = camera.aspect * viewHalfHeight;
        var panValue = Mathf.Clamp(transform.position.x / viewHalfWidth, -1f, 1f);
        rootSound.setParam("MoveSpeed", Mathf.Clamp01(rigidbody.velocity.magnitude / maxReferenceSpeedToSound));
        rootSound.setParam("Panning", panValue);
    }

    public void AddSpeedupBuff(float speedBonus, float fadeoutTime)
    {
        activeBuffs.Add(new SpeedBuff(fadeoutTime, speedBonus));
    }

    public void AddSpeedupDebuff(float speedPenalty, float fadeoutTime)
    {
        activeDebuffs.Add(new SpeedBuff(fadeoutTime, speedPenalty));
    }
}
