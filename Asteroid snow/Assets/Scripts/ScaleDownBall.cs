using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleDownBall : MonoBehaviour
{
    float reductionAmount = -.3f;
    //public CameraShake camShakeRef;
    //Cam shake vars 
     float duration= .1f;
     float magnitude= .3f;

    bool coolDown= false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        
        if (!coolDown)
        {
            StartCoroutine(Cooldown());
            BallScaler.instance.ChangeBallSize(reductionAmount);
            StartCoroutine(CameraShake.instance.Shake(duration, magnitude));//gets instance of cam shake
        }
            
    }

    IEnumerator Cooldown()
    {
        coolDown = true;
        yield return new WaitForSeconds(1f);
        coolDown = false;
    }
}
