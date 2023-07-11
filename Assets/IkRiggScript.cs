using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class IkRiggScript : MonoBehaviour
{
    public GameObject aimTarget;
    public RigBuilder RB;
    public Rig Rig;
    // Start is called before the first frame update
    void Start()
    {
        if (GetComponentInParent<Player1>() == true)
        {
            aimTarget = FindObjectOfType<Player1Aim>().gameObject;

        }
        else if (GetComponentInParent<Player2>() == true)
        {
            aimTarget = FindObjectOfType<Player2Aim>().gameObject;

        }
        var sourceObject = gameObject.GetComponent<MultiAimConstraint>().data.sourceObjects;
        sourceObject.SetTransform(0, aimTarget.transform);
        this.gameObject.GetComponent<MultiAimConstraint>().data.sourceObjects = sourceObject;
        RB=GetComponentInParent<RigBuilder>();
        RB.Build();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
