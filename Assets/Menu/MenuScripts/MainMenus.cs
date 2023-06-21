using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenus : MonoBehaviour
{
    public static MainMenus instance;
    private void Awake()
    {
        instance = this;
    }

    [Header("Paineis Menus:")]
    [SerializeField] public GameObject panelStarMenuMode;
    //
    [SerializeField] public GameObject panelStoryMode;
    [SerializeField] public GameObject panelVersusMode;
    [SerializeField] public GameObject panelSettings;
    [SerializeField] public GameObject panelExtras;
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
    [SerializeField] Button bt_VersusMode;
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
        else {  adviseTipsText.gameObject.SetActive(true); }

    }
    //

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
        adviseTipsText.text = "";//no inicio nao deverá ter nada
        //1º Butão Selecionado ao iniciar este menu!
        bt_StoryMode.GetComponent<Button>().Select();
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
            SwitchExtrasPanel();
        });
        bt_Settings.onClick.AddListener(() =>
        {
            SwitchSettingsPanel();
        });
        bt_Quit.onClick.AddListener(() =>
        {
            Application.Quit();
        });

    }
    #endregion
}

