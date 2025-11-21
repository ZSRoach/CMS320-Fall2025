using UnityEngine; // Gives access to Unity functions, MonoBehaviour, Transform, etc.

public class SpriteRotator : MonoBehaviour // Allows this script to be attached to a GameObject
{
    public float minSpeed = -100f; // Minimum possible rotation speed (negative = clockwise)
    public float maxSpeed = 100f;  // Maximum possible rotation speed (positive = counterclockwise)
    public float changeInterval = 2f; // Time (in seconds) before rotation direction changes again

    private float rotationSpeed; // Stores the current rotation speed for this specific object
    private float timer; // Tracks how much time has passed since the last direction change

    void Start()
    {
        // Pick a random rotation speed at the start between minSpeed and maxSpeed
        rotationSpeed = Random.Range(minSpeed, maxSpeed);
    }

    void Update()
    {
        // Increase timer by the time that passed since last frame
        timer += Time.deltaTime;

        // If enough time has passed, choose a new random rotation direction
        if (timer >= changeInterval)
        {
            rotationSpeed = Random.Range(minSpeed, maxSpeed); // Pick a new random speed
            timer = 0f; // Reset timer after changing direction
        }

        // Rotate the object around Z-axis (2D rotation)
        // Time.deltaTime makes rotation smooth and consistent
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
}
