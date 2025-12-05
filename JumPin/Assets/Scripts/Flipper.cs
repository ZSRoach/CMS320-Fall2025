using UnityEngine;

public class Flipper : MonoBehaviour
{
    public bool triggered;
    HingeJoint2D hinge;
    JointAngleLimits2D limits;
    JointMotor2D motor;
    public bool hingeLeft;
    public bool upsideDown;
    int count;
    float orSpeed;
    JointAngleLimits2D orLimits;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        triggered = false;
        hinge = GetComponent<HingeJoint2D>();
        hinge.useMotor = false;
        motor = hinge.motor;
        limits = hinge.limits;
        orLimits = limits;
 
        if (hingeLeft) motor.motorSpeed = -300;
        else { motor.motorSpeed = 300; }

        orSpeed = motor.motorSpeed;
        motor.maxMotorTorque = 200;
        count = 0;
        hinge.motor = motor;

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
            if (!upsideDown)
            {
                hinge.useMotor = false;
            }
            else {
                if (count == 11) {
                    limits.max = 0;
                    limits.min = 0;
                    motor.motorSpeed *= -1f;
                    hinge.motor = motor;
                    hinge.limits = limits;
                }
                
                if (count == 40) {
                    hinge.useMotor = false;
                    limits = orLimits;
                    motor.motorSpeed = orSpeed;
                    hinge.motor = motor;
                    hinge.limits = limits;
                    hinge.useMotor = false;
                    
                }
            }
        }
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!triggered&&collision.gameObject.CompareTag("Player")) {
            triggered = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        triggered = false;
    }
}
