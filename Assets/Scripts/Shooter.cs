    using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;
using FMODUnity;
public class Shooter : MonoBehaviour
{
    public float rechargeTimeEast;
    public float rechargeTimeWest;
    public float TimerForRechargeEast;
    public float TimerForRechargeWest;
    //public float TimerForFirerate;
    public bool Shooted;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public Transform FullAutoFirePoint1;
    public Transform FullAutoFirePoint2;
    public Transform lookat;
    public LockOn LO;
    public float melleeDamage;
    public bool shootFullAuto;
    private PlayerMovements PM;
    private bool MeleeEast;
    private bool MeleeWest;
    [SerializeField]
    public Animator m_Animator;
    public bool plAreClose;
    public bool Attacked;
    public float mineBulletSpeed;
    public Transform thisPlayer;
    [SerializeField]
    public UIbuttons ub;
    public UIbuttons1 ub1;
    public float fireRateRevolver;
    public float fireRateFullAuto;
    public float delayFullAuto;
    public int bulletsShotCount = 0;
    public bool lastBulletCrit = false;
    [Header("Guns Damage and MagSizes")]
    public float smgDamage;
    public float revolverDamage;
    public int magSizeFullAuto;
    private int MaxMagFullAuto;
    private int bulletsInMagazineRev;
    public int maxMagazineSizeRevolver = 6;

    [Header("Sounds")]
    public string shootRevSoundEvent;
    private FMOD.Studio.EventInstance shootRevSoundInstance;
    public string shootAutoSoundEvent;
    private FMOD.Studio.EventInstance shootAutoSoundInstance;

    // Start is called before the first frame update
    void Start()
    {
        LO = GetComponent<LockOn>();
        MaxMagFullAuto = magSizeFullAuto;
        PM = GetComponent<PlayerMovements>();
        this.bulletPrefab.GetComponent<Bullet>().bulletSpeed=mineBulletSpeed;
        // this.thisPlayer=GetComponentInParent<Transform>();
        m_Animator = GetComponentInChildren<Animator>();
        bulletsInMagazineRev = maxMagazineSizeRevolver;
        shootRevSoundInstance = FMODUnity.RuntimeManager.CreateInstance(shootRevSoundEvent);
        shootAutoSoundInstance = FMODUnity.RuntimeManager.CreateInstance(shootAutoSoundEvent);
    }

    public void East()
    {
        this.Shooted = true;
        if (PM.closeRange == false)
        {
            if (Shooted == true && TimerForRechargeEast <= 0)
            {
                StartCoroutine(Fire(fireRateRevolver));
              return;
            }
           
        }
        if (PM.closeRange == true)
        {
            MeleeEast = true;
            m_Animator.SetTrigger("MeleeEast");
            
            
            return;
        }
        this.Shooted = false;

    }

    /// <summary>
    /// tenho de fazer a verificação do inicio, final, e se aind a estou a disparar e com essa inforamação chamar a couroutine do fullauto
    /// </summary>
    public void West()
    {
       this. shootFullAuto = true;
        if (PM.closeRange == false)
        {
            if (shootFullAuto == true && TimerForRechargeEast <= 0)
            {
               
                StartCoroutine(FireFullAuto());
                return;
            }
        }
        if (PM.closeRange == true)
        {
            MeleeWest = true;
            m_Animator.SetTrigger("MeleeWest");
           return ;
        }
      
        
    }

    public void WestRelease()
    {
        StopCoroutine(FireFullAuto());
        this.shootFullAuto = false;
        
        m_Animator.SetBool("ShootingLeft", false);
        StopCoroutine(FireFullAuto());

    }
    public void EastRelease()
    {
        this.Shooted = false;
        StopCoroutine(Fire(fireRateRevolver));
    }


    public void ResetBulletShotCount()
    {
        bulletsShotCount = 0;
        lastBulletCrit = false;
    }

    public bool LastBulletCrit()
    {
        return lastBulletCrit;
    }




