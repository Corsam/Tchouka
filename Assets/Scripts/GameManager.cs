﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public MinionManager mm;
    public NoteManager nm;
    public TimeManager tm;
    //public GameObject trainGO;
    public LevelManager lm;
    public MainMenuScriptCorsam mms;
    public LevelSelectorManager lsm;
    AudioSource source; 

    public AudioClip[] clips;


    private bool firstLoad = true;
    private Train train;
    public GameState state = GameState.Ingame;

    public int coins = 0;

    public enum GameState { Menu, Ingame, Starting, LevelSelection};


    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("GameManager");
        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);

        source = GetComponent<AudioSource>();
    }

    private void Start()
    {
        state = GameState.Menu;
        if (firstLoad)
        {
            GetLevelInfo();
        }
    }

    void Update()
    {
        if (state == GameState.Ingame)
        {
            //Tchou Tchou
            if (Input.GetButtonDown("Tchoutchou"))
            {
                //Debug.Log("TCHOU TCHOU EN FAIT MEC");
                train.LaunchTchouTchou();
            }

            if (Input.GetButtonDown("Slowmo"))
            {
                train.LaunchPause();
                //Debug.Log("C'EST LA PAUSE !");
            }

            //Système de réparation
            if (Input.GetButtonDown("Repair"))
            {
                //Debug.Log("OULA, ÇA RÉPARE COMME JAJA");
                train.RepairBegins();
            }
            if (Input.GetButtonUp("Repair"))
            {
                train.RepairEnds();
            }

            //Recharge de charbon
            if (Input.GetButtonDown("Coal"))
            {
                //Debug.Log("JE CHARBONNE, MDR TU L'AS ?");
                train.RefillCoal();
            }

            //Changement de rail
            if (Input.GetButtonDown("Left"))
            {
                train.ChangeRail(-1);
            }
            if (Input.GetButtonDown("Right"))
            {
                train.ChangeRail(1);
            }

            //Système de frain d'urgence
            if (Input.GetButtonDown("Brake"))
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
                train.SetSpeed(0);
            }
            if (Input.GetButtonDown("Speed1"))
            {
                train.SetSpeed(1);
            }
            if (Input.GetButtonDown("Speed2"))
            {
                train.SetSpeed(2);
            }
        }
        else if (state == GameState.Starting)
        {
            if (Input.GetButtonDown("Tchoutchou"))
            {
                lm.Tchoutchou();
            }

            if (Input.GetButton("Speed1"))
            {
                lm.Vitesse1(true);
            }

            if (Input.GetButtonDown("Speed2") || Input.GetButtonDown("Speed0"))
            {
                lm.Vitesse1(false);
            }
        }
    }

    public void GetLevelInfo()
    {
        mm = FindObjectOfType<MinionManager>();
        train = FindObjectOfType<Train>();
        lm = FindObjectOfType<LevelManager>();
        MenuManager meMa = FindObjectOfType<MenuManager>();
        if (lm != null)
        {
            lm.enabled = true;
            if (source.clip != clips[1])
            {
                source.Stop();
                source.clip = clips[1];
                source.Play();
            }
        }
        mms = FindObjectOfType<MainMenuScriptCorsam>();
        if (mms != null)
        {
            mms.enabled = true;
            if (source.clip != clips[0])
            {
                source.Stop();
                source.clip = clips[0];
                source.Play();
            }
        }
        if (meMa != null)
        {
            meMa.enabled = true;
        }
        lsm = FindObjectOfType<LevelSelectorManager>();
        if (lsm != null)
        {
            lsm.enabled = true;
            if (source.clip != clips[0])
            {
                source.Stop();
                source.clip = clips[0];
                source.Play();
            }
        }
    }

    public void LoadLevel(string levelName)
    {
        //DontDestroyOnLoad(this.gameObject);
        firstLoad = false;
        StartCoroutine(LoadYourAsyncScene(levelName));
        GetLevelInfo();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void TestBouton()
    {
        Debug.Log("BUJURE");
    }

    IEnumerator LoadYourAsyncScene(string levelName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(levelName);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        GetLevelInfo();
    }

    public void SetGameState(GameState newState)
    {
        state = newState;
    }

    public int GetCoinsAmount()
    {
        return coins;
    }

    public void AddCoins(int coinsToAdd)
    {
        coins += coinsToAdd;
    }

    public void RemoveCoins(int coinsToRemove)
    {
        coins -= coinsToRemove;
    }
}
