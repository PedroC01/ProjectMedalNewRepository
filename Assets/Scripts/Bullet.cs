using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody rb;
    public float bulletSpeed;
    public float damagePerBullet;
    public GameObject impactVFX;
    public Vector3 firstposition;
    public bool hasCrit;
    public float critValue;
    private float crit;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
      
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 bulletVelocity=this.transform.forward*bulletSpeed;
        rb.velocity= bulletVelocity;
        /// this.rb.AddForce(velocity,ForceMode.Impulse);
        firstposition = this.transform.position;
    }
    private void Update()
    {
        if (90 <= this.transform.position.x || this.transform.position.x <= -90 || 90 <= this.transform.position.z || this.transform.position.z <= -90)
        {
            Destroy(this.gameObject);
        }
    }
    /* private void OnCollisionEnter(Collision collision)
     {
         if (other.GetComponent<MedaPartScript>().playerX == 2|| other.GetComponent<MedaPartScript>().playerX == 1)
        {
            other.GetComponent<MedaPartScript>().Damage = other.GetComponent<MedaPartScript>().Damage - 2;
            Destroy(this.gameObject);
        }
     }*/

    public void OnTriggerEnter(Collider other)
    {
        if (hasCrit)
        {
            this.crit = critValue;
        }
        else
        {
            this.crit = 1;
        }
      
    
        Vector3 hitPoint = other.ClosestPoint(firstposition);
        Vector3 hitNormal = (transform.position - hitPoint).normalized;

        // Calculate the new position outside the mesh
        Vector3 newPosition = hitPoint + hitNormal * 0.1f;


        var Medapart = other.GetComponent<MedaPartScript>();

        if(Medapart != null)
        {
           // Vector3 HitPoint= transform.position;
            if (Medapart.playerX == 2 || Medapart.playerX == 1)
            {
                other.GetComponent<MedaPartScript>().ApplyDamage(damagePerBullet*crit);
               Instantiate(this.impactVFX, newPosition, this.transform.rotation);
             
            }
            

        }
        else { return; }

        if (other.CompareTag("Floor"))
        {
            
            Destroy(this.gameObject);
        }
   
        Destroy(this.gameObject);
    }
}
