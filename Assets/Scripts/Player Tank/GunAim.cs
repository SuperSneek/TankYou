using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAim : MonoBehaviour
{
    private Vector3 startRot = Vector3.up;
    [SerializeField]
    private Turret turret;
    [SerializeField]
    private Turret gun;
    [SerializeField]
    private Transform playercam;

    private bool aiming = false;

    public void StartAim()
    {
        turret.StartTurn(Vector2.zero);
        aiming = true;
    }

    public void StopAim()
    {
        turret.StopTurn();
        aiming = false;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (!aiming)
            return;
        Vector3 aimRotation = playercam.localRotation * Vector3.forward;
        aimRotation = aimRotation.normalized;
        //Get Yaw
        Vector3 yawDirection = new Vector3(aimRotation.x, 0f, aimRotation.z);
        float yawAngle = Vector3.Angle(yawDirection, Vector3.right) - 90;
        yawAngle *= -0.025f;
        //Get Pitch
        Vector3 pitchDirection = new Vector3(0, aimRotation.y, aimRotation.z);
        float pitchAngle = Vector3.Angle(pitchDirection, Vector3.up) - 90;
        pitchAngle *= 0.025f;
        Debug.Log(pitchAngle + " " + yawAngle);
        yawAngle = Mathf.Clamp(yawAngle, -0.5f, 0.5f);
        pitchAngle = Mathf.Clamp(pitchAngle, -0.2f, 0.2f);
        Vector2 output = new Vector2(yawAngle, pitchAngle);
        
        turret.UpdateTurn(output);

    }

    private Vector2 GetAimInput(Vector3 reference, Vector3 lookRotation)
    {
        //Step 1: Normalize vectors
        reference = reference.normalized;
        lookRotation = lookRotation.normalized;

        //Step 2: Get Polar coords
        Vector2 first = ToPolarCoordinates(reference);
        Vector2 second = ToPolarCoordinates(lookRotation);

        //Step 3: Get difference
        float xInput = first.x - second.x;
        float yInput = first.y - second.y;

        return new Vector2(xInput, yInput);
    }

    private Vector2 ToPolarCoordinates(Vector3 input)
    {
        float phi = Mathf.Acos(input.z);
        float rho = Mathf.Asin(input.y / Mathf.Sin(phi));

        return new Vector2(phi, rho);

    }
}
