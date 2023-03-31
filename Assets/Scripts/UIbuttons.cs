using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIbuttons : MonoBehaviour
{
    public int UINumber;
    [SerializeField]
    public Animator UiNorthAttack;
    public bool leftAnimation;
    public int timer;
    [SerializeField]
    public UnityEvent TimeZero;
    [SerializeField]
    public UnityEvent TimeActive;
    [SerializeField]
    public GameObject rechargeUi;
    public Shooter sh;
    [SerializeField]
    public TMP_Text currentTimeText;
    // Start is called before the first frame update
    void Start()
    {
        if (this.UINumber == 1)
        {

            sh = FindObjectOfType<Player1>().GetComponentInChildren<Shooter>();
        }
        if (this.UINumber == 2)
        {

            sh = FindObjectOfType<Player2>().GetComponentInChildren<Shooter>();
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (timer > 0)
        {
            UiNorthAttack.SetBool("ShootWest", true);
            this.TimeActive.Invoke();

        }

        if (timer <= 0)
        {
            UiNorthAttack.SetBool("ShootWest", false);

            this.TimeZero.Invoke();
        }




        timer = (int)sh.TimerForRechargeWest;
        this.currentTimeText.text = timer.ToString();


    }
}


