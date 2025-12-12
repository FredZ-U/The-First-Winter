using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundBallScaleUp : MonoBehaviour
{
    private bool onGround;
    //private BallScaler BallScaler;
    public float amountToScale;
    // Start is called before the first frame update
    void Start()
    {
        //GetComponent<BallScaler>();
    }

    // Update is called once per frame
    void Update()
    {
        if (onGround)
        {
            BallScaler.instance.ChangeBallSize(amountToScale* Time.deltaTime);
        }
        else
        {
            BallScaler.instance.ChangeBallSize(0);
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        //if(collision.gameObject.tag == "Player")//Weird way to do this but it should work. It will only say you're on the ground when your on snow
        BallScaler.instance.ActivateSnowFX();
        onGround = true;
    }
    private void OnCollisionExit(Collision collision)
    {
        BallScaler.instance.DeActivateSnowFX();
        onGround = false;
    }
}
