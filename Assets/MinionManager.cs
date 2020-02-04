using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation.Examples;

public class MinionManager : MonoBehaviour
{
    public PathFollower[] minions;
    public PathFollower leader;

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
                minions[i].Activate(i == newLeader);
                minions[i].leader = minions[newLeader].gameObject;
                minions[i].isLeader = (i == newLeader);
            }
        }
    }
}
