using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation.Examples;

public class MinionManager : MonoBehaviour
{
    public GameObject[] minions;
    public GameObject leader;

    private void Start()
    {
        leader = minions[0];
    }

    public void ChangeLeader (int newLeader)
    {
        if (newLeader >= 0 && newLeader < minions.Length)
        {
            leader = minions[newLeader];
            for (int i = 0; i < minions.Length; i++)
            {
                minions[i].GetComponent<PathFollower>().Activate(i == newLeader);
                minions[i].GetComponent<PathFollower>().leader = minions[newLeader];
                minions[i].GetComponent<PathFollower>().isLeader = (i == newLeader);
            }
        }
    }
}
