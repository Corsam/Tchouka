using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gare : MonoBehaviour
{
    public int passagersToTake = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Train train = other.GetComponent<Train>();
            train.SetNearestGare(this);
            Debug.Log("J'entre dans la gare");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Train train = other.GetComponent<Train>();
            train.SetNearestGare(null);
            Debug.Log("Je pars de la gare");
        }
    }
}
