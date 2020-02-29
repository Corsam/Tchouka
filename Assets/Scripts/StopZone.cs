using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Train train = other.GetComponent<Train>();
            train.EnterStopZone();
            Debug.Log("FREINE C'EST LA FIN !");
        }
    }
}
