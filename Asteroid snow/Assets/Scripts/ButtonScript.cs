using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    public GameObject startingScreen;
    public GameObject timerForRoll;
    public BallMovement ballRef;

    public GameObject fakeHead;
    public GameObject realHead;
    public GameObject winLossObj;
    private void Awake()
    {
        //Time.timeScale = 0;
        
    }
    public void StartGame()
    {
        fakeHead.SetActive(false);//Yeah this is terrible coding lol but it scuff works :)
        realHead.SetActive(true);
        startingScreen.SetActive(false);
        timerForRoll.SetActive(true);
        winLossObj.GetComponent<WinLoseCondition>().gameStarted = true;
        ballRef.startGame = true;
        Time.timeScale = 1.0f;
        //start music player here
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
