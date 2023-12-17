using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Horizontal Movement Settings")]
        
    [SerializeField] private float walkSpeed = 1;
    [SerializeField] private float jumpForce = 45;
    private float jumpBufferTimer = 0f;
    [SerializeField] private float jumpBufferTime = 0.2f;

    [Header("Ground Check Settings")]
    [SerializeField] private Transform groundCheckPoint;
    [SerializeField] private float groundCheckY = 0.2f;
    [SerializeField] private float groundCheckX = 0.5f;
    [SerializeField] private LayerMask whatIsGround;

    PlayerStateList pState;
    private Rigidbody2D rb;
    private float xAxis;
    Animator anim;

    public static PlayerController Instance;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
     pState = GetComponent<PlayerStateList>();
     rb = GetComponent<Rigidbody2D>();
     anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInputs();
        UpdateJumpVariables();
        Flip();
        
    }

    void FixedUpdate()
    {
        Move();
        Jump();
    }

    void GetInputs()
    {
        xAxis = Input.GetAxisRaw("Horizontal");
    }

    void Flip()
    {
        if(xAxis < 0)
        {
            transform.localScale = new Vector2(-1, transform.localScale.y);
        }
        else if (xAxis > 0) {
            transform.localScale = new Vector2(1, transform.localScale.y);
        }
    }
    private void Move()
    {
        rb.velocity = new Vector2(walkSpeed * xAxis, rb.velocity.y);
        anim.SetBool("walking", rb.velocity.x != 0 && Grounded());
        
    }

    public bool Grounded()
    {
        if(Physics2D.Raycast(groundCheckPoint.position, Vector2.down, groundCheckY, whatIsGround) 
            || Physics2D.Raycast(groundCheckPoint.position + new Vector3(groundCheckX, 0, 0), Vector2.down, groundCheckY, whatIsGround)
            || Physics2D.Raycast(groundCheckPoint.position + new Vector3(-groundCheckX, 0, 0), Vector2.down, groundCheckY, whatIsGround)) 
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void Jump()
    {
        if(Input.GetButtonUp("Jump") && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);

            pState.jumping = false;
        }
        if (!pState.jumping)
        {
            if (jumpBufferTimer > 0 && Grounded())
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                pState.jumping = true;
                jumpBufferTimer = 0;
            }

        }
      
        anim.SetBool("jumping", !Grounded());
    }

    void UpdateJumpVariables()
    {
        if (Grounded())
        {
            pState.jumping = false;
        }
        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferTimer = jumpBufferTime;  // Use time instead of frames
        }
        else if (jumpBufferTimer > 0)
        {
            jumpBufferTimer -= Time.deltaTime;  // Decrease timer based on time
        }
    }
}
