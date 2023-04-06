using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BulletEvent : MonoBehaviour
{
    private float speed = 10.0f;
    private int wallLayer = 10;
    // Start is called before the first frame update
    void Start()
    {
        wallLayer = LayerMask.NameToLayer("Wall");
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == wallLayer)
        {
            GameObject.Destroy(gameObject);
        }
    }
}