using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    private int wallLayer = 10;

    public GameObject bullet;
    private float shootCoolTime = 0.0f;

    private float moveSpeed = 2.0f;
    private float turnSpeed = 180.0f;
    private Vector3 moveDirection = Vector3.zero;

    private float jumpTime = 0.0f;
    private float jumpSpeed = 5.0f;
    private bool jumping;
    private bool flying;

    public bool sceneCamera = true;
    private Camera mainCamera;
    private Quaternion initCameraRotation;
    private Vector3 initCameraPosition;

    private Rigidbody rg;
    public bool usePhysics = false;
    public float moveForce = 300.0f;

    // Start is called before the first frame update
    void Start()
    {
        rg = GetComponent<Rigidbody>();

        flying = false;
        jumping = false;
        mainCamera = Camera.main;

        initCameraRotation = mainCamera.transform.rotation;
        initCameraPosition = mainCamera.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(shootCoolTime > 0.0f)
        {
            shootCoolTime -= Time.deltaTime;
        }

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
            if (!flying)
            {
                flying = true;
                jumping = true;
                rg.useGravity = false;
            }
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

            if (!sceneCamera)
                UnityEngine.Cursor.lockState = CursorLockMode.Locked;
            else
                UnityEngine.Cursor.lockState = CursorLockMode.None;

        }

        if (Input.GetMouseButtonDown(0) && shootCoolTime <= 0.0f)
        {
            Vector3 newBulletPostion = transform.position + transform.forward * 0.7f;
            newBulletPostion.y += 1.0f;
            GameObject newBullet;
            if (!sceneCamera)
            {
                newBullet = GameObject.Instantiate(bullet, newBulletPostion, mainCamera.transform.rotation);
            }
            else
            {
                newBullet = GameObject.Instantiate(bullet, newBulletPostion, transform.rotation);
            }
            Destroy(newBullet, 3.0f);
            shootCoolTime = 1.0f;
        }

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
        }

        //float jumpforce = jumpaAceleration * rg.mass;
        if (jumping)
        {
            jumpTime += Time.deltaTime;
            transform.position += Vector3.up * jumpSpeed * Time.deltaTime;
            if (jumpTime > 0.3f)
            {
                rg.useGravity = true;
                jumpTime = 0.0f;
                jumping = false;
            }
        }

        if (!sceneCamera)
        {
            Vector3 cameraOffset = transform.position;
            cameraOffset.y += 1.5f;
            mainCamera.transform.SetPositionAndRotation(cameraOffset, mainCamera.transform.rotation);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 9 || collision.gameObject.layer == wallLayer)
        {
            if(!jumping)
                flying = false;
        }
    }
}

