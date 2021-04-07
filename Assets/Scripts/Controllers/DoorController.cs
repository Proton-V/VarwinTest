using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : DestructibleObjectController
{
    public HingeJoint HingeJoint;
    public bool IsActive = false;

    public override void Start()
    {
        base.Start();

        Manager.HealthSlider.maxValue = Health;
        Manager.HealthSlider.value = Health;
    }
    private void Update()
    {
        if (IsActive)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                HingeJoint.useMotor = false;
                HingeJoint.useMotor = true;
            }
        }
    }

    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        if (other.tag == "Player")
        {
            CanvasManager.SetTextMsg("Press E to Open Door");
            IsActive = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            CanvasManager.SetTextMsg("");
            IsActive = false;
            HingeJoint.useMotor = false;
        }
    }
}
