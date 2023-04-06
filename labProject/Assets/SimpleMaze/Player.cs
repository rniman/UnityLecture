using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2.0f;
    [SerializeField] private float turnSpeed = 90.0f;

    public GameObject bullet;

    Camera mainCamera;

    //private GameObject initCamera;
    public bool sceneCamera = true;
    private Quaternion initCameraRotation;
    private Vector3 initCameraPosition;

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

        initCameraRotation = mainCamera.transform.rotation;
        initCameraPosition = mainCamera.transform.position;

        //initCamera = new GameObject();
        //initCamera.name = "initCamera";
        //initCamera.transform.SetPositionAndRotation(mainCamera.transform.position, mainCamera.transform.rotation);
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
            Destroy(newBullet, 5.0f);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            sceneCamera = !sceneCamera;
            if (sceneCamera)
            {
                mainCamera.transform.SetPositionAndRotation(initCameraPosition, initCameraRotation);
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

        if (!sceneCamera)
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        else
            UnityEngine.Cursor.lockState = CursorLockMode.None;

        if (!sceneCamera || Input.GetMouseButton(0)) 
        {
            float horizontal = Input.GetAxis("Mouse X");
            transform.Rotate(Vector3.up * horizontal * turnSpeed * Time.deltaTime);
            if (!sceneCamera)
            {
                float vertical = -Input.GetAxis("Mouse Y");
                mainCamera.transform.rotation = Quaternion.Euler(
                    mainCamera.transform.rotation.eulerAngles.x + vertical * turnSpeed * Time.deltaTime,
                    mainCamera.transform.rotation.eulerAngles.y + horizontal * turnSpeed * Time.deltaTime,
                    0.0f);
            }
        }
    }

    void FixedUpdate()
    {
        if (!usePhysics && moveDirection != Vector3.zero)
        {
            moveDirection = moveDirection.normalized;
            transform.position += moveDirection * moveSpeed * Time.deltaTime;
            if (!sceneCamera)
            {
                Vector3 cameraOffset = transform.position;
                cameraOffset.y += 1.5f;
                mainCamera.transform.SetPositionAndRotation(cameraOffset, mainCamera.transform.rotation);
            }
        }
    }

    private void OnMouseEnter()
    {
        print("Enter");
    }
}

