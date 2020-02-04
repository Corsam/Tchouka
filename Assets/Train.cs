using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour
{
    public MinionManager mm;

    public float currentSpeed;
    public float speedLeverValue = 5;
    public float brakeForce = 0.06f;
    public float speedChangeForce = 0.02f;

    bool isBraking = false;

    void Update()
    {

        //Vérifie si le frein est appuyé ou pas
        if (isBraking)
        {
            currentSpeed = Mathf.Lerp(currentSpeed, 0, brakeForce);
        }
        else
        {
            currentSpeed = Mathf.Lerp(currentSpeed, speedLeverValue, speedChangeForce);
        }

        mm.leader.speed = currentSpeed;

    }

    public void Brake()
    {
        isBraking = true;
    }

    public void StopBrake()
    {
        isBraking = false;
    }

    public void SetSpeedLeverValue(float newSpeed)
    {
        speedLeverValue = newSpeed;
    }
}
