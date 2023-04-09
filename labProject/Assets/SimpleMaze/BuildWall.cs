using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject wall;

    // Start is called before the first frame update
    void Start()
    {
        GameObject wallObject = GameObject.Instantiate(wall, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity, transform);
        wallObject.isStatic = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
