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

    public float speed0 = 0;
    public float speed0_CoalConso = 1;
    public float speed1 = 5;
    public float speed1_CoalConso = 5;
    public float speed2 = 10;
    public float speed2_CoalConso = 10;

    public float coalReload = 20;

    bool canChangeTrack = true;
    float timerTrackChange = 0;
    public float cooldownTrackChange = 3;


    enum GameState { Menu, Ingame };

    private void Start()
    {
        train = trainGO.GetComponent<Train>();
    }

    void Update()
    {
        if (state == GameState.Ingame)
        {
            trainGO.transform.position = mm.leader.transform.position;
            trainGO.transform.rotation = mm.leader.transform.rotation;

            //Gestion du cooldown du changement de rail
            if (!canChangeTrack)
            {
                timerTrackChange += Time.deltaTime;
                if (timerTrackChange > cooldownTrackChange)
                {
                    canChangeTrack = true;
                    timerTrackChange = 0;
                }
            }

            //Tchou Tchou
            if (Input.GetButtonDown("Tchoutchou"))
            {
                //Debug.Log("TCHOU TCHOU EN FAIT MEC");
                train.LaunchTchouTchou();
            }

            if (Input.GetButtonDown("Slowmo"))
            {
                Debug.Log("JE RALENTIS LE TEMPS PELO");
            }
            if (Input.GetButtonDown("Repair"))
            {
                Debug.Log("OULA, ÇA RÉPARE COMME JAJA");
            }

            //Recharge de charbon
            if (Input.GetButtonDown("Coal"))
            {
                //Debug.Log("JE CHARBONNE, MDR TU L'AS ?");
                train.RefillCoal(coalReload);
            }

            //Changement de rail
            if (Input.GetButtonDown("Left"))
            {
                ChangeRail(currentRail - 1);
            }
            if (Input.GetButtonDown("Right"))
            {
                ChangeRail(currentRail + 1);
            }

            //Système de frain d'urgence
            if (Input.GetButton("Brake"))
            {
                train.Brake();
            }
            if (Input.GetButtonUp("Brake"))
            {
                train.StopBrake();
            }

            //Gestion des différentes vitesses
            if (Input.GetButtonDown("Speed0"))
            {
                train.SetSpeedLeverValue(speed0);
                train.SetCoalConso(speed0_CoalConso);
            }
            if (Input.GetButtonDown("Speed1"))
            {
                train.SetSpeedLeverValue(speed1);
                train.SetCoalConso(speed1_CoalConso);
            }
            if (Input.GetButtonDown("Speed2"))
            {
                train.SetSpeedLeverValue(speed2);
                train.SetCoalConso(speed2_CoalConso);
            }
        }
        else if (state == GameState.Menu)
        {
            if (Input.GetButtonDown("Tchoutchou"))
            {
                //sélectionner
            }

            if (Input.GetButtonDown("Brake"))
            {
                //retour
            }

            if (Input.GetButtonDown("Left"))
            {
                //bouger à gauche
            }
            if (Input.GetButtonDown("Right"))
            {
                //bouger à droite
            }
        }
    }

    void ChangeRail(int newRail)
    {
        if (canChangeTrack && newRail >= 0 && newRail < mm.minions.Length)
        {
            mm.ChangeLeader(newRail);
            currentRail = newRail;
            canChangeTrack = false;
        }
    }
}
