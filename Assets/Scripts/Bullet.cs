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

        var Medapart = other.GetComponent<MedaPartScript>();

        if(Medapart != null)
        {
            if (Medapart.playerX == 2 || Medapart.playerX == 1)
            {
                other.GetComponent<MedaPartScript>().ApplyDamage(damagePerBullet);
                Instantiate(this.impactVFX, this.transform.position, this.transform.rotation);
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
