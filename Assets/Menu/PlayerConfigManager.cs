using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class PlayerConfigManager : MonoBehaviour
{
    [SerializeField] Button GoBackP1;
    public static PlayerConfigManager instance;
    List<PlayerConfig> playerConfigs;
    [SerializeField] int MaxPlayers = 2;
    public bool isVersus = false;
    public GameObject playerPrefab = null;
    GameObject player2RefToDestroy = null;

    private void Awake()
    {
        if (!GetComponent<PlayerInputManager>())
        {
            Debug.LogError("PlayerInputManager");
        }
        else
        {
            MaxPlayers = GetComponent<PlayerInputManager>().maxPlayerCount;
        }
        if (instance != null)
        {
            Debug.LogError("TRYING TO CREATE ANOTHER INSTANCE !!! ERROR!");
        }
        else
        {
            instance = this;//singleton
            DontDestroyOnLoad(instance);
            playerConfigs = new List<PlayerConfig>();
        }

    }

    void Start()
    {
        GoBackP1.onClick.AddListener(() =>
        {
            PlayerConfigManager.instance.isVersus = false;
            Debug.Log("Actual Max Players Forced = 1");
            MainMenus.instance.SwitchStartMenuPanel();
            PlayerConfigManager.instance.RemoveJoinedPlayer2();
        });
    }
 
    public void BT_Player2Creation()
    {
        isVersus = true;
        InstantiatePlayerPref();
    }
    void InstantiatePlayerPref() 
    {
        GameObject objRef = Instantiate(playerPrefab, this.transform);
        Debug.LogWarning("1.1 " + playerConfigs.Count);
        PlayerInput playerInput = objRef.GetComponent<PlayerInput>();
        Debug.LogWarning("1.2 " + playerConfigs.Count);
        if (playerInput != null)
        {
            playerConfigs.Add(new PlayerConfig(playerInput));

            Debug.LogWarning("1.4 " + playerConfigs.Count);
        }
    }

    public void RemoveJoinedPlayer2()
    {
        Debug.Log("Player Removed!");
        playerConfigs.RemoveAt(playerConfigs.Count - 1);
        Destroy(player2RefToDestroy);
    }

    public void WhenPlayershouldJoin(PlayerInput _playerInput)
    {
        if (!isVersus)
        {
            if (playerConfigs.Count < 1)
            {
                playerConfigs.Add(new PlayerConfig(_playerInput));
                _playerInput.transform.SetParent(transform);
            }
            
            return;
        }
        else if (isVersus)
        {
       
            if (playerConfigs.Count < 2)
            {
                playerConfigs.Add(new PlayerConfig(_playerInput));
                _playerInput.transform.SetParent(transform);
                Debug.Log("Player joined: " + _playerInput.playerIndex);
            }
            return;
        }
        else
        {
            return;
        }
    }

    private PlayerInput chosenPlayer; 
    public GameObject playerPrefabs;

    public void JoinPlayer(PlayerInput playerInput)
    {
        if (chosenPlayer == null)
        {
            // First player to click the button becomes the chosen player
            chosenPlayer = playerInput;
            Debug.Log("Player chosen: " + chosenPlayer.playerIndex);
        }
        else
        {
            GameObject objRef = Instantiate(playerPrefabs, transform);
            PlayerInput newPlayerInput = objRef.GetComponent<PlayerInput>();

            if (newPlayerInput != null)
            {
                Debug.Log("Player joined: " + newPlayerInput.playerIndex);
            }
            else
            {
                Debug.LogError("PlayerInput component not found on the playerPrefabs.");
            }
        }
    }

    public void LeavePlayer(PlayerInput playerInput)
    {
        if (playerInput == chosenPlayer)
        {
            // Reset the chosen player if they leave
            chosenPlayer = null;
            Debug.Log("Chosen player left");
        }
        else
        {
            // Remove the player from the game
            Debug.Log("Player left: " + playerInput.playerIndex);
            Destroy(playerInput.gameObject);
        }
    }

    public void SetPlayerColor(int index, Material color)
    {
        playerConfigs[index].PlayerMaterial = color;
    }
 
    public void ReadyPlayer(int index)
    {
        playerConfigs[index].isReady = true;
        CheckAllPlayersReady();
    }
 
    void CheckAllPlayersReady()
    {
        if (playerConfigs.Count == (isVersus ? 2 : 1) && playerConfigs.All(p => p.isReady))
        {
            SceneManager.LoadScene("NextScene");
        }
    }


}


public class PlayerConfig
{
    public PlayerConfig(PlayerInput _playerinput)
    {
        PlayerIndex = _playerinput.playerIndex;
        playerinput = _playerinput;
    }

    public PlayerInput playerinput { get; private set; }
    public int PlayerIndex { get; private set; }
    public bool isReady { get; set; }
    public Material PlayerMaterial { get; set; }
}
