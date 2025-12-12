using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BallScaler : MonoBehaviour
{
    public static BallScaler instance;  
    [SerializeField] private float scaleMultiplier = 1.0f;
    public AudioSource hitSource;//Kevin Added this 
    public AudioClip hitClip;

    //Kevin added vfx here 
    public ParticleSystem ballGrowFX;
    public ParticleSystem ballHitFX;
    public AudioSource moveAudio;

    int safeGuard = 0; //this safeguards the coroutine from getting spammed


    //private float previousScaleMultiplier = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
            instance = this;
        Physics.IgnoreLayerCollision(gameObject.layer, 6, false);
        moveAudio.Pause();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.W))
        {
            ChangeBallSize(1f);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            ChangeBallSize(-1f);
        }*/

    }

    public void ChangeBallSize(float changeSize)
    {
        Debug.Log(changeSize);
        /*if (scaleMultiplier + changeSize <= 0)
        {
            return;
        }*/ 
        /*if(changeSize == 0)
        {
            //stop playing snow vfx here
            return;
        }*/
        //added check here for audio 
        if (changeSize <0)
        {
            StartCoroutine(IFrame());
            //gun shot here
            hitSource.PlayOneShot(hitClip);
            ballHitFX.Play();
            
        }
  
        scaleMultiplier += changeSize;
        transform.localScale = Vector3.one * scaleMultiplier;


    }

    /// <summary>
    /// These two functions activate the snow trail fx
    /// </summary>
    public void ActivateSnowFX()
    {
        ballGrowFX.Play();
        moveAudio.UnPause();
    }

    public void DeActivateSnowFX()
    {
        ballGrowFX.Stop();
        moveAudio.Pause();
    }
    /// <summary>
    /// Activates Iframes after getting hit
    /// </summary>
    /// <returns></returns>
    public IEnumerator IFrame()
    {
        if (safeGuard < 1)
        {
            safeGuard++;
            Debug.Log("IFrames");
            Physics.IgnoreLayerCollision(gameObject.layer, 6, true);
            yield return new WaitForSeconds(.5f);
            Physics.IgnoreLayerCollision(gameObject.layer, 6, false);
            safeGuard = 0;
        }
        else
            yield return null;

    }

}
