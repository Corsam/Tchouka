using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public CameraTarget target;

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.transform.position, 0.2f);
    }
}
