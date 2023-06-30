using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public float RocketDamage;
    private Rigidbody rb;
    public float rocketSpeed;
    //public float maxTimePrediciton;
   // public float leadTimePercentage;
    public GameObject target;
    public  Rigidbody targetRb;
    public float autoDestructTimer=5;
    private LockOn LO;
    private bool thisGotShooted;
    public bool canDestroy;
    [Header("REFERENCES")]
   // [SerializeField] private Rigidbody _rb;
   // [SerializeField] private Target, _target;
    [SerializeField] 
    private GameObject _explosionPrefab;
    public float explosionRadious;

    [Header("MOVEMENT")]
    //[SerializeField] private float _speed = 15;
    [SerializeField] private float rotateSpeed = 95;

    [Header("PREDICTION")]
    [SerializeField] private float maxDistancePredict = 100;
    [SerializeField] private float minDistancePredict = 5;
    [SerializeField] private float maxTimePrediction = 5;
    private Vector3 standardPrediction, deviatedPrediction;

    [Header("DEVIATION")]
    [SerializeField] private float deviationAmount = 50;
    [SerializeField] private float deviationSpeed = 2;
    private bool isFollowing;
    private Vector3 storedLastPosition;

    [Header("Sounds")]
    public string ExplosionSound;
    private FMOD.Studio.EventInstance explosionSoundInstance;
    public string collisionSoundEvent;
    private StudioEventEmitter eventEmitter;
    // Start is called before the first frame update
    void Start()
    {

        eventEmitter = GetComponent<StudioEventEmitter>();

        explosionSoundInstance = FMODUnity.RuntimeManager.CreateInstance(ExplosionSound);

        rb = GetComponent<Rigidbody>();
        //targetRb=target.GetComponent<Rigidbody>(); 
       
        thisGotShooted = true;
        this.isFollowing = true;

    }



    private void RotateRocket()
    {
        var heading = deviatedPrediction - transform.position;
        var rotation = Quaternion.LookRotation(heading);
        rb.MoveRotation(Quaternion.RotateTowards(transform.rotation,rotation,rotateSpeed *Time.deltaTime));
    }

    private void MovementPrediction(float leadTimePercentage)
    {
        var predictionTime = Mathf.Lerp(0, maxTimePrediction, leadTimePercentage);
        standardPrediction = targetRb.transform.position + targetRb.velocity*predictionTime;
       
    }

    private void AddDeviation(float leadTimePercentage)
    {
        var deviation = new Vector3(Mathf.Cos(Time.time * deviationSpeed), 0, 0);

        var predictionOffset = transform.TransformDirection(deviation) * deviationAmount * leadTimePercentage;

        deviatedPrediction = standardPrediction + predictionOffset;
    }

    // Update is called once per frame
    void Update()
    {
        if (thisGotShooted)
        {
            if (isFollowing)
            {
                this.autoDestructTimer = math.clamp(this.autoDestructTimer - Time.deltaTime, 0, 10);
                if (this.autoDestructTimer <= 0)
                {
                  //  canDestroy = true;
                    storedLastPosition = new Vector3(targetRb.transform.position.x,0, targetRb.transform.position.z);
                isFollowing = false;
                }
                else
                {
                  //  canDestroy = false;
                    return;

                }
            }
           
        }
        
    }


    void FixedUpdate()
    {
        rb.velocity = transform.forward * rocketSpeed;
       if(isFollowing==true)
        {
            var leadTimePercentage = Mathf.InverseLerp(minDistancePredict, maxDistancePredict, Vector3.Distance(transform.position, targetRb.transform.position));

            MovementPrediction(leadTimePercentage);

            AddDeviation(leadTimePercentage);

            RotateRocket();
        }
        if (isFollowing==false)
        {
            this.rb.transform.LookAt(storedLastPosition);

            
        }
        
        /*if (canDestroy == true)
        {
            Instantiate(_explosionPrefab, this.transform.position, this.transform.rotation);

            Collider[] colliders = Physics.OverlapSphere(this.transform.position, explosionRadious);
            foreach (Collider coll in colliders)
            {
                if (coll.GetComponent<MedaPartScript>())
                {

                    coll.GetComponent<MedaPartScript>().ApplyDamage(30);

                }

            }
            Destroy(this.gameObject);
        }
        else
        {
            return;
        }*/
    }

    private void OnCollisionEnter(Collision collision)
    {

        /*Instantiate(_explosionPrefab, this.transform.position, this.transform.rotation);

        Collider[] colliders = Physics.OverlapSphere(this.transform.position, explosionRadious);
        foreach (Collider coll in colliders)
        {
            if (coll.GetComponent<MedaPartScript>())
            {

                coll.GetComponent<MedaPartScript>().ApplyDamage(30);

            }

        }*/
    }
    void OnTriggerEnter(Collider other)
    {

        var Medapart = other.GetComponent<MedaPartScript>();
      
        if (Medapart != null)
        {
           
            if (Medapart.playerX == 2 || Medapart.playerX == 1)
            {

                RuntimeManager.PlayOneShot(collisionSoundEvent);
                Instantiate(_explosionPrefab, this.transform.position, this.transform.rotation);
                explosionSoundInstance.start();
                Collider[] colliders = Physics.OverlapSphere(this.transform.position, explosionRadious);
                foreach (Collider coll in colliders)
                {
                    if (coll.GetComponent<MedaPartScript>())
                    {

                        coll.GetComponent<MedaPartScript>().ApplyDamage(RocketDamage);

                    }

                }
                Debug.Log("usou trigger");

                Destroy(this.gameObject);
            }
           
        }
        if (other.CompareTag("Floor"))
        {
            Instantiate(_explosionPrefab, this.transform.position, this.transform.rotation);
            explosionSoundInstance.start();
            Collider[] colliders = Physics.OverlapSphere(this.transform.position, explosionRadious);
            foreach (Collider coll in colliders)
            {
                if (coll.GetComponent<MedaPartScript>())
                {

                    coll.GetComponent<MedaPartScript>().ApplyDamage(RocketDamage);

                }

            }

            Destroy(this.gameObject);
        }
    }
}
