using UnityEngine;

public class PlayerMedapartsController : MonoBehaviour
{
    public MedaPartScript[] medaparts;
    public float lowEnergyThreshold = 0;
    public bool isBlocking = false;
    private LockOn enLockON;
    private MedaPartScript currentTarget;
    private RocketLaucher RL;
    private Shooter shooter;
    private PlayerMovements PM;
    [Header("Base Attacks, Defenses and Speed Values")]
    public float Defense;
    [Header("perBullet")]
    public float lastBulletCritMultiplyer;
    public float DamageWestAttack;
    public int MagSizeSmg;
    public float DamageEastAttack;
    public int MagSizeRev;
    [Header("in total")]
    public float DamageNorthAttack;
    public float MovementSpeed;
    [Header("Damaged Attack, Defense and Speed Values")]
    public float LE_Defense;
    public float LE_DamageWestAttack;
    public int LE_MagSizeSmg;
    public float LE_DamageEastAttack;
    public int LE_MagSizeRev;
    public float LE_MovementSpeed;
    private void Start()
    {
        medaparts = GetComponentsInChildren<MedaPartScript>();
        this.shooter = GetComponent<Shooter>();
        this.shooter.smgDamage = DamageWestAttack;
        this.shooter.bulletPrefab.GetComponent<Bullet>().critValue = this.lastBulletCritMultiplyer;
        this.shooter.revolverDamage = DamageEastAttack;
        PM = GetComponent<PlayerMovements>();
        PM.playerSpeed = MovementSpeed;
        this.RL = GetComponent<RocketLaucher>();
        this.RL.damagePerRocket = DamageNorthAttack / 2;
        if (GetComponent<Player1>() != null)
        {
            enLockON = FindObjectOfType<Player2>().GetComponent<LockOn>();
        }
        else if (GetComponent<Player2>() != null)
        {
            enLockON = FindObjectOfType<Player1>().GetComponent<LockOn>();
        }
    }

    private void Update()
    {
        DetermineTarget();
        CheckLowEnergy();
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
                        // Decrease damage or change behavior for arms
                        this.shooter.magSizeFullAuto = this.LE_MagSizeSmg;
                       
                        break;
                    case 3: // Arm Medapart
                            // Decrease damage or change behavior for arms
                        this.shooter.maxMagazineSizeRevolver = this.LE_MagSizeRev;
                        break;
                    case 4: // Leg Medapart
                        // Decrease movement speed or change behavior for legs
                        PM.playerSpeed = LE_MovementSpeed;
                        break;
                    default:
                        // Handle other medaparts if needed
                        break;
                }
            }
        }
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

