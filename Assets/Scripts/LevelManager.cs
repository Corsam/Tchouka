using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameManager gm;
    public Starting starting;
    public Train train;
    int passengersFinal;
    float timeFinal;
    int score;
    int coins;

    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
        starting = GetComponent<Starting>();
        starting.enabled = true;
        train.enabled = false;
    }

    public void GetTrainInfo(int _passengers, float _time, int _coins)
    {
        passengersFinal = _passengers;
        timeFinal = _time;
        coins = _coins;
    }

    public void CalculateScore()
    {
        score = passengersFinal * 100 + (int)(18000 - timeFinal * 10);
    }

    public void LaunchGame()
    {
        Debug.Log("C'est parti mon kiki !");
        train.Initialize();
        train.enabled = true;
        gm.SetGameState(GameManager.GameState.Ingame);
    }
}
