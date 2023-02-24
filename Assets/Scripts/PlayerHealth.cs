using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float playerHealth;
    public GameObject[] MedaParts;
    private MedaPartScript mScript;
    public float partsTotalHealth;
    public GameObject Enemy;
    public float eastDamage;
    public float northDamage;

    // Start is called before the first frame update
    void Start()
    {

        if (this.gameObject.GetComponent<Player2>()==true)
        {
            Enemy = FindObjectOfType<Player1>().gameObject;
        }

        MedaParts = GameObject.FindGameObjectsWithTag("Player2Parts");
            //Find<MedaPartScript>(mScript);
        foreach (var part in MedaParts)
        {
            mScript = GetComponent<MedaPartScript>();
            partsTotalHealth += part.GetComponent<MedaPartScript>().partEnergy;
        }
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
