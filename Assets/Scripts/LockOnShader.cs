using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UnityEditor.Experimental.GraphView.GraphView;

public class LockOnShader : MonoBehaviour
{

    [SerializeField]
    public UnityEvent LockOnSh;
    private LockOn LO;
    public int thisPieceNum;
    public int thisLockOnNum;
    private GameObject thisMedapartParent;
    public GameObject[] thisPartPieces;
    public GameObject Enemy;
    public Material ShaderMaterial;
    // Start is called before the first frame update
    void Start()
    {
        if (this.gameObject.GetComponentInParent<Player2>() == true)
        {
            this.Enemy = FindObjectOfType<Player1>().gameObject;
            this.Enemy.GetComponent<LockOn>();
            this.thisPartPieces = GameObject.FindGameObjectsWithTag("Player2PartPiece");
        
        }
        if (this.gameObject.GetComponentInParent<Player1>() == true)
        {
            this.Enemy = FindObjectOfType<Player2>().gameObject;
            this.Enemy.GetComponent<LockOn>();
            this.thisPartPieces = GameObject.FindGameObjectsWithTag("Player1PartPiece");
       
         
        }
        foreach (GameObject piece in thisPartPieces)
        {
            if (piece.GetComponentInChildren<PartPiece>())
            {

                piece.GetComponentInChildren<PartPiece>().PieceNum= thisPieceNum;

            }

        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }



    public void ChangePieceEffect()
    {
        this.thisLockOnNum = LO.pieceReference;
        foreach (GameObject piece in thisPartPieces)
        {
            if (piece.GetComponent<PartPiece>().PieceNum==thisLockOnNum)
            {

                piece.GetComponentInChildren<SkinnedMeshRenderer>().material = ShaderMaterial;

            }
            else
            {
           //  piece.GetComponentInChildren<SkinnedMeshRenderer>().material.;
            }

        }
    }



}
