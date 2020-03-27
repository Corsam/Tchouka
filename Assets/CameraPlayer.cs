using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayer : MonoBehaviour
{
    public Transform targetSpeed0;
    public Transform targetSpeed1;
    public Transform targetSpeed2;
    public Transform targetBoost;

    Transform currentTarget;
    Transform backTarget;

    public float lerpForce;

    public enum SpeedParam { Speed0, Speed1, Speed2, Boost};

    private void Awake()
    {
        SetBothTarget(SpeedParam.Speed0);
    }

    void Update()
    {
        if ((transform.position - currentTarget.position).magnitude > 0.05)
        {
            transform.position = Vector3.Lerp(transform.position, currentTarget.position, lerpForce);
        }
    }

    public void SetCurrentTarget(SpeedParam param)
    {
        currentTarget = GiveTarget(param);
    }

    public void SetBackTarget(SpeedParam param)
    {
        backTarget = GiveTarget(param);
    }

    public void SetBothTarget(SpeedParam param)
    {
        currentTarget = GiveTarget(param);
        backTarget = GiveTarget(param);
    }

    public void SwitchToBackTarget()
    {
        currentTarget = backTarget;
    }

    private Transform GiveTarget(SpeedParam param)
    {
        switch (param)
        {
            case SpeedParam.Speed0:
                return targetSpeed0;
            case SpeedParam.Speed1:
                return targetSpeed1;
            case SpeedParam.Speed2:
                return targetSpeed2;
            case SpeedParam.Boost:
                return targetBoost;
            default:
                return null;
        }
    }
}
