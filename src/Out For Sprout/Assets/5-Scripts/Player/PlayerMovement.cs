using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    private GameActions gameActions;
    private Rigidbody2D rigidbody;

    public float baseVelocity;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        rigidbody.velocity = Vector2.down * baseVelocity;
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
        var direction = gameActions.Player.Move.ReadValue<Vector2>();
        if (direction.magnitude > 0)
        {
            rigidbody.velocity = rigidbody.velocity.magnitude * direction.normalized;
        }
    }
}
