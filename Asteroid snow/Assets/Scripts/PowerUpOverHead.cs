using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PowerUpOverHead : MonoBehaviour
{
    public enum powerUpType
    {
        speed,
        grow,
        shrink,
        slow
    }
    powerUpType powerType;
    public static PowerUpOverHead instance;
    public GameObject playerRef;
    public BallScaler scaleRef;
    public BallMovement moveRef;
    bool speedUp = false;
    bool shrink = false;
    bool slow = false;
    public float newSpeed;
    public float speedDuration;
    public float scaleUpAmount;
    public float slowDownDuration;

    public AudioSource powerUpAudio;
    public AudioClip speedClip;
    public AudioClip growAudio;

    public GameObject[] uiObjects;//0 is speed 1 is grow

    float originalSpeed;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
            instance = this;
        StartCoroutine(SetVelocity());
        foreach (GameObject go in uiObjects)
        {
            go.SetActive(false);
        }
    }

    public void SpeedUp()
    {
        moveRef.targetVelocity.z += newSpeed;
        powerUpAudio.PlayOneShot(speedClip);
        uiObjects[0].SetActive(true);
        StartCoroutine(PowerUpTimer(speedDuration, powerUpType.speed));
    }

    public void Grow()
    {
        //instance ballscaler
        scaleRef.ChangeBallSize(scaleUpAmount);
        powerUpAudio.PlayOneShot(growAudio);
        uiObjects[1].SetActive(true);   
        StartCoroutine(PowerUpTimer(speedDuration, powerUpType.grow));
    }

    public void Shrink()
    {
        //ball scaler as well but difficult
        //StartCoroutine(PowerUpTimer(speedDuration, powerUpType.shrink));
    }
    public void Slow()
    {
        /*//time delta slowed in a coroutine
        Time.timeScale = .5F;
        StartCoroutine(PowerUpTimer(slowDownDuration, powerUpType.slow));*/
    }

    IEnumerator PowerUpTimer(float time, powerUpType power)
    {
        if(power == powerUpType.speed)
        {
            Physics.IgnoreLayerCollision(7, 6, true);
        }
        yield return new WaitForSeconds(time);
        switch (power)
        {
            case powerUpType.speed:
                moveRef.targetVelocity.z = originalSpeed;
                Physics.IgnoreLayerCollision(7, 6, false);
                uiObjects[0].SetActive(false);
                break;
            /*            case powerUpType.slow:
                            Time.timeScale = 1f;
                            break;*/
            case powerUpType.grow:
                uiObjects[1].SetActive(false);
                break;
            default:
                Debug.Log("No powerup selected");
                break;
        }
    }

    IEnumerator SetVelocity()
    {
        yield return new WaitForSeconds(.5f);
        originalSpeed = moveRef.targetVelocity.z;

    }
}
