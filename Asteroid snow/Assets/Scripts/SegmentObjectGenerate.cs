using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegmentObjectGenerate : MonoBehaviour
{

    public Transform[] row1;
    public Transform[] row2;
    public Transform[] row3;

    private List<GameObject> itemList = new List<GameObject>();

    bool allowOnEnable = false;

    //bool delayDone = false;
    // Start is called before the first frame update
    void Start()
    {
        GenerateObject();
        allowOnEnable = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        if (allowOnEnable)
        {
            GenerateObject();
        }
    }

    void GenerateObject()
    {
        StartCoroutine(FrameSpawnDelay());

    }

    private void OnDisable()
    {
        for (int i = 0; i < itemList.Count; i++)
        {
            ObjectPoolManager.Instance.ReturnItem(itemList[i]);
        }
        itemList.Clear();
    }

    IEnumerator FrameSpawnDelay()//Moved spawn logic here to give a buffer to update locations to spawn
    {
        //delayDone = false;
        yield return new WaitForSeconds(.05f);
        GameObject object1 = ObjectPoolManager.Instance.GetItem();
        GameObject object2 = ObjectPoolManager.Instance.GetItem();
        GameObject object3 = ObjectPoolManager.Instance.GetItem();
        object1.transform.position = row1[Random.Range(0, row1.Length)].position;//changed these to.Length so I could add more positions
        object2.transform.position = row2[Random.Range(0, row2.Length)].position;
        object3.transform.position = row3[Random.Range(0, row3.Length)].position;
        object1.SetActive(true);
        object2.SetActive(true);
        object3.SetActive(true);
        itemList.Add(object1);
        itemList.Add(object2);
        itemList.Add(object3);
        //delayDone = true;
    }
}
