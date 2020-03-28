using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public GameManager gm;
    public Train train;
    public bool[] checklist;
    public StopWall finalWall;
    public float distance;

    public GameObject beginMenu;
    public GameObject endMenu;

    public GameObject check1;
    public GameObject check2;

    public Text passagersText;
    public Text timeText;
    public Image noteImage;
    public Image commentPassagersImage;
    public Image commentTimeImage;

    int passengersFinal;
    float timeFinal;
    float score;
    int coins;
    Sprite note;
    Sprite comment_passager;
    Sprite comment_time;

    public float passagersEnTout;
    public float piècesEnTout;
    public float TempsFixé;


    public Sprite[] commentsPassagers;
    public Sprite[] commentsTime;

    private float passengerRatio;
    private float timeRatio;

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
        int pa;
        int ti;

        if (passengerRatio < 1 / 3f)
        {
            pa = 2;
        }
        else if (passengerRatio < 2 / 3f)
        {
            pa = 1;
        }
        else
        {
            pa = 0;
        }

        if (timeRatio < 1 / 3f)
        {
            ti = 2;
        }
        else if (passengerRatio < 2 / 3f)
        {
            ti = 1;
        }
        else
        {
            ti = 0;
        }
        comment_passager = commentsPassagers[pa];
        comment_time = commentsTime[ti];
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
        passengerRatio = (float)passengersFinal / passagersEnTout;
        timeRatio = TempsFixé / timeFinal;

        score = passengerRatio + timeRatio + (float)coins/piècesEnTout;
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

    public void Rejouer()
    {
        gm.LoadLevel(SceneManager.GetActiveScene().name);
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
        beginMenu.SetActive(false);
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

    public void Vitesse1(bool activated)
    {
        if (activated)
        {
            if (!checklist[0])
            {
                checklist[0] = true;
                check1.SetActive(true);
            }
        }
        else
        {
            if (checklist[0])
            {
                checklist[0] = false;
                check1.SetActive(false);
            }
        }
    }

    public void Tchoutchou()
    {
        if (checklist[0])
        {
            checklist[1] = true;
            check2.SetActive(true);
            StartCoroutine(Lancer());
        }
    }

    IEnumerator Lancer()
    {
        yield return new WaitForSeconds(1);
        LaunchGame();
    }
}
