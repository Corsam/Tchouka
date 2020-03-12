using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    public Vector3 offset;

    public void SetPosition(Vector3 position)
    {
        Debug.Log("Je set comme jaja frère");
        transform.position = position + offset;
    }
}
