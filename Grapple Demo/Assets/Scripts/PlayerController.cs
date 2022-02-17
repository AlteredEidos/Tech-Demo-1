using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //variable
    public GameObject bangSpawn;
    public GameObject bang;
    public Camera mCam;
    public Rigidbody2D gun;
    public LayerMask grappleMask;
    public LayerMask jumpMask;
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
    private float jumpHeight = 7.5f;
    private float angle;
    private float bangSpeed = 60f;
    private float bangLifespan = 4f;

    public int health = 3;
    public int ammo = 10;

    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        rope = gameObject.AddComponent<SpringJoint2D>();
        line = GetComponent<LineRenderer>();
        spawnPos = new Vector2(0, 0);
        levelLimit = new Vector2(0, -4.5f);
        line.enabled = false;
        rope.enabled = false;
    }

    void Update()
    {
        //follow camera
        mCam.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
        gun.gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, 0);

        //tracks player mouse position
        lookPos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        //gun rotation
        angle = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg;
        //rotation of the gun
        gun.rotation = angle;

        if (Input.GetMouseButtonDown(0))
        {
            //create the bullet
            GameObject b = Instantiate(bang, bangSpawn.transform.position, bangSpawn.transform.rotation, transform);
            //igonore the guns collision
            Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), GetComponent<CircleCollider2D>(), b.GetComponent<BoxCollider2D>());
            //set the speed of the bullet
            b.GetComponent<Rigidbody2D>().velocity = new Vector2(lookPos.x * bangSpeed, lookPos.y * bangSpeed);
            //set how long the bullet will stay in scene for
            Destroy(b, bangLifespan);
        }

        //grapple
        //set line in the player
        line.SetPosition(0, transform.position);
        
        //on left mouse click
        if (Input.GetMouseButtonDown(1) && ropeCheck == false)
        {
            //send raycast
            RaycastHit2D hit = Physics2D.Raycast(bangSpawn.transform.position, lookPos, 5, grappleMask);

            //on hit set grapple
            if (hit.collider != null)
            {
                ropeCheck = true;
                SetRope(hit);
                doubleJump = false;
            }
            //on left mouse up remove any grapple
        }
        else if (Input.GetMouseButtonUp(1) && ropeCheck == true)
        {
            ropeCheck = false;
            DestroyRope();
            doubleJump = true;
        }

        //player movemnet
        velocity = playerRB.velocity;
        velocity.x = Input.GetAxisRaw("Horizontal") * speed;

        //jump
        groundDetection = new Vector2(transform.position.x, transform.position.y - .75f);

        if (Input.GetKeyDown(KeyCode.W) && Physics2D.Raycast(groundDetection, Vector2.down, groundDetectionDistance, jumpMask))
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

    //set grapple
    void SetRope(RaycastHit2D hit)
    {
        //set spring joint variables and location
        rope.enabled = true;
        rope.autoConfigureConnectedAnchor = false;
        rope.autoConfigureDistance = false;
        rope.connectedAnchor = hit.point;

        float distanceFromPoint = Vector2.Distance(bangSpawn.transform.position, hit.point);

        rope.distance = 0.5f;
        rope.frequency = 0;
        rope.dampingRatio = 1f;

        //set the line between 
        line.enabled = true;
        line.SetPosition(1, hit.point);
    }

    //destroy grapple
    void DestroyRope()
    {
        rope.enabled = false;
        line.enabled = false;
    }
}
