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
    [SerializeField] private GameObject score;
    [SerializeField] private GameObject bestScore;


    //private bool isGameOver = false;
    private bool isPaused = false;
    
    void Awake(){
        Time.timeScale = 0; 
        
    }
    void Start()
    {
        gameOverPanel.SetActive(false);
        joyStick.SetActive(false);
        winPanel.SetActive(false);


        GameManager.Instance.OnPlayerDeath.AddListener(SetGameOver);    
        GameManager.Instance.OnPlayerWin.AddListener(SetWin);      
    

        pausePanel.SetActive(false);
        progressPanel.SetActive(false);
        GameManager.Instance.OnPlayerDeath.AddListener(SetGameOver);             

    }

    void Update(){

        if(Input.GetKeyDown(KeyCode.A)){

            SetWin();
        }
    }

    void SetGameOver(){
        
        StartCoroutine(GameOverSequence());
        
    }

    void SetWin(){
        Time.timeScale = 0;
        winPanel.SetActive(false);
        joyStick.SetActive(false);
        progressPanel.SetActive(false);

        float timer = ProgressTracker.Instance.GetTimer();
        score.GetComponent<TMP_Text>().text = Timeformat(timer);

        
        float bestFloat = PlayerPrefs.GetFloat("myFloat", float.MaxValue);
        if(bestFloat >= float.MaxValue){
            bestScore.GetComponent<TMP_Text>().text = "";
        }
        if(bestFloat>timer){
            
            PlayerPrefs.SetFloat("myFloat", timer);
            bestScore.GetComponent<TMP_Text>().text = Timeformat(timer);
        }
        if(bestFloat<timer){
            bestScore.GetComponent<TMP_Text>().text = Timeformat(bestFloat);

        }        

    }

    private string Timeformat(float timer){
        int minutes = Mathf.FloorToInt(timer / 60F);
        int seconds = Mathf.FloorToInt(timer - minutes * 60);
        string time = string.Format("{0:00}:{1:00}", minutes, seconds);
        return time;
    }

    public void StartGame(){
        Time.timeScale = 1; 
        joyStick.SetActive(true);
        progressPanel.SetActive(true);
        GameManager.Instance.StartGame();
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
