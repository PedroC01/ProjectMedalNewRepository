using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEditor.Performance.ProfileAnalyzer;
using UnityEngine.Audio;
using Unity.VisualScripting;
using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.InputSystem.UI;

public class MainMenus : MonoBehaviour
{
    public AudioSource audioSource;



    public static MainMenus instance;
    private void Awake()
    {
        instance = this;
    }

    [Header("Paineis Menus:")]
    [SerializeField] public GameObject panelStarMenuMode;
    //
    [SerializeField] public GameObject panelStoryMode;//descontinuado....
    [SerializeField] public GameObject panelVersusMode;
    [SerializeField] public GameObject panelSettings;
    [SerializeField] public GameObject panelExtras;
    [SerializeField] public GameObject panelExtrasCredits;
    [SerializeField] public GameObject panelExtrasHowToPlay;

    //Saber Paneil actual activo e fazer Switch.
    GameObject actualActivePanel;
    //
    [Space(5)]
    [Header("Advise Tip Text:")]
    [SerializeField] public TextMeshProUGUI adviseTipsText;
    //
    [Space(10)]
    [Header("Butões StartMenu:")]
    [SerializeField] Button bt_StoryMode;
    [SerializeField] public Button bt_VersusMode;
    [SerializeField] Button bt_Extras;
    [SerializeField] Button bt_Settings;
    [SerializeField] Button bt_Quit;

    //
    //Metodos usados por outras classes:
    public void SwitchPanels(GameObject desactivate, GameObject activate)
    {
        actualActivePanel = activate;
        desactivate.SetActive(false);
        activate.SetActive(true);
        if (desactivate == panelStarMenuMode)
        { adviseTipsText.gameObject.SetActive(false); }
        else { adviseTipsText.gameObject.SetActive(true); }
    }
    //

    //----------------------------Set first bt ----> Remember!
    void SetFirstButton()
    {
        //  bt_StoryMode.GetComponent<Button>().Select();
        bt_VersusMode.GetComponent<Button>().Select();

    }


    #region Inicializações //Awake()  
    void Initialize()
    {
        actualActivePanel = panelStarMenuMode;//é o 1º activo
        //Todos os paineis começam off, so o de StartMenu ON
        panelStarMenuMode.SetActive(true);
        //
        panelStoryMode.SetActive(false);
        panelVersusMode.SetActive(false);
        panelSettings.SetActive(false);
        panelExtras.SetActive(false);
        panelExtrasCredits.SetActive(false);
        panelExtrasHowToPlay.SetActive(false);
        adviseTipsText.text = "";//no inicio nao deverá ter nada
        //1º Butão Selecionado ao iniciar este menu!
        // bt_StoryMode.GetComponent<Button>().Select();
        SetFirstButton();
    }
    #endregion

    //
    //
    #region Switch Menu Panels// CallBacks Butões ----> Start()
    public void SwitchStartMenuPanel()
    {
        SwitchPanels(actualActivePanel, panelStarMenuMode);
    }
    void SwitchStoryModePanel()
    {
        SwitchPanels(actualActivePanel, panelStoryMode);
    }
    //Este Chamado por butao-> mudar depois mas tem de ter a ordem dos "metodos como la esta"
    public void SwitchVersusPanel()
    {
        SwitchPanels(actualActivePanel, panelVersusMode);
    }
    void SwitchExtrasPanel()
    {
        SwitchPanels(actualActivePanel, panelExtras);
    }
    void SwitchSettingsPanel()
    {
        SwitchPanels(actualActivePanel, panelSettings);
    }
    //
    //Buttons Callbacks StartMenu:
    private void Start()
    {
        Initialize();

        bt_StoryMode.onClick.AddListener(() =>
        {
            SwitchStoryModePanel();
        });
        bt_VersusMode.onClick.AddListener(() =>
        {
            Debug.Log("Aqui nao estamos a dizer . isverus = true ao iniciar versus! ver diferenças!");
            //Feito de outra forma POREM AGORA NAO ESTOU A USAR O .ISVERSUS!!!!!!!!!!!!!!!!!!!!!!!!!!!
            //Feito via button, e tem de ser naquela ordem!
            //    SwitchVersusPanel();
            //   PlayerConfigManager.instance.BT_Player2Creation();//----------------------1
            //PlayerConfigManager.instance.isVersus = true;
            //SceneManager.LoadScene("VersusScene");//********************************--2
        });
        bt_Extras.onClick.AddListener(() =>
        {
            MenuExtras.instance.FristSelectedExtras();
            SwitchExtrasPanel();
        });
        bt_Settings.onClick.AddListener(() =>
        {
            MenuSetings.Instance.FristSelectedSettings();
            SwitchSettingsPanel();
        });
        bt_Quit.onClick.AddListener(() =>
        {
            Application.Quit();
        });

    }
    #endregion










    #region Others StartMenuAreas:
    //Mostrar nomes em efeito ciruclar up->Down "Em giro"
    Animation animCreditsText;




    //Extras:
    //Colocar Tutorial View:
    //Credits-> Text + Anim a rodar p/ baixo

    //Settings:
    //Video-Quality Unity Settings
    //Som

    #endregion





    //Main Menu Sound Callbacks:
    #region Call Menu Sounds
    [Space(5)]
    [Header("Menu Sounds:")]
    //public List<AudioSource> listMenuSounds = new List<AudioSource>(); //Colocar os sons nesta list e chamar com metodo abaixo via codigo: MenuSoundsIndex(int musicIndex)
    public AudioSource menuMusicSound;
    //
    public AudioMixer audioMixer;
    public AudioSource menuSubmitSound;
    public AudioSource menuCancelSound;
    public AudioSource menuNavigateSound;
    //
    //Versus
    public AudioSource menuVersusMusicSound;
    //
    public AudioSource menuReadySound;
    public AudioSource menuAllReadySound;
    //
    public void Play_MenuMusicSound()
    {
        // menuMusicSound.Play();
    }
    public void Play_MenuSubmitSound()
    {
        //  menuSubmitSound.Play();
    }
    public void Play_MenuCancelSound()
    {
        // menuCancelSound.Play();
    }
    public void Play_MenuNavigateSound()
    {
        // menuNavigateSound.Play();
    }
    //
    //Versus
    public void Play_MenuVersusMusicSound()
    {
        //menuVersusMusicSound.Play();
    }

    public void Play_MenuReadySound()
    {
        //   menuReadySound.Play();
    }
    public void Play_MenuAllReadySound()
    {
        //   menuAllReadySound.Play();
    }


    #endregion




   












    //Som
    //Antes com lista:
    #region
    //int 0 = menu int 1 = versus
    // [Header("Menu Sounds List:")]
    //  public List<AudioSource> listMenuSounds = new List<AudioSource>(); //Colocar os sons nesta list e chamar com metodo abaixo via codigo: MenuSoundsIndex(int musicIndex)

    /* void MenuSoundsIndex(int soundIndex)
     {
         listMenuSounds[soundIndex].Play();
     }*/
    #endregion
    //Main Menu Init/Reset?<- meto aqui ref mais easy de call all???!?!?!?
}

