﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{

    public CharacterController2D controller;
    public Animator animator;

    public float runSpeed = 20F;
    float horizontalMove = 0F;
    bool jump = false;
    bool crouch = false;
    private bool canMove = true;

    // Update is called once per frame
    void Update() {
        if (canMove) {
            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
            if (Input.GetButtonDown("Jump")) {
                jump = true;
                animator.SetBool("isJumping", true);
            }

            if (Input.GetButtonUp("Crouch")) {
                crouch = false;
            } else if (Input.GetButtonDown("Crouch")) {
                crouch = true;
            }
        } else {
            horizontalMove = 0;
        }

        // If the input is moving the player...
        if (horizontalMove == 0) {
            animator.SetBool("isRunning", false);   //animate standing still
        } else {
            animator.SetBool("isRunning", true);    //animate running
        }

        // EVERYTHING BELOW THIS LINE MUST BE DELETED FOR FINAL PRODUCT
        // Demo hack teleport script
        if (Input.GetKey("1")) {
            Debug.Log("If you see this message, this is for the game demo, delete in PlayerMovement");
            transform.position = new Vector3(46.5f, -3f, 0f);
        }
        if (Input.GetKey("2")) {
            Debug.Log("If you see this message, this is for the game demo, delete in PlayerMovement");
            transform.position = new Vector3(65.5f, 18f, 0f);
        }
        if (Input.GetKey("3")) {
            Debug.Log("If you see this message, this is for the game demo, delete in PlayerMovement");
            transform.position = new Vector3(0f, 5f, 0f);
        }
        if (Input.GetKey("4")) {
            Debug.Log("If you see this message, this is for the game demo, delete in PlayerMovement");
            transform.position = new Vector3(101f, 9f, 0f);
        }
        // EVERYTHING ABOVE THIS LINE MUST BE DELETED FOR FINAL PRODUCT

    }

    public void OnLanding()
    {
        animator.SetBool("isJumping", false);
    }

    public void OnCrouching(bool isCrouching)
    {
        animator.SetBool("isCrouching", isCrouching);
    }

    void OnEnable()
    {
        crouch = false;
    }
    
    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }

    // Character follows momentum of a moving platform
    void OnCollisionEnter2D(Collision2D collide)
    {
        if (collide.gameObject.CompareTag("Moving platform"))
        {
            this.transform.parent = collide.transform;
        }
    }
    void OnCollisionExit2D(Collision2D collide)
    {
        if (collide.gameObject.CompareTag("Moving platform"))
        {
            this.transform.parent = null;
        }
    }
    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Coin")) {
            Destroy(other.gameObject);
        }
    }
    
    public void ToggleStartStopMovement()
    {
        canMove = !canMove;
    }

    public void CrouchOnce()
    {
        StartCoroutine(CrouchOnceCoroutine());
    }

    IEnumerator CrouchOnceCoroutine()
    {
        crouch = true;
        yield return new WaitForSeconds(0.2f);
        crouch = false;
    }
}
