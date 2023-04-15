using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Rocket : MonoBehaviour
{
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
    [SerializeField] private float _speed = 15;
    [SerializeField] private float rotateSpeed = 95;

    [Header("PREDICTION")]
    [SerializeField] private float maxDistancePredict = 100;
    [SerializeField] private float minDistancePredict = 5;
    [SerializeField] private float maxTimePrediction = 5;
    private Vector3 standardPrediction, deviatedPrediction;

    [Header("DEVIATION")]
    [SerializeField] private float deviationAmount = 50;
    [SerializeField] private float deviationSpeed = 2;

    
    // Start is called before the first frame update
    void Start()
    {


        

        
        rb = GetComponent<Rigidbody>();
        //targetRb=target.GetComponent<Rigidbody>(); 
       
        thisGotShooted = true;

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
            
            this.autoDestructTimer = math.clamp(this.autoDestructTimer - Time.deltaTime, 0, 10);
            if (this.autoDestructTimer <= 0)
            {
                canDestroy = true;
                this.thisGotShooted = false;
            }
            else
            {
                canDestroy = false;
                return;

            }
        }
        
    }

   
    void FixedUpdate()
    {
        rb.velocity = transform.forward * rocketSpeed;
       
        var leadTimePercentage = Mathf.InverseLerp(minDistancePredict, maxDistancePredict, Vector3.Distance(transform.position, targetRb.transform.position));

        MovementPrediction(leadTimePercentage);

        AddDeviation(leadTimePercentage);

        RotateRocket();
        if (canDestroy == true)
        {
            Destroy(this);
        }
        else
        {
            return;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(_explosionPrefab, this.transform.position, this.transform.rotation);
        Destroy(this.gameObject);
    }
    void OnTriggerEnter(Collider other)
    {
        var othercolliders=Physics.OverlapSphere(this.transform.position, explosionRadious);
        Instantiate(_explosionPrefab,this.transform.position,this.transform.rotation);
       /* foreach(Collider coll in othercolliders)
        {
            if (coll.GetComponent<MedaPartScript>().playerX == 2 || coll.GetComponent<MedaPartScript>().playerX == 1)
            {
             //   coll.GetComponent<MedaPartScript>().Damage = -30;
                Destroy(this.gameObject);
            }
            
        }*/
        Destroy(this.gameObject);

        if (other.GetComponent<MedaPartScript>().playerX == 2 || other.GetComponent<MedaPartScript>().playerX == 1)
        {
            other.GetComponent<MedaPartScript>().Damage = -30;
            Destroy(this.gameObject);
        }
        if (other.CompareTag("Floor"))
        {

            Destroy(this.gameObject);
        }

    }

}
