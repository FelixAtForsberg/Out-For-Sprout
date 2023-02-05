using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{

    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject progressPanel;
    [SerializeField] private GameObject joyStick;
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private Sprite pauseSprite;
    [SerializeField] private Sprite playSprite;
    [SerializeField] private TextMeshPro score;
    [SerializeField] private TextMeshPro bestScore;


    private bool isGameOver = false;
    private bool isPaused = false;
    
    void Awake(){
        Time.timeScale = 0; 
        
    }
    void Start()
    {
        gameOverPanel.SetActive(false);
        joyStick.SetActive(false);
        winPanel.SetActive(false);
        pausePanel.SetActive(false);
        progressPanel.SetActive(false);
        GameManager.Instance.OnPlayerDeath.AddListener(SetGameOver);             
    }



    void SetGameOver(){
        
        //Debug.Log(ProgressTracker.Instance.GetTimer());
        StartCoroutine(GameOverSequence());
        
    }

    public void StartGame(){
        Time.timeScale = 1; 
        joyStick.SetActive(true);
        progressPanel.SetActive(true);
    }

    public void ReloadScene(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame(){
        Application.Quit();
    }

    public void PauseResumeGame(){
        if (isPaused){
            Time.timeScale = 1;
            pausePanel.SetActive(false);
            joyStick.SetActive(true);
            pauseButton.GetComponent<Image>().sprite = pauseSprite;
            isPaused = false;
        }else{
            Time.timeScale = 0;
            pausePanel.SetActive(true);
            joyStick.SetActive(false);
            pauseButton.GetComponent<Image>().sprite = playSprite;
            isPaused = true;
        }
    }


    private IEnumerator GameOverSequence(){
        gameOverPanel.SetActive(true);
        progressPanel.SetActive(false);
        joyStick.SetActive(false);


        Time.timeScale = 0;
        yield return new WaitForSeconds(5.0f);

    }

    
}
