using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerScript player = other.GetComponent<PlayerScript>();

            player.speed = Mathf.Clamp(player.speed - 2, 0, 3);
            player.realSpeed = Mathf.Clamp(player.speed - 1.5f, 0f, 5f);
            Debug.Log("J'AI MAAAAAAAL");
        }
    }
}
