using Unity.VisualScripting;
using UnityEngine;

public class PlayerMedapartsController : MonoBehaviour
{
    public CharacterStatsSO[] characterStatsArray;
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
    public float defenseHead;
    public float defenseLeftArm;
    public float defenseRightArm;
    public float denseLegs;
    public float baseDamageWestAttack;
    public int baseMagSizeSmg;
    public float baseDamageEastAttack;
    public int baseMagSizeRev;
    public float baseMovementSpeed;
    public float baseRechargeSmg;
    public float baseRechargeRevolver;
    public float baseRechargeMissiles;
    public float baseRechargeDash;

    [Header("Low Energy Attacks, Defenses, and Speed Values")]
    public float lowEnergyDefense;
    public float lowEnergyDamageWestAttack;
    public int lowEnergyMagSizeSmg;
    public float lowEnergyDamageEastAttack;
    public int lowEnergyMagSizeRev;
    public float lowEnergyMovementSpeed;
    public float lowRechargeSmg;
    public float lowRechargeRevolver;
    public float lowRechargeMissiles;
    public float lowRechargeDash;

    [Header("MedaForce Attacks, Defenses, and Speed Values")]
    public float MedaForceNorthAttack;
    public float medaForceDefense;
    public float medaForceDamageWestAttack;
    public int medaForceMagSizeSmg;
    public float medaForceDamageEastAttack;
    public int medaForceMagSizeRev;
    public float medaForceMovementSpeed;
    public float medaforceRechargeSmg;
    public float medafroceRechargeRevolver;

    [Header("Sounds")]
    public string turningOffSound;
    private FMOD.Studio.EventInstance turningOffSoundInstance;
    private bool playedRArmSound;
    private bool playedLArmSound;
    private bool playedLegSound;

    [Header("MedaForce Related")]
    public float MedaForceDuration = 5f;
    public bool MedaForceActive = false;
    // Adjust the duration as needed
    private float MedaForceTimer = 0f;
    private float originalNorthAttack;
    private float originalDamageWestAttack;
    private float originalDamageEastAttack;
    private float originalRechargeSmg;
    private float originalRechargeRevolver;
    private int originalMagSizeSmg;
    private int originalMagSizeRev;
    private float originalMovementSpeed;
     

    private void Awake()
    {
        characterStatsSO = characterStatsArray[0];
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

      
        PM = GetComponent<PlayerMovements>();
        if (this.characterStatsSO.characterReferenceNumber == 1)
        {
            shooter = GetComponentInChildren<Shooter>();
            RL = GetComponentInChildren<RocketLaucher>();
          //  characterStatsSO = characterStatsArray[0].GetComponent<MetabeeStats>();
        }
        medaparts = GetComponentsInChildren<MedaPartScript>();

        // Set metabee stats if metabee(in the future gotta see if he is a shooter or a melle and should work for everything)
        if (this.characterStatsSO.characterReferenceNumber == 1&&characterStatsSO is MetabeeStats metabeeStats)
        {
            foreach (MedaPartScript medapart in medaparts)
            {
                int medapartNumber = medapart.MedapartNumber;
                switch (medapartNumber)
                {
                    case 1:
                        medapart.defense = metabeeStats.defenseHead;
                        break;
                    case 2:
                        medapart.defense = metabeeStats.defenseLeftArm;
                        break;
                    case 3:
                        medapart.defense = metabeeStats.defenseRightArm;
                        break;
                    case 4:
                        medapart.defense = metabeeStats.defenseLegs;
                        break;
                    default:
                        // Handle other medaparts if needed
                        break;
                }
            }
            this.PM.dashCoolDown = metabeeStats.baseRechargeDash; 
            this.lowEnergyDamageWestAttack=metabeeStats.lowEnergyDamageWestAttack;
            this.lowEnergyMagSizeSmg = metabeeStats.lowEnergyMagSizeSmg;
            this.lowEnergyDamageEastAttack = metabeeStats.lowEnergyDamageEastAttack;
            this.lowEnergyMagSizeRev = metabeeStats.lowEnergyMagSizeRev;
            this.lowEnergyMovementSpeed=metabeeStats.lowEnergyMovementSpeed;
            this.lowRechargeSmg = metabeeStats.lowRechargeSmg;
            this.lowRechargeRevolver = metabeeStats.lowRechargeRevolver;
            this.lowRechargeDash = metabeeStats.lowRechargeDash;

    SetMedapartStatsShooter(metabeeStats.baseDamageWestAttack, metabeeStats.baseDamageEastAttack, metabeeStats.baseMagSizeSmg, metabeeStats.baseMagSizeRev, metabeeStats.baseMovementSpeed, metabeeStats.BaseDamageNorthAttack, metabeeStats.baseRechargeSmg, metabeeStats.baseRechargeRevolver);
            this.shooter.bulletPrefab.GetComponent<Bullet>().critValue = metabeeStats.lastBulletCritMultiplyer;
        }

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
                SetMedapartStatsShooter(originalDamageWestAttack, originalDamageEastAttack, originalMagSizeSmg, originalMagSizeRev, originalMovementSpeed,originalNorthAttack,originalRechargeSmg,originalRechargeRevolver);
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
    private void SetMedapartStatsShooter(float damage_WestAttack, float damage_EastAttack, int mag_SizeSmg, int mag_SizeRev, float movement_Speed,
    float damage_NorthAttack, float Recharge_Smg, float Recharge_Revolver)
    {
        RL.damagePerRocket = damage_NorthAttack;
        shooter.smgDamage = damage_WestAttack;
        shooter.revolverDamage = damage_EastAttack;
        shooter.rechargeTimeEast= Recharge_Revolver;
        shooter.rechargeTimeWest= Recharge_Smg;
        shooter.magSizeFullAuto = mag_SizeSmg;
        shooter.maxMagazineSizeRevolver = mag_SizeRev;
        PM.playerSpeed = movement_Speed;
    }

    public void UseMedaForce()
    {
        MedaForceActive = true;
        MedaForceTimer = MedaForceDuration;

        // Save the original medapart stats to restore them later
        originalNorthAttack = RL.damagePerRocket;
        originalDamageWestAttack = shooter.smgDamage;
        originalDamageEastAttack = shooter.revolverDamage;
        originalMagSizeSmg = shooter.magSizeFullAuto;
        originalMagSizeRev = shooter.maxMagazineSizeRevolver;
        originalRechargeRevolver=shooter.rechargeTimeEast;
        originalRechargeSmg=shooter.rechargeTimeWest;

        originalMovementSpeed = PM.playerSpeed;
        SetMedapartStatsShooter(medaForceDamageWestAttack, medaForceDamageEastAttack, medaForceMagSizeSmg, medaForceMagSizeRev, medaForceMovementSpeed,MedaForceNorthAttack,medaforceRechargeSmg,medafroceRechargeRevolver);
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