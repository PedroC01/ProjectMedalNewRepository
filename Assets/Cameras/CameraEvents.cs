using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Cinemachine;

public class CameraEvents : MonoBehaviour
{
    [SerializeField]
    public UnityEvent changeCamera2;
    [SerializeField]
    public UnityEvent changeCamera1;
    public Transform cameraTransform;
    public float distance;
    [SerializeField]
    public Transform player1;
    [SerializeField]
    public Transform player2;
    public float minDistanceCamera;
    public int currentCamera;
    public int TimeToBlend;
    public CinemachineVirtualCamera cameraXZpositivos;
    public CinemachineVirtualCamera cameraXZnegativos;
    public CinemachineVirtualCamera cameraXpositivoZnegativo;
    public CinemachineVirtualCamera cameraXnegativoZpositivo;
    public CinemachineVirtualCamera closeCameraXZpositivos;
    public CinemachineVirtualCamera closeCameraXZnegativos;
    public CinemachineVirtualCamera closeCameraXpositivoZnegativo;
    public CinemachineVirtualCamera closeCameraXnegativoZpositivo;
    [SerializeField]
    public Animator camerasAnimator;


    // Start is called before the first frame update
    void Start()
    {
        this.cameraTransform = GetComponent<Transform>();
        currentCamera = 1;
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(player1.position, player2.position);
        //x+z+ both
        if (player1.position.x > 0 && player1.position.z > 0 && player2.position.x > 0 && player2.position.z > 0)
        {
            if (distance <= 15)
            {
                closeCameraXnegativoZpositivo.Priority=1;
                closeCameraXpositivoZnegativo.Priority=0;
                closeCameraXZnegativos.Priority=0;
                closeCameraXZpositivos.Priority = 0;
                cameraXnegativoZpositivo.Priority = 0;
                cameraXpositivoZnegativo.Priority = 0;
                cameraXZnegativos.Priority = 0;
                cameraXZpositivos.Priority = 0;

            }
            if (distance > 15)
            {
                closeCameraXnegativoZpositivo.Priority = 0;
                closeCameraXpositivoZnegativo.Priority = 0;
                closeCameraXZnegativos.Priority = 0;
                closeCameraXZpositivos.Priority = 0;
                cameraXnegativoZpositivo.Priority = 0;
                cameraXpositivoZnegativo.Priority = 0;
                cameraXZnegativos.Priority = 1;
                cameraXZpositivos.Priority = 0;
            }
        }
        //x-z+ both
        if (player1.position.x < 0 && player1.position.z > 0 && player2.position.x < 0 && player2.position.z > 0)
        {
            if (distance <= 15)
            {
                closeCameraXnegativoZpositivo.Priority = 0;
                closeCameraXpositivoZnegativo.Priority = 1;
                closeCameraXZnegativos.Priority = 0;
                closeCameraXZpositivos.Priority = 0;
                cameraXnegativoZpositivo.Priority = 0;
                cameraXpositivoZnegativo.Priority = 0;
                cameraXZnegativos.Priority = 0;
                cameraXZpositivos.Priority = 0;

            }
            if (distance > 15)
            {
                closeCameraXnegativoZpositivo.Priority = 0;
                closeCameraXpositivoZnegativo.Priority = 0;
                closeCameraXZnegativos.Priority = 0;
                closeCameraXZpositivos.Priority = 0;
                cameraXnegativoZpositivo.Priority = 0;
                cameraXpositivoZnegativo.Priority = 0;
                cameraXZnegativos.Priority = 1;
                cameraXZpositivos.Priority = 0;
            }
        }
        //x+z- both
        if (player1.position.x > 0 && player1.position.z < 0 && player2.position.x > 0 && player2.position.z < 0)
        {
            if (distance <= 15)
            {
                closeCameraXnegativoZpositivo.Priority = 1;
                closeCameraXpositivoZnegativo.Priority = 0;
                closeCameraXZnegativos.Priority = 0;
                closeCameraXZpositivos.Priority = 0;
                cameraXnegativoZpositivo.Priority = 0;
                cameraXpositivoZnegativo.Priority = 0;
                cameraXZnegativos.Priority = 0;
                cameraXZpositivos.Priority = 0;

            }
            if (distance > 15)
            {
                closeCameraXnegativoZpositivo.Priority = 0;
                closeCameraXpositivoZnegativo.Priority = 0;
                closeCameraXZnegativos.Priority = 0;
                closeCameraXZpositivos.Priority = 0;
                cameraXnegativoZpositivo.Priority = 0;
                cameraXpositivoZnegativo.Priority = 0;
                cameraXZnegativos.Priority = 0;
                cameraXZpositivos.Priority = 1;
            }
        }
        //x-z+ both
        if (player1.position.x > 0 && player1.position.z > 0 && player2.position.x > 0 && player2.position.z > 0)
        {
            if (distance <= 15)
            {
                closeCameraXnegativoZpositivo.Priority = 0;
                closeCameraXpositivoZnegativo.Priority = 1;
                closeCameraXZnegativos.Priority = 0;
                closeCameraXZpositivos.Priority = 0;
                cameraXnegativoZpositivo.Priority = 0;
                cameraXpositivoZnegativo.Priority = 0;
                cameraXZnegativos.Priority = 0;
                cameraXZpositivos.Priority = 0;

            }
            if (distance > 15)
            {
                closeCameraXnegativoZpositivo.Priority = 0;
                closeCameraXpositivoZnegativo.Priority = 0;
                closeCameraXZnegativos.Priority = 0;
                closeCameraXZpositivos.Priority = 0;
                cameraXnegativoZpositivo.Priority = 0;
                cameraXpositivoZnegativo.Priority = 0;
                cameraXZnegativos.Priority = 0;
                cameraXZpositivos.Priority = 1;
            }
        }

        if (player1.position.x > 0 && player1.position.z > 0 && player2.position.x > 0 && player2.position.z > 0)
        {
            if (distance <= 15)
            {
                closeCameraXnegativoZpositivo.Priority = 1;
                closeCameraXpositivoZnegativo.Priority = 0;
                closeCameraXZnegativos.Priority = 0;
                closeCameraXZpositivos.Priority = 0;
                cameraXnegativoZpositivo.Priority = 0;
                cameraXpositivoZnegativo.Priority = 0;
                cameraXZnegativos.Priority = 0;
                cameraXZpositivos.Priority = 0;

            }
            if (distance > 15)
            {
                closeCameraXnegativoZpositivo.Priority = 0;
                closeCameraXpositivoZnegativo.Priority = 0;
                closeCameraXZnegativos.Priority = 0;
                closeCameraXZpositivos.Priority = 0;
                cameraXnegativoZpositivo.Priority = 0;
                cameraXpositivoZnegativo.Priority = 0;
                cameraXZnegativos.Priority = 1;
                cameraXZpositivos.Priority = 0;
            }
        }




        if ((player1.position.x > 0 && player1.position.z > 0 && player2.position.x < 0 && player2.position.z > 0)|| (player1.position.x < 0 && player1.position.z > 0 && player2.position.x > 0 && player2.position.z > 0))
        {
            if (distance <= 15)
            {
                closeCameraXnegativoZpositivo.Priority = 0;
                closeCameraXpositivoZnegativo.Priority = 0;
                closeCameraXZnegativos.Priority = 1;
                closeCameraXZpositivos.Priority = 0;
                cameraXnegativoZpositivo.Priority = 0;
                cameraXpositivoZnegativo.Priority = 0;
                cameraXZnegativos.Priority = 0;
                cameraXZpositivos.Priority = 0;

            }
            if (distance > 15)
            {
                closeCameraXnegativoZpositivo.Priority = 0;
                closeCameraXpositivoZnegativo.Priority = 0;
                closeCameraXZnegativos.Priority = 0;
                closeCameraXZpositivos.Priority = 0;
                cameraXnegativoZpositivo.Priority = 0;
                cameraXpositivoZnegativo.Priority = 0;
                cameraXZnegativos.Priority = 1;
                cameraXZpositivos.Priority = 0;
            }
        }
        //x-z+ both
        if ((player1.position.x > 0 && player1.position.z < 0 && player2.position.x < 0 && player2.position.z > 0) || (player1.position.x < 0 && player1.position.z > 0 && player2.position.x > 0 && player2.position.z < 0))
            {
            if (distance <= 15)
            {
                closeCameraXnegativoZpositivo.Priority = 0;
                closeCameraXpositivoZnegativo.Priority = 0;
                closeCameraXZnegativos.Priority = 0;
                closeCameraXZpositivos.Priority = 1;
                cameraXnegativoZpositivo.Priority = 0;
                cameraXpositivoZnegativo.Priority = 0;
                cameraXZnegativos.Priority = 0;
                cameraXZpositivos.Priority = 0;

            }
            if (distance > 15)
            {
                closeCameraXnegativoZpositivo.Priority = 0;
                closeCameraXpositivoZnegativo.Priority = 0;
                closeCameraXZnegativos.Priority = 0;
                closeCameraXZpositivos.Priority = 0;
                cameraXnegativoZpositivo.Priority = 0;
                cameraXpositivoZnegativo.Priority = 0;
                cameraXZnegativos.Priority = 0;
                cameraXZpositivos.Priority = 1;
            }
        }
        //x+z- both
       /* if (player1.position.x > 0 && player1.position.z < 0 && player2.position.x > 0 && player2.position.z < 0)
        {
            if (distance <= 15)
            {
                closeCameraXnegativoZpositivo.Priority = 1;
                closeCameraXpositivoZnegativo.Priority = 0;
                closeCameraXZnegativos.Priority = 0;
                closeCameraXZpositivos.Priority = 0;
                cameraXnegativoZpositivo.Priority = 0;
                cameraXpositivoZnegativo.Priority = 0;
                cameraXZnegativos.Priority = 0;
                cameraXZpositivos.Priority = 0;

            }
            if (distance > 15)
            {
                closeCameraXnegativoZpositivo.Priority = 0;
                closeCameraXpositivoZnegativo.Priority = 0;
                closeCameraXZnegativos.Priority = 0;
                closeCameraXZpositivos.Priority = 0;
                cameraXnegativoZpositivo.Priority = 0;
                cameraXpositivoZnegativo.Priority = 0;
                cameraXZnegativos.Priority = 0;
                cameraXZpositivos.Priority = 1;
            }
        }
        //x-z+ both
        if (player1.position.x > 0 && player1.position.z > 0 && player2.position.x > 0 && player2.position.z > 0)
        {
            if (distance <= 15)
            {
                closeCameraXnegativoZpositivo.Priority = 0;
                closeCameraXpositivoZnegativo.Priority = 1;
                closeCameraXZnegativos.Priority = 0;
                closeCameraXZpositivos.Priority = 0;
                cameraXnegativoZpositivo.Priority = 0;
                cameraXpositivoZnegativo.Priority = 0;
                cameraXZnegativos.Priority = 0;
                cameraXZpositivos.Priority = 0;

            }
            if (distance > 15)
            {
                closeCameraXnegativoZpositivo.Priority = 0;
                closeCameraXpositivoZnegativo.Priority = 0;
                closeCameraXZnegativos.Priority = 0;
                closeCameraXZpositivos.Priority = 0;
                cameraXnegativoZpositivo.Priority = 0;
                cameraXpositivoZnegativo.Priority = 0;
                cameraXZnegativos.Priority = 0;
                cameraXZpositivos.Priority = 1;
            }
        }

        if (player1.position.x > 0 && player1.position.z > 0 && player2.position.x > 0 && player2.position.z > 0)
        {
            if (distance <= 15)
            {
                closeCameraXnegativoZpositivo.Priority = 1;
                closeCameraXpositivoZnegativo.Priority = 0;
                closeCameraXZnegativos.Priority = 0;
                closeCameraXZpositivos.Priority = 0;
                cameraXnegativoZpositivo.Priority = 0;
                cameraXpositivoZnegativo.Priority = 0;
                cameraXZnegativos.Priority = 0;
                cameraXZpositivos.Priority = 0;

            }
            if (distance > 15)
            {
                closeCameraXnegativoZpositivo.Priority = 0;
                closeCameraXpositivoZnegativo.Priority = 0;
                closeCameraXZnegativos.Priority = 0;
                closeCameraXZpositivos.Priority = 0;
                cameraXnegativoZpositivo.Priority = 0;
                cameraXpositivoZnegativo.Priority = 0;
                cameraXZnegativos.Priority = 1;
                cameraXZpositivos.Priority = 0;
            }
        }
        */
    }
   
 
}
