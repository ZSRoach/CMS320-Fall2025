using UnityEngine;

public class Pinball : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public Rigidbody2D body;
    public CircleCollider2D collider;
    public LayerMask ground;

    public bool isGrounded(){
        return Physics2D.Raycast(body.position, Vector2.down, collider.bounds.extents.y+.1f, ground);
    }

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        collider = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {


        if ((Input.GetKey(KeyCode.A))){
            body.AddForceX(-2);
        }
        if ((Input.GetKey(KeyCode.D))){
            body.AddForceX(2);
        }

        if ((Input.GetKey(KeyCode.W))&&isGrounded()) {
            body.AddForceY(10);
        }
    }
}
