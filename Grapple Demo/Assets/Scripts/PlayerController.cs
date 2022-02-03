using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //variable
    public LayerMask grappleMask;
    LineRenderer line;
    SpringJoint2D rope;
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
    public float ropeDistance = 1;

    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        rope = gameObject.AddComponent<SpringJoint2D>();
        line = GetComponent<LineRenderer>();
        spawnPos = new Vector2 (0, 0);
        levelLimit = new Vector2 (0, -4.5f);
        line.enabled = false;
        rope.enabled = false;
    }
    
    void Update()
    {
        //grapple
        line.SetPosition(0, transform.position);

        lookPos = Camera.main.ScreenToWorldPoint(Input.mousePosition)-transform.position;
        
        if (Input.GetMouseButtonDown(0) && ropeCheck == false)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, lookPos, 10, grappleMask);

            if (hit.collider != null)
            {
             ropeCheck = true;
             SetRope(hit);
             doubleJump = false;
            }
        }
        else if (Input.GetMouseButtonUp(0) && ropeCheck == true)
        {
            ropeCheck = false;
            DestroyRope();
            doubleJump = true;
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
        rope.autoConfigureConnectedAnchor = false;
        rope.autoConfigureDistance = false;
        rope.connectedAnchor = hit.point;

        float distanceFromPoint = Vector2.Distance(transform.position, hit.point);

        rope.distance = 0.5f;
        rope.frequency = 0;
        rope.dampingRatio = 1f;

        line.enabled = true;
        line.SetPosition(1, hit.point);
    }

    void DestroyRope()
    {
        rope.enabled = false;
        line.enabled = false;
    }
}
