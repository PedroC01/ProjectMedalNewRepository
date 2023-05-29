using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;


public class PartPiece : MonoBehaviour
{
   
    public int PieceNum;
    [SerializeField]
     Material loShader;
    [SerializeField]
     Material thisPeaceMaterial;
    public bool isLocked;
    [SerializeField]
    private Material[] ShaderMaterialsList;
    private Material[] originalMaterialsList;
    private SkinnedMeshRenderer renderer1;
    private LockOnShader lockOnShader;
    private void Start()
    {
      renderer1 = GetComponent<SkinnedMeshRenderer>();
        originalMaterialsList = this.renderer1.sharedMaterials;
    }
    
    void Update()
    {
        
        if (this.isLocked == false)
        {
            // this.renderer1.material= thisPeaceMaterial;
            renderer1.sharedMaterials = originalMaterialsList;
           // Base.Invoke();
        }
        if(this.isLocked == true)
        {
            renderer1.sharedMaterials= ShaderMaterialsList;
            // this.renderer1.material= loShader;
            // LockOnSh.Invoke();
        }
    }

    
}
