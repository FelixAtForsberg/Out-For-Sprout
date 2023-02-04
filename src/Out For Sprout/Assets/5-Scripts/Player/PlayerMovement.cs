using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    private GameActions gameActions;
    private Rigidbody2D rigidbody;
    private Vector2 lastDirection;

    public float baseVelocity;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        rigidbody.velocity = Vector2.down * baseVelocity;
        lastDirection = rigidbody.velocity.normalized;
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
        UpdateMovingDirection();
    }

    private void UpdateMovingDirection()
    {
        var inputDirection = gameActions.Player.Move.ReadValue<Vector2>();
        var newVelocity = Mathf.Max(rigidbody.velocity.magnitude, baseVelocity);

        var finalDirection = lastDirection;
        if (inputDirection.magnitude > 0)
        {
            finalDirection = inputDirection.normalized;
        }
        
        rigidbody.velocity = newVelocity * finalDirection;
        lastDirection = rigidbody.velocity.normalized;
    }
}
