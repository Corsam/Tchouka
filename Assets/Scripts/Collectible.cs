using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public int coinsToGive = 1;
    public AudioClip clip;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Train train = FindObjectOfType<Train>();
            train.TakeCoins(coinsToGive);
            train.CollectibleSound(clip);
            Destroy(this.gameObject);
            //Debug.Log("Par ici la monnaie");
        }
    }
}
