using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class MedaPartScript : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("0=Body,1=Head,2=LeftArm,3=RightArm,4=legs")]
    public int MedapartNumber;
    public float partEnergy=20;
    private float partEnergyInitial;
    public float defense;
    public float Damage;
    public MedaHealthSlider healthSlider;
    public Animator animator_UI;
    [Header("1=player_1,2 player_2, Filled Auto")]
    public int playerX;
    public int mdpart;
    private PlayerMovements PM;
    void Start()
    {
      //  healthSlider.SetEnergy();
        if (this.gameObject.GetComponentInParent<Player2>()==true)
        {
            animator_UI = GameObject.FindGameObjectWithTag("UIPlayer2").GetComponent<Animator>();
            PM = GetComponentInParent<PlayerMovements>();
            playerX = 2;
        }
        if (this.gameObject.GetComponentInParent<Player1>() == true)
        {
            animator_UI = GameObject.FindGameObjectWithTag("UIPlayer1").GetComponent<Animator>();
            PM = GetComponentInParent<PlayerMovements>();
            playerX = 1;
        }
        mdpart = this.MedapartNumber;
        this.partEnergyInitial = this.partEnergy;
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    //void OnCollisionEnter(Collision collision)
    //{
     
    //    if (collision.collider.CompareTag("Player1Bullet"))
    //    {
    //        Debug.Log("hitByPlayer1Bullet");
    //        this.partEnergy += this.Damage;
    //      //  healthSlider.SetEnergy();
      
    //            animator_UI.SetTrigger(this.mdpart.ToString());
            
           
    //    }
    //}
    void OnTriggerEnter(Collider other)
    {
     /*   if (playerX == 2)
        {
            if (other.CompareTag("Player1Bullet"))
            {

                this.partEnergy += this.Damage;
                //  healthSlider.SetEnergy();

                animator_UI.SetTrigger(this.mdpart.ToString());
                Destroy(other.gameObject);

            }
        }
        if (playerX == 1)
        {
            if (other.CompareTag("Player2Bullet"))
            {

                this.partEnergy += this.Damage;
                //  healthSlider.SetEnergy();

                animator_UI.SetTrigger(this.mdpart.ToString());
                Destroy(other.gameObject);

            }
        }*/
    }
     public void ApplyDamage(float damage)
    {
        if (PM.block == true)
        {
            if (this.MedapartNumber == 2 || this.MedapartNumber == 3)
            {
                damage = ((damage/partEnergyInitial)*100) - ((defense/damage)*100);
            }
            else
            {
                damage = 0;
            }
            

        }

        this.partEnergy = Mathf.Clamp(this.partEnergy -= ((damage / partEnergyInitial) * 100) - ((defense / damage) * 100), 0, partEnergyInitial);
      




       return;

    }
}
