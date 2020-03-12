using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class LevelSelectorManager : MonoBehaviour
{
    GameManager gm;

    public EventSystem es;

    GameObject currentSelected;

    GameObject[] levels;
    GameObject[] lines;

    public Sprite[] notesSprites;
    public Sprite[] flèchesSprites;

    //public int levelTarget;
    //public int levelCurrent;

    public CameraTarget cameraTarget;

    public Image flècheGauche;
    public Image flècheDroite;

    public GameObject levelInfoUI;
    public Image levelImage;
    public Text levelTempsRecordText;
    public Image levelNoteRecordImage;

    public GameObject linePrefab;

    bool initDone = false;

    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
        //gm.SetGameState(GameManager.GameState.LevelSelection);

        //levelInfoUI.SetActive(false);

        levels = new GameObject[0];

        for (int i = 0; i < transform.childCount; i++)
        {
            string nameToCheck = "Level " + (i + 1);

            if (transform.GetChild(i).name == nameToCheck)
            {
                GameObject[] newLevels = new GameObject[levels.Length + 1];
                for (int j = 0; j < levels.Length; j++)
                {
                    newLevels[j] = levels[j];
                }
                newLevels[levels.Length] = transform.GetChild(i).gameObject;
                levels = newLevels;
            }
        }

        if (levels.Length > 1)
        {
            lines = new GameObject[levels.Length - 1];
        }

        for (int i = 0; i < levels.Length - 1; i++)
        {
            lines[i] = Instantiate(linePrefab);
            LineRenderer lr = lines[i].GetComponent<LineRenderer>();
            lr.positionCount = 2;
            lr.SetPosition(0, levels[i].transform.position);
            lr.SetPosition(1, levels[i+1].transform.position);
            lr.enabled = true;
        }

        initDone = true;
    }

    private void Update()
    {
        if (initDone)
        {
            UpdateLevelCurrent();
            UpdateFlèches();
        }
    }

    void UpdateLevelCurrent()
    {
        currentSelected = es.currentSelectedGameObject;
        GetLevelInfo(currentSelected);
        levelInfoUI.SetActive(true);
        cameraTarget.SetPosition(currentSelected.transform.position);
    }

    void UpdateFlèches()
    {
        flècheGauche.gameObject.SetActive(currentSelected != levels[0]);

        flècheDroite.gameObject.SetActive(currentSelected != levels[levels.Length - 1]);
    }

    void GetLevelInfo(GameObject level)
    {
        LevelInfo infos = level.GetComponent<LevelInfo>();

        levelImage.sprite = infos.image;
        levelTempsRecordText.text = infos.tempsRecord.ToString();
        levelNoteRecordImage.sprite = GetNoteImage(infos.noteRecord);
    }

    Sprite GetNoteImage(string note)
    {
        switch (note)
        {
            case "A":
                return notesSprites[0];
            case "B":
                return notesSprites[1];
            case "C":
                return notesSprites[2];
            case "D":
                return notesSprites[3];
            default:
                return null;
        }
    }

    public void LoadLevel(string name)
    {
        gm.LoadLevel(name);
    }
}
