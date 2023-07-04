using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class GiveLayers : MonoBehaviour
{
    private PlayerMedapartsController pmc;
    private GameObject parent;
    public int parentLayer;
    public bool assignToNestedChildren = false;
    void Start()
    {
        pmc=GetComponentInParent<PlayerMedapartsController>();
        parent=pmc.gameObject;
        parentLayer=parent.layer;
        int layerIndex = parentLayer;
      
            if (layerIndex != -1)
            {
            this.gameObject.layer = parentLayer;
            AssignLayerRecursively(transform, layerIndex);
        }
        else
        {
            Debug.LogError("Layer does not exist: " + parentLayer);
        }
    }

    void AssignLayerRecursively(Transform parent, int layer)
    {
        // Set layer for current game object
        parent.gameObject.layer = layer;

        // Iterate through all child objects and assign the layer recursively
        for (int i = 0; i < parent.childCount; i++)
        {
            Transform child = parent.GetChild(i);
            AssignLayerRecursively(child, layer);
        }
    }
}



   
   

