using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{

    public int checkMenu = 0;

    public float effetBig;

    public GameObject[] images;

    // Update is called once per frame
    void Update()
    {
        checkInput();
        level();
        shop();
        settings();
        quit();
        Bigger();
    }
    
    public void checkInput()
    {
        if (Input.GetButtonDown("Left") && checkMenu > 0)
        {
            checkMenu = checkMenu - 1;
        }

        if (Input.GetButtonDown("Right") && checkMenu < images.Length - 1)
        {
            checkMenu = checkMenu + 1;
        }
    }

    public void Bigger()
    {
        for (int i = 0; i < images.Length; i++)
        {
            images[i].SetActive(checkMenu == i);
        }
    }

    public void level()
    {
        if (Input.GetButtonDown("Tchoutchou") && checkMenu == 0)
        {
            SceneManager.LoadScene("Level Selection");
        }
    }

    public void shop()
    {
        if (Input.GetButtonDown("Tchoutchou") && checkMenu == 1)
        {
            SceneManager.LoadScene("Shop");
        }
    }

    public void settings()
    {
        if (Input.GetButtonDown("Tchoutchou") && checkMenu == 2)
        {
            Debug.Log("settings");
        }
    }

    public void quit()
    {
        if (Input.GetButtonDown("Tchoutchou") && checkMenu == 3)
        {
            Debug.Log("quit");
        }
    }
}
