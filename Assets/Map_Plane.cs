using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_Plane : MonoBehaviour
{
    public GameObject cube;
    public List<GameObject> map = new List<GameObject>();

    private bool isCoroutineRunning = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnTriggerEnter()
    {
        map[0].gameObject.SetActive(true);
        map[1].gameObject.SetActive(false);
    }
}
