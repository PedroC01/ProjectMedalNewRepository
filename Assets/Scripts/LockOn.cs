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
    public GameObject Target3D1;
    public GameObject Target3D2;
    public GameObject Enemy;
    public LockOnShader LOS;
    private int enemyReference;
    public int pieceReference;
        // Start is called before the first frame update
    void Start()
    {
        if (this.gameObject.GetComponent<Player1>()==true)
        {
           this.medaparts = GameObject.FindGameObjectsWithTag("Player2Parts");
            this.Enemy = FindObjectOfType<Player2>().gameObject;
            enemyReference = 2;
            this.LOS=this.Enemy.GetComponent<LockOnShader>();
        }
        


        //quando criar as peças direitas para o player 1 descomentar a baixo--------------------------------
        if (this.gameObject.GetComponent<Player2>() == true)
        {
           this.medaparts = GameObject.FindGameObjectsWithTag("Player1Parts");
           this.Enemy = FindObjectOfType<Player1>().gameObject;
            enemyReference = 1;
            this.LOS = this.Enemy.GetComponent<LockOnShader>();
        }
        //------------------------------

        this.medapartsLock = new Transform[this.medaparts.Length];

        //Organiazar as peças na ordem certa pq vai ser necessario para selecionar onde queremos o LockOn
        for (int i=0; i < this.medaparts.Length-1; i++)
        {
            for(int j=i+1; j < this.medaparts.Length; j++)
            {
               if(this.medaparts[i].GetComponent<MedaPartScript>().MedapartNumber > this.medaparts[j].GetComponent<MedaPartScript>().MedapartNumber)
                {
                    this.temp= this.medaparts[i];
                    this.medaparts[i] = this.medaparts[j];
                    this.medaparts[j]=this.temp;
                   
                }

            }
          
        }
        //Agora guardar as transforms já na ordem certa ou guardar logo só a posição da target;
        for (int i = 0; i < medaparts.Length; i++)
        {
            this.medapartsLock[i] = medaparts[i].GetComponent<Transform>();
        }

/*
        for (int i = 0; i < medapartsLock.Length; i++)
        {
            Debug.Log(i+"="+medapartsLock[i]);
        }*/

       
        this.lockOnTarget = medaparts[0].transform;
        this.pieceReference = 0;

    }

    public void DPadUp()
    {
        lockOnTarget = medaparts[1].transform;
        this.pieceReference = 1;
      
    }

    public void DPadLeft()
    {
        lockOnTarget = medaparts[2].transform;
        this.pieceReference = 2;

    }
    public void DPadRight()
    {
        lockOnTarget = medaparts[3].transform;
        this.pieceReference = 3;
  
    }
    public void DPadDown()
    {
        lockOnTarget = medaparts[4].transform;
        this.pieceReference = 4;
   
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
        if (this.enemyReference == 1)
        {
            Target3D1.transform.position = targetDir;
        }
        if (this.enemyReference == 2)
        {
            Target3D2.transform.position = targetDir;
        }

    }




   
    




}
