using UnityEngine;

public class Flipper : MonoBehaviour
{
    public bool triggered;
    HingeJoint2D hinge;
    JointMotor2D motor;
    public bool hingeLeft;
    int count;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        triggered = false;
        hinge = GetComponent<HingeJoint2D>();
        if (hingeLeft) motor.motorSpeed = -300;
        else motor.motorSpeed = 300;
        motor.maxMotorTorque = 200;
        hinge.motor = motor;
        hinge.useMotor = false;
        count = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (triggered)
        {
            count = 0;
            hinge.useMotor = true;
        }
        if (triggered == false) {
            count += 1;
        }
        if (count > 10)
        {
            hinge.useMotor = false;
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
    }
}
