using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static UnityEditor.Experimental.GraphView.GraphView;

public class LockOnShader : MonoBehaviour
{

    
    private LockOn LO;
    public int thisPieceNum;
    public static int thisLockOnNum;
    private GameObject thisMedapartParent;
    public GameObject[] thisPartPieces;
    new GameObject[] singlePartpieces;
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
        foreach (GameObject piece in thisPartPieces)
        {
            if (piece.GetComponentInChildren<PartPiece>())
            {

                piece.GetComponentInChildren<PartPiece>().PieceNum= thisPieceNum;

            }

        }
        thisLockOnNum = LO.pieceReference;
        ChangePieceEffect();

    }
    // Update is called once per frame
    void Update()
    {
        
    }



    public void ChangePieceEffect()
    {
        thisLockOnNum = LO.pieceReference;
        foreach (GameObject piece in thisPartPieces)
        {
            if (thisLockOnNum == thisPieceNum)
            {
                piece.GetComponentInChildren<PartPiece>().isLocked = true;
            }
            else
            {
                piece.GetComponentInChildren<PartPiece>().isLocked = false;
            }
        
       piece.GetComponentInChildren<PartPiece>().ChangeMaterial();

       }
         

    }


 }




