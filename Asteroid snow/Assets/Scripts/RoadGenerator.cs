using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadGenerator : MonoBehaviour
{
    public GameObject[] roadSegmentPrefab; //Kevin changed to array    
    public Transform player;                 

    private GameObject[] roadSegments;       
    private int activeSegmentIndex = 0;

    int numOfSegments;

    // Start is called before the first frame update
    void Start()
    {
        roadSegments = new GameObject[roadSegmentPrefab.Length];
        for (int i = 0; i < roadSegments.Length; i++)
        {
            roadSegments[i] = Instantiate(roadSegmentPrefab[i]);
            roadSegments[i].SetActive(true);
            SetSegmentPosition(roadSegments[i], i);
        }

        numOfSegments = roadSegments.Length;
        activeSegmentIndex = roadSegments.Length - 1;
    }

    // Update is called once per frame
    void Update()
    {
        // Detect player already pass the target distance, and update, move old road to next place
        if (player.position.z > roadSegments[(activeSegmentIndex - 5 + numOfSegments) % numOfSegments].transform.position.z)
        {
            RecycleSegment();
        }
    }

    void SetSegmentPosition(GameObject segment, int index)
    {
        // if it is first segment, generate it at origin location
        if (index == 0)
        {
            segment.transform.position = Vector3.zero;
        }
        else
        {
            // else set it after last segment
            float lastSegmentEndZ = roadSegments[index - 1].transform.position.z + GetSegmentLength(roadSegments[index - 1]);
            segment.transform.position = new Vector3(0, 0, lastSegmentEndZ);
        }
    }


    void RecycleSegment()
    {
        // Caculate Next segment generate positon
        int nextSegmentIndex = (activeSegmentIndex + 1) % numOfSegments;

        // Get end of the current segment's z positon
        float currentSegmentEndZ = roadSegments[activeSegmentIndex].transform.position.z + GetSegmentLength(roadSegments[activeSegmentIndex]);

        roadSegments[nextSegmentIndex].SetActive(false);
        roadSegments[nextSegmentIndex].SetActive(true);

        // move last segment to next position
        roadSegments[nextSegmentIndex].transform.position = new Vector3(0, 0, currentSegmentEndZ);

         // switch to next segment
         activeSegmentIndex = nextSegmentIndex;


    }


    float GetSegmentLength(GameObject segment)
    {
        // use Renderer.bounds get segment length
        Renderer renderer = segment.GetComponentInChildren<Renderer>();
        return renderer.bounds.size.z;
    }
}
