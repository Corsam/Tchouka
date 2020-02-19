using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tremplin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Train train = other.GetComponent<Train>();
            train.Jump();
            Debug.Log("débisou je manvole");
        }
    }
}
