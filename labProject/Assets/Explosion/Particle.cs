using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Particle : MonoBehaviour
{
    public GameObject particleCube;
    public Vector3 direction;
    public float speed;
    public int additional;

    private float elapsedTime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        additional = Random.Range(0, 4);
        speed = Random.Range(3.0f, 7.0f);
        direction = Random.onUnitSphere;
        Renderer renderer = GetComponent<Renderer>();
        renderer.material.color = Random.ColorHSV();

        //Destroy(gameObject, 3.0f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Random.rotation;
        transform.Translate(direction * speed * Time.deltaTime, Space.World);

        elapsedTime += Time.deltaTime;
    }

    void LateUpdate()
    {
        if (elapsedTime > 3.0f)
        {
            if (additional >= 3)
            {
                for (int i = 0; i < (5 - additional); ++i)
                {
                    GameObject newCube = GameObject.Instantiate(particleCube, transform.position, transform.rotation);
                    newCube.SetActive(true);
                }
            }
            GameObject.DestroyImmediate(gameObject);
        }
    }
}
