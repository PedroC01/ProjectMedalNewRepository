using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;
//using static UnityEngine.InputSystem.InputAction;

public class RocketLaucher : MonoBehaviour
{
    public float rechargeTime;
    public float TimerForRecharge;
    public bool Shooted;
    public GameObject _rocket;
    public GameObject rocketPrefab;
    public GameObject rocketPrefab2;
    public Transform firePointRocket;
    public Transform firePointRocket2;
    public Transform lookat;
    public LockOn LO;
    public float damagePerRocket;
    public float delayInSecondRocket = 0.5f;
    [Header("Sounds")]
    public string RocketLaunchSound;
    private FMOD.Studio.EventInstance RocketLaunchSoundInstance;
    private PlayerMovements pm;
    private Transform thisPlayer;
    public VisualEffect rocketSmoke;
    public VisualEffect rocketSmoke2;
    public float TimerForSmoke=1.5f;
    private float rechargeSmoke;
    // Start is called before the first frame update
    void Start()
    {
           rocketSmoke.Stop();
         rocketSmoke2.Stop();
        RocketLaunchSoundInstance = FMODUnity.RuntimeManager.CreateInstance(RocketLaunchSound);
        TimerForRecharge = rechargeTime;
        rechargeSmoke = TimerForSmoke;
        pm=GetComponentInParent<PlayerMovements>();
        this.thisPlayer = pm.gameObject.GetComponent<Transform>();
        if (GetComponentInParent<Player1>() == true)
        {
          
            _rocket = rocketPrefab;
          
        }
        else if (GetComponentInParent<Player2>() == true)
        {
   
            _rocket = rocketPrefab2;
         
        }
    }

    public void North()
    {

        Shooted = true;
        if (Shooted == true && TimerForRecharge <= 0)
        {
            StartCoroutine(Fire());
           
        

        }
    }
    // Update is called once per frame
    void Update()
    {
        if (TimerForRecharge > 0)
        {
            Mathf.Clamp(TimerForRecharge -= Time.deltaTime, 0, TimerForRecharge);
            if (TimerForRecharge <= 0)
            {
                Shooted = false;
            }
        }
        if (TimerForSmoke > 0)
        {
            TimerForSmoke = Mathf.Clamp(TimerForSmoke - Time.deltaTime, 0, rechargeSmoke);
             rocketSmoke.Stop();
             rocketSmoke2.Stop();
        }


    }

    private IEnumerator Fire()
    {
        TimerForRecharge = rechargeTime;
        if (LO.Locked == true)
        {
            this.transform.LookAt(new Vector3(LO.lockOnTarget.transform.position.x, LO.lockOnTarget.transform.position.y, LO.lockOnTarget.transform.position.z));
            lookat = LO.lockOnTarget;
        }
        if (!pm.IsMoving)
        {
            pm.rb.velocity = Vector3.zero;
            pm.canMove = false;
            pm.horizontalInput.x = 0;
            pm.horizontalInput.y = 0;
            thisPlayer.LookAt(new Vector3(LO.lockOnTarget.transform.position.x, LO.lockOnTarget.transform.position.y, LO.lockOnTarget.transform.position.z));
         
            lookat = LO.lockOnTarget.transform;
            pm.m_Animator1.SetBool("ShootRocket", true);
        }
        RocketLaunchSoundInstance.start();
        GameObject HeadPrefabBullet = Instantiate(_rocket, firePointRocket.position, firePointRocket.rotation);
        HeadPrefabBullet.GetComponent<Rocket>().targetRb = this.LO.Enemy.GetComponent<Rigidbody>();
        HeadPrefabBullet.GetComponent<Rocket>().RocketDamage = damagePerRocket;
        rocketSmoke.Play();
        yield return new WaitForSecondsRealtime(delayInSecondRocket);

        RocketLaunchSoundInstance.start();
        GameObject HeadPrefabBullet2 = Instantiate(_rocket, firePointRocket2.position, firePointRocket2.rotation);
        HeadPrefabBullet2.GetComponent<Rocket>().targetRb = this.LO.Enemy.GetComponent<Rigidbody>();
        HeadPrefabBullet2.GetComponent<Rocket>().RocketDamage = damagePerRocket;
        rocketSmoke2.Play();
        TimerForSmoke = rechargeSmoke;
        pm.canMove = true;
        pm.m_Animator1.SetBool("ShootRocket", false);
    }
}
