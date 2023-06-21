using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class VersusStartScene : MonoBehaviour
{
    //Singleton, players ao entrarem nesta scene irao procurar por este:
    public static VersusStartScene instance;

    //Colocar na Scene Lobby.
    //No inicio desliga o input durante 1sec +-, para deixar criar os paineis de cada e depois sim deixa os jogadores "mover"
    [SerializeField] public bool isInputEnabled = false;
    [SerializeField] float inputWaitTime = 1.2f;
   
    public bool onStartScene = false;
    void Awake()
    {
        instance = this;
        isInputEnabled = false;
    }

    void Start()
    {
        onStartScene = true;
    }

    public void PlayerJoinedLobbyWaitXtime(PlayerInput pInput)
    {
        pInput.DeactivateInput();
        StartCoroutine(DisableInputsForTime(inputWaitTime, pInput));
    }
    private IEnumerator DisableInputsForTime(float duration, PlayerInput pInput)
    {
        isInputEnabled = false; // Disable inputs

        yield return new WaitForSeconds(duration); // Wait for the specified duration

        isInputEnabled = true; // Re-enable inputs
    }
    public void StartDisableInputsCoroutine(float duration, PlayerInput pInput)
    {
        StartCoroutine(DisableInputsForTime(duration, pInput));
    }

}
