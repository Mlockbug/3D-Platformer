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

            Vector3 newVelocity = ((transform.forward * Input.GetAxis("Vertical")) + (transform.right * Input.GetAxis("Horizontal"))) * maxSpeed;
            rb.velocity = new Vector3(newVelocity.x, rb.velocity.y, newVelocity.z);

            if (rb.velocity != new Vector3(0f,0f,0f))
			{
                Debug.Log("E");
			}
            if (wantToRotate)
            {
                rotation += Input.GetAxis("Mouse X") * rotationSpeed;
                transform.rotation = Quaternion.Euler(new Vector3(0.0f, rotation, 0.0f));
                camRotation += Input.GetAxis("Mouse Y") * camRotationSpeed * -1;
                cam.transform.localRotation = Quaternion.Euler(new Vector3(camRotation, 0.0f, 0.0f));
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                cam.SetActive(!(cam.activeInHierarchy));
                mazeCam.SetActive(!(cam.activeInHierarchy));
                transform.rotation = Quaternion.Euler(new Vector3(0.0f, 90.0f, 0.0f));
                wantToRotate = !wantToRotate;
            }
        }

        //cam.transform.position = Vector3.Lerp(cam.transform.position, mazeCam.transform.position, Time.deltaTime);
        //cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, mazeCam.transform.rotation, Time.deltaTime);
    }
}
