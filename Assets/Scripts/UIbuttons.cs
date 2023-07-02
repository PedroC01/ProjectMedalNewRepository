using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIbuttons : MonoBehaviour
{
    public int UINumber;
    private Vector3 initialScale = new Vector3(1, 1, 1);
    public RectTransform coverRectTransformEast;
    public RectTransform coverRectTransformWest;
    public RectTransform coverRectTransformRocket; 
    public RectTransform coverRectTransformDash;
    public int timerEast; // Timer for recharge East
    public int timerWest; // Timer for recharge West
    public int timerRocket;
    public int timerDash;// Timer for recharge Rocket
    public float rechargeTimeEast; // Recharge time for East ability
    public float rechargeTimeWest; // Recharge time for West ability
    public float rechargeTimeRocket; // Recharge time for Rocket ability
    public float rechargeTimeDash = 1f;
    private Shooter sh;
    private RocketLaucher RL;
    private PlayerMovements pm;
    public float maxScale = 1f;
    public float minScale = 0f;
    [SerializeField]
    public TMP_Text eastTimeText; // TMP_Text for East ability time
    [SerializeField]
    public TMP_Text westTimeText; // TMP_Text for West ability time
    public TMP_Text rocketTimeText; // TMP_Text for Rocket ability time
    public TMP_Text dashTimeText;
    private float eastScale = 0f; // Current scale value for East ability
    private float westScale = 0f; // Current scale value for West ability
    private float rocketScale = 0f; // Current scale value for Rocket ability
    private float dashScale = 0f;
    private float eastScaleVelocity = 0f; // Velocity for smoothing East ability scale
    private float westScaleVelocity = 0f; // Velocity for smoothing West ability scale
    private float rocketScaleVelocity = 0f; // Velocity for smoothing Rocket ability scale
    private float dashScaleVelocity = 0f;

    [Header("Sounds")]
    public string attackReady;
    private FMOD.Studio.EventInstance attackReadySoundInstanceEast;
    private FMOD.Studio.EventInstance attackReadySoundInstanceWest;
    private FMOD.Studio.EventInstance attackReadySoundInstanceRocket;

    // Flag to track if the attack is ready
    private bool isAttackReadyA = false;
    private bool isAttackReadyB = false;
    private bool isAttackReadyC = false;
    private bool isDashReady = false;
    // Start is called before the first frame update
    void Start()
    {
        if (this.UINumber == 1)
        {
            sh = FindObjectOfType<Player1>().GetComponentInChildren<Shooter>();
            RL = FindObjectOfType<Player1>().GetComponentInChildren<RocketLaucher>();
            pm = FindObjectOfType<Player1>().GetComponent<PlayerMovements>();
        }
        if (this.UINumber == 2)
        {
            sh = FindObjectOfType<Player2>().GetComponentInChildren<Shooter>();
            RL = FindObjectOfType<Player2>().GetComponentInChildren<RocketLaucher>();
            pm = FindObjectOfType<Player2>().GetComponent<PlayerMovements>();
        }
        attackReadySoundInstanceEast = FMODUnity.RuntimeManager.CreateInstance(attackReady);
        attackReadySoundInstanceWest = FMODUnity.RuntimeManager.CreateInstance(attackReady);
        attackReadySoundInstanceRocket = FMODUnity.RuntimeManager.CreateInstance(attackReady);
    }

    // Update is called once per frame
    void Update()
    {
        float rechargePercentageEast = Mathf.Clamp01(this.sh.TimerForRechargeEast / rechargeTimeEast);
        float rechargePercentageWest = Mathf.Clamp01(this.sh.TimerForRechargeWest / rechargeTimeWest);
        float rechargePercentageRocket = Mathf.Clamp01(this.RL.TimerForRecharge / rechargeTimeRocket); // Get Rocket recharge percentage
        float rechargePercentDash = Mathf.Clamp01(this.pm.dashCoolDown / rechargeTimeDash);
        // Handle Recharge East
        if (rechargePercentageEast > 0f)
        {
            eastScale = Mathf.SmoothDamp(eastScale, maxScale, ref eastScaleVelocity, 0.2f);
            coverRectTransformEast.localScale = Vector3.Lerp(Vector3.zero, initialScale, eastScale);
            timerEast = (int)sh.TimerForRechargeEast; // Update the timer for recharge East
            eastTimeText.text = timerEast.ToString();
            eastTimeText.gameObject.SetActive(timerEast > 0);

            if (!isAttackReadyA && rechargePercentageEast >= 1f)
            {
                isAttackReadyA = true;
               // attackReadySoundInstanceEast.start();
            }
            
        }
        else
        {
            eastScale = Mathf.SmoothDamp(eastScale, minScale, ref eastScaleVelocity, 0.2f);
            coverRectTransformEast.localScale = Vector3.Lerp(Vector3.zero, initialScale, eastScale);

            if (sh.TimerForRechargeEast == 0f)
            {
                if (isAttackReadyA)
                {
                    attackReadySoundInstanceEast.start(); // Play the sound when attack becomes ready
                }
                isAttackReadyA = false;
                eastTimeText.gameObject.SetActive(timerEast > 0);
            }
        }

        // Handle Recharge West
        if (rechargePercentageWest > 0f)
        {
            westScale = Mathf.SmoothDamp(westScale, maxScale, ref westScaleVelocity, 0.2f);
            coverRectTransformWest.localScale = Vector3.Lerp(Vector3.zero, initialScale, westScale);
            timerWest = (int)sh.TimerForRechargeWest;
            westTimeText.text = timerWest.ToString();
            westTimeText.gameObject.SetActive(timerWest > 0);

            if (!isAttackReadyB && rechargePercentageWest >= 1f)
            {
                isAttackReadyB = true;
               // attackReadySoundInstanceWest.start();
            }
           
        }
        else
        {
            westScale = Mathf.SmoothDamp(westScale, minScale, ref westScaleVelocity, 0.2f);
            coverRectTransformWest.localScale = Vector3.Lerp(Vector3.zero, initialScale, westScale);

            if (sh.TimerForRechargeWest == 0f)
            {
                if (isAttackReadyB)
                {
                   attackReadySoundInstanceWest.start(); // Play the sound when attack becomes ready
                }
                isAttackReadyB = false;
                westTimeText.gameObject.SetActive(timerWest > 0);
            }
        }

        // Handle Recharge Rocket
        if (rechargePercentageRocket > 0f)
        {
            rocketScale = Mathf.SmoothDamp(rocketScale, maxScale, ref rocketScaleVelocity, 0.2f);
            coverRectTransformRocket.localScale = Vector3.Lerp(Vector3.zero, initialScale, rocketScale);
            timerRocket = (int)RL.TimerForRecharge;
            rocketTimeText.text = timerRocket.ToString();
            rocketTimeText.gameObject.SetActive(timerRocket > 0);

            if (!isAttackReadyC && rechargePercentageRocket >= 1f)
            {
                isAttackReadyC = true;
               // attackReadySoundInstanceRocket.start();
            }
           
        }
        else
        {
            rocketScale = Mathf.SmoothDamp(rocketScale, minScale, ref rocketScaleVelocity, 0.2f);
            coverRectTransformRocket.localScale = Vector3.Lerp(Vector3.zero, initialScale, rocketScale);

            if (RL.TimerForRecharge == 0f)
            {
                if (isAttackReadyC)
                {
                    attackReadySoundInstanceRocket.start(); // Play the sound when attack becomes ready
                }
                isAttackReadyC = false;
                rocketTimeText.gameObject.SetActive(timerRocket > 0);
            }
        }


        //change this to dash
        if (rechargePercentDash > 0f)
        {
            dashScale = Mathf.SmoothDamp(dashScale, maxScale, ref dashScaleVelocity, 0.2f);
            coverRectTransformDash.localScale = Vector3.Lerp(Vector3.zero, initialScale, dashScale);
            timerDash = (int)pm.dashCoolDown;
            dashTimeText.text = timerDash.ToString();
            dashTimeText.gameObject.SetActive(timerDash > 0);

            if (!isDashReady && rechargePercentDash >= 1f)
            {
                isDashReady = true;
                
            }

        }
        else
        {
            dashScale = Mathf.SmoothDamp(dashScale, minScale, ref dashScaleVelocity, 0.2f);
            coverRectTransformDash.localScale = Vector3.Lerp(Vector3.zero, initialScale, dashScale);

            if (pm.dashCoolDown == 0f)
            {
                if (isDashReady)
                {
                                                    // Play the sound when dash becomes ready
                }
                isDashReady = false;
               dashTimeText.gameObject.SetActive(timerDash > 0);
            }
        }
    }
}


