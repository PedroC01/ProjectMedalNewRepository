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
    public List<PartPiece> singlePartpieces;
    public GameObject Enemy;
   

    // Start is called before the first frame update
    void Start()
    {
        if (this.gameObject.GetComponentInParent<Player2>() == true)
        {
            this.Enemy = FindObjectOfType<Player1>().gameObject;
            this.LO=this.Enemy.GetComponent<LockOn>();
            this.thisPartPieces = GameObject.FindGameObjectsWithTag("Player2PartPiece");
        
        }
        if (this.gameObject.GetComponentInParent<Player1>() == true)
        {
            this.Enemy = FindObjectOfType<Player2>().gameObject;
            this.LO=this.Enemy.GetComponent<LockOn>();
            this.thisPartPieces = GameObject.FindGameObjectsWithTag("Player1PartPiece");
       
         
        }
       
           foreach(PartPiece ppiece in this.gameObject.GetComponentsInChildren<PartPiece>())
            {

                ppiece.PieceNum = this.thisPieceNum;

            }
        


      
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




