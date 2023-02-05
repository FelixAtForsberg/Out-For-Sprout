using System;
using UnityEngine;

public enum AudioPlayType
{
    Insect,
    Rock,
    UI,
    GameOver,
    Start,
    Water
}

public class AudioHandler : MonoBehaviour
{
    
    public static AudioHandler Instance;
    
    public FMOD_Instantiator insect;
    public FMOD_Instantiator rock;
    public FMOD_Instantiator ui;
    public FMOD_Instantiator gameOver;
    public FMOD_Instantiator start;
    public FMOD_Instantiator water;

    public FMOD_Instantiator backgroundSound;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        GameManager.Instance.OnGameStart.AddListener(OnGameStart);
        GameManager.Instance.OnPlayerWin.AddListener(OnPlayerWin);
        GameManager.Instance.OnNewLayer.AddListener(OnNewLayer);
    }

    private void OnNewLayer(int layer)
    {
        backgroundSound.setParam("Progression", layer + 1);
    }

    private void OnPlayerWin()
    {
        backgroundSound.setParam("Progression", 7);
    }

    private void OnGameStart()
    {
        backgroundSound.setParam("Progression", 1);
    }

    public void Play(AudioPlayType waterType)
    {
        switch (waterType)
        {
            case AudioPlayType.Insect:
                insect.playEvent();
                break;
            case AudioPlayType.Rock:
                rock.playEvent();
                break;
            case AudioPlayType.UI:
                ui.playEvent();
                break;
            case AudioPlayType.GameOver:
                gameOver.playEvent();
                break;
            case AudioPlayType.Start:
                start.playEvent();
                break;
            case AudioPlayType.Water:
                water.playEvent();
                break;
        }
    }
}
