using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

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
    public float dealDamage;
    public bool shootFullAuto;
    public int magSize;
    private int magSizeRecharge;
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
    // Start is called before the first frame update
    void Start()
    {
        LO = GetComponentInParent<LockOn>();
        magSizeRecharge = magSize;
        PM = GetComponentInParent<PlayerMovements>();
        this.bulletPrefab.GetComponent<Bullet>().bulletSpeed=mineBulletSpeed;
       // this.thisPlayer=GetComponentInParent<Transform>();

    }

    public void East()
    {
        this.Shooted = true;
        if (PM.closeRange == false)
        {
            if (Shooted == true && TimerForRechargeEast <= 0)
            {
                StartCoroutine(Fire());
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
        shootFullAuto = true;
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
        shootFullAuto = false;
        
    }


    // Update is called once per frame
    void Update()
    {
        lookat = LO.lockOnTarget.transform;
        if (TimerForRechargeEast > 0)
        {
            Mathf.Clamp(TimerForRechargeEast -= Time.deltaTime, 0, TimerForRechargeEast);
           
        }
        if(TimerForRechargeWest > 0)
        {
            
            Mathf.Clamp(TimerForRechargeWest -= Time.deltaTime, 0, TimerForRechargeWest);
         
            if (TimerForRechargeWest <= 0)
            {
                magSize = magSizeRecharge;
            }
        }
        if (shootFullAuto == false)
        {
            StopCoroutine(FireFullAuto());
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

        private IEnumerator Fire()
    {
        //if (LO.Locked == true)
        //{
            this.thisPlayer.LookAt(new Vector3(LO.lockOnTarget.transform.position.x, LO.lockOnTarget.transform.position.y, LO.lockOnTarget.transform.position.z));
            this.transform.LookAt(new Vector3(LO.lockOnTarget.transform.position.x, LO.lockOnTarget.transform.position.y, LO.lockOnTarget.transform.position.z));
            this.firePoint.LookAt(new Vector3(LO.lockOnTarget.transform.position.x, LO.lockOnTarget.transform.position.y, LO.lockOnTarget.transform.position.z));
        lookat = LO.lockOnTarget.transform;
       // }

        TimerForRechargeEast = rechargeTimeEast;
        GameObject newBullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        
        yield return new WaitForSeconds(0.1f);
    }




    private IEnumerator FireFullAuto()
    {
     while(shootFullAuto == true)
        {
          //  if (LO.Locked == true)
          //  {
           // this.thisPlayer.LookAt(new Vector3(LO.lockOnTarget.transform.position.x, LO.lockOnTarget.transform.position.y, LO.lockOnTarget.transform.position.z));
             lookat = LO.lockOnTarget;
            this.transform.LookAt(new Vector3(LO.lockOnTarget.transform.position.x, LO.lockOnTarget.transform.position.y, LO.lockOnTarget.transform.position.z));
            this.FullAutoFirePoint1.LookAt(new Vector3(LO.lockOnTarget.transform.position.x, LO.lockOnTarget.transform.position.y, LO.lockOnTarget.transform.position.z));
            this.FullAutoFirePoint2.LookAt(new Vector3(LO.lockOnTarget.transform.position.x, LO.lockOnTarget.transform.position.y, LO.lockOnTarget.transform.position.z));
            //  }
            if (magSize > 1)
            {
                GameObject newBullet = Instantiate(bulletPrefab, FullAutoFirePoint1.position, FullAutoFirePoint1.rotation);
                Mathf.Clamp(magSize--, 0, magSizeRecharge);
                
            }
            if (magSize > 0)
            {
                GameObject newBullet2 = Instantiate(bulletPrefab, FullAutoFirePoint2.position, FullAutoFirePoint2.rotation);
                Mathf.Clamp(magSize--, 0, magSizeRecharge);

            }
            if (magSize <= 0)
            {
                shootFullAuto = false;
                TimerForRechargeWest = rechargeTimeWest;
               
               
            }
            
            yield return new WaitForSecondsRealtime(1f);
        }
        yield return new WaitForSecondsRealtime(0.1f);
    }
   
}
