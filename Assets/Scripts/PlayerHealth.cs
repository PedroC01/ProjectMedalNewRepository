using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float playerHealth;
    public GameObject[] MedaParts;
    private List<GameObject> medapartsList = new List<GameObject>();
    private GameObject temp;
    public MedaPartScript[] medapartsScripts;
    private MedaPartScript mScript;
    public float partsTotalHealth;
    public GameObject Enemy;
   public float averageHealth;
    public GameObject HeadTorsoHealth;
    public float HeadHealth;
    public float lifePerToMedaforce=30f;
    public bool canGoBerserk=false;//Can use MedaForce
    public float LowEnergyMark=0;
    public float LowEnergyMarkHead= 35;
    private SoundManagerScript soundManager;
 //   private bool playedHeadCritDamageSound=false;
   // private bool playedLeftArmDestroyedSound=false;
    //private bool playedRightArmDestroyedSound=false;
    //private bool playedLegsDestroyedSound=false;
    private HashSet<int> destroyedParts = new HashSet<int>();
    public GameObject mbot;
    public PlayerMovements pm;
    // Start is called before the first frame update
    void Start()
    {
        
        soundManager = FindObjectOfType<SoundManagerScript>();
     
        if (this.gameObject.GetComponent<Player2>()!=null)
        {
            this.mbot = GetComponentInChildren<Medabot>().gameObject;
            medapartsScripts = mbot.GetComponentsInChildren<MedaPartScript>(false);


            foreach (MedaPartScript medap in medapartsScripts)
            {
                GameObject temp = medap.gameObject;
                medapartsList.Add(temp);
            }


        }
        else if (this.gameObject.GetComponent<Player1>()!=null)
        {
            this.mbot = GetComponentInChildren<Medabot>().gameObject;
            medapartsScripts = mbot.GetComponentsInChildren<MedaPartScript>(false);


            foreach (MedaPartScript medap in medapartsScripts)
            {
                GameObject temp = medap.gameObject;
                medapartsList.Add(temp);
            }

        }



        this.MedaParts = this.medapartsList.ToArray();
       

        for (int i = 0; i < this.MedaParts.Length - 1; i++)
        {
            for (int j = i + 1; j < this.MedaParts.Length; j++)
            {
                if (this.MedaParts[i].GetComponent<MedaPartScript>().MedapartNumber > this.MedaParts[j].GetComponent<MedaPartScript>().MedapartNumber)
                {
                    this.temp = this.MedaParts[i];
                    this.MedaParts[i] = this.MedaParts[j];
                    this.MedaParts[j] = this.temp;

                }

            }

        }
        
        //Find<MedaPartScript>(mScript);
        foreach (GameObject part in this.MedaParts)
        {
            mScript = part.GetComponent<MedaPartScript>();
            partsTotalHealth += part.GetComponent<MedaPartScript>().partEnergy;
            part.GetComponent<MedaPartScript>().medaparts = this.MedaParts;
            if (mScript.MedapartNumber == 1)
            {
                this.HeadTorsoHealth = part;
            }
        }
        this.pm = GetComponent<PlayerMovements>();
   
    }

    // Update is called once per frame
    void Update()
    {

        CheckLowEnergy();


        ////terminar o jogo
        HeadHealth = HeadTorsoHealth.GetComponent<MedaPartScript>().partEnergy;
        if (HeadHealth <= 0)
        {
            pm.canMove = false;
            pm.m_Animator1.SetBool("Lost",true); 
        }

        //ativar medaforce se a media da vida passar x-*-------------------------------------------verify
        averageHealth = partsTotalHealth / (MedaParts.Length);
        if (averageHealth < lifePerToMedaforce)
        {
            canGoBerserk = true;
        }
        
    }
    private void CheckLowEnergy()
    {
        foreach (MedaPartScript medapart in medapartsScripts)
        {
            if (medapart.MedapartNumber == 1 && medapart.partEnergy <= 35)
            {
                if (!destroyedParts.Contains(1))
                {
                    this.soundManager.PlayHeadCritDamageSound();
                    destroyedParts.Add(1);
                }
            }

            if (medapart.partEnergy <= LowEnergyMark)
            {
                // Perform actions for low energy Medaparts
                int partNumber = medapart.MedapartNumber;
                if (!destroyedParts.Contains(partNumber))
                {
                    switch (partNumber)
                    {
                        case 2:
                           this.soundManager.PlayLeftArmDestroyedSound();
                            break;

                        case 3:
                            this.soundManager.PlayRightArmDestroyedSound();
                            break;

                        case 4:
                            this.soundManager.PlayLegsDestroyedSound();
                            break;

                        default:
                            // Handle other medaparts if needed
                            break;
                    }

                    destroyedParts.Add(partNumber);
                }
            }
        }
    }

}
