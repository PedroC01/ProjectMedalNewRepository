using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnTime : MonoBehaviour
{
    public float TimeToAutoDestroy;
   
    void Update()
    {
        if (0 < TimeToAutoDestroy)
        {
            TimeToAutoDestroy-= Time.deltaTime;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
