using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessCharacterController : MonoBehaviour
{
    [SerializeField] private GameController gameController;
    [SerializeField] private bool isGrounded = true;
    private void Awake()
    {
        //Debug.Log("Awake - Time - " + Time.time);
    }
    // Start is called before the first frame update
    void Start()
    {
        if (!gameController)
            gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        //Debug.Log("Start - Time - " + Time.time);
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("Update - Time - " + Time.deltaTime);
        // Debug.Log("Update - Time - " + Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * gameController.jumpIntensity, ForceMode.Impulse);
        if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && !isGrounded)
            gameObject.GetComponent<Rigidbody>().AddForce(Vector3.down * gameController.jumpIntensity * 2, ForceMode.Impulse);
        transform.Translate(new Vector3(Input.GetAxis("Horizontal"), 0, Time.deltaTime * gameController.speed));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Platform")
            isGrounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Platform")
            isGrounded = false;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            Debug.Log("Obstacle Hit");
            gameController.health -= 10;
        }
    }

    /*  void FixedUpdate()
      {
       //   Debug.Log("Fixed Update - Time - " + Time.deltaTime);
      }
  */
}
