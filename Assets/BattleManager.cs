using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering;

public class BattleManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject _player1;
    public GameObject _player2;
    public float timeAfterBattle;
    [SerializeField]
    public UnityEvent CloseDistance;
    [SerializeField]
    public UnityEvent FarDistance;
    [SerializeField]
    public UnityEvent OnGameEnd;
    [SerializeField]
    public UnityEvent OnBattleStart;
    [SerializeField]
    public UnityEvent OnDisable;
    private float dist;
    public float closeDistanceThreshold;
    [SerializeField]
    public static bool playing=true;
    private float _Player1Health;
    private float _Player2Health;
    public CinemachineVirtualCamera player2EndCamera;
    public CinemachineVirtualCamera player1EndCamera;
    public float timeToDeadBody;
    public float timeBeforeStartVideo;
    public float timerAfterVideoEnds;
    public bool BattleStarted;
    public int PlayerReady;
    public GameObject fakeMainMenu;
    public bool CheckingPlayers=true;
    private Coroutine checkPlayersCoroutine;
    public PlayerInputManager pim;
    public TMP_Text Player1ReadyText;
    public TMP_Text Player1ReadyText2;
    public TMP_Text Player2ReadyText;
    public TMP_Text Player2ReadyText2;
    public GameObject outFunction;
    public GameObject WinnerMetabee;

    void Start()
    {
        // playing = false;
        //StartCoroutine(BattleBegin());
        pim=FindObjectOfType<PlayerInputManager>();
        checkPlayersCoroutine = StartCoroutine(CheckPlayers());
        PlayerInputHandler.play = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (BattleStarted)
        {
            _Player1Health = _player1.GetComponent<PlayerHealth>().HeadHealth;
            _Player2Health = _player2.GetComponent<PlayerHealth>().HeadHealth;
            if (_Player1Health <= 0 || _Player2Health <= 0)
            {
                playing = false;
                PlayerInputHandler.play = playing;
                StartCoroutine(ChangeCameraOnEnd());

            }
            dist = Vector3.Distance(_player1.transform.position, _player2.transform.position);
            if (dist <= closeDistanceThreshold)
            {

                CloseDistance.Invoke();
            }
            else
            {

                FarDistance.Invoke();
            }
            if (!playing)
            {



            }
        }
    }
    public void IsPlaying()
    {
        PlayerInputHandler.play = true;
        playing = true;
    }
    public void IsNotPlaying()
    {
        PlayerInputHandler.play = false;
        playing = false;
    }
    private IEnumerator BattleBegin() { 
    
        
        
            yield return new WaitForSecondsRealtime(timeBeforeStartVideo);
        
             OnBattleStart.Invoke();

            yield return new WaitForSecondsRealtime(timerAfterVideoEnds);
            OnDisable.Invoke();
            PlayerInputHandler.play = true;
            playing = true;
             BattleStarted = true;
             Time.timeScale = 1.0f;
    }

    private IEnumerator CheckPlayers()
    {
        while (CheckingPlayers)
        {
            if (PlayerReady == 1)
            {
                Player1ReadyText.gameObject.SetActive(false);
                Player1ReadyText2.gameObject.SetActive(true);
                Player2ReadyText.gameObject.SetActive(true);
            }
            if (PlayerReady >= 2)
            {
                Player2ReadyText.gameObject.SetActive(false);
                Player2ReadyText2.gameObject.SetActive(true);
                fakeMainMenu.SetActive(false);
                StopCoroutine(CheckPlayers());
                StopCoroutine(checkPlayersCoroutine);
                StartCoroutine(BattleBegin());
                
                yield break;
            }

            yield return null;
        }
        
    }

    private IEnumerator ChangeCameraOnEnd()
    {
        OnGameEnd.Invoke();

        if (_Player1Health <= 0)
        {
            player1EndCamera.Priority = 2;
            player2EndCamera.Priority = 0;
        }
        else if (_Player2Health <= 0)
        {
            player2EndCamera.Priority = 2;
            player1EndCamera.Priority = 0;
        }
        outFunction.SetActive(true);
        yield return new WaitForSecondsRealtime(timeToDeadBody);
        outFunction.SetActive(false);
        

        if (_Player1Health <= 0)
        {
            player1EndCamera.Priority = 0;
            player2EndCamera.Priority = 2;
        }
        else if (_Player2Health <= 0)
        {
            player2EndCamera.Priority = 0;
            player1EndCamera.Priority = 2;
        }
        WinnerMetabee.SetActive(true);
    } 
}
