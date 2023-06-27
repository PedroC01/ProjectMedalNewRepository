using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Assertions.Must;
using UnityEngine.InputSystem;

/// <summary>
/// Este menu deve saber qual player � qual??
/// 
/// </summary>

public class PlayerSidePainelMenu : MonoBehaviour
{
    int PlayerIndex;
    public PlayerInput input;
    [SerializeField] TextMeshProUGUI titleText;
    [SerializeField] TextMeshProUGUI titleTextReady;
    [SerializeField] GameObject readyPanel;
    [SerializeField] Button bt_Ready;
    [SerializeField] Button bt_UnReady;

    float ignoreTime = 0.8f;
    bool inptEnable = false;
    private bool isActive;

    private void OnEnable()
    {
        if (input != null)
        {
            input.onActionTriggered += OnPlayerInput;
        }
    }

    private void OnDisable()
    {
        if (input != null)
        {
            input.onActionTriggered -= OnPlayerInput;
        }
    }

    private void OnPlayerInput(InputAction.CallbackContext context)
    {
        if (!isActive)
        {
            return;
        }
        if (context.action.name == "Navigate")
        {
        }
        else if (context.action.name == "Submit")
        {

        }
    }


    public void SetGetPlayerIndex(int pi)
    {
        PlayerIndex = pi;
        titleText.text = "Player " + (PlayerIndex + 1).ToString();//se 0 = 1 se 1 = 2, isto pq index come�a em 0 e nao queremos "Player 0" e sim "Player 1" etc
        titleTextReady = titleText;//Saber logo o nome para painel Ready!!
        Ignore();
    }

    //Se for encess�rio:
    void Ignore()
    {
        ignoreTime = Time.time + ignoreTime;
    }
    void Update()
    {
        if (Time.time > ignoreTime)
        {
            inptEnable = true;
        }
    }
    public void SetPlayerColor(Material color)
    {
        if (!inptEnable) { return; }
        PlayerConfigManager.instance.SetPlayerColor(PlayerIndex,color);
        bt_Ready.Select();
    }

    public void SetPlayerReady()
    {
        if (!inptEnable) { return; }
        PlayerConfigManager.instance.ReadyPlayer(PlayerIndex);
        readyPanel.SetActive(true);
        bt_UnReady.Select();
    }
    public void SetPlayerUnReady()
    {
        if (!inptEnable) { return; }
        PlayerConfigManager.instance.ReadyPlayer(PlayerIndex);
        readyPanel.SetActive(false);
        bt_Ready.Select();
    }
    void Start()
    {
        bt_Ready.Select();
        readyPanel.SetActive(false);
        bt_Ready.onClick.AddListener(() =>
         {
             SetPlayerReady();
         });
        bt_UnReady.onClick.AddListener(() =>
        {
            SetPlayerUnReady();
        });
    }

}