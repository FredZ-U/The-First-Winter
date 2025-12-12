using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBoost : MonoBehaviour
{
    public bool isLeft;
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
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Player Collide");
            if (isLeft)
            {
                collision.gameObject.GetComponent<BallMovement>().StartControlToggle();
                collision.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.left * 60f, ForceMode.Impulse);

            }
            else
            {
                collision.gameObject.GetComponent<BallMovement>().StartControlToggle();
                collision.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.right * 60f, ForceMode.Impulse);

            }
        }
    }
}
