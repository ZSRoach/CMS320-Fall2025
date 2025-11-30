using UnityEngine;

public class Flipper : MonoBehaviour
{
    public bool triggered;
    HingeJoint2D hinge;
    JointMotor2D motor;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        triggered = false;
        hinge = GetComponent<HingeJoint2D>();
        motor.motorSpeed = 300;
        motor.maxMotorTorque = 200;
        hinge.useMotor = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (triggered)
        {
            hinge.useMotor = true;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!triggered) {
            triggered = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        triggered = false;
        hinge.useMotor = false;
    }
}
