using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public GameManager gm;
    public Train train;
    public bool[] checklist;
    public StopWall finalWall;
    public float distance;

    public GameObject endMenu;

    public Text passagersText;
    public Text piecesText;
    public Text timeText;
    public Text noteText;

    int passengersFinal;
    float timeFinal;
    int score;
    int coins;
    string note;

    public string[] notes;

    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
        //gm.GetLevelInfo();
        gm.SetGameState(GameManager.GameState.Starting);
        //GetEndInfo();
        Starting();
        train.enabled = false;
    }

    public void EndGame(int _passengers, float _time, int _coins)
    {
        GetTrainInfo(_passengers, _time, _coins);
        CalculateScore();
        DefineNote();
        DisplayEndInfo();
        gm.AddCoins(coins);
        gm.SetGameState(GameManager.GameState.Menu);
        endMenu.SetActive(true);
    }

    void GetEndInfo()
    {
        foreach (var minion in gm.mm.minions)
        {
            distance += minion.pathCreator.path.GetClosestDistanceAlongPath(finalWall.transform.position);
        }
        distance = distance / (float)gm.mm.minions.Length;
    }

    public void LaunchEndMenu()
    {
        endMenu.SetActive(true);
        gm.SetGameState(GameManager.GameState.Menu);
    }

    void DefineNote()
    {
        note = notes[Random.Range(0, 5)];
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

    void DisplayEndInfo()
    {
        passagersText.text = "nb de passagers : " + passengersFinal;
        piecesText.text = "nb de pieces : " + coins;
        timeText.text = "temps écoulé : " + timeFinal;
        noteText.text = "Note finale : " + note;
    }

    public void ReturnToMenu()
    {
        gm.SetGameState(GameManager.GameState.Menu);
        gm.LoadLevel("MainMenu-Tweaks");
    }

    public void Rejouer(string sceneName)
    {
        gm.LoadLevel(sceneName);
    }

    public void EndPause()
    {
        train.enabled = true;
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
        //Debug.Log("C'est parti mon kiki !");
        train.Initialize();
        train.enabled = true;
        gm.SetGameState(GameManager.GameState.Ingame);
    }

    private void Starting()
    {
        //lm = GetComponent<LevelManager>();
        train.Initialize();
        //Debug.Log("Normalement c'est bon");
    }

    public void Tchoutchou()
    {
        checklist[0] = true;
        LaunchGame();
    }
}
