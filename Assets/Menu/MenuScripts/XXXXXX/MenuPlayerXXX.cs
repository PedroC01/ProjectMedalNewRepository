using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPlayerXXX : MonoBehaviour
{
    [SerializeField] public PlayerInput playerInputUsingThis;
    public int playerIndexUsingThis;
    [SerializeField] public TMP_Text txtPlayerIndex;
    [SerializeField] public TMP_Text CharName;
    [SerializeField] Image ImgInMenu;
    [Space(5)]
    //Bts:---->Tornar Slides:
    [SerializeField] Button bt_SelectChamp;
    [SerializeField] Button bt_NextSelectChamp;
    [SerializeField] Button bt_BackSelectChamp;
 
    [Space(5)]
    //bts:
    [SerializeField] Button bt_Ready;
    [Space(5)]
    [SerializeField] GameObject panel_ReadyUnready;
    [SerializeField] Button bt_UnReady;
    //Por Add Bts:
    [SerializeField] Button bt_ColorSelect;
    //Novo->Para slider de characters:
    [SerializeField] public List<GameObject> prefabsMedaBots;

    private void Awake()
    {
        panel_ReadyUnready.SetActive(false);
    }
    private void Start()
    {
        UpdateCharacterShow(selectOption);

        bt_NextSelectChamp.onClick.AddListener(() =>
        {
            NextOption();
        });


        bt_BackSelectChamp.onClick.AddListener(() =>
        {
            BackOption();
        });
        bt_Ready.onClick.AddListener(() =>
        {
            SendCharSelectInfoToPlayrMedabotSelected(charSelected);
            panel_ReadyUnready.SetActive(true);
            bt_UnReady.Select();

        });
        bt_UnReady.onClick.AddListener(() =>
        {
            UnReady();
            panel_ReadyUnready.SetActive(false);
            bt_Ready.Select();


        });
        // SendCharSelectInfoToPlayrMedabotSelected(selectOption);
    }

    int selectOption = 0;
    int charSelected = 0;
    void NextOption()
    {
        Debug.Log("NextOption");
        selectOption++;
        //Caso chegemos a ultima opçáo
        if (selectOption >= prefabsMedaBots.Count)
        {
            selectOption = 0;
        }
        UpdateCharacterShow(selectOption);


    }
    public void BackOption()
    {
        Debug.Log("BackOption");
        selectOption--;
        //Caso chegemos a ultima opçáo
        if (selectOption < 0)
        {
            selectOption = 0;
        }
        UpdateCharacterShow(selectOption);

    }
    void UpdateCharacterShow(int index)
    {
        charSelected = index;
        ImgInMenu.sprite = prefabsMedaBots[index].GetComponent<Image>().sprite;//funciona mas esta invisivel?
        txtPlayerIndex.text = prefabsMedaBots[index].GetComponent<SelectMedabotToShowSideMenu>().CharacterName;


    }
    void SendCharSelectInfoToPlayrMedabotSelected(int index)
    {
        playerInputUsingThis.gameObject.GetComponent<PlayerMedabotSelected>().CharacterName = prefabsMedaBots[charSelected].GetComponent<SelectMedabotToShowSideMenu>().CharacterName;
        playerInputUsingThis.gameObject.GetComponent<PlayerMedabotSelected>().prefabCharacterSlected = prefabsMedaBots[charSelected].GetComponent<SelectMedabotToShowSideMenu>().prefabCharacter;
        playerInputUsingThis.gameObject.GetComponent<PlayerMedabotSelected>().imgMedaInGame = prefabsMedaBots[charSelected].GetComponent<SelectMedabotToShowSideMenu>().imgMedaInGame;
        VersusManager.instance.amountReady++;
    }
    void UnReady()
    {
        VersusManager.instance.amountReady--;
    }

}
