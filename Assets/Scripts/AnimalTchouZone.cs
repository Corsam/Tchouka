using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalTchouZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Train train = FindObjectOfType<Train>();
            train.SetNearestHerd(this.GetComponentInParent<Animal>());
            //Debug.Log("Attention le nanimal");
        }
    }
}
