using UnityEngine;

public class PlayerMedapartsController : MonoBehaviour
{
    public MedaPartScript[] medaparts;
    public float lowEnergyThreshold = 0;
    public bool isBlocking = false;
    public LockOn enLockON;
    private MedaPartScript currentTarget;
    private RocketLaucher RL;
    private Shooter shooter;
    private PlayerMovements PM;
    public CharacterStatsSO characterStatsSO;


 
    [Header("perBullet")]
    public float lastBulletCritMultiplyer;
   
    [Header("Base Attacks, Defenses, and Speed Values")]

    public float BaseDamageNorthAttack;
    public float baseDefense;
    public float baseDamageWestAttack;
    public int baseMagSizeSmg;
    public float baseDamageEastAttack;
    public int baseMagSizeRev;
    public float baseMovementSpeed;

    [Header("Low Energy Attacks, Defenses, and Speed Values")]
    public float lowEnergyDefense;
    public float lowEnergyDamageWestAttack;
    public int lowEnergyMagSizeSmg;
    public float lowEnergyDamageEastAttack;
    public int lowEnergyMagSizeRev;
    public float lowEnergyMovementSpeed;

    [Header("MedaForce Attacks, Defenses, and Speed Values")]
    public float MedaForceNorthAttack;
    public float medaForceDefense;
    public float medaForceDamageWestAttack;
    public int medaForceMagSizeSmg;
    public float medaForceDamageEastAttack;
    public int medaForceMagSizeRev;
    public float medaForceMovementSpeed;

    [Header("Sounds")]
    public string turningOffSound;
    private FMOD.Studio.EventInstance turningOffSoundInstance;
    private bool playedRArmSound;
    private bool playedLArmSound;
    private bool playedLegSound;

    [Header("MedaForce Related")]
    public float MedaForceDuration = 5f;
    private bool MedaForceActive = false;
    // Adjust the duration as needed
    private float MedaForceTimer = 0f;
    private float originalDamageWestAttack;
    private float originalDamageEastAttack;
    private int originalMagSizeSmg;
    private int originalMagSizeRev;
    private float originalMovementSpeed;
   

    private void Awake()
    {
        if (GetComponent<Player1>() != null)
        {
            enLockON = FindObjectOfType<Player2>().gameObject.GetComponent<LockOn>();
        }
        else if (GetComponent<Player2>() != null)
        {
            enLockON = FindObjectOfType<Player1>().GetComponent<LockOn>();
        }

    }
    private void Start()
    {
        playedRArmSound = false;
        playedLArmSound = false;
        playedLegSound = false;
        medaparts = GetComponentsInChildren<MedaPartScript>();
        shooter = GetComponent<Shooter>();
        PM = GetComponent<PlayerMovements>();
        RL = GetComponent<RocketLaucher>();
      
        // Set the base medapart stats
        SetMedapartStats(baseDamageWestAttack, baseDamageEastAttack, baseMagSizeSmg, baseMagSizeRev, baseMovementSpeed);
        this.shooter.bulletPrefab.GetComponent<Bullet>().critValue = this.lastBulletCritMultiplyer;

       
        turningOffSoundInstance = FMODUnity.RuntimeManager.CreateInstance(turningOffSound);
    }

    private void Update()
    {
        DetermineTarget();
       

        if (MedaForceActive)
        {
            MedaForceTimer -= Time.deltaTime;

            if (MedaForceTimer <= 0f)
            {
                // Combo time is over, restore the original medapart stats
                MedaForceActive = false;
                SetMedapartStats(originalDamageWestAttack, originalDamageEastAttack, originalMagSizeSmg, originalMagSizeRev, originalMovementSpeed);
            }
        }
        else
        {
            CheckLowEnergy();
        }
    }

    private void DetermineTarget()
    {
        currentTarget = null;

        foreach (MedaPartScript medapart in medaparts)
        {
            medapart.SetTargeted(false);
        }

        if (enLockON.lockedOnMedapart != null)
        {
            foreach (MedaPartScript medapart in medaparts)
            {
                if (medapart == enLockON.lockedOnMedapart)
                {
                    currentTarget = medapart;
                    medapart.SetTargeted(true);
                    break;
                }
            }
        }
    }

    private void CheckLowEnergy()
    {
        foreach (MedaPartScript medapart in medaparts)
        {
            if (medapart.partEnergy <= lowEnergyThreshold)
            {
                // Perform actions for low energy Medaparts
                switch (medapart.MedapartNumber)
                {
                    case 2: // Arm Medapart

                        this.shooter.magSizeFullAuto = this.lowEnergyMagSizeSmg;
                        if (!playedLArmSound)
                            {
                                turningOffSoundInstance.start();
                                playedLArmSound = true;
                            }
                        
                        break;

                    case 3:

                        this.shooter.maxMagazineSizeRevolver = this.lowEnergyMagSizeRev;
                        if (!playedRArmSound)
                            {
                                turningOffSoundInstance.start();
                                playedRArmSound = true;
                            }
                        
                        break;

                    case 4: // Leg Medapart

                        PM.playerSpeed = lowEnergyMovementSpeed;

                        if (!playedLegSound)
                            {
                                turningOffSoundInstance.start();
                                playedLegSound = true;
                            }
                        
                        break;

                    default:
                        // Handle other medaparts if needed(body if we decide to make it separate)
                        break;
                }
            }
        }
    }

    //For Medaforce, Base Stats and go back to the originals
    private void SetMedapartStats(float damageWestAttack, float damageEastAttack, int magSizeSmg, int magSizeRev, float movementSpeed)
    {
        shooter.smgDamage = damageWestAttack;
        shooter.revolverDamage = damageEastAttack;
        shooter.magSizeFullAuto = magSizeSmg;
        shooter.maxMagazineSizeRevolver = magSizeRev;
        PM.playerSpeed = movementSpeed;
    }

    public void UseMedaForce()
    {
        MedaForceActive = true;
        MedaForceTimer = MedaForceDuration;

        // Save the original medapart stats to restore them later
        originalDamageWestAttack = shooter.smgDamage;
        originalDamageEastAttack = shooter.revolverDamage;
        originalMagSizeSmg = shooter.magSizeFullAuto;
        originalMagSizeRev = shooter.maxMagazineSizeRevolver;
        originalMovementSpeed = PM.playerSpeed;
        SetMedapartStats(medaForceDamageWestAttack, medaForceDamageEastAttack, medaForceMagSizeSmg, medaForceMagSizeRev, medaForceMovementSpeed);
    }

    public void SetBlocking(bool isBlocking)
    {
        this.isBlocking = isBlocking;

        if (isBlocking && currentTarget != null && currentTarget.MedapartNumber == 1)
        {
            // Nullify damage or change behavior for head Medapart when blocking
        }
    }
}