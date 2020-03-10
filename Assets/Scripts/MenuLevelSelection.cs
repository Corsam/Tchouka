using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuLevelSelection : MonoBehaviour
{

    public GameObject Levels;
    public int checkLevels;
    public bool rightPush = false;
    public bool leftPush = false;
    public int xVector = 1920;
    public int push = 10;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        checkInput();
        checkLeftPush();
        checkRightPush();

        Levels.transform.position = new Vector3(xVector, Levels.transform.position.y, Levels.transform.position.z);
    }

    public void checkInput()
    {
        if (Input.GetButtonDown("Left") && checkLevels > 0)
        {
            checkLevels = checkLevels - 1;
            leftPush = true;
            //Levels.transform.position += new Vector3 (+1920, 0, 0);
        }

        if (Input.GetButtonDown("Right") && checkLevels < 4)
        {
            checkLevels = checkLevels + 1;
            rightPush = true;
            //Levels.transform.position += new Vector3(-1920, 0, 0);

        }

        if (Input.GetButtonDown("Brake"))
        {
            SceneManager.LoadScene("MainMenu");
        }

    }

    public void checkRightPush()
    {
        if (rightPush && checkLevels == 1)
        {
            xVector -= push;
            if (xVector <= 2880)
            {
                rightPush = false;
            }
        }

        if (rightPush && checkLevels == 2)
        {
            xVector -= push;
            if (xVector <= 960)
            {
                rightPush = false;
            }
        }

        if(rightPush && checkLevels == 3)
        {
            xVector -= push;
            if (xVector <= -960)
            {
                rightPush = false;
            }
        }

        if(rightPush && checkLevels == 4)
        {
            xVector -= push;
            if (xVector <= -2880)
            {
                rightPush = false;
            }
        }


    }

    public void checkLeftPush()
    {
        if (leftPush && checkLevels == 0)
        {
            xVector += push;
            if (xVector >= 4800)
            {
                leftPush = false;
            }
        }

        if (leftPush && checkLevels == 1)
        {
            xVector += push;
            if (xVector >= 2880)
            {
                leftPush = false;
            }
        }

        if (leftPush && checkLevels == 2)
        {
            xVector += push;
            if (xVector >= 960)
            {
                leftPush = false;
            }
        }

        if (leftPush && checkLevels == 3)
        {
            xVector += push;
            if (xVector >= -960)
            {
                leftPush = false;
            }
        }


    }

}
