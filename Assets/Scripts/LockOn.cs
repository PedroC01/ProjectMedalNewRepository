using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
//using static UnityEngine.InputSystem.InputAction;

public class LockOn : MonoBehaviour
{


    private List<GameObject> medapartsList = new List<GameObject>();
    [SerializeField]
    private GameObject[] medaparts;
    [SerializeField]
    private MedaPartScript[] medapartsScript;
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
    public MedaPartScript lockedOnMedapart;
    public int startPiece = 1;
    private RocketLaucher rl;
    private Shooter sh;
    public PlayerMedapartsController pmc;
   


    private void Awake()
    {
        this.pmc = GetComponent<PlayerMedapartsController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (this.gameObject.GetComponent<Player1>() == true)
        {
            this.Enemy = FindObjectOfType<Player2>().gameObject;

            medapartsScript = Enemy.GetComponentsInChildren<MedaPartScript>(false);


            foreach (MedaPartScript medap in medapartsScript)
            {
                GameObject temp = medap.gameObject;
                medapartsList.Add(temp);
            }

          
            enemyReference = 2;
            this.LOS = this.Enemy.GetComponent<LockOnShader>();

        }
        else if (this.gameObject.GetComponent<Player2>() == true)
        {
            this.Enemy = FindObjectOfType<Player1>().gameObject;

            medapartsScript = Enemy.GetComponentsInChildren<MedaPartScript>(false);


            foreach (MedaPartScript medap in medapartsScript)
            {
                GameObject temp = medap.gameObject;
                medapartsList.Add(temp);
            }

            enemyReference = 1;
            this.LOS = this.Enemy.GetComponent<LockOnShader>();

        }

        this.medaparts = this.medapartsList.ToArray();
        this.medapartsLock = new Transform[this.medaparts.Length];
        //Organiazar as peças na ordem certa pq vai ser necessario para selecionar onde queremos o LockOn
        for (int i = 0; i < this.medaparts.Length - 1; i++)
        {
            for (int j = i + 1; j < this.medaparts.Length; j++)
            {
                if (this.medaparts[i].GetComponent<MedaPartScript>().MedapartNumber > this.medaparts[j].GetComponent<MedaPartScript>().MedapartNumber)
                {
                    this.temp = this.medaparts[i];
                    this.medaparts[i] = this.medaparts[j];
                    this.medaparts[j] = this.temp;

                }

            }

        }
        //Agora guardar as transforms já na ordem certa ou guardar logo só a posição da target;
        for (int i = 0; i < medaparts.Length; i++)
        {
            this.medapartsLock[i] = medaparts[i].GetComponent<Transform>();
        }

        if (pmc.characterStatsSO.characterReferenceNumber == 1)
        {
            sh = GetComponentInChildren<Shooter>();
            sh.LO = this;
            sh.Enemy = this.Enemy;
            rl = GetComponentInChildren<RocketLaucher>();
            rl.LO = this;
        }





        /*
                for (int i = 0; i < medapartsLock.Length; i++)
                {
                    Debug.Log(i+"="+medapartsLock[i]);
                }*/


        this.lockOnTarget = medaparts[startPiece].transform;
        this.pieceReference = startPiece;

    }

    public void DPadUp()
    {
        if (medaparts[1].GetComponent<MedaPartScript>().partEnergy > 0)
        {
            lockOnTarget = medaparts[1].transform;
            this.pieceReference = 1;
            this.lockedOnMedapart = medaparts[1].GetComponent<MedaPartScript>();
        }
    }

    public void DPadLeft()
    {
        if (medaparts[2].GetComponent<MedaPartScript>().partEnergy > 0)
        {
            lockOnTarget = medaparts[2].transform;
            this.pieceReference = 2;
            this.lockedOnMedapart = medaparts[2].GetComponent<MedaPartScript>();
        }
    }
    public void DPadRight()
    {
        if (medaparts[3].GetComponent<MedaPartScript>().partEnergy > 0)
        {
            lockOnTarget = medaparts[3].transform;
            this.pieceReference = 3;
            this.lockedOnMedapart = medaparts[3].GetComponent<MedaPartScript>();
        }
    }
    public void DPadDown()
    {
        if (medaparts[4].GetComponent<MedaPartScript>().partEnergy > 0)
        {
            lockOnTarget = medaparts[4].transform;
            this.pieceReference = 4;
            this.lockedOnMedapart = medaparts[4].GetComponent<MedaPartScript>();
        }
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


        if (lockedOnMedapart != null && lockedOnMedapart.partEnergy <= 0 && medaparts[1] != null)
        {
            lockOnTarget = medaparts[1].transform;
            // Update the pieceReference to the head medapart's index
            this.pieceReference = 1;
            // Set the lockedOnMedapart to the head medapart
            this.lockedOnMedapart = medaparts[1].GetComponent<MedaPartScript>();
        }
    }








}


