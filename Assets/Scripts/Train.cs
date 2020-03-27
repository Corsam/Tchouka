using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Train : MonoBehaviour
{
    public MinionManager mm;
    public LevelManager lm;

    public Slider progressionSlider;
    public Slider repairBar;
    public Image barFill;
    public Text speedDebugText;
    //public GameObject tchouTchouDebug;
    public Text healthDebugText;
    public Text passagersDebugText;
    public Text coinsDebugText;
    public Text timeDebugText;

    public BarreAnnonces barreAnnonces;

    public GameObject pauseMenu;

    public Text StateDebugText;

    public Image coalBarImage;

    //public GameObject tchouCollider;
    Animator anim;
    public CameraPlayer cameraPlayer;


    public AudioSource sourceMoveSounds;


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
    float timeTrackChangeBegining = 0;


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
    public bool isJumping = false;
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

    float timerMoveSound = 0f;
    public AudioClip[] moveSounds;


    void Update()
    {
        timeSpent += Time.deltaTime;
        UpdateTimeDisplay();

        UpdatePosition();

        UpdateProgressionDisplay();

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
        UpdateCoalDisplay();

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
                currentSpeed = Mathf.Lerp(currentSpeed, 0, animalBrakeForce + (0.01f*Time.deltaTime));
            }
            else if (isBoosting)
            {
                currentSpeed = Mathf.Lerp(currentSpeed, speedLeverValue * currentSpeedMult * currentHealthMult * boostValue, collisionBrakeForce + (0.01f * Time.deltaTime));
            }
            else if (collided)
            {
                currentSpeed = Mathf.Lerp(currentSpeed, 0, collisionBrakeForce + (0.01f * Time.deltaTime));
                timerCollision += Time.deltaTime;
                if (timerCollision >= collisionBrakeTime)
                {
                    CollisionEnds();
                }
            }
            else if (isBraking)
            {
                currentSpeed = Mathf.Lerp(currentSpeed, 0, brakeForce + (0.01f * Time.deltaTime));
            }
            else
            {
                currentSpeed = Mathf.Lerp(currentSpeed, speedLeverValue * currentSpeedMult * currentHealthMult, speedChangeForce + (0.01f * Time.deltaTime));
            }
        }

        mm.leader.speed = currentSpeed;

        //speedDebugText.text = "Vitesse : " + (int)(currentSpeed * 2000) / 100 + " km/h";

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


    public void Initialize()
    {
        SetSpeed(0);
        mm.leader.speed = 0;
        timeSpent = 0;
        UpdateTimeDisplay();
        passagersCount = 0;
        UpdatePassagersDisplay();
        coinsCollected = 0;
        UpdateCoinsDisplay();  
        UpdatePosition();
        UpdateHealthDisplay();
        RepairEnds();
        UpdatePosition();
        anim = GetComponent<Animator>();
    }

    /*#region SOUND

    void PlayMoveSound()
    {
        if (timerMoveSound > 1f/(currentSpeed + 2f))
        {
            audioSource.clip = moveSounds[Random.Range(0, moveSounds.Length)];
            audioSource.Play
        }
    }

    #endregion SOUND*/

    void UpdatePosition()
    {
        if (isChangingTrack /*&& false*/)
        {
            float lambda = Mathf.Clamp01((timeSpent - timeTrackChangeBegining) / /*anim.GetCurrentAnimatorStateInfo(0).length*/ 0.5f);

            transform.position = Vector3.Lerp(mm.GetMinion(currentRail).transform.position, mm.GetMinion(goalRail).transform.position, lambda);           
            transform.rotation = Quaternion.Lerp(mm.GetMinion(currentRail).transform.rotation, mm.GetMinion(goalRail).transform.rotation, lambda);

            if (lambda == 1f)
            {
                TrackChanged();
            }
        }
        else
        {
            transform.position = mm.leader.transform.position;
            transform.rotation = mm.leader.transform.rotation;
        }
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

    public void LaunchPause()
    {
        pauseMenu.SetActive(true);
        lm.gm.SetGameState(GameManager.GameState.Menu);
        Time.timeScale = 0;
        this.enabled = false;
    }

    public void EndPause()
    {
        pauseMenu.SetActive(false);
        lm.gm.SetGameState(GameManager.GameState.Ingame);
        Time.timeScale = 1;
    }

    void LaunchEnd()
    {
        if (isInStopZone)
        {
            //Debug.Log("Bah GG mon con");
            lm.EndGame(passagersCount, timeSpent, coinsCollected);
            this.enabled = false;
        }
    }

    public void SetNearestGare(Gare gare)
    {
        nearestGare = gare;
    }

    public void SetNearestHerd(Animal herd)
    {
        nearestHerd = herd;
    }

    public void AnimalBrake()
    {
        anim.SetTrigger("Brake");
        isAnimalBraking = true;
        cameraPlayer.SetCurrentTarget(CameraPlayer.SpeedParam.Speed0);
    }

    public void Jump()
    {
        isJumping = true;
        anim.SetTrigger("Jump");
    }

    public void EndJump()
    {
        Debug.Log("jatéri");
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
        Debug.Log("On a tapé");
        timerCollision = 0;
        collided = true;
        cameraPlayer.SetCurrentTarget(CameraPlayer.SpeedParam.Speed0);
    }

    void CollisionEnds()
    {
        Debug.Log("ça va mieux");
        collided = false;
    }

    public void Brake()
    {
        anim.SetTrigger("Brake");
        isBraking = true;
        cameraPlayer.SetCurrentTarget(CameraPlayer.SpeedParam.Speed0);
    }

    public void StopBrake()
    {
        isBraking = false;
        cameraPlayer.SwitchToBackTarget();
    }

    public void SetSpeed(int speed)
    {
        speedIs0 = (speed == 0);
        switch(speed)
        {
            case 0 :
                SetSpeedLeverValue(speed0);
                if (isBraking || isAnimalBraking || collided || isBoosting)
                {
                    cameraPlayer.SetBackTarget(CameraPlayer.SpeedParam.Speed0);
                }
                else
                {
                    cameraPlayer.SetBothTarget(CameraPlayer.SpeedParam.Speed0);
                }
                SetCoalConso(speed0_CoalConso);
                break;
            case 1:
                SetSpeedLeverValue(speed1);
                if (isBraking || isAnimalBraking || collided || isBoosting)
                {
                    cameraPlayer.SetBackTarget(CameraPlayer.SpeedParam.Speed1);
                }
                else
                {
                    cameraPlayer.SetBothTarget(CameraPlayer.SpeedParam.Speed1);
                }
                SetCoalConso(speed1_CoalConso);
                break;
            case 2:
                SetSpeedLeverValue(speed2);
                if (isBraking || isAnimalBraking || collided || isBoosting)
                {
                    cameraPlayer.SetBackTarget(CameraPlayer.SpeedParam.Speed2);
                }
                else
                {
                    cameraPlayer.SetBothTarget(CameraPlayer.SpeedParam.Speed2);
                }
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
        int newRail = currentRail + railShift;
        if (canChangeTrack && newRail >= 0 && newRail < mm.minions.Length)
        {
            timeTrackChangeBegining = timeSpent;
            goalRail = newRail;
            if (!isJumping)
            {
                anim.SetTrigger("ChangeTrack");
            }
            isChangingTrack = true;
            canChangeTrack = false;
        }
    }

    public void TrackChanged()
    {
        if (isChangingTrack)
        {
            isChangingTrack = false;
            timeTrackChangeBegining = 0;
            mm.ChangeLeader(goalRail);
            currentRail = goalRail;
            goalRail = -1;
        }
    }

    void UpdateSpeedMult()
    {
        if (currentCoalLevel < threshold_1)
        {
            currentSpeedMult = speedMult_0;
        }
        else if (currentCoalLevel < threshold_2)
        {
            currentSpeedMult = speedMult_1;
        }
        else
        {
            currentSpeedMult = speedMult_2;
        }
    }

    void UpdateHealthMult()
    {
        if (currentHealth <= threshold_health)
        {
            barreAnnonces.DisplayAnnonce("TrainEndomage");
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
    }

    void RepairHealth(int heal)
    {
        currentHealth = Mathf.Clamp(currentHealth + heal, 0, maxHealth);
        UpdateHealthMult();
        UpdateHealthDisplay();
    }

    void UpdateCoalDisplay()
    {
        coalBarImage.fillAmount = currentCoalLevel * 0.66f / 100f + 0.3f;
    }

    void UpdateHealthDisplay()
    {
        //healthDebugText.text = "Santé : " + currentHealth + " / " + maxHealth;
    }

    void UpdatePassagersDisplay()
    {
        passagersDebugText.text = passagersCount.ToString();
    }

    void UpdateCoinsDisplay()
    {
        coinsDebugText.text = coinsCollected.ToString();
    }
    
    void UpdateTimeDisplay()
    {
        timeDebugText.text = lm.gm.tm.TimeDisplay(timeSpent, false);
    }

    void UpdateProgressionDisplay()
    {
        progressionSlider.value = mm.leader.distanceTravelled / mm.leader.pathCreator.path.length;
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
        if (passagersCount != 0)
        {
            barreAnnonces.DisplayAnnonce("ChutePassagers");
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
    }

    void HerdFlee(Animal herd)
    {
        if (herd != null)
        {
            herd.brakeZone.gameObject.SetActive(false);
            isAnimalBraking = false;
            cameraPlayer.SwitchToBackTarget();
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
        anim.SetTrigger("Tchoutchou");
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
            //Debug.Log("Du balais !");
            HerdFlee(nearestHerd);
            nearestHerd = null;
        }

    }

    IEnumerator Boost()
    {
        isBoosting = true;
        cameraPlayer.SetCurrentTarget(CameraPlayer.SpeedParam.Boost);

        yield return new WaitForSeconds(boostTime);

        isBoosting = false;
        cameraPlayer.SwitchToBackTarget();

    }
}
