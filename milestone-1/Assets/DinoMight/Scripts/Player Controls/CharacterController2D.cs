using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
    [SerializeField] private float m_JumpForce = 600f;                          // Amount of force added when the player jumps.
    [Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;          // Amount of maxSpeed applied to crouching movement. 1 = 100%
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement
    [SerializeField] private bool m_AirControl = false;                         // Whether or not a player can steer while jumping;
    public LayerMask m_WhatIsGround;                            // A mask determining what is ground to the character
    public Transform m_GroundCheck;                         // A position marking where to check if the player is grounded.
    public Transform m_CeilingCheck;                            // A position marking where to check for ceilings
    public BoxCollider2D m_CrouchResizeCollider;				// A collider that will be resized when crouching
    public ParticleSystem dust;
    public float dashForce = 600f;                             // Amount of force added when player dashes

    private Vector2 standingColliderSize = new Vector2(0.4106f, 0.497f);
    private Vector2 standingColliderOffset = new Vector2(0f, 0.08f);
    private Vector2 crouchColliderSize = new Vector2(0.66f, 0.33f);
    private Vector2 crouchColliderOffset = new Vector2(0.13f, 0.03f);

    const float k_GroundedRadius = 0.2057883f; // Radius of the overlap circle to determine if grounded
    private bool m_Grounded = true;            // Whether or not the player is grounded.
    private bool falling = false;
    const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
    private Rigidbody2D m_Rigidbody2D;
    private bool m_FacingRight = true;  // For determining which way the player is currently facing.
    private Vector3 m_Velocity = Vector3.zero;

    [Header("Events")]
    [Space]

    public UnityEvent OnLandEvent;

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }

    public BoolEvent OnCrouchEvent;
    private bool m_wasCrouching = false;
    private bool dashing = false;           // will be reset to false after dashing by animation state
    private bool hadDashed = false;         // dash midair once only
    private Animator animator;

    private PlayerSFXControl soundEffect;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        soundEffect = GetComponent<PlayerSFXControl>();

        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();

        if (OnCrouchEvent == null)
            OnCrouchEvent = new BoolEvent();
    }

    private void FixedUpdate()
    {
        bool wasGrounded = m_Grounded;
        m_Grounded = false;

        if (m_Rigidbody2D.velocity.y < 0f && !wasGrounded)
        {
            falling = true;
        }
        // else
        // {
        //     falling = false;
        // }

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (!colliders[i].gameObject.CompareTag("Player"))
            {
                // if (m_Rigidbody2D.velocity.y == 0)
                //{
                m_Grounded = true;
                //}
                if (!wasGrounded && falling)
                {
                    OnLandEvent.Invoke();
                    falling = false;
                    hadDashed = false;

                    CreateDust();
                }
            }
        }
    }


    public void Move(float move, bool crouch, bool jump, bool dash)
    {
        // If crouching, check to see if the character can stand up
        if (!crouch)
        {
            // If the character has a ceiling preventing them from standing up, keep them crouching
            if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
            {
                crouch = true;
            }
        }

        //only control the player if grounded or airControl is turned on
        if (m_Grounded || m_AirControl)
        {

            // If crouching
            if (crouch)
            {
                if (!m_wasCrouching)
                {
                    m_wasCrouching = true;
                    OnCrouchEvent.Invoke(true);
                }

                // Reduce the speed by the crouchSpeed multiplier
                move *= m_CrouchSpeed;

                // Alter top collider for crouching when crouching
                if (m_CrouchResizeCollider != null)
                {
                    m_CrouchResizeCollider.size = crouchColliderSize;
                    m_CrouchResizeCollider.offset = crouchColliderOffset;
                }
            }
            else
            {
                // Alter top collider for standing when not crouching
                if (m_CrouchResizeCollider != null)
                {
                    m_CrouchResizeCollider.size = standingColliderSize;
                    m_CrouchResizeCollider.offset = standingColliderOffset;
                }

                if (m_wasCrouching)
                {
                    m_wasCrouching = false;
                    OnCrouchEvent.Invoke(false);
                }
            }

            if (!dashing)
            {
                // Move the character by finding the target velocity
                Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
                // And then smoothing it out and applying it to the character
                m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
            }

            // If the input is moving the player right and the player is facing left...
            if (move > 0 && !m_FacingRight)
            {
                // ... flip the player.
                Flip();
            }
            // Otherwise if the input is moving the player left and the player is facing right...
            else if (move < 0 && m_FacingRight)
            {
                // ... flip the player.
                Flip();
            }
        }
        // If the player should jump...
        if (m_Grounded && jump)
        {
            // Add a vertical force to the player.
            m_Grounded = false;
            m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, 0f);
            m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));

            CreateDust();

            soundEffect.playJump();
            Debug.Log("Jumped");
        }

        // if moving horizontally or in the air, and not crouching and dashing, and had not dashed (in air)
        if ((!m_Grounded) && !crouch && dash && !hadDashed)
        {
            if (!dashing)
            {
                animator.SetTrigger("isDashing");
                m_Rigidbody2D.gravityScale = 2f;
                m_Rigidbody2D.drag = 1f;
                m_Rigidbody2D.velocity = Vector2.zero;

                if (m_FacingRight)
                {
                    m_Rigidbody2D.AddForce(new Vector2(dashForce, 150f));
                }
                else
                {
                    m_Rigidbody2D.AddForce(new Vector2(-dashForce, 150f));
                }

                // screen shake
                if (CinemachineShake.Instance != null)
                {
                    CinemachineShake.Instance.ShakeCamera(4f, 0.2f);
                }

                hadDashed = true;
                dashing = true;

                soundEffect.playDash();
            }
        }
    }


    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        transform.Rotate(0f, 180f, 0f);
    }

    private void CreateDust()
    {
        // Plays dust particle system
        dust.Play();
    }

    public void StopDash()
    {
        m_Rigidbody2D.gravityScale = 4f;
        m_Rigidbody2D.drag = 0f;
        //m_Rigidbody2D.velocity = Vector2.zero;
        dashing = false;
    }
}
