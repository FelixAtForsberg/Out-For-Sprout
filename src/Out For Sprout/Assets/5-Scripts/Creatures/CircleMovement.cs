using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody2D))]
public class CircleMovement : MonoBehaviour
{
    [FormerlySerializedAs("startingTourque")] public float angularVelocity;
    public float startingVelocity;
    private Rigidbody2D rigid2d;

    private void Awake()
    {
        rigid2d = GetComponent<Rigidbody2D>();
        rigid2d.angularVelocity = (Random.Range(0, 2) * 2 - 1) * angularVelocity;
        transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
    }

    private void FixedUpdate()
    {
        rigid2d.velocity = transform.right * startingVelocity;
    }
}
