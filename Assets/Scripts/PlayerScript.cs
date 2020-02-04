using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour
{
    public GameObject[] railsList;

    public int speed = 0;
    public float realSpeed = 0f;

    bool canTchouTchou = true;
    bool isCoalLoaded = false;

    float coalFillRatio = 0f;
    float coalMax = 30f;
    float coalCurrent = 25f;
    float coalDecreaseRate = 0.5f;
    float coalToGive = 2f;

    float speedValue = 3f;

    int currentRail = 1;

    public Text speedDebugText;
    public Slider coalBar;
    public GameObject tchouTchou;

    private void Update()
    {
        coalFillRatio = coalCurrent / coalMax;
        coalBar.value = coalFillRatio;

        coalCurrent = Mathf.Clamp(coalCurrent - coalDecreaseRate * Time.deltaTime, 0, coalMax);

        speedDebugText.text = "Vitesse : " + speed.ToString();

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            speed = Mathf.Clamp(speed + 1, 0, 3);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            speed = Mathf.Clamp(speed - 1, 0, 3);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (currentRail < railsList.Length - 1)
            {
                currentRail++;

                Vector3 newPos = new Vector3(railsList[currentRail].transform.position.x, transform.position.y, transform.position.z);

                transform.position = newPos;
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (currentRail > 0)
            {
                currentRail--;

                Vector3 newPos = new Vector3(railsList[currentRail].transform.position.x, transform.position.y, transform.position.z);

                transform.position = newPos;
            }
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            coalCurrent = Mathf.Clamp(coalCurrent + coalToGive, 0, coalMax);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (canTchouTchou)
                StartCoroutine(TchouTchou());             
        }

    }

    IEnumerator TchouTchou()
    {
        Debug.Log("POUET EN FAIT");

        canTchouTchou = !canTchouTchou;
        tchouTchou.SetActive(true);

        yield return new WaitForSeconds(2f);

        tchouTchou.SetActive(false);
        canTchouTchou = !canTchouTchou;
    }

    private void FixedUpdate()
    {
        realSpeed = Mathf.Lerp(realSpeed, speed, 0.01f);

        Vector3 newPos = Vector3.forward * realSpeed * speedValue * SpeedMultiplier() * Time.fixedDeltaTime;
        transform.position += newPos;
    }

    private float SpeedMultiplier()
    {
        float retVal = 0f;

        if (coalFillRatio >= 0.9f)
        {
            retVal = 1.5f;
        }
        else if (coalFillRatio >= 0.5f)
        {
            retVal = 1f;
        }
        else if (coalFillRatio >= 0.25f)
        {
            retVal = 0.75f;
        }
        else
        {
            retVal = 0.5f;
        }

        return retVal;
    }
}
