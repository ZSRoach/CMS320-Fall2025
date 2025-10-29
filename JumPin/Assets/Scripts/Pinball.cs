using UnityEngine;

public class Pinball : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public Rigidbody2D body;
    public bool isGrounded = true;

  

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
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

        if (isGrounded&&(Input.GetKey(KeyCode.W))) {
            body.AddForceY(10);
        }
    }
}
