using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour {

    public CharacterController2D controller;
	public Animator animator;

    public float runSpeed = 20F;
    float horizontalMove = 0F;
    bool jump = false;
    bool crouch = false;

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        
        // If the input is moving the player...
        if (horizontalMove == 0)
        {
            animator.SetBool("isRunning", false);   //animate standing still
        }
        else
        {
            animator.SetBool("isRunning", true);    //animate running
        }

        if (Input.GetButtonDown("Jump")) {
            jump = true;
            animator.SetBool("isJumping", true);
        }

        if (Input.GetButtonDown("Crouch")) {
            crouch = true;
        } else if (Input.GetButtonUp("Crouch")) {
            crouch = false;
        }
    }

    public void OnLanding() 
    {
        animator.SetBool("isJumping", false);
    }

    public void OnCrouching(bool isCrouching)
    {
        animator.SetBool("isCrouching", isCrouching);
    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }

    // Character follows momentum of a moving platform
    void OnCollisionEnter2D(Collision2D collide) {
        if (collide.gameObject.tag == "Moving platform") {
            this.transform.parent = collide.transform;
        }
    }
    void OnCollisionExit2D(Collision2D collide) {
        if (collide.gameObject.tag == "Moving platform") {
            this.transform.parent = null;
        }
    }
}
