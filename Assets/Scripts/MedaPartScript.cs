using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class MedaPartScript : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("0=Body,1=Head,2=LeftArm,3=RightArm,4=legs")]
    public int MedapartNumber;
    public float partEnergy = 100;
    private float partEnergyInitial;
    public float defense;
    public float damage;
    public MedaHealthSlider healthSlider;
    public Animator animatorUI;
    public int playerX;
    private PlayerMovements PM;
    private PlayerMedapartsController Pmc;
    private bool isBeingTargeted;
    private float damageReductionPercentage;
    private float reducedDamage;
    public GameObject[] medaparts;
    private void Start()
    {
        if (GetComponentInParent<Player2>() != null)
        {
            animatorUI = GameObject.FindGameObjectWithTag("UIPlayer2").GetComponent<Animator>();
            PM = GetComponentInParent<PlayerMovements>();
            playerX = 2;
        }
        else if (GetComponentInParent<Player1>() != null)
        {
            animatorUI = GameObject.FindGameObjectWithTag("UIPlayer1").GetComponent<Animator>();
            PM = GetComponentInParent<PlayerMovements>();
            playerX = 1;
        }
       
        partEnergyInitial = partEnergy;
    }

    public void SetTargeted(bool targeted)
    {
        isBeingTargeted = targeted;
        if (isBeingTargeted)
        {
         
        }
        else
        {
            // Medapart is not being targeted, perform necessary actions
        }
        // Implement behavior when the medapart is targeted
    }

    public void ApplyDamage(float damage)
    {
        if (!this.PM.isInvencible)
        {
            if (this.PM.block == true)
            {
                if ((this.MedapartNumber == 2 || this.MedapartNumber == 3) && !isBeingTargeted)
                {
                    damageReductionPercentage = Mathf.Clamp(defense, 0f, 100f);
                    reducedDamage = damage * (damageReductionPercentage / 100f);
                } 
                else if ((this.MedapartNumber == 2 || this.MedapartNumber == 3) && isBeingTargeted) 
                {
                    damageReductionPercentage = Mathf.Clamp(defense, 0f, 100f);
                    reducedDamage = damage * (damageReductionPercentage / 100f);
                    medaparts[2].GetComponent<MedaPartScript>().ApplyDamage(reducedDamage / 2);
                    medaparts[3].GetComponent<MedaPartScript>().ApplyDamage(reducedDamage / 2);
                }
                else
                {
                    damage = 0;
                }


            }

            damageReductionPercentage = Mathf.Clamp(defense, 0f, 100f);
            reducedDamage = damage * (damageReductionPercentage / 100f);

            this.partEnergy = Mathf.Clamp(this.partEnergy - reducedDamage, 0f, partEnergyInitial);

        }
        else
        {
            return;
        }

        return;

    }
}
