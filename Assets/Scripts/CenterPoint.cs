using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterPoint : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform[] CFocusPoints;

    private Vector3 centerPlayers;
    void Start()
    {
        GameObject[] allFocusPoints = GameObject.FindGameObjectsWithTag("Player");
      //  GameObject[] allFocusPoints = FindObjectsOfType<FocusPoint>();
        CFocusPoints = new Transform[allFocusPoints.Length];
        // CFocusPoints[0] = centroArena;
        for (int i = 0; i < allFocusPoints.Length; i++)
        {
            CFocusPoints[i] = allFocusPoints[i].transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = getCenterPoint();
        //this.transform.LookAt(CFocusPoints[0].transform.position, Vector3.up);

    }
    void LateUpdate()
    {
    
    }
    public Vector3 getCenterPoint()
    {

        var bounds = new Bounds(centerPlayers, Vector3.zero);
        for (int i = 0; i < CFocusPoints.Length; i++)
        {
            bounds.Encapsulate(CFocusPoints[i].position);
        }
       // Debug.Log(bounds.center);
        return bounds.center;

    }
}
