using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
//using static UnityEngine.InputSystem.InputAction;

public class RocketLaucher : MonoBehaviour
{
    public float rechargeTime;
    public float TimerForRecharge;
    public bool Shooted;
    public GameObject rocketPrefab;
    public Transform firePointRocket;
    public Transform firePointRocket2;
    public Transform lookat;
    public LockOn LO;
    


    // Start is called before the first frame update
    void Start()
    {
        LO = GetComponentInParent<LockOn>();
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
            
        
    }

    private IEnumerator Fire()
    {
        if (LO.Locked == true)
        {
            this.transform.LookAt(new Vector3(LO.lockOnTarget.transform.position.x, LO.lockOnTarget.transform.position.y, LO.lockOnTarget.transform.position.z));
            lookat = LO.lockOnTarget;
        }
        GameObject HeadPrefabBullet = Instantiate(rocketPrefab, firePointRocket.position, firePointRocket.rotation);
        GameObject HeadPrefabBullet2 = Instantiate(rocketPrefab, firePointRocket2.position, firePointRocket2.rotation);
        HeadPrefabBullet.GetComponent<Rocket>().targetRb = this.LO.Enemy.GetComponent<Rigidbody>();
        HeadPrefabBullet2.GetComponent<Rocket>().targetRb = this.LO.Enemy.GetComponent<Rigidbody>();
        TimerForRecharge = rechargeTime;
        yield return new WaitForSecondsRealtime(0.1f);
    }
}
