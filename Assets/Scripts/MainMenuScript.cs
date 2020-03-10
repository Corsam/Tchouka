using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{

    public int checkMenu = 0;

    public float effetBig;

    public GameObject ImageLevel;
    public GameObject ImageShop;
    public GameObject ImageSettings;
    public GameObject ImageQuit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

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

        if (Input.GetButtonDown("Right") && checkMenu < 3)
        {
            checkMenu = checkMenu + 1;
        }
    }

    public void Bigger()
    {
        if (checkMenu == 0)
        {     
            ImageLevel.SetActive(true);

            ImageShop.SetActive(false);
            ImageSettings.SetActive(false);
            ImageQuit.SetActive(false);
        }

        else if (checkMenu == 1)
        {
            ImageShop.SetActive(true);

            ImageLevel.SetActive(false);
            ImageSettings.SetActive(false);
            ImageQuit.SetActive(false);
        }

        else if (checkMenu == 2)
        {
            ImageSettings.SetActive(true);

            ImageLevel.SetActive(false);
            ImageShop.SetActive(false);
            ImageQuit.SetActive(false);
        }

        else if (checkMenu == 3)
        {
            ImageQuit.SetActive(true);

            ImageLevel.SetActive(false);
            ImageShop.SetActive(false);
            ImageSettings.SetActive(false);
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
