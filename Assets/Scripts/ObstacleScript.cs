using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    public int damage = 1;
    public int passagersToLose = 3;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Train train = other.GetComponent<Train>();
            train.TakeDamage(damage);
            train.LosePassagers(passagersToLose);
            train.Collision();
            Debug.Log("Santé - " + damage + ", passagers - " + passagersToLose);
        }
    }
}
