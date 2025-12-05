using UnityEngine;

public class Pinball : MonoBehaviour
{
    Rigidbody2D body;
    LayerMask ground, slope,water;
    bool leftPressed, rightPressed, jumpPressed, grounded, swimming, canSwim;
    AudioSource audioSource;
    public AudioClip jumpSound, fallSound;
    float airTime;
    public bool stunned;
    float timeStunned;
    public float swimDelay;
    public float maxSwimDelay;


    [SerializeField]
    public float waterGravityScale;
    [SerializeField]
    public int jumpAirTimeSetback;
    [SerializeField]
    public int airTimeMax;
    [SerializeField]    
    public int maxStunTime;

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
    [SerializeField]
    [Range(0, 1)]
    public float swimmingMovementPercentage;

    public Camera[] cameras; // Array to hold the cameras
    private int currentCameraIndex = 0;

    void roll(float direction, float percent)
    {
        float maxRollVel = maxRollSpeed * direction;
        float newVel = body.linearVelocity.x + percent * maxRollVel;
        if (Mathf.Abs(newVel) > maxRollSpeed)
            newVel = maxRollVel;
        body.linearVelocity = new Vector2(newVel, body.linearVelocity.y);
    }

    bool IsVisibleToCamera(Camera c)
    {
        Vector3 visTest = c.WorldToViewportPoint(transform.position);
        return (visTest.x >= 0 && visTest.y >= 0) && (visTest.x <= 1 && visTest.y <= 1) && visTest.z >= 0;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        ground = LayerMask.GetMask("Ground");
        slope = LayerMask.GetMask("Slope");
        water = LayerMask.GetMask("Water");
        audioSource = GetComponent<AudioSource>();
        stunned = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!stunned&&!Finale.finished)
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
        else {
            timeStunned += 200*Time.deltaTime;
        }
        if (timeStunned >= maxStunTime) {
            stunned = false;
            timeStunned = 0;
        }
        if (!grounded&&!swimming)
        {
            airTime += 200*Time.deltaTime;
        }
        else if (stunned) {
            airTime = 0;
        }
        if (swimming)
        {
            airTime = 0;
            body.gravityScale = waterGravityScale;
        }
        else {
            body.gravityScale = 1f;
        }

        // If pinball goes offscreen of current camera
        if (!IsVisibleToCamera(cameras[currentCameraIndex])) {
            // Disable the current camera
            cameras[currentCameraIndex].enabled = false;

            // Check which camera it's visible on
            if (currentCameraIndex > 0) {
                currentCameraIndex--;
                cameras[currentCameraIndex].enabled = true;
                if (!IsVisibleToCamera(cameras[currentCameraIndex])) {
                    cameras[currentCameraIndex].enabled = false;
                    currentCameraIndex += 2;
                    cameras[currentCameraIndex].enabled = true;
                }
            } else {
                currentCameraIndex++;
                cameras[currentCameraIndex].enabled = true;
            }
        }
    }

    // Put physics update stuff here
    void FixedUpdate()
    {
        swimming = false;
        if (!canSwim&&swimDelay<maxSwimDelay)
        {
            swimDelay += 1;
        }
        if (swimDelay >= maxSwimDelay) {
            canSwim = true;
        }

        // If ball is touching ground...
        if (Physics2D.Raycast(transform.position, Vector2.down,
            maxRayGrndCheckDist, ground) && !Finale.finished)
        {
            if (grounded == false)
            {
                if (OptionsMenu.soundsOn)
                {
                    audioSource.PlayOneShot(fallSound);
                }
                grounded = true;
                //stunning condition
                if (stunned)
                {
                    timeStunned -= 200;
                }
                if (airTime >= airTimeMax)
                {
                    stunned = true;
                    timeStunned = 0;
                }
                else
                {
                    airTime = 0;
                }
                
            }

            //only run when not stunned
            if (!stunned)
            {
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
                    if (OptionsMenu.soundsOn) {
                        audioSource.PlayOneShot(jumpSound);
                    }

                    airTime = jumpAirTimeSetback;
                    jumpPressed = false;
                }
            }

        }
        // Handle jump on slope
        // Second raycast to check for slope
        else if (Physics2D.Raycast(transform.position, Vector2.down, maxRaySlopeCheckDist, slope) && !Finale.finished)
        {
            if (grounded == false)
            {
                if (OptionsMenu.soundsOn)
                {
                    audioSource.PlayOneShot(fallSound);
                }
                grounded = true;
                //stunning condition
                if (stunned)
                {
                    timeStunned -= 200;
                }
                if (airTime >= airTimeMax)
                {
                    stunned = true;
                    timeStunned = 0;
                }
                else
                {
                    airTime = 0;
                }
                
            }

            //only run when not stunned
            if (!stunned)
            {
                if (jumpPressed)
                {
                    body.linearVelocity = new Vector2(body.linearVelocity.x, jumpSpeed);
                    if (OptionsMenu.soundsOn)
                    {
                        audioSource.PlayOneShot(jumpSound);
                    }
                    airTime = jumpAirTimeSetback;
                }
                if (leftPressed)
                    roll(-1f, midairMovementPercentage);
                if (rightPressed)
                    roll(1f, midairMovementPercentage);
            }

        }
        //water handling
        else if (Physics2D.CircleCast(transform.position,.1f, Vector2.zero,1f, water) && !Finale.finished)
        {
            swimming = true;
            if (leftPressed)
                roll(-1f, swimmingMovementPercentage);
            if (rightPressed)
                roll(1f, swimmingMovementPercentage);
            // Add friction to ball if no left or right input detected
            if (!leftPressed && !rightPressed)
                body.AddForceX(-body.linearVelocity.x * noInputFriction / 5,
                               ForceMode2D.Impulse);
            if (Input.GetKey(KeyCode.W)&&canSwim)
            {
                body.linearVelocity = new Vector2(body.linearVelocity.x, jumpSpeed/2);
                if (OptionsMenu.soundsOn)
                {
                    audioSource.PlayOneShot(jumpSound);
                }
                canSwim = false;
                swimDelay = 0;
            }
        }
        //not grounded on either flat or sloped surface
        else if (!Finale.finished)
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
