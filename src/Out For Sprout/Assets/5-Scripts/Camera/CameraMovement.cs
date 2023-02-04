using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float cameraSpeed;
    [SerializeField] private float maxSpeedupMultiplierByPlayerVelocity;
    [SerializeField] [Range(0, 1)] private float nonSpeedupAreaPercentage = 0.5f;
    [SerializeField] AnimationCurve catchUpCurve;

    private Camera camera;

    private void Awake()
    {
        camera = GetComponent<Camera>();
    }
    
    private void Update()
    {
        MoveCameraByBaseSpeed();
        CatchUpToPlayer();
    }

    private void MoveCameraByBaseSpeed()
    {
        transform.position += Vector3.down * Time.deltaTime * cameraSpeed;
    }
    
    private void CatchUpToPlayer()
    {
        var furthestPlayer = GetFurthestPlayerYPosPlayer();
        if (furthestPlayer == null)
        {
            return;
        }
        
        var playerYPos = furthestPlayer.transform.position.y;
        var speedupStartWorldPos = GetSpeedupStartWorldPosition();
        if (playerYPos < speedupStartWorldPos)
        {
            SpeedupByDistance(furthestPlayer, playerYPos, speedupStartWorldPos);
        }
    }

    private GameObject GetFurthestPlayerYPosPlayer()
    {
        if (PlayerTracker.Instance == null)
        {
            Debug.Log("No tracker present");
            return null;
        }
        var players = PlayerTracker.Instance.GetPlayers();
        if (players.Count == 0)
        {
            return null;
        }
        
        var furthestPlayer = players[0];
        var furthestPos = furthestPlayer.transform.position.y;

        for (int i = 1; i < players.Count; i++)
        {
            furthestPos = Mathf.Min(furthestPos, players[i].transform.position.y);
        }

        return furthestPlayer;
    }

    private float GetSpeedupStartWorldPosition()
    {
        // TODO get map height
        var mapHeight = 10.0f;
        var halfHeight = mapHeight * 0.5f;
        var cameraTop = transform.position.y + halfHeight;
        var speedupStartOffset = mapHeight * nonSpeedupAreaPercentage;
        
        return cameraTop - speedupStartOffset;
    }
    
    private void SpeedupByDistance(GameObject furthestPlayer, float playerYPos, float speedupStartWorldPos)
    {
        // TODO get map height
        var mapHeight = 10.0f;
        var speedupRange = mapHeight * (1.0f - nonSpeedupAreaPercentage);
        var catchUpDistancePercentage = (speedupStartWorldPos - playerYPos) / speedupRange;

        var playerSpeed = furthestPlayer.GetComponent<Rigidbody2D>().velocity.magnitude;
        var maxCameraSpeed = maxSpeedupMultiplierByPlayerVelocity * playerSpeed;
        var speedAlpha = catchUpCurve.Evaluate(catchUpDistancePercentage);
        var catchUpSpeed = Mathf.Lerp(0, maxCameraSpeed, speedAlpha);
        transform.position += Vector3.down * Time.deltaTime * catchUpSpeed;
    }
}
