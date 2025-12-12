using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Camera mainCamera; 
    public float zoomSpeed = 2f; 
    public float minZoom = 20f; 
    public float maxZoom = 60f; 

    public Transform player;
    Vector3 offset;

    //Added code to adjust FOV depending on scale
    public CinemachineVirtualCamera virtualCamera; // Reference to the Cinemachine virtual camera
    public float baseFOV = 60f; //changed in inspector
    public float scaleMultiplier = 10f; // Multiplier to control the effect of scale on FOV

    // Start is called before the first frame update
    void Start()
    {
        offset = new Vector3(0, 3,  this.transform.position.z - player.position.z );
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.position + offset;

        // Calculate the average scale of the object (or you can use one specific axis)
        float averageScale = (player.localScale.x + player.localScale.y + player.localScale.z) / 3f;

        // Adjust the FOV based on the object's scale
        float newFOV = baseFOV + (averageScale - 1f) * scaleMultiplier;

        // Apply the new FOV to the virtual camera
        virtualCamera.m_Lens.FieldOfView = Mathf.Clamp(newFOV, 50f, 100f); // Optional clamp to limit FOV range




    }

    
}
