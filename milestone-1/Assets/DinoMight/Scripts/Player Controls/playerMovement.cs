using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{

    public CharacterController2D controller;
    public Animator animator;

    public float runSpeed = 20F;
    public PhysicsMaterial2D friction;
    public PhysicsMaterial2D noFriction;
    public bool canDash = false;           // can be used while walking and jumping

    private float horizontalMove = 0F;
    private bool jump = false;
    private bool crouch = false;
    private bool dash = false;
    private bool canMove = true;
    private Rigidbody2D rbPlayer;


    void Start()
    {
        rbPlayer = gameObject.GetComponent<Rigidbody2D>();
        canDash = Global.canDash;
    }

    void Update()
    {
        if (canMove)
        {
            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
            if (Input.GetButtonDown("Jump"))
            {
                jump = true;
                animator.SetBool("isJumping", true);
            }

            if (Input.GetButtonUp("Crouch"))
            {
                crouch = false;
            }
            else if (Input.GetButtonDown("Crouch"))
            {
                crouch = true;
            }

            if (canDash)
            {
                if (Input.GetButtonDown("Fire3"))
                {     // left and right shift
                    dash = true;
                    //animator.SetTrigger("isDashing");
                }
                else if (Input.GetButtonUp("Fire3"))
                {
                    dash = false;
                    animator.ResetTrigger("isDashing");
                }
            }
        }
        else
        {
            horizontalMove = 0;
        }

        // If the input is moving the player...
        if (horizontalMove == 0)
        {
            animator.SetBool("isRunning", false);   //animate standing still
            rbPlayer.sharedMaterial = friction;
        }
        else
        {
            animator.SetBool("isRunning", true);    //animate running
            rbPlayer.sharedMaterial = noFriction;
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

    void OnEnable()
    {
        crouch = false;
    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump, dash);
        jump = false;
        dash = false;
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
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
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

    public void EnableDash()
    {
        Global.canDash = true;
        canDash = true;
    }
}
