using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIbuttons : MonoBehaviour
{
    public int UINumber;
    public GameObject coverObjectEast;
    public GameObject coverObjectWest;
    public GameObject coverObjectRocket;
    public GameObject coverObjectDash;
    public int timerEast; // Timer for recharge East
    public int timerWest; // Timer for recharge West
    public int timerRocket;
    public int timerDash; // Timer for recharge Rocket
    public float rechargeTimeEast; // Recharge time for East ability
    public float rechargeTimeWest; // Recharge time for West ability
    public float rechargeTimeRocket; // Recharge time for Rocket ability
    public float rechargeTimeDash = 1f;
    private Shooter sh;
    private RocketLaucher RL;
    private PlayerMovements pm;
    [SerializeField]
    public TMP_Text eastTimeText; // TMP_Text for East ability time
    [SerializeField]
    public TMP_Text westTimeText; // TMP_Text for West ability time
    public TMP_Text rocketTimeText; // TMP_Text for Rocket ability time
    public TMP_Text dashTimeText;
    public TMP_Text SmgBulletsText;
    public TMP_Text RevolverBulletsText;
    private float eastRechargePercentage = 0f; // Current recharge percentage for East ability
    private float westRechargePercentage = 0f; // Current recharge percentage for West ability
    private float rocketRechargePercentage = 0f; // Current recharge percentage for Rocket ability
    private float dashRechargePercentage = 0f;
    public Slider eastSlider; // Slider for East ability
    public Slider westSlider; // Slider for West ability
    public Slider rocketSlider; // Slider for Rocket ability
    public Slider dashSlider; // Slider for Dash ability

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

        SmgBulletsText.text=sh.magSizeFullAuto.ToString()+"/"+sh.MaxMagFullAuto.ToString();
        RevolverBulletsText.text = sh.bulletsInMagazineRev.ToString() + "/" + sh.maxMagazineSizeRevolver.ToString();

    }

    // Update is called once per frame
    void Update()
    {
        // Handle Recharge East
        eastSlider.maxValue = rechargeTimeEast;
        eastSlider.value = Mathf.Clamp(rechargeTimeEast - sh.TimerForRechargeEast, 0f, rechargeTimeEast);
        timerEast = Mathf.CeilToInt(sh.TimerForRechargeEast);
        eastTimeText.text = timerEast.ToString();
        eastTimeText.gameObject.SetActive(timerEast > 0);
        SmgBulletsText.text = sh.magSizeFullAuto.ToString() + "/" + sh.MaxMagFullAuto.ToString();
        RevolverBulletsText.text = sh.bulletsInMagazineRev.ToString() + "/" + sh.maxMagazineSizeRevolver.ToString();

        if (sh.TimerForRechargeEast <= 0f)
        {
            coverObjectEast.SetActive(false);
            eastSlider.gameObject.SetActive(false);
        }
        else
        {
            coverObjectEast.SetActive(true);
            eastSlider.gameObject.SetActive(true);
        }

        // Handle Recharge West
        westSlider.maxValue = rechargeTimeWest;
        westSlider.value = Mathf.Clamp(rechargeTimeWest - sh.TimerForRechargeWest, 0f, rechargeTimeWest);
        timerWest = Mathf.CeilToInt(sh.TimerForRechargeWest);
        westTimeText.text = timerWest.ToString();
        westTimeText.gameObject.SetActive(timerWest > 0);

        if (sh.TimerForRechargeWest <= 0f)
        {
            coverObjectWest.SetActive(false);
            westSlider.gameObject.SetActive(false);
        }
        else
        {
            coverObjectWest.SetActive(true);
            westSlider.gameObject.SetActive(true);
        }

        // Handle Recharge Rocket
        rocketSlider.maxValue = rechargeTimeRocket;
        rocketSlider.value = Mathf.Clamp(rechargeTimeRocket - RL.TimerForRecharge, 0f, rechargeTimeRocket);
        timerRocket = Mathf.CeilToInt(RL.TimerForRecharge);
        rocketTimeText.text = timerRocket.ToString();
        rocketTimeText.gameObject.SetActive(timerRocket > 0);

        if (RL.TimerForRecharge <= 0f)
        {
            coverObjectRocket.SetActive(false);
            rocketSlider.gameObject.SetActive(false);
        }
        else
        {
            coverObjectRocket.SetActive(true);
            rocketSlider.gameObject.SetActive(true);
        }

        // Handle Recharge Dash
        dashSlider.maxValue = rechargeTimeDash;
        dashSlider.value = Mathf.Clamp(rechargeTimeDash - pm.dashCoolDown, 0f, rechargeTimeDash);
        timerDash = Mathf.CeilToInt(pm.dashCoolDown);
        dashTimeText.text = timerDash.ToString();
        dashTimeText.gameObject.SetActive(timerDash > 0);

        if (pm.dashCoolDown <= 0f)
        {
            coverObjectDash.SetActive(false);
            dashSlider.gameObject.SetActive(false);
        }
        else
        {
            coverObjectDash.SetActive(true);
            dashSlider.gameObject.SetActive(true);
        }
    }
}