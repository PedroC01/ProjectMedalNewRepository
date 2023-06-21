using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class PlayerDataVersus : MonoBehaviour
{
    [SerializeField] GameObject pref;
    public PlayerInput pInput;
    public int playerIndex;
    [SerializeField] List<GameObject> PlayerPanels = new List<GameObject>();
    public InputActionAsset saveFromManager;
    public void KnowPlayerInputAndIndex(PlayerInput _pInput, int _playerIndex)
    {
        pInput = _pInput;
        playerIndex = _playerIndex;
        this.GetComponent<PlayerMedabotSelected>().pInput = _pInput;
        this.GetComponent<PlayerMedabotSelected>().pInputIndex = _playerIndex;
    }
    public void CreatesideMenuCalledButton()
    {
        CreateSideMenuForThisPlayer(pInput);
    }
    void CreateSideMenuForThisPlayer(PlayerInput pInput)
    {
        CreateSideMenu(pInput);
    }
    public void CreateSideMenu(PlayerInput input)
    {
        var objParent = GameObject.Find("Canvas");
        GameObject playerPanel = Instantiate(pref, objParent.transform);
        PlayerPanels.Add(playerPanel);
        playerPanel.GetComponent<MenuPlayerXXX>().playerInputUsingThis = input;
        playerPanel.GetComponent<MenuPlayerXXX>().playerIndexUsingThis = input.playerIndex;
       playerPanel.GetComponent<InputSystemUIInputModule>().actionsAsset = this.gameObject.GetComponent<PlayerInput>().actions;
        //Agora a enviar o que se obtem do inicio:
       playerPanel.GetComponent<InputSystemUIInputModule>().actionsAsset = saveFromManager;
    }
}
