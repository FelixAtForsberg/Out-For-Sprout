using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    [SerializeField] private GameObject gameOverPanel;

    private bool isGameOver = false;
    
    void Awake(){
        Time.timeScale = 0; 
    }
    void Start()
    {
        gameOverPanel.SetActive(false); 
             
    }

    // Update is called once per frame
    void Update()
    {
        //TODO set game over conditions
        if(Input.GetKeyDown(KeyCode.G) && !isGameOver){

            isGameOver = true;

            StartCoroutine(GameOverSequence());

        }

        if(isGameOver){

            //reset game
            if(Input.GetKey(KeyCode.R)){
                ReloadScene();
            }

            //quit game
            if(Input.GetKeyDown(KeyCode.Q)){
                QuitGame();
            }

        }
    }

    public void StartGame(){
        Time.timeScale = 1;   
    }

    public void ReloadScene(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame(){
        Application.Quit();
    }
   

    private IEnumerator GameOverSequence(){
        gameOverPanel.SetActive(true);
        Time.timeScale = 0;
        yield return new WaitForSeconds(5.0f);

    }

    
}
