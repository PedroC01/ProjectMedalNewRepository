using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Cinemachine;

public class JustOneCameraBehave : MonoBehaviour
{
    [SerializeField]
    public Transform player1;
    [SerializeField]
    public Transform player2;
    

    public Transform lastFollowed;
    public int distToChange;
    private float distance;
    public int intDistance;
    private float disToPlayer1;
    private float disToPlayer2;
    public int intDisToPlayer1;
    public int intDisToPlayer2;
    public Camera cam;
    public Transform cameraTransform;
    public CinemachineVirtualCamera cinemachineTheCamera;
    public bool SwitchFollowTarget;
    public bool changed = false;
    public float counterToChange;
    private float counterReset;
    [SerializeField]
    public Transform groupTr;

    [SerializeField]
    public UnityEvent offCamera;
    [SerializeField]
    public UnityEvent onCamera;




    // Start is called before the first frame update
    void Start()
    {
        this.cameraTransform = GetComponent<Transform>();
        counterReset = counterToChange;
        if (disToPlayer1 < disToPlayer2)
        {
            cinemachineTheCamera.Follow = player1;

        }
        if (disToPlayer1 > disToPlayer2)
        {
            cinemachineTheCamera.Follow = player2;

        }
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(player2.position, player1.position);
        intDistance = (int)distance;
        disToPlayer1 = Vector3.Distance(this.transform.position, player1.position);
        disToPlayer2 = Vector3.Distance(this.transform.position, player2.position);

        intDisToPlayer1 = (int)disToPlayer1;
        intDisToPlayer2 = (int)disToPlayer2;

       

        if (distance>26)
        {
     
            cinemachineTheCamera.m_Lens.FieldOfView = 30;
        }
        if (distance<=26)
        {
            cinemachineTheCamera.m_Lens.FieldOfView = 20;
        }
       

        if (distance <= distToChange)
        {
            if (changed == false)
            {
                if (intDisToPlayer1 < intDisToPlayer2)
                {
                   cinemachineTheCamera.Follow = player1;
                    counterToChange = counterReset;
                    lastFollowed = cinemachineTheCamera.Follow;
                    cinemachineTheCamera.Follow = groupTr;
                    changed = true;
                }
                if (intDisToPlayer1 > intDisToPlayer2)
                {
                   cinemachineTheCamera.Follow = player2;
                    counterToChange = counterReset;
                    lastFollowed = cinemachineTheCamera.Follow;
                    cinemachineTheCamera.Follow = groupTr;
                    changed = true;
                }
                if (distance > distToChange)
                {
                    counterToChange = counterReset;
                    changed = true;
                }
            }
            if (changed == true)
            {
                counterToChange -= Time.deltaTime;
                if (counterToChange <= 0)
                {
                    changed = false;

                }
            }
            if (intDisToPlayer1 == intDisToPlayer2)
            {
                counterToChange = counterReset - 1;
                changed = true;
            }


        }
    }
}
