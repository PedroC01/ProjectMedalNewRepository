using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem.UI;

public class MenuExtras : MonoBehaviour
{

    public static MenuExtras instance;
    private void Awake()
    {
        instance = this;
    }

    public Button Goback;
    public Button Credits;
    public Button HowToPlay;

    public GameObject panelCredits;
    public GameObject panelHowToplay;

    public Button GobackCredits;
    public Button GobackHowToPlay;
    //Extras 1º obj selected:
    public void FristSelectedExtras()
    {
        MenuSelectedObject(Credits.gameObject);//troca 1º obj selected.
    }
    //Paineis Extras e Creditos, 1º obj selected:
    public void FristSelectedCredits()
    {
        MenuSelectedObject(GobackCredits.gameObject);
    }
    public void FristSelectedHowToPlay()
    {
        MenuSelectedObject(GobackHowToPlay.gameObject);
    }

    //Quando entramos em settings queremos um bt/ui selecionado para navegar.------------------------------------------------
    void MenuSelectedObject(GameObject firstSelected)
    {
        FindObjectOfType<MultiplayerEventSystem>().SetSelectedGameObject(firstSelected);
    }


    private void Start()
    {
        //Painel ExtraS:
        //Voltamos ao StartMenu?:
        Goback.onClick.AddListener(() =>
        {
            MenuSelectedObject(MainMenus.instance.bt_VersusMode.gameObject);//troca 1º obj selected.
            //Turn this painel off, and show again 
            MainMenus.instance.SwitchPanels(MainMenus.instance.panelExtras, MainMenus.instance.panelStarMenuMode);
        });
        //Vamos creditos?-1
        //See Credits:
        Credits.onClick.AddListener(() =>
        {
            //Mudar 1º Obj Selected
            FristSelectedCredits();
            MainMenus.instance.SwitchPanels(MainMenus.instance.panelExtras, panelCredits);

        });
        //Vamos how ToPlay?2-
        //See How To Play:
        HowToPlay.onClick.AddListener(() =>
        {
            //Mudar 1º Obj Selected
            FristSelectedHowToPlay();
            MainMenus.instance.SwitchPanels(MainMenus.instance.panelExtras, panelHowToplay);
        });

        //Estamos em creditos queremos voltar ao panel extras:
        //GoCredits:
        GobackCredits.onClick.AddListener(() =>
        {
            //Mudar 1º Obj Selected
            FristSelectedExtras();
            MainMenus.instance.SwitchPanels(panelCredits, MainMenus.instance.panelExtras);
        });
        //Estamos em how To Play queremos voltar a extras:
        //Go Back How To Play:
        GobackHowToPlay.onClick.AddListener(() =>
        {
            //Mudar 1º Obj Selected
            FristSelectedExtras();

            MainMenus.instance.SwitchPanels(panelHowToplay, MainMenus.instance.panelExtras);
        });
    }
   
}
