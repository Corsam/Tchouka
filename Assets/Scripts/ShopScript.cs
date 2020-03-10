using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ShopScript : MonoBehaviour
{
    public GameObject Shop;
    public int checkColors;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        checkInput();
    }

    public void checkInput()
    {
        if (Input.GetButtonDown("Left") && checkColors > 0)
        {
            checkColors = checkColors - 1;
            Shop.transform.position += new Vector3(+1920, 0, 0);
        }

        if (Input.GetButtonDown("Right") && checkColors < 4)
        {
            checkColors = checkColors + 1;
            Shop.transform.position += new Vector3(-1920, 0, 0);

        }

        if (Input.GetButtonDown("Brake"))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    


}

