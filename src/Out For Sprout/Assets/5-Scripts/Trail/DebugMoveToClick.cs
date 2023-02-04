using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Thanks ChatGPT for providing the base
public class DebugMoveToClick : MonoBehaviour
{
    public float speed = 5f; // The speed at which the object moves towards the target
    public float forceZ = 0f;
    
    void Update()
    {
        // Check if the left mouse button was clicked
        if (Input.GetMouseButtonDown(0))
        {
            // Get the mouse click position in world space
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
            // Start moving the object towards the target position
            StopAllCoroutines();
            StartCoroutine(MoveObject(new Vector3(mousePos.x, mousePos.y, forceZ)));
        }
    }

    IEnumerator MoveObject(Vector3 targetPos)
    {
        while (Vector3.Distance(transform.position, targetPos) > 0.1f)
        {
            transform.position = Vector3.Lerp(transform.position, targetPos, speed * Time.deltaTime);
            yield return null;
        }
    }
}
