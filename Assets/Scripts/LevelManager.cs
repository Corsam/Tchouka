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
    public Text timeText;
    public Image noteImage;
    public Image commentPassagersImage;
    public Image commentTimeImage;

    int passengersFinal;
    float timeFinal;
    int score;
    int coins;
    Sprite note;
    Sprite comment_passager;
    Sprite comment_time;

    public Sprite[] commentsPassagers;
    public Sprite[] commentsTime;

    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
        gm.SetGameState(GameManager.GameState.Starting);
        Starting();
        train.enabled = false;
    }

    public void EndGame(int _passengers, float _time, int _coins)
    {
        GetTrainInfo(_passengers, _time, _coins);
        CalculateScore();
        DefineNote();
        DefineComments();
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
        note = gm.nm.CalculateNote(score);
    }

    //TO DEFINE
    void DefineComments()
    {
        comment_passager = commentsPassagers[Random.Range(0, commentsPassagers.Length)];
        comment_time = commentsTime[Random.Range(0, commentsTime.Length)];
    }

    public void GetTrainInfo(int _passengers, float _time, int _coins)
    {
        passengersFinal = _passengers;
        timeFinal = _time;
        coins = _coins;
    }

    //TO DEFINE
    public void CalculateScore()
    {
        score = passengersFinal * 100 + (int)(18000 - timeFinal * 10);
    }

    void DisplayEndInfo()
    {
        passagersText.text = passengersFinal.ToString();        
        timeText.text = gm.tm.TimeDisplay(timeFinal, true);
        noteImage.sprite = note;
        commentPassagersImage.sprite = comment_passager;
        commentTimeImage.sprite = comment_time;
    }

    public void ReturnToMenu()
    {
        gm.SetGameState(GameManager.GameState.Menu);
        gm.LoadLevel("LevelSelection-Test");
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
