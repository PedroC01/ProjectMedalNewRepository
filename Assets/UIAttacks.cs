using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class UIAttacks : MonoBehaviour
{
    // Start is called before the first frame update
    public RocketLaucher RL;
    //UI Animators--------------------------------------
    [SerializeField]
    public Animator UiNorthAttack;
    [SerializeField]
    public TMP_Text currentTimeText;

    private PlayerMovements pMovement;
    public int UINumber;
    public int timer;
    [SerializeField]
    public GameObject rechargeUi;
    [SerializeField]
    public UnityEvent TimeZero;
    [SerializeField]
    public UnityEvent TimeActive;


    void Start()
    {
        if(this.UINumber == 1)
        {
            RL=FindObjectOfType<Player1>().GetComponentInChildren<RocketLaucher>();
            pMovement= FindObjectOfType<Player1>().GetComponentInChildren<PlayerMovements>();
        }
        if (this.UINumber == 2)
        {
            RL = FindObjectOfType<Player2>().GetComponentInChildren<RocketLaucher>();
            pMovement = FindObjectOfType<Player2>().GetComponentInChildren<PlayerMovements>();
        }
    }

    // Update is called once per frame
    void Update()
    {
      
        if (timer>0)
            {
            UiNorthAttack.SetBool("ShootUp", true);
            TimeActive.Invoke();
           // rechargeUi.transform.localScale = new Vector3(timer, timer, timer);
        }
           

        if (RL.TimerForRecharge <= 0)
        {
          UiNorthAttack.SetBool("ShootUp", false);
             
          TimeZero.Invoke();
        }

        timer = (int)RL.TimerForRecharge;
        currentTimeText.text = timer.ToString();



    }
}
