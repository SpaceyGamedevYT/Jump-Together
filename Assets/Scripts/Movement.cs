using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public bool one;
    float speed;
    float input;

    float jumpForce;

    public GameObject arrow;
    public Transform spriteObj;

    protected Vector2 velocity;
    protected const float minMoveDistance = 0.001f;
    protected const float shellRadius = 0.01f;
    protected ContactFilter2D contactFilter;
    protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
    protected List<RaycastHit2D> hitBufferList = new List<RaycastHit2D>(16);
    protected bool grounded;
    protected Vector2 groundNormal;
    Vector2 deltaPosition;
    Vector2 move;
    Vector2 targetVelocity;

    Rigidbody2D rb;
    Animator anim;
    public PlayerController playerSelect;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        contactFilter.useTriggers = false;
        contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
        contactFilter.useLayerMask = true;

        speed = playerSelect.speed;
        jumpForce = playerSelect.jumpForce;
    }

    void Update()
    {
        input = Input.GetAxisRaw("Horizontal");

        // jumping
        if(one == playerSelect.player1Selected)
        {
            if(Input.GetKeyDown(KeyCode.Z) && grounded)
            {
                velocity.y = jumpForce;
                anim.SetTrigger("takeoff");
            }
            else if(Input.GetKeyUp(KeyCode.Z))
            {
                if(velocity.y > 0)
                {
                    //velocity.y = velocity.y * 0.5f;
                }
            }

            // walking animation
            if(input != 0)
            {
                anim.SetBool("isRunning", true);
            }
            else
            {
                anim.SetBool("isRunning", false);
            }

            // jumping animation
            if(grounded)
            {
                anim.SetBool("isJumping", false);
            }
            else
            {
                anim.SetBool("isJumping", true);
            }

            // flip sprites
            if(input < 0)
            {
                spriteObj.eulerAngles = new Vector3(0, 180, 0);
            }
            else if(input > 0)
            {
                spriteObj.eulerAngles = new Vector3(0, 0, 0);
            }

            // show and hide arrow
            arrow.SetActive(true);
        }
        else
        {
            arrow.SetActive(false);

            anim.SetBool("isRunning", false);
            anim.SetBool("isJumping", false);
        }
    }

    private void FixedUpdate() 
    {
        // move the character
        velocity.x = targetVelocity.x;

        if(one == playerSelect.player1Selected)
        {
            Vector2 moveAlongGround = new Vector2(groundNormal.y, -groundNormal.x);
            move = moveAlongGround * deltaPosition.x;
            GravityAndCollisions(move, false);
            targetVelocity.x = input * speed;
        }

        velocity += playerSelect.gravityModifier * Physics2D.gravity * Time.deltaTime;

        grounded = false;

        deltaPosition = velocity * Time.deltaTime;
        move = Vector2.up * deltaPosition.y;
        GravityAndCollisions(move, true);
    }

    void GravityAndCollisions(Vector2 move, bool yMovement)
    {
        float distance = move.magnitude;
        if(distance > minMoveDistance)
        {
            int count = rb.Cast(move, contactFilter, hitBuffer, distance + shellRadius);
            hitBufferList.Clear();
            for (int i = 0; i < count; i++)
            {
                hitBufferList.Add(hitBuffer[i]);
            }

            for (int i = 0; i < hitBufferList.Count; i++)
            {
                Vector2 currentNormal = hitBufferList[i].normal;
                if(currentNormal.y > playerSelect.minGroundNormalY)
                {
                    grounded = true;
                    if(yMovement)
                    {
                        groundNormal = currentNormal;
                        currentNormal.x = 0;
                    }
                }
                float projection = Vector2.Dot(velocity, currentNormal);
                if(projection < 0)
                {
                    velocity = velocity - projection * currentNormal;
                }
                float modifiedDistance = hitBuffer[i].distance - shellRadius;
                distance = modifiedDistance < distance ? modifiedDistance : distance;
            }
        }
        rb.position = rb.position + move.normalized * distance;
    }
}