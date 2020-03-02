using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starting : MonoBehaviour
{
    LevelManager lm;
    //bool tchoutchou;
    public bool[] checklist;

    private void Start()
    {
        lm = GetComponent<LevelManager>();
        lm.train.Initialize();
        Debug.Log("Normalement c'est bon");
    }

    public void Tchoutchou()
    {
        checklist[0] = true;
        LaunchGame();
    }

    void LaunchGame()
    {
        foreach (bool req in checklist)
        {
            if (!req)
            {
                return;
            }
        }
        lm.LaunchGame();
    }
}
