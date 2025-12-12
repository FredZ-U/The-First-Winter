using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class WinLoseCondition : MonoBehaviour
{
    //public Light deathLight;
    public GameObject snowBallRef;
    private Vector3 snowBallTransform;
    float averageSnowBallScale;
    public TextMeshProUGUI timeAliveText;
    public GameObject gameTimeUI;
    public GameObject loseUI;
    public TextMeshProUGUI timeAliveEnd;
    private float currentTime = 0;
    public bool gameStarted;

    /*public float maxIntensity = 60f;
    public float minIntensity = 0.0f;
    public float maxScale = 1f;
    public float minScale = 0.1f;*/

    //bad name for the script but it controls game state and conditions

    //This is a god aweful script I am very much panic coding rn lol
    // Update is called once per frame
    void Update()
    {
        /*//light changes depending on player scale
        float currentScale = snowBallRef.transform.localScale.x; 
        float scaleRatio = Mathf.InverseLerp(minScale, maxScale, currentScale);
        deathLight.intensity = Mathf.Lerp(maxIntensity, minIntensity, scaleRatio);*/

        //Temp, calc distance traveled
        if (gameStarted)
        {
            //currentTime += Time.deltaTime;
            timeAliveText.text = snowBallRef.transform.position.z.ToString("0");
        }

        snowBallTransform = new Vector3(snowBallRef.transform.localScale.x, snowBallRef.transform.localScale.y, snowBallRef.transform.localScale.z);
        averageSnowBallScale = (snowBallTransform.x + snowBallTransform.y + snowBallTransform.z) / 3;
        if (averageSnowBallScale <= .2f)//activate lose condition and screen here
            LoseScreen();
        
        //deathLight.intensity += 10;
        //Lose when ball gets too small 
        //make the screen go red the smaller the ball gets maybe???
    }

    public void LoseScreen()
    {
        Time.timeScale = 0;
        //gameTimeUI.SetActive(false);
        loseUI.SetActive(true);
        timeAliveEnd.text = snowBallRef.transform.position.z.ToString("0");
    }
}
