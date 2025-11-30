using UnityEngine;

public class StunHandler : MonoBehaviour
{

    Transform transform;
    public Pinball pinball;
    SpriteRenderer renderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform = GetComponent<Transform>();
        renderer = GetComponent<SpriteRenderer>();
        renderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        float y = pinball.transform.position[1]+.3f;
        transform.position = new Vector3(pinball.transform.position[0], y, pinball.transform.position[2]);
        if (pinball.stunned)
        {
            renderer.enabled = true;
        }
        else {
            renderer.enabled = false;
        }
    }
}
