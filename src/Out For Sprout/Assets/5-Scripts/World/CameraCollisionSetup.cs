using System;
using UnityEngine;

public class CameraCollisionSetup : MonoBehaviour
{
    public Camera camera;
    public BoxCollider2D backTrigger;
    public BoxCollider2D leftCollision;
    public BoxCollider2D rightCollision;
    private float colliderHalfSize = 0.5f;
    
    private void Start()
    {
        var viewHalfHeight = camera.orthographicSize;
        var viewHalfWidth = camera.aspect * viewHalfHeight;

        SetupBackTrigger(viewHalfHeight, viewHalfWidth);
        SetLeftCollider(viewHalfHeight, viewHalfWidth);
        SetRightCollider(viewHalfHeight, viewHalfWidth);
    }

    private void SetupBackTrigger(float viewHalfHeight, float viewHalfWidth)
    {
        backTrigger.transform.localPosition = new Vector3(0, viewHalfHeight + colliderHalfSize, 0);
        backTrigger.transform.localScale = new Vector3(viewHalfWidth * 2, 1, 0);
    }
    
    private void SetLeftCollider(float viewHalfHeight, float viewHalfWidth)
    {
        leftCollision.transform.position = new Vector3(-viewHalfWidth - colliderHalfSize, 0, 0);
        leftCollision.transform.localScale = new Vector3(1, viewHalfHeight * 2, 1);
    }
    
    private void SetRightCollider(float viewHalfHeight, float viewHalfWidth)
    {
        rightCollision.transform.position = new Vector3(viewHalfWidth + colliderHalfSize, 0, 0);
        rightCollision.transform.localScale = new Vector3(1, viewHalfHeight * 2, 1);
    }
}
