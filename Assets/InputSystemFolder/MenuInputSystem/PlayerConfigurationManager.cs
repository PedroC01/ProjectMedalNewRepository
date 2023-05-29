using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;
using UnityEngine.SceneManagement;

public class PlayerConfigurationManager : MonoBehaviour
{
  private List<PlayerConfiguration> playerConfigs;

    [SerializeField]
    private int MaxPlayers=2; 

    public static PlayerConfigurationManager Instance { get; private set; }

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.Log("There's Two Managers");
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
            playerConfigs = new List<PlayerConfiguration>();
        }
      
    }

    public void SetPlayerMaterial(int index, Material color)
    {
        playerConfigs[index].MedabotColor = color;
    }

    public void ReadyPlayer(int index)
    {
        playerConfigs[index].IsReady= true;
        if (playerConfigs.Count == MaxPlayers && playerConfigs.All(p=>p.IsReady==true))
        {
           /////// SceneManager.LoadScene("")---------------------------------------- alterar de cena se os jogadores estiverem prontos
        }
    }



    public void HandlePlayerJoin(PlayerInput pi)
    {
        Debug.Log("Player join," + pi.playerIndex);
        pi.transform.SetParent(this.transform);
        if (!playerConfigs.Any(p => p.PlayerIndex == pi.playerIndex))
        {
            playerConfigs.Add(new PlayerConfiguration(pi));
        }
      }

}



public class PlayerConfiguration
{
    public PlayerConfiguration(PlayerInput pi)
    {
        PlayerIndex=pi.playerIndex;
        Input = pi;
    }
    public PlayerInput Input { get; set; }
    public int PlayerIndex { get; set; }
    public bool IsReady { get; set; }

    public Material MedabotColor { get; set; }

}
