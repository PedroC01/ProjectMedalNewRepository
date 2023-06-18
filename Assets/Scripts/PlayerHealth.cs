using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float playerHealth;
    public GameObject[] MedaParts;
    private MedaPartScript mScript;
    public float partsTotalHealth;
    public GameObject Enemy;
    float averageHealth;
    public GameObject HeadTorsoHealth;
    public float HeadHealth;
    public float lifePerToMedaforce;
    public bool canGoBerserk=false;//Can use MedaForce
    // Start is called before the first frame update
    void Start()
    {

        if (this.gameObject.GetComponent<Player2>()==true)
        {
            this.MedaParts = GameObject.FindGameObjectsWithTag("Player2Parts");
        }
        if (this.gameObject.GetComponent<Player1>() == true)
        {
            this.MedaParts = GameObject.FindGameObjectsWithTag("Player1Parts");
        }

        //Find<MedaPartScript>(mScript);
        foreach (var part in this.MedaParts)
        {
            mScript = GetComponent<MedaPartScript>();
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
}
