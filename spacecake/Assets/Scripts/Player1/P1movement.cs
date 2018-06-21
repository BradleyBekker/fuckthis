using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class P1movement : MonoBehaviour
{
    [SerializeField] public GameObject runFX;//
    float animspeed = 0;
    public bool _isOnGround = false;

    [SerializeField] private GameObject playerrocket;

    [SerializeField] private GameObject part1;
    [SerializeField] private GameObject part2;
    [SerializeField] private GameObject part3;
    Animator anim;
    public Rigidbody2D rb;

    public Vector2 position;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        animspeed = 0;
        
        position.x = transform.position.x;
        position.y = transform.position.y;

        calculateForce();
        particle();

        anim.SetFloat("speed", animspeed);
    }

    private void calculateForce()
    {
        calculateMovement();
        calculateJump();
        calculateGravity();

        rb.MovePosition( position );
    }

    // movement variables
    public float minMovementSpeed = .1f;
    public float maxMovementSpeed = .12f;
    private float movementSpeedRange;
    private float movementFactor = 0;
    public float movementFactorGrowth = 11;
    // jump variables
    public float jumpHeight = .12f;
    private float jumpFactor = 60;
    public float jumpFactorGrowth = 2;
    private float maxJumpFalldown = 180;
    private bool jumping = false;
    // gravity variables
    private float minFalldown = 180;
    private float maxFalldown = 270; // used for both jump and gravity so the gravity after the jump aligns with fall gravity
    private float falldownFactor = 180;
    public float falldownFactorGrowth = 1;

    private float gravityFactor = 0.01f;

    private void calculateMovement()
    {
        if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            position.x -= calculateMovementSin();
            transform.localScale = new Vector3(-1, 1, 1);
            animspeed = 1;
        }
        if (Input.GetKey(KeyCode.D)&& !Input.GetKey(KeyCode.A))
        {
            position.x += calculateMovementSin();
            transform.localScale = new Vector3(1, 1, 1);
            animspeed = 1;
        }
    }

    private float calculateMovementSin()
    {
        movementSpeedRange = maxMovementSpeed - minMovementSpeed;
        movementFactor += movementFactorGrowth;
        movementFactor %= 360;
        return minMovementSpeed + (movementSpeedRange / 2) + movementSpeedRange * (float) Math.Sin(Math.PI * movementFactor / 180);
    }

    private void calculateJump() // when the jumpFactor equals 180 the player falls down
    {
        if (Input.GetKeyDown(KeyCode.W) && _isOnGround)
        {
            _isOnGround = false;
            jumping = true;
            jumpFactor = 60;
            anim.SetTrigger("prep");
            anim.SetBool("jumping",true);
        }

        if(!_isOnGround)
        {
            float jumpValue = (float) (jumpHeight * Math.Sin(Math.PI * jumpFactor / 180));
            if(jumpFactor + jumpFactorGrowth > maxJumpFalldown)
            {
                jumping = false;
            }
            else
            {
                jumpFactor += jumpFactorGrowth;
                position.y += jumpValue;
            }

        }
        if (jumpFactor == 180)
        {
            anim.SetBool("jumping", false);
            anim.SetBool("falling", true);
        }
    }

    private void calculateGravity()
    {
        position.y -= gravityFactor;
        if(!jumping)
        {
            position.y += (float) (jumpHeight * Math.Sin(Math.PI * falldownFactor / 180));
            if(falldownFactor + falldownFactorGrowth > maxFalldown)
            {
                falldownFactor = maxFalldown;
            }
            else falldownFactor += falldownFactorGrowth;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground") 
        {
            _isOnGround = true;
            jumping = false;
            jumpFactor = 60;
            falldownFactor = minFalldown;
            anim.SetBool("falling", false);
            anim.SetTrigger("land");
        }
        if(collision.gameObject.tag == "floor")
        {
            jumping = false;
            jumpFactor = 180;
        }
    }

    private void Death()
    {
        Vector2 spawn = new Vector2(1.82f, 0.25f);
        Vector3 item1spawn = new Vector3(transform.position.x + 5, transform.position.y, transform.position.z);
        Vector3 item2spawn = new Vector3(transform.position.x + -5, transform.position.y, transform.position.z);
        Vector3 item3spawn = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        if (playerrocket.GetComponent<P1rocket>().part1 == true)
        {
            playerrocket.GetComponent<P1rocket>().part1 = false;
            Instantiate(part1, item1spawn, Quaternion.identity);

        }
        if (playerrocket.GetComponent<P1rocket>().part2 == true)
        {
            playerrocket.GetComponent<P1rocket>().part2 = false;
            Instantiate(part2, item2spawn, Quaternion.identity);

        }
        if (playerrocket.GetComponent<P1rocket>().part3 == true)
        {
            playerrocket.GetComponent<P1rocket>().part3 = false;
            Instantiate(part3, item3spawn, Quaternion.identity);

        }
        transform.position = spawn;

    }
    void particle()
    {
        if (animspeed > 0f && _isOnGround)//
        {
            runFX.SetActive(true);//
        }
        else//
        {
            runFX.SetActive(false);//
        }
    }


}
