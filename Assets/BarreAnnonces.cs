using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarreAnnonces : MonoBehaviour
{
    [System.Serializable]
    public class Annonce
    {
        public string name;
        public Sprite sprite;
    }

    public Annonce[] annonces;
    public Image annonceImage;
    public Animator anim;

    public float timeMaxPopedOut;
    float timePopedOut = 0;
    bool popedOut = false;

    private void Update()
    {
        if (popedOut)
        {
            timePopedOut += Time.deltaTime;

            if (timePopedOut >= timeMaxPopedOut)
            {
                popedOut = false;
                timePopedOut = 0;
            }
        }

        anim.SetBool("IsOut", popedOut);
    }

    public Sprite FindAnnonceByName(string _name)
    {
        foreach (var annonce in annonces)
        {
            if (_name == annonce.name)
            {
                return annonce.sprite;
            }
        }
        return null;
    }

    public void DisplayAnnonce(string _name)
    {
        annonceImage.sprite = FindAnnonceByName(_name);
        popedOut = true;
        timePopedOut = 0;
    }
}
