using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.Events;


public class PlayersManager : MonoBehaviour
{
    public GameObject playerPrefab;

    public List<PlayerInput> listJoinedPlayers = new List<PlayerInput>();
    int player1Index;
    int player2Index;
    [SerializeField] GameObject PlayersideMenu;//Prefab menu de cada atribuido aqui sera chamado agr no inicio

    private AssignPlayerInput assignPlayerInput;

    PlayerInput playerInput;

    public InputSystemUIInputModule uiInputModule; // Reference to the UIInputModule


    void Awake()
    {
  
    }
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        var assigns = FindObjectsOfType<AssignPlayerInput>();
        var index = playerInput.playerIndex;

        InstantiatePlayer();
        InstantiatePlayer();
        //
        assignPlayerInput = assigns.FirstOrDefault(m => m.GetPlayerIndex() == index);



    }


    //Verificar paineis criados:
    public List<GameObject> PlayerPanels = new List<GameObject>();

    public void LeavePlayer(PlayerInput playerInput)
    {
        if (listJoinedPlayers.Contains(playerInput))
        {
            listJoinedPlayers.Remove(playerInput);
            Destroy(playerInput.gameObject);

            Debug.Log("Player left: " + playerInput.playerIndex);
        }
    }


    //Instanciar Player, 1 de cada x:

   
    //Chamada em start para criar logo o 1º jogador!
    //Cria jogadores e depois chama metodo JoinPlayer, que deixara escolher/detetar qual o Controlador que este está a usar
    private void InstantiatePlayer()
    {
        // Instantiate the player prefab
        GameObject newPlayer = Instantiate(playerPrefab, this.transform.position, Quaternion.identity);
        newPlayer.transform.parent = this.transform;
        //---------------------
        PlayerInput playerInput = newPlayer.GetComponent<PlayerInput>();

        //Atribuir input a assingPlayeRInpuyt()
        newPlayer.GetComponent<AssignPlayerInput>().AssignPlayerInputs(playerInput);



        //
        var index = playerInput.playerIndex;

        //

        //

        // Assign the UIInputModule to the PlayerInput component
        playerInput.uiInputModule = uiInputModule;
        listJoinedPlayers.Add(playerInput);//Add a lista de jogadores:
       




     //   InstantiatePlayerMenu(playerInput, index);
    }

    public void InstantiatePlayerMenu(PlayerInput input, int index)
    {
        var objParent = GameObject.Find("Canvas");
        GameObject playerPanel = Instantiate(PlayersideMenu, objParent.transform);
        PlayerPanels.Add(playerPanel);
        //-----------------------------------------
        

        AssignPlayerInput assignPlayerInput = playerPanel.GetComponent<AssignPlayerInput>();
        if (assignPlayerInput != null)
        {
            assignPlayerInput.AssignPlayerInputs(input);
        }
        else
        {
            Debug.LogError("AssignPlayerInput component not found on the playerSideMenuPrefab.");
        }
       // playerPanel.GetComponent<AssignPlayerInput>().AssignPlayerInputs(input);
    }



}

