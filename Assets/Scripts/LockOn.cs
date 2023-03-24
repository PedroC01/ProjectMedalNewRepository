using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
//using static UnityEngine.InputSystem.InputAction;

public class LockOn : MonoBehaviour
{


  
    [SerializeField]
    private GameObject[] medaparts;
    private Transform[] medapartsLock;
    private GameObject temp;
    public Transform lockOnTarget;
    private Vector3 targetDir;
    public int getNewTarget;
    public bool Locked;
    private Vector3 horizontalInput = Vector2.zero;
    public GameObject Target3D;
    public GameObject Enemy;
    // Start is called before the first frame update
    void Start()
    {
        if (this.gameObject.GetComponent<Player1>()==true)
        {
            medaparts = GameObject.FindGameObjectsWithTag("Player2Parts");
            this.Enemy = FindObjectOfType<Player2>().gameObject;

        }
        


        //quando criar as peças direitas para o player 1 descomentar a baixo--------------------------------
        if (this.gameObject.GetComponent<Player2>() == true)
        {
            medaparts = GameObject.FindGameObjectsWithTag("Player1Parts");
           this.Enemy = FindObjectOfType<Player1>().gameObject;
        }
        //------------------------------

        medapartsLock = new Transform[medaparts.Length];

        //Organiazar as peças na ordem certa pq vai ser necessario para selecionar onde queremos o LockOn
        for (int i=0; i < medaparts.Length-1; i++)
        {
            for(int j=i+1; j < medaparts.Length; j++)
            {
               if(medaparts[i].GetComponent<MedaPartScript>().MedapartNumber> medaparts[j].GetComponent<MedaPartScript>().MedapartNumber)
                {
                    temp= medaparts[i];
                    medaparts[i] = medaparts[j];
                    medaparts[j]=temp;
                   
                }

            }
          
        }
        //Agora guardar as transforms já na ordem certa ou guardar logo só a posição da target;
        for (int i = 0; i < medaparts.Length; i++)
        {
            medapartsLock[i] = medaparts[i].GetComponent<Transform>();
        }


        for (int i = 0; i < medapartsLock.Length; i++)
        {
            Debug.Log(i+"="+medapartsLock[i]);
        }

       
        lockOnTarget = medapartsLock[0];


    }

    public void DPadUp()
    {
            lockOnTarget = medapartsLock[1];
      
    }

    public void DPadLeft()
    {
        lockOnTarget = medaparts[2].transform;
    }
    public void DPadRight()
    {
        lockOnTarget = medaparts[3].transform;
    }
    public void DPadDown()
    {
        lockOnTarget = medaparts[4].transform;
    }

    public void LeftShoulderL1()
    {
        if (Locked == true)
        {
            Locked = false;
        }
        else
        {
            Locked = true;
        }
    }

    // Update is called once per frame
    void Update()
    {

        targetDir = lockOnTarget.position;
        Target3D.transform.position = targetDir;
    }




   
    




}
