using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering;

public class PartPiece : MonoBehaviour
{
   
    public int PieceNum;
    [SerializeField]
     public Material loShader;
    [SerializeField]
     Material thisPeaceMaterial;
    public bool isLocked;
    [SerializeField]
    private Material[] ShaderMaterialsList;
    [SerializeField]
    public Material[] destroyedMaterialList;
    public Material[] originalMaterialsList;
    private SkinnedMeshRenderer renderer1;
    public LockOnShader lShader;
    public Texture[] thisPieceTexture;
    private Renderer ShaderRenderer;
    public Material[] NewMaterialsList;
    public PlayerMedapartsController pmc;
    [SerializeField]
    public Material destroyedMaterial;
    public bool destroyed;
    private void Start()
    {
       pmc=GetComponentInParent<PlayerMedapartsController>();
         renderer1 = GetComponent<SkinnedMeshRenderer>();
        originalMaterialsList = this.renderer1.sharedMaterials;
        thisPieceTexture = new Texture[originalMaterialsList.Length];
        NewMaterialsList = new Material[originalMaterialsList.Length];
        destroyedMaterialList= new Material[originalMaterialsList.Length];
        for (int i = 0; i <= originalMaterialsList.Length - 1; i++)
        {
            this.thisPieceTexture[i] = originalMaterialsList[i].mainTexture;
           //this.ShaderMaterialsList[i].SetTexture("_Texture", this.thisPieceTexture[i]);
            Material newMaterial = new Material(this.loShader);
            newMaterial.SetTexture("_Texture", this.thisPieceTexture[i]);
            NewMaterialsList[i] = newMaterial;
        }
        for (int i = 0; i <= originalMaterialsList.Length - 1; i++)
        {
            this.thisPieceTexture[i] = originalMaterialsList[i].mainTexture;
          //  this.ShaderMaterialsList[i].SetTexture("_Texture", this.thisPieceTexture[i]);
            Material newEletric = new Material(this.destroyedMaterial);
            newEletric.SetTexture("_FirstTexture", this.thisPieceTexture[i]);
            destroyedMaterialList[i] = newEletric;
        }
    }
 
    void Update()
    {
        /* 
         if (this.isLocked == false)
         {
             // this.renderer1.material= thisPeaceMaterial;
             renderer1.sharedMaterials = originalMaterialsList;
            // Base.Invoke();
         }
         if(this.isLocked == true)
         {
             // ShaderMaterialsList[1].SetTexture
             for (int i = 0; i <= originalMaterialsList.Length - 1; i++)
             {
                 this.NewMaterialsList[i].SetTexture("_Texture", this.thisPieceTexture[i]);
             }
             renderer1.sharedMaterials= NewMaterialsList;

             // this.renderer1.material= loShader;
             // LockOnSh.Invoke();
         }*/
        if (this.destroyed == true)
        {
            for (int i = 0; i <= originalMaterialsList.Length - 1; i++)
            {
                this.destroyedMaterialList[i].SetTexture("_FirstTexture", this.thisPieceTexture[i]);
            }
            this.renderer1.sharedMaterials = this.destroyedMaterialList;

        }

        if (pmc.MedaForceActive == false&&destroyed==false)
        {
            // this.renderer1.material= thisPeaceMaterial;
            renderer1.sharedMaterials = originalMaterialsList;
            // Base.Invoke();
        }
        if (pmc.MedaForceActive == true)
        {
            // ShaderMaterialsList[1].SetTexture
            for (int i = 0; i <= originalMaterialsList.Length - 1; i++)
            {
                this.NewMaterialsList[i].SetTexture("_Texture", this.thisPieceTexture[i]);
            }
            this.renderer1.sharedMaterials = this.NewMaterialsList;

            // this.renderer1.material= loShader;
            // LockOnSh.Invoke();
        }
    }


}
