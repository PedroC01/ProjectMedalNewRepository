using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemPBot : MonoBehaviour
{
    // Start is called before the first frame update
    public bool Attacked1;
    [SerializeField]
    public Shooter shooter;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Attacked1 = shooter.Attacked;
    }
}
