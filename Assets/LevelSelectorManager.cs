using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectorManager : MonoBehaviour
{
    GameObject[] levels;
    GameObject[] lines;

    public int levelTarget;
    public int levelCurrent;

    public bool isTraveling;

    public GameObject levelInfoUI;

    public Text levelMessageText;
    public Text levelTempsRecordText;
    public Text levelNoteRecordText;

    public GameObject linePrefab;

    private void Start()
    {
        levelInfoUI.SetActive(false);

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

        UpdateLevelCurrent(0);
    }

    void UpdateLevelCurrent(int newLevelCurrent)
    {
        levelCurrent = newLevelCurrent;
        isTraveling = false;
        GetLevelInfo(levelCurrent);
        levelInfoUI.SetActive(true);
        //WIP
    }

    void GetLevelInfo(int level)
    {
        LevelInfo infos = levels[level].GetComponent<LevelInfo>();

        levelMessageText.text = infos.message;
        levelTempsRecordText.text = infos.tempsRecord.ToString();
        levelNoteRecordText.text = infos.noteRecord;
        //WIP
    }
}
