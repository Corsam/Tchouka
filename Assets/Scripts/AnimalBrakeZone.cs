using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalBrakeZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Train train = other.GetComponent<Train>();
            train.AnimalBrake();
            //Debug.Log("LE FREIN !");
        }
    }
}
