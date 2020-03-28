using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starting : MonoBehaviour
{
    LevelManager lm;
    //bool tchoutchou;
    public bool[] checklist = new bool[2];

    private void Start()
    {
        lm = GetComponent<LevelManager>();
        lm.train.Initialize();
        //Debug.Log("Normalement c'est bon");
    }

    public void Vitesse1(bool activated)
    {
        if (activated)
        {
            if (!checklist[0])
            {
                checklist[0] = true;
            }
        }
        else
        {
            if (checklist[0])
            {
                checklist[0] = false;
            }
        }  
    }

    public void Tchoutchou()
    {
        if (checklist[0])
        {
            checklist[1] = true;
            LaunchGame();
        }
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
