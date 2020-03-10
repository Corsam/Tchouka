using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuScriptCorsam : MonoBehaviour
{
    GameManager gm;
    public Text coinsText;

    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        coinsText.text = "Pièces : " + gm.GetCoinsAmount();
    }
}
