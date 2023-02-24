using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody rb;
    public float bulletSpeed;
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<MedaPartScript>().playerX == 2)
        {
            collision.collider.GetComponent<MedaPartScript>().Damage = collision.collider.GetComponent<MedaPartScript>().Damage-2;
            Destroy(this.gameObject);
        }
        Destroy(this.gameObject);
      //  this.bulletSpeed = 0;
       // this.rb.velocity = Vector3.zero;
    }
}
