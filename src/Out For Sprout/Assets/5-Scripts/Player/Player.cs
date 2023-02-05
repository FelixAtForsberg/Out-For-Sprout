using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerMovement playerMovement;
    [SerializeField] private FMOD_Instantiator playerSound;
    
    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Start()
    {
        GameManager.Instance.OnGameStart.AddListener(OnGameStart);
    }

    private void OnGameStart()
    {
        playerSound.playEvent();
    }

    private void OnEnable()
    {
        if (PlayerTracker.Instance != null)
        {
            PlayerTracker.Instance.RegisterPlayer(gameObject);
        }
    }

    private void OnDisable()
    {
        if (PlayerTracker.Instance != null)
        {
            PlayerTracker.Instance.DeregisterPlayer(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<DeathTrigger>())
        {
            playerSound.stopEventWithFade();
            GameManager.Instance.TriggerPlayerDeath();
        }
        if (col.GetComponent<VictoryTrigger>())
        {
            playerSound.stopEventWithFade();
            GameManager.Instance.TriggerVictory();
        }

        var speedPickup = col.GetComponent<PickupTrigger>();
        if (speedPickup != null)
        {
            speedPickup.TriggerPickup(this);
        }
    }
}
