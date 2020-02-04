using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Train : MonoBehaviour
{
    public MinionManager mm;

    public Slider coalBar;
    public Text speedDebugText;
    public GameObject tchouTchouDebug;

    public float currentSpeed;
    public float speedLeverValue = 5;
    public float brakeForce = 0.06f;
    public float speedChangeForce = 0.02f;

    public float coalConso = 5;
    float currentCoalLevel = 95;

    bool isBraking = false;

    void Update()
    {
        currentCoalLevel = Mathf.Clamp(currentCoalLevel - coalConso * Time.deltaTime, 0, 100);
        coalBar.value = currentCoalLevel / 100f;

        //Vérifie si le frein est appuyé ou pas
        if (isBraking)
        {
            currentSpeed = Mathf.Lerp(currentSpeed, 0, brakeForce);
        }
        else
        {
            currentSpeed = Mathf.Lerp(currentSpeed, speedLeverValue, speedChangeForce);
        }

        mm.leader.speed = currentSpeed;

        speedDebugText.text = "Vitesse : " + (int)(currentSpeed * 2000) / 100f + " km/h";
    }

    public void Brake()
    {
        isBraking = true;
    }

    public void StopBrake()
    {
        isBraking = false;
    }

    public void SetSpeedLeverValue(float newSpeed)
    {
        speedLeverValue = newSpeed;
    }

    public void SetCoalConso(float newCoalConso)
    {
        coalConso = newCoalConso;
    }

    public void RefillCoal(float coalAmmount)
    {
        currentCoalLevel = Mathf.Clamp(currentCoalLevel + coalAmmount, 0, 100);
    }

    public void LaunchTchouTchou()
    {
        StartCoroutine(TchouTchou());
    }

    IEnumerator TchouTchou()
    {
        //Debug.Log("POUET EN FAIT");

        tchouTchouDebug.SetActive(true);

        yield return new WaitForSeconds(2f);

        tchouTchouDebug.SetActive(false);
    }
}
