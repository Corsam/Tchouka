using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Train : MonoBehaviour
{
    public MinionManager mm;
    public LevelManager lm;

    public Slider coalBar;
    public Slider repairBar;
    public Image barFill;
    public Text speedDebugText;
    public GameObject tchouTchouDebug;
    public Text healthDebugText;
    public Text passagersDebugText;
    public Text coinsDebugText;
    public Text timeDebugText;

    public GameObject tchouCollider;
    Animator anim;

    int passagersCount = 0;

    float timeSpent = 0;

    public float currentSpeed;
    public float speedLeverValue = 5;
    public float brakeForce = 0.06f;
    public float speedChangeForce = 0.02f;

    public float coalConso = 5;
    float currentCoalLevel = 95;

    public int coinsCollected = 0;

    public int maxHealth = 5;
    int currentHealth = 5;
    public float healthMult_0 = 0.75f;
    public int threshold_health = 2;
    public float healthMult_1 = 1f;
    float currentHealthMult = 1f;

    public float boostValue = 1.5f;
    public float boostTime = 1f;


    int currentRail = 0;
    int goalRail = -1;
    bool isChangingTrack = false;


    public float speed0 = 0;
    public float speed0_CoalConso = 1;
    public float speed1 = 5;
    public float speed1_CoalConso = 5;
    public float speed2 = 10;
    public float speed2_CoalConso = 10;

    public float coalReload = 20;

    bool canChangeTrack = true;
    float timerTrackChange = 0;
    public float cooldownTrackChange = 3;


    public float speedMult_0 = 0.5f;
    public Color barColor_0 = new Color(100, 0, 0);
    public float threshold_1 = 20;
    public float speedMult_1 = 0.75f;
    public Color barColor_1 = new Color(175, 85, 0);
    public float threshold_2 = 80;
    public float speedMult_2 = 1f;
    public Color barColor_2 = new Color(0, 85, 0);

    float currentSpeedMult = 1f;

    public bool speedIs0 = false;
    bool collided = false;
    bool isBraking = false;
    bool isAnimalBraking = false;
    bool isReparing = false;
    bool isJumping = false;
    bool hasSpinned = false;
    bool isBoosting = false;
    bool isInStopZone = false;

    public Animal nearestHerd = null;
    public Gare nearestGare = null;

    float timerCollision = 0f;
    public float collisionBrakeTime = 1f;
    public float collisionBrakeForce = 0.1f;
    public float animalBrakeForce = 0.2f;

    public float timeNeeededToRepair = 3f;
    float timerRepairing = 0f;

    void Update()
    {
        timeSpent += Time.deltaTime;
        UpdateTimeDisplay();

        UpdatePosition();

        //Gestion du cooldown du changement de rail
        if (!canChangeTrack)
        {
            timerTrackChange += Time.deltaTime;
            if (timerTrackChange > cooldownTrackChange)
            {
                canChangeTrack = true;
                timerTrackChange = 0;
            }
        }

        currentCoalLevel = Mathf.Clamp(currentCoalLevel - coalConso * Time.deltaTime, 0, 100);
        coalBar.value = currentCoalLevel / 100f;

        UpdateSpeedMult();


        if ((speedIs0 || isAnimalBraking || isBraking) && currentSpeed < 0.15f)
        {
            currentSpeed = 0;
            Stoped();
        }
        else
        {
            if (isAnimalBraking)
            {
                currentSpeed = Mathf.Lerp(currentSpeed, 0, animalBrakeForce);
            }
            else if (isBoosting)
            {
                currentSpeed = Mathf.Lerp(currentSpeed, speedLeverValue * currentSpeedMult * currentHealthMult * boostValue, collisionBrakeForce);
            }
            else if (collided)
            {
                currentSpeed = Mathf.Lerp(currentSpeed, 0, collisionBrakeForce);
                timerCollision += Time.deltaTime;
                if (timerCollision >= collisionBrakeTime)
                {
                    CollisionEnds();
                }
            }
            else if (isBraking)
            {
                currentSpeed = Mathf.Lerp(currentSpeed, 0, brakeForce);
            }
            else
            {
                currentSpeed = Mathf.Lerp(currentSpeed, speedLeverValue * currentSpeedMult * currentHealthMult, speedChangeForce);
            }
        }


        mm.leader.speed = currentSpeed;

        speedDebugText.text = "Vitesse : " + (int)(currentSpeed * 2000) / 100 + " km/h";

        if (isReparing)
        {
            timerRepairing += Time.deltaTime;

            if (timerRepairing >= timeNeeededToRepair)
            {
                timerRepairing = 0f;
                RepairHealth(1);
            }

            repairBar.value = timerRepairing / timeNeeededToRepair;
        }

    }

    /*public void Setup()
    {
        SetSpeedLeverValue(0);
        transform.position = mm.leader.transform.position;
        transform.rotation = mm.leader.transform.rotation;
    }*/

    public void Initialize()
    {
        SetSpeed(0);
        mm.leader.speed = 0;
        transform.position = mm.leader.transform.position;
        transform.rotation = mm.leader.transform.rotation;
        UpdateHealthDisplay();
        RepairEnds();
        anim = GetComponent<Animator>();
    }

    void UpdatePosition()
    {
        transform.position = mm.leader.transform.position;
        transform.rotation = mm.leader.transform.rotation;
    }

    void Stoped ()
    {
        HerdFlee(nearestHerd);
        LaunchEnd();
    }

    public void HitStopWall()
    {
        SetSpeed(0);
        currentSpeed = 0;
        Debug.Log("Ici on enlève des passagers");
    }

    void LaunchEnd()
    {
        if (isInStopZone)
        {
            Debug.Log("Bah GG mon con");
            lm.GetTrainInfo(passagersCount, timeSpent, coinsCollected);
            this.enabled = false;
        }
    }

    public void SetNearestGare (Gare gare)
    {
        nearestGare = gare;
    }

    public void SetNearestHerd(Animal herd)
    {
        nearestHerd = herd;
    }

    public void AnimalBrake()
    {
        isAnimalBraking = true;
    }

    public void Jump()
    {
        isJumping = true;
        anim.SetTrigger("Jump");
    }

    public void EndJump()
    {
        isJumping = false;
        if (hasSpinned)
        {
            //Debug.Log("à fond les ballons !");
            StartCoroutine(Boost());
        }
        hasSpinned = false;
    }

    public void Collision()
    {
        collided = true;
    }

    void CollisionEnds()
    {
        collided = false;
    }

    public void Brake()
    {
        isBraking = true;
    }

    public void StopBrake()
    {
        isBraking = false;
    }

    public void SetSpeed(int speed)
    {
        speedIs0 = (speed == 0);
        switch(speed)
        {
            case 0 :
                SetSpeedLeverValue(speed0);
                SetCoalConso(speed0_CoalConso);
                break;
            case 1:
                SetSpeedLeverValue(speed1);
                SetCoalConso(speed1_CoalConso);
                break;
            case 2:
                SetSpeedLeverValue(speed2);
                SetCoalConso(speed2_CoalConso);
                break;
            default:
                break;
        }
    }

    public void SetSpeedLeverValue(float newSpeed)
    {
        speedLeverValue = newSpeed;
    }

    public void SetCoalConso(float newCoalConso)
    {
        coalConso = newCoalConso;
    }

    public void RefillCoal()
    {
        currentCoalLevel = Mathf.Clamp(currentCoalLevel + coalReload, 0, 100);
    }

    public void ChangeRail(int railShift)
    {
        anim.SetTrigger("ChangeTrack");
        int newRail = currentRail + railShift;
        if (canChangeTrack && newRail >= 0 && newRail < mm.minions.Length)
        {
            mm.ChangeLeader(newRail);
            currentRail = newRail;
            canChangeTrack = false;
        }
    }

    public void TrackChanged()
    {
        isChangingTrack = false;
        mm.ChangeLeader(goalRail);
        currentRail = goalRail;
        goalRail = -1;
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

    void UpdateHealthMult()
    {
        if (currentHealth <= threshold_health)
        {
            currentHealthMult = healthMult_0;
        }
        else
        {
            currentHealthMult = healthMult_1;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);
        UpdateHealthMult();
        UpdateHealthDisplay();
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void RepairHealth(int heal)
    {
        currentHealth = Mathf.Clamp(currentHealth + heal, 0, maxHealth);
        UpdateHealthMult();
        UpdateHealthDisplay();
    }

    void Die()
    {
        Debug.Log("J'SUIS DEAD SA CHAKAL !");
    }

    void UpdateHealthDisplay()
    {
        healthDebugText.text = "Santé : " + currentHealth + " / " + maxHealth;
    }

    void UpdatePassagersDisplay()
    {
        passagersDebugText.text = "Passagers : " + passagersCount;
    }

    void UpdateCoinsDisplay()
    {
        coinsDebugText.text = "Pièces : " + coinsCollected;
    }
    
    void UpdateTimeDisplay()
    {
        int min = (int)timeSpent / 60;
        int sec = (int)(timeSpent - min*60);
        int cent = (int)((timeSpent - min*60 - sec)*100);

        timeDebugText.text = min + ":" + sec + "." +cent;
    }

    public void TakeCoins(int coins)
    {
        coinsCollected += coins;
        UpdateCoinsDisplay();
    }

    public void RepairBegins()
    {
        isReparing = true;
        repairBar.gameObject.SetActive(true);
    }

    public void RepairEnds()
    {
        isReparing = false;
        repairBar.gameObject.SetActive(false);
        timerRepairing = 0f;
    }

    void TakePassagers(int passagers)
    {
        passagersCount += passagers;
        UpdatePassagersDisplay();
    }

    public void LosePassagers(int passagers)
    {
        if (passagersCount - passagers <= 0)
        {
            passagersCount = 0;
        }
        else
        {
            passagersCount -= passagers;
        }
        UpdatePassagersDisplay();
    }

    void HerdFlee(Animal herd)
    {
        if (herd != null)
        {
            herd.brakeZone.gameObject.SetActive(false);
            isAnimalBraking = false;
            herd.forceFields.SetActive(true);
            herd.herdParticles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }
    }

    public void EnterStopZone()
    {
        isInStopZone = true;
    }

    public void LaunchTchouTchou()
    {
        if (isJumping && !hasSpinned)
        {
            //Debug.Log("ça tourne mdr");
            anim.SetTrigger("Spin");
            hasSpinned = true;
        }
        else if (nearestGare != null)
        {
            TakePassagers(nearestGare.passagersToTake);
            nearestGare = null;
        }
        else if (nearestHerd != null)
        {
            Debug.Log("Du balais !");
            HerdFlee(nearestHerd);
            nearestHerd = null;
        }
        StartCoroutine(TchouTchou());
    }

    IEnumerator TchouTchou()
    {
        //Debug.Log("POUET EN FAIT");

        tchouCollider.SetActive(true);

        yield return new WaitForSeconds(2f);

        tchouCollider.SetActive(false);
    }

    IEnumerator Boost()
    {
        isBoosting = true;

        yield return new WaitForSeconds(boostTime);

        isBoosting = false;
    }
}
