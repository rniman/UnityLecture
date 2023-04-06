using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject wall;
    
    // Start is called before the first frame update
    void Start()
    {
        for(int i= -15; i < 15; ++i)
        {
            GameObject wallObject = GameObject.Instantiate(wall, new Vector3(i + 0.5f, 0.5f, 15 + 0.5f), Quaternion.identity, transform);
            wallObject.isStatic = true;
            wallObject = GameObject.Instantiate(wall, new Vector3(i + 0.5f, 0.5f, -15 - 0.5f), Quaternion.identity, transform);
            wallObject.isStatic = true;
            wallObject = GameObject.Instantiate(wall, new Vector3(15 + 0.5f, 0.5f, i + 0.5f), Quaternion.identity, transform);
            wallObject.isStatic = true;
            wallObject = GameObject.Instantiate(wall, new Vector3(-15 - 0.5f, 0.5f, i + 0.5f), Quaternion.identity, transform);
            wallObject.isStatic = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
