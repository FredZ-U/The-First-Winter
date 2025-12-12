using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class PowerUp : MonoBehaviour
{
    
    public enum powerUpType
    {
        speed,
        grow,
        shrink,
        slow
    }

    public powerUpType powerUpSelect;

    private void OnCollisionEnter(Collision collision)
    {
        
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("HITTTTT");
            switch (powerUpSelect)
            {
                case powerUpType.speed:
                    PowerUpOverHead.instance.SpeedUp();
                    break;
                case powerUpType.grow:
                    PowerUpOverHead.instance.Grow();
                    break;
                case powerUpType.shrink:
                    PowerUpOverHead.instance.Shrink();
                    break;
                case powerUpType.slow:
                    PowerUpOverHead.instance.Slow();    
                    break;
                default:
                    Debug.Log("No powerup selected");
                    break;
            }
            Destroy(gameObject);
        }
        

    }
    


}
