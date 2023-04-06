using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionCube : MonoBehaviour
{
    public GameObject particleCube;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Random.rotation;
    }

    private void OnMouseDown()
    {
        for(int i = 0; i < 50; ++i)
        {
            GameObject newCube = GameObject.Instantiate(particleCube, transform.position, transform.rotation);
            newCube.SetActive(true);
        }
    }
}
