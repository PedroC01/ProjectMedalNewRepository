using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float playerHealth;
    public GameObject[] MedaParts;
    public MedaPartScript[] medapartsScripts;
    private MedaPartScript mScript;
    public float partsTotalHealth;
    public GameObject Enemy;
    float averageHealth;
    public GameObject HeadTorsoHealth;
    public float HeadHealth;
    public float lifePerToMedaforce;
    public bool canGoBerserk=false;//Can use MedaForce
    public float LowEnergyMark=0;
    public float LowEnergyMarkHead= 35;
    private SoundManagerScript soundManager;
 //   private bool playedHeadCritDamageSound=false;
   // private bool playedLeftArmDestroyedSound=false;
    //private bool playedRightArmDestroyedSound=false;
    //private bool playedLegsDestroyedSound=false;
    private HashSet<int> destroyedParts = new HashSet<int>();

    // Start is called before the first frame update
    void Start()
    {
        
        soundManager = FindObjectOfType<SoundManagerScript>();
        medapartsScripts = GetComponentsInChildren<MedaPartScript>();
        if (this.gameObject.GetComponent<Player2>()==true)
        {
            this.MedaParts = GameObject.FindGameObjectsWithTag("Player2Parts");
        }
        if (this.gameObject.GetComponent<Player1>() == true)
        {
            this.MedaParts = GameObject.FindGameObjectsWithTag("Player1Parts");
        }
        
        //Find<MedaPartScript>(mScript);
        foreach (GameObject part in this.MedaParts)
        {
            mScript = part.GetComponent<MedaPartScript>();
            partsTotalHealth += part.GetComponent<MedaPartScript>().partEnergy;
            if (mScript.MedapartNumber == 1)
            {
                this.HeadTorsoHealth = part;
            }
        }
    
    }

    // Update is called once per frame
    void Update()
    {

        CheckLowEnergy();


        ////terminar o jogo
        HeadHealth = HeadTorsoHealth.GetComponent<MedaPartScript>().partEnergy;
        if (HeadHealth <= 0)
        {

        }

        //ativar medaforce se a media da vida passar x
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
