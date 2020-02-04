using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public MinionManager mm;
    public GameObject trainGO;

    private Train train;
    private GameState state = GameState.Ingame;

    int currentRail = 0;    



    enum GameState { Menu, Ingame };

    private void Start()
    {
        train = trainGO.GetComponent<Train>();
    }

    void Update()
    {
        trainGO.transform.position = mm.leader.transform.position;
        trainGO.transform.rotation = mm.leader.transform.rotation;


        if (Input.GetButtonDown("Tchoutchou"))
        {
            Debug.Log("TCHOU TCHOU EN FAIT MEC");
        }
        if (Input.GetButtonDown("Slowmo"))
        {
            Debug.Log("JE RALENTIS LE TEMPS PELO");
        }
        if (Input.GetButtonDown("Repair"))
        {
            Debug.Log("OULA, ÇA RÉPARE COMME JAJA");
        }
        if (Input.GetButtonDown("Coal"))
        {
            Debug.Log("JE CHARBONNE, MDR TU L'AS ?");
        }
        if (Input.GetButtonDown("Left"))
        {
            ChangeRail(currentRail - 1);
        }
        if (Input.GetButtonDown("Right"))
        {
            ChangeRail(currentRail + 1);
        }

        //WIP
        if (Input.GetButton("Brake"))
        {
            if (state == GameState.Ingame)
            {
                //train.brake();
                Debug.Log("CALMOS LES AMIS !");
            }
        }


        if (Input.GetButtonDown("Speed0"))
        {
            Debug.Log("VITESSE 0 MON POTE !");
        }
        if (Input.GetButtonDown("Speed1"))
        {
            Debug.Log("VITESSE 1 MON POTE !");
        }
        if (Input.GetButtonDown("Speed2"))
        {
            Debug.Log("VITESSE 2 MON POTE !");
        }

    }

    void ChangeRail(int newRail)
    {
        if (newRail >= 0 && newRail < mm.minions.Length)
        {
            mm.ChangeLeader(newRail);
            currentRail = newRail;
        }
    }
}
