using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class HelperMenuCall : MonoBehaviour
{
    public string targetNames = "PlayerPrefabMedabots(Clone)";

    private void Awake()
    {
        

               GameObject[] objects = GameObject.FindObjectsOfType<GameObject>();

        var filteredObjects = objects.Where(obj => targetNames.Contains(obj.name));

        foreach (var obj in filteredObjects)
        {
            obj.GetComponent<PlayerInputHandler>().enabled = true;
        }
      
    }
}
