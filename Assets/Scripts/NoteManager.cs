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
        return notesSprites[Random.Range(0, notesSprites.Length)];
    }
}
