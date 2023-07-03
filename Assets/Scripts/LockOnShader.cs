using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static UnityEditor.Experimental.GraphView.GraphView;

public class LockOnShader : MonoBehaviour
{

    
    private LockOn LO;
    public int thisPieceNum;
    public int thisLockOnNumA;
    public int thisLockOnNum;
    private GameObject thisMedapartParent;
    public GameObject[] thisPartPieces;
    private LockOnShader[] LShaderScripts;
    private List<GameObject> thisPartList;
    public List<PartPiece> singlePartpieces;
    public GameObject Enemy;
    [SerializeField]
    public Material thisPiecesLockOnShader;
    public GameObject medabot;

    // Start is called before the first frame update
    void Awake()
    {
       
        if (this.gameObject.GetComponentInParent<Player2>() == true)
        {
            this.thisPartList = new List<GameObject>();
            this.medabot=GetComponentInParent<Medabot>().gameObject;
            this.Enemy = FindObjectOfType<Player1>().gameObject;
            this.LO=this.Enemy.GetComponent<LockOn>();
            LShaderScripts = this.medabot.GetComponentsInChildren<LockOnShader>(false);
            foreach (LockOnShader ls in LShaderScripts)
            {
                GameObject temp = ls.gameObject;
                thisPartList.Add(temp);
            }
        }
        if (this.gameObject.GetComponentInParent<Player1>() == true)
        {
            this.thisPartList = new List<GameObject>();
            this.medabot = GetComponentInParent<Medabot>().gameObject;
            this.Enemy = FindObjectOfType<Player2>().gameObject;
            this.LO=this.Enemy.GetComponent<LockOn>();
            LShaderScripts = this.medabot.GetComponentsInChildren<LockOnShader>(false);
            foreach (LockOnShader ls in LShaderScripts)
            {
                GameObject temp = ls.gameObject;
                thisPartList.Add(temp);
            }
      
        }
       this.thisPartPieces=this.thisPartList.ToArray();

           foreach(PartPiece ppiece in this.gameObject.GetComponentsInChildren<PartPiece>())
            {
            ppiece.lShader = this;
            ppiece.PieceNum = this.thisPieceNum;
            ppiece.loShader=this.thisPiecesLockOnShader;
               
             }
        


      
        

    }
    void Start()
    {
        thisLockOnNum = LO.pieceReference;
        ChangePieceEffect();
    }
    // Update is called once per frame
    void Update()
    {
        
        if (thisLockOnNum != LO.pieceReference)
        {
            thisLockOnNum = LO.pieceReference;
            ChangePieceEffect();
        }
    }



    public void ChangePieceEffect()
    {
       
       
        foreach (GameObject piece in thisPartPieces)
        {
            foreach (PartPiece ppiece in piece.GetComponentsInChildren<PartPiece>())
            {

                if (ppiece.PieceNum==thisLockOnNum)
                {
                    ppiece.isLocked = true;
                }
                if (ppiece.PieceNum != thisLockOnNum)
                {
                    ppiece.isLocked = false;
                }

            }


       //piece.GetComponentInChildren<PartPiece>().ChangeMaterial();

       }

      
    }


 }




