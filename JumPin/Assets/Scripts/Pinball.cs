using UnityEngine;

public class Pinball : MonoBehaviour
{
    Rigidbody2D body;
    LayerMask ground, slope;
    bool leftPressed, rightPressed, jumpPressed, grounded;
    AudioSource audioSource;
    public AudioClip jumpSound, fallSound;

    // Maximum distance of raycast for checking if the ball is grounded
    [SerializeField]
    public float maxRayGrndCheckDist;
    [SerializeField]
    public float maxRaySlopeCheckDist;
    
    [SerializeField]
    public float maxRollSpeed; // Maximum speed the ball will roll at
    [SerializeField]
    public float jumpSpeed; // Upwards speed of ball when jump is pressed

    // Ground friction coefficient when not trying to roll
    [Range(0, 1)]
    public float noInputFriction;

    //midair direction change slowdown
    [SerializeField]
    [Range(0,1)]
    public float midairMovementPercentage;
    [SerializeField]
    [Range(0, 1)]
    public float groundedMovementPercentage;

    void roll(float direction, float percent)
    {
        float maxRollVel = maxRollSpeed * direction;
        float newVel = body.linearVelocity.x + percent * maxRollVel;
        if (Mathf.Abs(newVel) > maxRollSpeed)
            newVel = maxRollVel;
        body.linearVelocity = new Vector2(newVel, body.linearVelocity.y);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        ground = LayerMask.GetMask("Ground");
        slope = LayerMask.GetMask("Slope");
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
            leftPressed = true;
        else
            leftPressed = false;

        if (Input.GetKey(KeyCode.D))
            rightPressed = true;
        else
            rightPressed = false;
        
        if (Input.GetKey(KeyCode.W))
            jumpPressed = true;
        else
            jumpPressed = false;
    }

    // Put physics update stuff here
    void FixedUpdate()
    {


        // If ball is touching ground...
        if (Physics2D.Raycast(transform.position, Vector2.down,
            maxRayGrndCheckDist, ground))
        {
            if (grounded == false)
            {
                audioSource.PlayOneShot(fallSound);
                grounded = true;
            }
            if (leftPressed)
                roll(-1f, groundedMovementPercentage);
            if (rightPressed)
                roll(1f, groundedMovementPercentage);
            // Add friction to ball if no left or right input detected
            if (!leftPressed && !rightPressed)
                body.AddForceX(-body.linearVelocity.x * noInputFriction,
                               ForceMode2D.Impulse);

            // Handle jump
            if (jumpPressed)
            {
                body.linearVelocity = new Vector2(body.linearVelocity.x, jumpSpeed);
                audioSource.PlayOneShot(jumpSound);
                jumpPressed = false;
            }
        }
        // Handle jump on slope
        // Second raycast to check for slope
        else if (Physics2D.Raycast(transform.position, Vector2.down, maxRaySlopeCheckDist, slope))
        {
            if (grounded == false)
            {
                audioSource.PlayOneShot(fallSound);
                grounded = true;
            }
            if (jumpPressed)
            {
                body.linearVelocity = new Vector2(body.linearVelocity.x, jumpSpeed);
                audioSource.PlayOneShot(jumpSound);
            }
            if (leftPressed)
                roll(-1f, midairMovementPercentage);
            if (rightPressed)
                roll(1f, midairMovementPercentage);
        }
        //not grounded on either flat or sloped surface
        else
        {
            grounded = false;
            //handles movement for in-air (slower directional change)
            if (leftPressed)
                roll(-1f, midairMovementPercentage);
            if (rightPressed)
                roll(1f, midairMovementPercentage);
        }
    }
}
