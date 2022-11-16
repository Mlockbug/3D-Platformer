using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    // Start is called before the first frame update
    public float maxSpeed = 3.0f;
    float rotation = 0.0f;
    float camRotation = 0.0f;
    float rotationSpeed = 2.0f;
    float camRotationSpeed = 1.5f;
    GameObject cam;
    public GameObject mazeCam;
    bool wantToRotate = true;
    Rigidbody rb;
    Vector3 newVelocity;
    bool isOnGround;
    public GameObject groundChecker;
    public LayerMask groundLayer;
    public float jumpForce = 300.0f;
    void Start()
    {
        cam = GameObject.Find("Main Camera");
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (true)
        {
            //transform.position += ((transform.forward * Input.GetAxis("Vertical")) + (transform.right * Input.GetAxis("Horizontal"))) * maxSpeed * Time.deltaTime;

            isOnGround = Physics.CheckSphere(groundChecker.transform.position, 0.1f, groundLayer);
            newVelocity = ((transform.forward * Input.GetAxis("Vertical")) + (transform.right * Input.GetAxis("Horizontal")));

            if (wantToRotate)
            {
                rotation += Input.GetAxis("Mouse X") * rotationSpeed;
                transform.rotation = Quaternion.Euler(new Vector3(0.0f, rotation, 0.0f));
                camRotation += Input.GetAxis("Mouse Y") * camRotationSpeed * -1;
                cam.transform.localRotation = Quaternion.Euler(new Vector3(camRotation, 0.0f, 0.0f));
            }

            if (isOnGround && Input.GetKeyDown(KeyCode.Space))
			{
                rb.AddForce(transform.up * jumpForce);
			}

            if (Input.GetKeyDown(KeyCode.E))
            {
                cam.SetActive(!(cam.activeInHierarchy));
                mazeCam.SetActive(!(cam.activeInHierarchy));
                transform.rotation = Quaternion.Euler(new Vector3(0.0f, 90.0f, 0.0f));
                wantToRotate = !wantToRotate;
            }
        }

        // will import these to make camera transition smooth if i have extra time

        //cam.transform.position = Vector3.Lerp(cam.transform.position, mazeCam.transform.position, Time.deltaTime);
        //cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, mazeCam.transform.rotation, Time.deltaTime);
    }

	private void FixedUpdate()
	{
        rb.velocity = new Vector3(newVelocity.x * maxSpeed * Time.fixedDeltaTime, rb.velocity.y, newVelocity.z * maxSpeed * Time.fixedDeltaTime) ;
    }
}
