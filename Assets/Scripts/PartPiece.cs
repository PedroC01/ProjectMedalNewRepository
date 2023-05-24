using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PartPiece : MonoBehaviour
{
   
    public int PieceNum;
    [SerializeField]
     Material loShader;
    [SerializeField]
     Material thisPeaceMaterial;
    public bool isLocked;
   
    private SkinnedMeshRenderer renderer1;
    private LockOnShader lockOnShader;
    private void Start()
    {
      renderer1 = GetComponent<SkinnedMeshRenderer>();
       
    }
    
    void Update()
    {
        
        if (this.isLocked == false)
        {
            this.renderer1.material= thisPeaceMaterial;
           // Base.Invoke();
        }
        if(this.isLocked == true)
        {
            this.renderer1.material= loShader;
           // LockOnSh.Invoke();
        }
    }

    
}