    // Update is called once per frame
    void Update()
    {
        lookat = LO.lockOnTarget.transform;
        if (TimerForRechargeEast > 0)
        {
            TimerForRechargeEast -= Time.deltaTime;
            TimerForRechargeEast = Mathf.Clamp(TimerForRechargeEast, 0f, Mathf.Infinity);
            if (TimerForRechargeEast <= 0)
            {
                bulletsInMagazineRev = maxMagazineSizeRevolver; // Reset the magazine size after recharge
            }
        }

        if (TimerForRechargeWest > 0)
        {
            TimerForRechargeWest -= Time.deltaTime;
            TimerForRechargeWest = Mathf.Clamp(TimerForRechargeWest, 0f, Mathf.Infinity);

            if (TimerForRechargeWest <= 0)
            {
                magSizeFullAuto = MaxMagFullAuto; // Reset the magazine size after recharge
            }
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
       // Gizmos.DrawLine(this.transform.position,lookat.position);


    }

    void FixedUpdate()
    {
        if (MeleeEast == true||MeleeWest)
        {
            PM.rb.velocity = Vector3.zero;
            this.thisPlayer.LookAt(new Vector3(LO.lockOnTarget.transform.position.x, this.thisPlayer.position.y, LO.lockOnTarget.transform.position.z));

            if(PM.dist <= PM.distClose){
                LO.lockOnTarget.GetComponent<MedaPartScript>().partEnergy-=melleeDamage;
            }

           /* Collider[] colliders = Physics.OverlapSphere(this.transform.position, PM.distClose,0,QueryTriggerInteraction.Collide);
            foreach (Collider hit in colliders)
            {
                if (hit.GetComponent<MedaPartScript>().playerX == 2 || hit.GetComponent<MedaPartScript>().playerX == 1)
                {
                    hit.GetComponent<MedaPartScript>().Damage = hit.GetComponent<MedaPartScript>().partEnergy -= melleeDamage;
                    Destroy(this.gameObject);
                }
            }*/
            MeleeEast = false;
            MeleeWest = false;
        }
    }

    private IEnumerator Fire(float fireRate)
    {
        
        while (Shooted && TimerForRechargeEast <= 0 && bulletsInMagazineRev > 0)
        {
            this.thisPlayer.LookAt(new Vector3(LO.lockOnTarget.transform.position.x, LO.lockOnTarget.transform.position.y, LO.lockOnTarget.transform.position.z));
            this.transform.LookAt(new Vector3(LO.lockOnTarget.transform.position.x, LO.lockOnTarget.transform.position.y, LO.lockOnTarget.transform.position.z));
            this.firePoint.LookAt(new Vector3(LO.lockOnTarget.transform.position.x, LO.lockOnTarget.transform.position.y, LO.lockOnTarget.transform.position.z));
            lookat = LO.lockOnTarget.transform;

            shootRevSoundInstance.start();
            GameObject newBullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            newBullet.GetComponent<Bullet>().damagePerBullet = revolverDamage;

            bulletsInMagazineRev--;
            this.bulletsShotCount++;
            if (bulletsInMagazineRev<=0)
            {
                this.Shooted = false;
                TimerForRechargeEast = rechargeTimeEast;
            }
            if (bulletsInMagazineRev <= 0 && bulletsShotCount > 0)
            {
                lastBulletCrit = true;
                newBullet.GetComponent<Bullet>().hasCrit = true;
            }
            yield return new WaitForSeconds(fireRate);
        }
        ResetBulletShotCount();
    }




    private IEnumerator FireFullAuto()
    {
        
        while (shootFullAuto == true)
        {
            if (PM.IsMoving == false)
            {
                this.transform.LookAt(new Vector3(LO.lockOnTarget.transform.position.x, this.transform.position.y, LO.lockOnTarget.transform.position.z));
                new WaitForSeconds(delayFullAuto);
            }
            m_Animator.SetBool("ShootingLeft", true);

            lookat = LO.lockOnTarget;

            this.FullAutoFirePoint1.LookAt(new Vector3(LO.lockOnTarget.transform.position.x, LO.lockOnTarget.transform.position.y, LO.lockOnTarget.transform.position.z));
            this.FullAutoFirePoint2.LookAt(new Vector3(LO.lockOnTarget.transform.position.x, LO.lockOnTarget.transform.position.y, LO.lockOnTarget.transform.position.z));

            if (magSizeFullAuto > 0)
            {
                shootAutoSoundInstance.start();
                GameObject newBullet = Instantiate(bulletPrefab, FullAutoFirePoint1.position, FullAutoFirePoint1.rotation);
                newBullet.GetComponent<Bullet>().damagePerBullet = smgDamage;
                this.bulletsShotCount++;
                magSizeFullAuto--;
                if (magSizeFullAuto <= 0 && bulletsShotCount > 0)
                {
                    lastBulletCrit = true;
                    newBullet.GetComponent<Bullet>().hasCrit = true;
                }
            }

            yield return new WaitForSecondsRealtime(fireRateFullAuto);
            
            if (magSizeFullAuto > 0)
            {
                shootAutoSoundInstance.start();
                GameObject newBullet2 = Instantiate(bulletPrefab, FullAutoFirePoint2.position, FullAutoFirePoint2.rotation);
                newBullet2.GetComponent<Bullet>().damagePerBullet = smgDamage;
                this.bulletsShotCount++;
                magSizeFullAuto--;
                if (magSizeFullAuto <= 0 && bulletsShotCount > 0)
                {
                    lastBulletCrit = true;
                    newBullet2.GetComponent<Bullet>().hasCrit = true;
                }
            }

            if (magSizeFullAuto <= 0)
            {
                shootFullAuto = false;
                TimerForRechargeWest = rechargeTimeWest;
            }
            
            yield return null;
        }
        ResetBulletShotCount();
    }

}
