using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnEnableOnDisable : MonoBehaviour
{
    private bool canStart=false;
    public bool Enable;
    [SerializeField]
    private UnityEvent ONEnableEVnt;
    [SerializeField] 
    private UnityEvent ONDisableEVnt;
    public float Timer;
    private float TimerMaX;
    // Start is called before the first frame update
    void Start()
    {
        canStart = true;
        TimerMaX = Timer;
    }

    // Update is called once per frame
    void Update()
    {
        if(canStart)
        {
            this.Timer = Mathf.Clamp(this.Timer - Time.deltaTime, 0, TimerMaX);
            if (Timer <= 0)
            {
                if (!Enable)
                {
                    ONDisableEVnt.Invoke();
                }
                else
                {
                    ONEnableEVnt.Invoke();
                }

            }
        }
        
    }
}
