using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public int coinsToGive = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Train train = other.GetComponent<Train>();
            train.TakeCoins(coinsToGive);
            Destroy(this.gameObject);
            //Debug.Log("Par ici la monnaie");
        }
    }
}
