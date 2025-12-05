using UnityEngine;
using UnityEngine.SceneManagement;
public class Finale : MonoBehaviour
{
    bool triggered = false;
    public static bool finished = false;
    Transform transform;
    GameObject pinball;
    Rigidbody2D body;
    int count = 0;
    public GameObject fade;
    public GameObject slidingWall;
    public GameObject credits;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (triggered) {
            count += 1;
            if (count >500&&count< 750) {
                transform.position = new Vector2(transform.position.x, transform.position.y - .01f);
            }
            if (count < 100) {
                slidingWall.transform.position = new Vector2(slidingWall.transform.position.x, slidingWall.transform.position.y + .02f);
            }
            if (count > 1050&&count<1060) {
                transform.position = new Vector2(transform.position.x, transform.position.y + .2f);
            }
            if (count > 1061&&count<1126) {
                body.linearVelocityY = 40;
                fade.transform.position = new Vector2(fade.transform.position.x, fade.transform.position.y + .2f);
            }
            if (count > 1350) {
                credits.transform.position = fade.transform.position;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!triggered)
        {
            triggered = true;
            finished = true;
            pinball = collision.gameObject;
            body = pinball.GetComponent<Rigidbody2D>();
        }
    }
}
