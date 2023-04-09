using NUnit.Framework.Internal;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BulletEvent : MonoBehaviour
{
    private static int floorLayer = 9;
    private static int wallLayer = 10;

    private float speed = 15.0f;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }
    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.layer == floorLayer)
        {
            Vector3 reflectVector = Vector3.Reflect(transform.forward, Vector3.up);
            transform.rotation = Quaternion.FromToRotation(Vector3.forward, reflectVector);

            speed = speed * 0.7f;
        }

        if (collision.gameObject.layer == wallLayer)
        {
            ContactPoint cp = collision.GetContact(0);
            Vector3 reflectVector = Vector3.Reflect(transform.forward, cp.normal);
            transform.rotation = Quaternion.FromToRotation(Vector3.forward, reflectVector);

            speed = speed * 0.7f;
            //GameObject.Destroy(gameObject);
        }
    }

}