using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Train : MonoBehaviour
{
    public MinionManager mm;

    public Slider coalBar;
    public Image barFill;
    public Text speedDebugText;
    public GameObject tchouTchouDebug;

    public GameObject tchouCollider;

    public float currentSpeed;
    public float speedLeverValue = 5;
    public float brakeForce = 0.06f;
    public float speedChangeForce = 0.02f;

    public float coalConso = 5;
    float currentCoalLevel = 95;

    public float speedMult_0 = 0.5f;
    public Color barColor_0 = new Color(100, 0, 0);
    public float threshold_1 = 20;
    public float speedMult_1 = 0.75f;
    public Color barColor_1 = new Color(175, 85, 0);
    public float threshold_2 = 80;
    public float speedMult_2 = 1f;
    public Color barColor_2 = new Color(0, 85, 0);

    float currentSpeedMult = 1f;

    bool isBraking = false;


    void Update()
    {
        currentCoalLevel = Mathf.Clamp(currentCoalLevel - coalConso * Time.deltaTime, 0, 100);
        coalBar.value = currentCoalLevel / 100f;

        UpdateSpeedMult();

        //Vérifie si le frein est appuyé ou pas
        if (isBraking)
        {
            currentSpeed = Mathf.Lerp(currentSpeed, 0, brakeForce);
        }
        else
        {
            currentSpeed = Mathf.Lerp(currentSpeed, speedLeverValue * currentSpeedMult, speedChangeForce);
        }

        mm.leader.speed = currentSpeed;

        speedDebugText.text = "Vitesse : " + (int)(currentSpeed * 2000) / 100 + " km/h";
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

    void UpdateSpeedMult()
    {
        if (currentCoalLevel < threshold_1)
        {
            currentSpeedMult = speedMult_0;
            barFill.color = barColor_0;
        }
        else if (currentCoalLevel < threshold_2)
        {
            currentSpeedMult = speedMult_1;
            barFill.color = barColor_1;
        }
        else
        {
            currentSpeedMult = speedMult_2;
            barFill.color = barColor_2;
        }
    }

    public void LaunchTchouTchou()
    {
        StartCoroutine(TchouTchou());
    }

    

    IEnumerator TchouTchou()
    {
        //Debug.Log("POUET EN FAIT");

        tchouCollider.SetActive(true);

        yield return new WaitForSeconds(2f);

        tchouCollider.SetActive(false);
    }
}
