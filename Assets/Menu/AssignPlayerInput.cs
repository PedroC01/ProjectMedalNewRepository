using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AssignPlayerInput : MonoBehaviour
{
    [SerializeField]private PlayerInput playerInput;
    [SerializeField] int playerIndex;
    public void AssignPlayerInputs(PlayerInput input)
    {
        playerInput = input;
        playerIndex = playerInput.playerIndex;
    }

    public int GetPlayerIndex()
    {
        return playerInput.playerIndex;
    }
}
