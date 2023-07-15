using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;


public class VersusManager : MonoBehaviour
{
    public static VersusManager instance;
    //Singleton:
    void Awake()
    {
        instance = this;//singleton!
        Debug.Log("Queremos dont destroy on load? ou queremos destruir e ao voltar menu refaz?!  confirmar depois e rever compotamentos!!!!");
        DontDestroyOnLoad(this);
    }


    public GameObject MiddleMenu;


    public int amountReady = 0;

    //
    [SerializeField] GameObject PlayerMenuPrefab;
    [Space(2)]
    public List<PlayerInput> listJoinedPlayers = new List<PlayerInput>();
   
    void Start()
    {
        //CreatePlayer();**************************************************************************************************************************

     
    }

    public void CreatePlayer(GameObject PlayerPrefab)
    {
        GetComponent<PlayerInputManager>().DisableJoining();
         PlayerPrefab.transform.SetParent(this.transform);
        PlayerInput playerInput = PlayerPrefab.GetComponent<PlayerInput>();
     
        PlayerPrefab.GetComponent<PlayerDataVersus>().KnowPlayerInputAndIndex(playerInput, playerInput.playerIndex);
        PlayerPrefab.GetComponent<PlayerDataVersus>().saveFromManager = PlayerPrefab.gameObject.GetComponent<PlayerInput>().actions;

        //Se 2º criar tbm menu
        if (playerInput.playerIndex == 1)
        {
            Create2PlayerMenu(GetComponent<PlayerMedabotSelected>().gameObject);

        }

        listJoinedPlayers.Add(playerInput);//Add a lista de jogadores:
    }

    public void BTTellPlayersToCreatesideMenu()
    {
        int i = 0;
        foreach (var player in listJoinedPlayers)
        {
            i++;
            if (i == 2)
            {
                var objParent = GameObject.Find("Canvas");
                GameObject middlepanel = Instantiate(MiddleMenu, objParent.transform);
            }
            player.GetComponent<PlayerDataVersus>().CreatesideMenuCalledButton();
        }
    }


    void Create2PlayerMenu(GameObject obj)
    {
        GetComponent<PlayerDataVersus>().CreatesideMenuCalledButton();

    }


    public void RemoveLastPlayer()
    {
        if (listJoinedPlayers.Count > 1)
        {
            int lastIndex = listJoinedPlayers.Count - 1;
            listJoinedPlayers.RemoveAt(lastIndex);
        }
    }











    //----------------------------------------------------------------------





    public void OnOFFSingle()
    {
        if (GameObject.Find("SinglePlayerEventSystem") == null)
        {
            return;
        }
        if (GameObject.Find("SinglePlayerEventSystem").GetComponent<MultiplayerEventSystem>().enabled)
        {
            GameObject.Find("SinglePlayerEventSystem").GetComponent<MultiplayerEventSystem>().enabled = false;
            GameObject.Find("SinglePlayerEventSystem").GetComponent<InputSystemUIInputModule>().enabled = false;
        }
        else
        {
            GameObject.Find("SinglePlayerEventSystem").GetComponent<MultiplayerEventSystem>().enabled = true;
            GameObject.Find("SinglePlayerEventSystem").GetComponent<InputSystemUIInputModule>().enabled = true;
        }
    }

    private void Update()
    {
        AllReady();
    }
    void AllReady()
    {   
        if (amountReady == 2) 
        {
            amountReady = 0;//Limpar
            ScenesManagerController.instance.LoadVersus();
        }
    }

}
