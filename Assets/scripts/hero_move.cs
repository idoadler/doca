using UnityEngine;
using System.Collections;

public class hero_move : MonoBehaviour {

    //Variables
    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    public GameObject model;
    private Vector3 moveDirection = Vector3.zero;

    float rotation = 0;
    Quaternion lastDirection = Quaternion.identity;
    void Update()
    {
        CharacterController controller = GetComponent<CharacterController>();
        // is the controller on the ground?
        if (controller.isGrounded)
        {
            //Feed moveDirection with input.
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            if (moveDirection != Vector3.zero)
            {
                rotation += speed * 100 * Time.deltaTime;
                lastDirection = Quaternion.LookRotation(moveDirection, Vector3.up);
                model.transform.rotation = lastDirection;
                model.transform.Rotate(Vector3.right, rotation);
            }
            else
            {
                rotation = 0;
                model.transform.rotation = lastDirection;
            }
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
}
