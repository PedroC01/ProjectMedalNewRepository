using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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


    // Start is called before the first frame update
    void Start()
    {
        this.cameraTransform = GetComponent<Transform>();
        currentCamera = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if ((distance=Vector3.Distance(this.transform.position,player1.transform.position))<=minDistanceCamera&&(distance = Vector3.Distance(this.transform.position, player2.transform.position)) <= minDistanceCamera)
        {

            StartCoroutine(CameraBlending());
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        
    }
    IEnumerator CameraBlending()
    {

        if (currentCamera == 1)
        {
            changeCamera1.Invoke();
            currentCamera = 2;
        }
        if (currentCamera == 2)
        {
            changeCamera1.Invoke();
            currentCamera = 1;
        }

        yield return new WaitForSecondsRealtime(TimeToBlend);
        
    }
}
