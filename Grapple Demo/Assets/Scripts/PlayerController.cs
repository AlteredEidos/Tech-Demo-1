using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //variable
    public LayerMask grappleMask;
    LineRenderer line;
    DistanceJoint2D rope;
    Rigidbody2D playerRB;
    private Vector2 velocity;
    private Vector2 lookPos;
    private Vector2 groundDetection;
    private Vector2 spawnPos; 
    private Vector2 levelLimit;
    private bool doubleJump = false;
    private bool ropeCheck = false;
    private float groundDetectionDistance = .1f;
    private float speed = 10f;
    private float jumpHeight = 10f;
    public float ropeDistance = 90f;

    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        rope = gameObject.AddComponent<DistanceJoint2D>();
        line = GetComponent<LineRenderer>();
        spawnPos = new Vector2 (0, 0);
        levelLimit = new Vector2 (0, -10);
        line.enabled = false;
        rope.enabled = false;
    }
    
    void Update()
    {
        //grapple
        line.SetPosition(0, transform.position);

        lookPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        if (Input.GetMouseButtonDown(0) && ropeCheck == false)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, lookPos, ropeDistance, grappleMask);

            if (hit.collider != null)
            {
             ropeCheck = true;
             SetRope(hit);
            }
        }
        else if (Input.GetMouseButtonDown(0) && ropeCheck == true)
        {
            ropeCheck = false;
            DestroyRope();
        }
        
        //player movemnet
        velocity = playerRB.velocity;
        velocity.x = Input.GetAxisRaw("Horizontal") * speed;

        //jump
        groundDetection = new Vector2(transform.position.x, transform.position.y-.75f);

        if (Input.GetKeyDown(KeyCode.W) && Physics2D.Raycast(groundDetection, Vector2.down, groundDetectionDistance))
        {
            velocity.y = jumpHeight;
            doubleJump = true;
        }
        else if (Input.GetKeyDown(KeyCode.W) && doubleJump == true)
        {
            velocity.y = jumpHeight;
            doubleJump = false;
        }

        //actually move player
        playerRB.velocity = velocity;

        //fall teleport
        if (transform.position.y <= levelLimit.y)
        {
            transform.position = spawnPos;
        }
    }

    void SetRope(RaycastHit2D hit)
    {
        rope.enabled = true;
        rope.connectedAnchor = hit.point;

        line.enabled = true;
        line.SetPosition(1, hit.point);
    }

    void DestroyRope()
    {
        rope.enabled = false;
        rope.enabled = false;
    }
}
