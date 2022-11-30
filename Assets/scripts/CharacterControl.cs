using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    // Start is called before the first frame update
    public float maxSpeed;
    public float normalSpeed = 500f;
    public float sprintSpeed = 750f;
    public float crouchSpeed = 350f;
    float rotation = -90.0f;
    float camRotation = 0.0f;
    float rotationSpeed = 2.0f;
    float camRotationSpeed = 1.5f;
    GameObject cam;
    public GameObject camWaypoint;
    public GameObject mazeCam;
    public GameObject mazeCamWaypoint;
    bool wantToRotate = true;
    Rigidbody rb;
    Vector3 newVelocity;
    bool isOnGround;
    public GameObject groundChecker;
    public LayerMask groundLayer;
    public float jumpForce = 300f;
    public float maxSprint = 5f;
    float sprintTimer;
    bool inMaze = false;
    int speedCubes = 0;
    public GameObject collectableSpeed;
    bool mazeCollected = false;
    bool speedCollected = false;
    bool parkourCollected = false;
    Vector3 respawnPoint = new Vector3(180f, 133f, 153f);
    public GameObject endDoor;
    public bool playingGame = false;
    public GameObject mainMenuScreen;
    public GameObject pauseMenuScreen;
    public GameObject winMenuScreen;
    public Animator anim;
    bool sprinting;
    bool jumping;
    void Start()
    {
        
        cam = GameObject.Find("Main Camera");
        rb = GetComponent<Rigidbody>();
        maxSpeed = normalSpeed;
        sprintTimer = maxSprint;
    }

    // Update is called once per frame
    void Update()
    {
        if (playingGame)
        {
            anim.SetFloat("speed", newVelocity.magnitude);
            anim.SetBool("sprint", sprinting);
            anim.SetFloat("velocity_y", rb.velocity.y);
            anim.SetBool("jumping", jumping);

            Cursor.lockState = CursorLockMode.Locked;
            mainMenuScreen.SetActive(false);
            //transform.position += ((transform.forward * Input.GetAxis("Vertical")) + (transform.right * Input.GetAxis("Horizontal"))) * maxSpeed * Time.deltaTime;

            //camWaypoint.transform.position = cam.transform.position;
            //camWaypoint.transform.rotation = cam.transform.rotation;

            isOnGround = Physics.CheckSphere(groundChecker.transform.position, 0.1f, groundLayer);
            newVelocity = ((transform.forward * Input.GetAxis("Vertical")) + (transform.right * Input.GetAxis("Horizontal")));

            if (wantToRotate)
            {
                rotation += Input.GetAxis("Mouse X") * rotationSpeed;
                transform.rotation = Quaternion.Euler(new Vector3(0.0f, rotation, 0.0f));
                camRotation += Input.GetAxis("Mouse Y") * camRotationSpeed * -1;
                cam.transform.localRotation = Quaternion.Euler(new Vector3(camRotation, 0.0f, 0.0f));
                camWaypoint.transform.localRotation = Quaternion.Euler(new Vector3(camRotation, 0.0f, 0.0f));
            }

            if (Input.GetKey(KeyCode.LeftShift) && sprintTimer>0f)
            {
                sprinting = true;
                maxSpeed = sprintSpeed;
                sprintTimer = sprintTimer - Time.deltaTime;
            }
            else
            {
                sprinting = false;
                maxSpeed = normalSpeed;
                if (!(Input.GetKey(KeyCode.LeftShift)))
                {
                    sprintTimer = sprintTimer + Time.deltaTime;
                }
            }
            sprintTimer = Mathf.Clamp(sprintTimer, 0.0f, maxSprint);

            /*if (Input.GetKey(KeyCode.LeftControl))
            {
                maxSpeed = crouchSpeed;
                transform.localScale = new Vector3(1f,0.5f,1f);
            }
            else
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
            }*/

            if (isOnGround && Input.GetKeyDown(KeyCode.Space))
			{
                jumping = true;
                rb.AddForce(transform.up * jumpForce);
			}
			else
			{
                jumping = false;
			}

            if (inMaze)
			{

                //mazeCam.SetActive(true);
                //mazeCam.transform.position = cam.transform.position;
                cam.transform.position = Vector3.Lerp(cam.transform.position, mazeCamWaypoint.transform.position, Time.deltaTime);
                cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, mazeCamWaypoint.transform.rotation, Time.deltaTime);
                cam.transform.parent = null;
                //cam.SetActive(false);
                wantToRotate = false;
                transform.rotation = Quaternion.Euler(new Vector3(0.0f, 90.0f, 0.0f));
                /*while (inMaze)
				{
                    cam.transform.position = mazeCamWaypoint.transform.position;
                    cam.transform.rotation = mazeCamWaypoint.transform.rotation;
                }*/
            }
			else
			{
                cam.SetActive(true);
                mazeCam.SetActive(false);
                wantToRotate = true;
                cam.transform.parent = transform;
                cam.transform.position = camWaypoint.transform.position;
                cam.transform.localRotation = camWaypoint.transform.localRotation;
            }

            if  (speedCubes == 7)
			{
                collectableSpeed.SetActive(true);
                speedCubes = 0;
			}

            /*if (Input.GetKeyDown(KeyCode.E))
            {
                cam.SetActive(!(cam.activeInHierarchy));
                mazeCam.SetActive(!(cam.activeInHierarchy));
                transform.rotation = Quaternion.Euler(new Vector3(0.0f, 90.0f, 0.0f));
                wantToRotate = !wantToRotate;
            }*/

            if (speedCollected && parkourCollected && mazeCollected)
            {
                endDoor.tag = "activated door";
                endDoor.transform.position = Vector3.Lerp(endDoor.transform.position, new Vector3(205.71f, 135.38f, 141.38f), Time.deltaTime);
            }
            else
            {
                endDoor.transform.position = Vector3.Lerp(endDoor.transform.position, new Vector3(205.71f, 127.37f, 141.38f), Time.deltaTime);
                endDoor.tag = "deactivated door";
            }

        }
    }

	private void FixedUpdate()
	{
        rb.velocity = new Vector3(newVelocity.x * maxSpeed * Time.fixedDeltaTime, rb.velocity.y, newVelocity.z * maxSpeed * Time.fixedDeltaTime) ;
    }

    void OnTriggerEnter(Collider other)
	{
        switch (other.tag)
        {
            case "cam orth":
                inMaze = true;
                break;
            case "respawn":
                transform.position = respawnPoint;
                break;
            case "speed cube":
                Destroy(other.gameObject);
                speedCubes++;
                break;
            case "collectable speed":
                speedCollected = true;
                Destroy(other.gameObject);
                break;
            case "collectable maze":
                mazeCollected = true;
                Destroy(other.gameObject);
                break;
            case "collectable parkour":
                parkourCollected = true;
                Destroy(other.gameObject);
                break;
            case "activated door":
                winMenuScreen.SetActive(true);
                playingGame = false;
                Cursor.lockState = CursorLockMode.Confined;
                newVelocity = Vector3.zero;
                break;

        }
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.tag == "cam orth")
		{
            inMaze = false;
		}
	}

    public void PressedPlay()
	{
        playingGame = true;
	}
}
