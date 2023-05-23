using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PartPiece : MonoBehaviour
{
    public int PieceNum;
    [SerializeField]
     Material loShader;
    [SerializeField]
     Material thisPeaceMaterial;
    public bool isLocked;
    [SerializeField]
    public UnityEvent LockOnSh;
    [SerializeField]
    public UnityEvent Base;
    private SkinnedMeshRenderer renderer1;

    private void Start()
    {
      renderer1 = GetComponent<SkinnedMeshRenderer>();
        
    }
    public void ChangeMaterial()
    {
        
        if (this.isLocked == false)
        {
          //  this.renderer1.material= thisPeaceMaterial;
            Base.Invoke();
        }
        if(this.isLocked == true)
        {
           // renderer1.material= loShader;
            LockOnSh.Invoke();
        }
    }

    
}
