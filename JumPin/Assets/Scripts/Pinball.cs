using UnityEngine;

public class Pinball : MonoBehaviour
{
    Rigidbody2D body;
    LayerMask ground;
    bool leftPressed, rightPressed, jumpPressed;

    // Maximum distance of raycast for checking if the ball is grounded
    public float maxRayGrndCheckDist;
    public float maxRollSpeed; // Maximum speed the ball will roll at
    public float jumpSpeed; // Upwards speed of ball when jump is pressed

    // Ground friction coefficient when not trying to roll
    [Range(0, 1)]
    public float noInputFriction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        ground = LayerMask.GetMask("Ground");
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
        if (leftPressed)
            body.linearVelocity = new Vector2(-maxRollSpeed, body.linearVelocity.y);

        if (rightPressed)
            body.linearVelocity = new Vector2(maxRollSpeed, body.linearVelocity.y);

        // If ball is touching ground...
        if (Physics2D.Raycast(transform.position, Vector2.down,
            maxRayGrndCheckDist, ground)) {
            // Add friction to ball if no left or right input detected
            if (!leftPressed && !rightPressed)
                body.AddForceX(-body.linearVelocity.x * noInputFriction,
                               ForceMode2D.Impulse);

            // Handle jump
            if (jumpPressed)
                body.linearVelocity = new Vector2(body.linearVelocity.x, jumpSpeed);
        }
    }
}
