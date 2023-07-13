using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class OverrideInput:MonoBehaviour
{
    public PlayerInputHandler PIH;
    public PlayerMedapartsController MpC;
    public Shooter sh;
    public RocketLaucher rl;
    public Metabee metabee;
    public int characterReference;
    private void Awake()
    {
        MpC = GetComponent<PlayerMedapartsController>();
    }
    void Start()
    {
        if (MpC.characterStatsSO.characterReferenceNumber == 1)
        {
            this.sh = this.GetComponentInChildren<Shooter>();
            this.rl = GetComponentInChildren<RocketLaucher>();
            metabee = this.GetComponent<Metabee>();

            if (metabee == null)
            {
                metabee = this.gameObject.AddComponent<Metabee>();
                characterReference = MpC.characterStatsSO.characterReferenceNumber;
            }

        }
    }
    public virtual void OnEast()
    {


        if (characterReference == 1)
        {
            metabee.OnEast();
        }
               
          
    }
    public virtual void OnEastRelease()
    {
        if (characterReference == 1)
        {
            metabee.OnEastRelease();
        }


    }
    public virtual void OnWest()
    {
        if (characterReference == 1)
        {
            metabee.OnWest();
        }
    }
    public virtual void OnWestRelease()
    {
        if (characterReference == 1)
        {
            metabee.OnWestRelease();
        }
    }
    public virtual void OnNorth()
    {
        if (characterReference == 1)
        {
            metabee.OnNorth();
        }
    }
}


public class Metabee : OverrideInput
{

    public override void OnEast()
    {
        sh.East(); 
    }
    public override void OnEastRelease()
    {
        sh.EastRelease();
    }
    public override void OnWest()
    {
        sh.West();
    }
    public override void OnWestRelease()
    {
        sh.WestRelease();
    }

    public override void OnNorth()
    {
        rl.North();

    }


}