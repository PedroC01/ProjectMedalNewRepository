using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class Shooter : MonoBehaviour
{
    public float rechargeTimeEast;
    public float rechargeTimeWest;
    private float TimerForRechargeEast;
    private float TimerForRechargeWest;
    //public float TimerForFirerate;
    public bool Shooted;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public Transform FullAutoFirePoint1;
    public Transform FullAutoFirePoint2;
    public Transform lookat;
    private LockOn LO;
    public float dealDamage;
    public bool shootFullAuto;
    public int magSize;
    private int magSizeRecharge;
    private PlayerMovements PM;
    private bool MeleeEast;
    [SerializeField]
    public Animator m_Animator;
    public bool plAreClose;
    public bool Attacked;
    // Start is called before the first frame update
    void Start()
    {
        LO=FindObjectOfType<LockOn>();
        magSizeRecharge = magSize;
        PM = GetComponentInParent<PlayerMovements>();
        
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
           // MeleeEast = true;
            m_Animator.SetTrigger("MeleeEast");
            return;
        }
        this.Shooted = false;

    }
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
            m_Animator.SetTrigger("MeleeWest");
           return ;
        }
        shootFullAuto = false;
    }


    // Update is called once per frame
    void Update()
    {
        if (TimerForRechargeEast > 0)
        {
            Mathf.Clamp(TimerForRechargeEast -= Time.deltaTime, 0, TimerForRechargeEast);
        }
        if(TimerForRechargeWest > 0)
        {
            Mathf.Clamp(TimerForRechargeWest -= Time.deltaTime, 0, TimerForRechargeWest);
           if(TimerForRechargeWest <= 0)
            {
                magSize = magSizeRecharge;
            }
        }
      
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
       // Gizmos.DrawLine(this.transform.position,lookat.position);


    }





        private IEnumerator Fire()
    {
        if (LO.Locked == true)
        {
            this.transform.LookAt(new Vector3(LO.lockOnTarget.transform.position.x, LO.lockOnTarget.transform.position.y, LO.lockOnTarget.transform.position.z));
            lookat = LO.lockOnTarget;
        }

        TimerForRechargeEast = rechargeTimeEast;

        GameObject newBullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        yield return new WaitForSeconds(0.1f);
    }




    private IEnumerator FireFullAuto()
    {
     while(shootFullAuto == true)
        {
            if (LO.Locked == true)
            {
                this.transform.LookAt(new Vector3(LO.lockOnTarget.transform.position.x, LO.lockOnTarget.transform.position.y, LO.lockOnTarget.transform.position.z));
                lookat = LO.lockOnTarget;
            }
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
   
        }
        yield return new WaitForSeconds(0.1f);
    }
   
}
