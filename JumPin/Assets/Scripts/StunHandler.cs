using UnityEngine;

public class StunHandler : MonoBehaviour
{

    Transform t;
    public Pinball pinball;
    SpriteRenderer sRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        t = GetComponent<Transform>();
        sRenderer = GetComponent<SpriteRenderer>();
        sRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        float y = pinball.transform.position[1]+.3f;
        t.position = new Vector3(pinball.transform.position[0], y, pinball.transform.position[2]);
        if (pinball.stunned)
        {
            sRenderer.enabled = true;
        }
        else {
            sRenderer.enabled = false;
        }
    }
}
