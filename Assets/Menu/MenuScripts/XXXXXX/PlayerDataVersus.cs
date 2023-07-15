using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class PlayerDataVersus : MonoBehaviour
{
    [SerializeField] GameObject prefP1;
    [SerializeField] GameObject prefP2;

    public PlayerInput pInput;
    public int playerIndex;
    [SerializeField] List<GameObject> PlayerPanels = new List<GameObject>();
    public InputActionAsset saveFromManager;
    private void Start()
    {
        DontDestroyOnLoad(this);
        VersusManager.instance.CreatePlayer(this.gameObject);
    }

    bool twoPlayer = false;

    private void Update()
    {
       /* if (!twoPlayer) {
            //Se ja ouver 2 jogadores
            if (GetComponent<PlayerInputManager>().playerCount == 2)
            {
                twoPlayer = true;

                //Criar 2 player

            }
        }*/
       
    }


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
        GameObject playerPanel = null;
        if (input.playerIndex == 0)
        {
             playerPanel = Instantiate(prefP1, objParent.transform);
        }
        else { 
             playerPanel = Instantiate(prefP2, objParent.transform);
        }
        PlayerPanels.Add(playerPanel);
        //  playerPanel.GetComponent<PlayerSideMenuData>().playerInputUsingThis = input;
        // playerPanel.GetComponent<PlayerSideMenuData>().playerIndexUsingThis = input.playerIndex;
        playerPanel.GetComponent<NewPlayerSideMenu>().playerInputUsingThis = input;
        playerPanel.GetComponent<NewPlayerSideMenu>().playerIndexUsingThis = input.playerIndex;

        playerPanel.GetComponent<InputSystemUIInputModule>().actionsAsset = this.gameObject.GetComponent<PlayerInput>().actions;
        //Agora a enviar o que se obtem do inicio:
       playerPanel.GetComponent<InputSystemUIInputModule>().actionsAsset = saveFromManager;
    }
}
