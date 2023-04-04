using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2.0f;
    [SerializeField] private float turnSpeed = 30.0f;

    public GameObject bullet;

    Camera mainCamera;

    //private GameObject mainCamera;
    private GameObject initCamera;
    public bool sceneCamera = true;

    new Rigidbody rigidbody;
    public bool usePhysics = false;
    public float moveForce = 300.0f;

    Vector3 moveDirection = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = Vector3.zero;
        rigidbody = GetComponent<Rigidbody>();

        mainCamera = Camera.main;

        initCamera = new GameObject();
        initCamera.name = "initCamera";
        initCamera.transform.SetPositionAndRotation(mainCamera.transform.position, mainCamera.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = Vector3.zero;
        if (Input.GetKey(KeyCode.W)) moveDirection += transform.forward;
        if (Input.GetKey(KeyCode.S)) moveDirection -= transform.forward;
        if (Input.GetKey(KeyCode.D)) moveDirection += transform.right;
        if (Input.GetKey(KeyCode.A)) moveDirection -= transform.right;

        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(Vector3.up * turnSpeed * Time.deltaTime);
            if (!sceneCamera)
            {
                mainCamera.transform.Rotate(Vector3.up * turnSpeed * Time.deltaTime);
            }

        }
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(Vector3.down * turnSpeed * Time.deltaTime);
            if (!sceneCamera)
            {
                mainCamera.transform.Rotate(-Vector3.up * turnSpeed * Time.deltaTime);
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.eulerAngles += new Vector3(0.0f, 45.0f, 0.0f);
            if (!sceneCamera)
            {
                mainCamera.transform.eulerAngles += new Vector3(0.0f, 45.0f, 0.0f);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 newBulletPostion = transform.position + transform.forward * 0.7f;
            newBulletPostion.y += 1.0f;
            GameObject newBullet = GameObject.Instantiate(bullet, newBulletPostion, transform.rotation);
            Destroy(newBullet, 10.0f);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            sceneCamera = !sceneCamera;
            if (sceneCamera)
            {
                mainCamera.transform.SetPositionAndRotation(initCamera.transform.position, initCamera.transform.rotation);
                mainCamera.nearClipPlane = 1.0f;
            }
            else
            {
                Vector3 cameraOffset = transform.position;
                cameraOffset.y += 1.5f;
                mainCamera.transform.SetPositionAndRotation(cameraOffset, transform.rotation);
                mainCamera.nearClipPlane = 0.5f;
            }
        }

        if (Input.GetMouseButton(0))
        {
            float horizontal = Input.GetAxis("Mouse X");
            transform.Rotate(Vector3.up * horizontal * turnSpeed * Time.deltaTime);
            if (!sceneCamera)
            {
                mainCamera.transform.Rotate(Vector3.up * horizontal * turnSpeed * Time.deltaTime);
            }
        }

        if (!usePhysics && moveDirection != Vector3.zero)
        {
            moveDirection = moveDirection.normalized;
            //rigidbody.MovePosition(transform.position + moveDirection * moveSpeed * Time.deltaTime);
            transform.position += moveDirection * moveSpeed * Time.deltaTime;
            if (!sceneCamera)
            {
                Vector3 cameraOffset = transform.position;
                cameraOffset.y += 1.5f;
                mainCamera.transform.SetPositionAndRotation(cameraOffset, transform.rotation);
            }
        }
    }

}
