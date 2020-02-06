﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    public int damage = 1;
    public float speedLoss = 30;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Train train = other.GetComponent<Train>();
            train.TakeDamage(damage);
            Debug.Log("J'AI MAL DE " + damage);
        }
    }
}
