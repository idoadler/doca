using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class hero_move : MonoBehaviour {

    //Variables
    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float rotationSpeed = 300;
    public float gravity = 20.0F;
    public GameObject model;
    public int health = 80;
    public Text healthText;
    private Vector3 moveDirection = Vector3.zero;

    Quaternion lastDirection = Quaternion.identity;
    void Update()
    {
        // rotate dice
        model.transform.Rotate(Vector3.right * rotationSpeed * Time.deltaTime * Input.GetAxis("Vertical"), Space.World);
        model.transform.Rotate(Vector3.back * rotationSpeed * Time.deltaTime * Input.GetAxis("Horizontal"), Space.World);
        
        CharacterController controller = GetComponent<CharacterController>();
        // is the controller on the ground?
        if (controller.isGrounded)
        {
            //Feed moveDirection with input.
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            //Multiply it by speed.
            moveDirection *= speed;
            //Jumping
            if (Input.GetButton("Jump"))
                moveDirection.y = jumpSpeed;

        }
        //Applying gravity to the controller
        moveDirection.y -= gravity * Time.deltaTime;
        //Making the character move
        controller.Move(moveDirection * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            Debug.DrawRay(contact.point, contact.normal, Color.white);
        }

        enemy bad = collision.gameObject.GetComponent<enemy>();
        if (bad != null)
        {
            Debug.Log("hit: " + collision.gameObject.name);
            // attack
            if (!GetComponent<CharacterController>().isGrounded)
            {
                bad.getHit(GetComponent<find_top_edge>().get_top_edge_result());
            }

            health -= bad.attack(); // getDemage()
            healthText.text = health.ToString();
        }
    }

}
