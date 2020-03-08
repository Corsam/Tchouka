using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameManager gm;
    public Train train;
    public bool[] checklist;
    public StopWall finalWall;
    public float distance;
    int passengersFinal;
    float timeFinal;
    int score;
    int coins;

    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
        //gm.GetLevelInfo();
        gm.SetGameState(GameManager.GameState.Starting);
        //GetEndInfo();
        Starting();
        train.enabled = false;
    }

    void GetEndInfo()
    {
        foreach (var minion in gm.mm.minions)
        {
            distance += minion.pathCreator.path.GetClosestDistanceAlongPath(finalWall.transform.position);
        }
        distance = distance / (float)gm.mm.minions.Length;
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

    public void ReturnToMenu()
    {
        gm.SetGameState(GameManager.GameState.Menu);
        gm.LoadLevel("Test-MainMenu");
    }

    public void EndPause()
    {
        train.EndPause();
    }

    public void LaunchGame()
    {
        foreach (bool req in checklist)
        {
            if (!req)
            {
                return;
            }
        }
        Debug.Log("C'est parti mon kiki !");
        train.Initialize();
        train.enabled = true;
        gm.SetGameState(GameManager.GameState.Ingame);
    }

    private void Starting()
    {
        //lm = GetComponent<LevelManager>();
        train.Initialize();
        Debug.Log("Normalement c'est bon");
    }

    public void Tchoutchou()
    {
        checklist[0] = true;
        LaunchGame();
    }
}
