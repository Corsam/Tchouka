using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteManager : MonoBehaviour
{
    public Sprite[] notesSprites;

    public Sprite GiveNote(int note)
    {
        if (note < notesSprites.Length && note >= 0)
        {
            return notesSprites[note];
        }

        return null;
    }

    public Sprite CalculateNote(float score)
    {
        int i = 5;

        if (score < 1.3f)
        {
            i = 4;
        }
        else if (score < 2f)
        {
            i = 3;
        }
        else if (score < 2.5f)
        {
            i = 2;
        }
        else if (score < 3)
        {
            i = 1;
        }
        else
        {
            i = 0;
        }

        return notesSprites[i];
    }
}
