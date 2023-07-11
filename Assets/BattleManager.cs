using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BattleManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject _player1;
    public GameObject _player2;
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
    void Start()
    {
        // playing = false;
        StartCoroutine(BattleBegin());
    
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

    private IEnumerator BattleBegin() { 
    
        
        
            yield return new WaitForSecondsRealtime(timeBeforeStartVideo);
        
             OnBattleStart.Invoke();

            yield return new WaitForSecondsRealtime(timerAfterVideoEnds);
            OnDisable.Invoke();
            PlayerInputHandler.play = true;
            playing = true;
             BattleStarted = true;
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
        yield return new WaitForSecondsRealtime(timeToDeadBody);
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
    }
}
