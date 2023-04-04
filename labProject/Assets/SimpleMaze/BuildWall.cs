using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject wall;
    
    // Start is called before the first frame update
    void Start()
    {
        for(int i= -10; i < 10; ++i)
        {
            GameObject wallObject = GameObject.Instantiate(wall, new Vector3(i + 0.5f, 0.5f, 10 + 0.5f), Quaternion.identity, transform);
            wallObject = GameObject.Instantiate(wall, new Vector3(i + 0.5f, 0.5f, -10 - 0.5f), Quaternion.identity, transform);
            wallObject = GameObject.Instantiate(wall, new Vector3(10 + 0.5f, 0.5f, i + 0.5f), Quaternion.identity, transform);
            wallObject = GameObject.Instantiate(wall, new Vector3(-10 - 0.5f, 0.5f, i + 0.5f), Quaternion.identity, transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
