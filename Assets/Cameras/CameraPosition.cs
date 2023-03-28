using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraPosition : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform rbp1;
    public Transform rbp2;
    public Transform centroArena;
    public Transform[] CFocusPoints;
   
    private Vector3 centerPlayers;
    private Camera camera;
    public Vector3 offset;
    public float XOffset;
    public float YOffset;
    public float ZOffset;




    public Vector3 testee;




    private float savZoffset;
    private float savXoffset;
    public float minDistance;
    private float xMin, xMax, yMin, yMax,zMin,zMax;
    void Awake()
    {
        GameObject[] allFocusPoints = GameObject.FindGameObjectsWithTag("Player");
        CFocusPoints = new Transform[allFocusPoints.Length];
       // CFocusPoints[0] = centroArena;
        for(int i= 0; i < allFocusPoints.Length; i++)
        {
            CFocusPoints[i] = allFocusPoints[i].transform;
        }
        savXoffset = XOffset;
        savZoffset = ZOffset;    
        this.camera = GetComponent<Camera>();
        centerPlayers = ((rbp1.position + rbp2.position) / 2);
    }

    // Update is called once per frame
    void Update()
    {



        //  centerPlayers =((rbp1.position + rbp2.position) / 2);
        // this.camera.transform.position = new Vector3(centerPlayers.x + offset.x, centerPlayers.y + offset.y, centerPlayers.z + offset.z);
        // this.camera.transform.LookAt(centerPlayers, Vector3.up);
       
    

    }

    private void LateUpdate()
    {
        xMin = xMax = CFocusPoints[0].position.x;
        yMin = yMax = CFocusPoints[0].position.y;
        for (int i = 1; i < CFocusPoints.Length; i++)
        {
            if (CFocusPoints[i].position.x < xMin)
            {
                xMin = CFocusPoints[i].position.x;
            }
            if (CFocusPoints[i].position.x > xMax)
            {
                xMax = CFocusPoints[i].position.x;
            }
            if (CFocusPoints[i].position.y < yMin)
            {
                yMin = CFocusPoints[i].position.y;
            }
            if (CFocusPoints[i].position.y < yMax)
            {
                yMax = CFocusPoints[i].position.y;
            }
            if (CFocusPoints[i].position.z < zMin)
            {
                zMin = CFocusPoints[i].position.z;
            }
            if (CFocusPoints[i].position.z > zMax)
            {
                zMax = CFocusPoints[i].position.z;
            }
        }
        float xMiddle = (xMin + xMax) / 2;
        float yMiddle = (yMin + yMax) / 2;
        float zMiddle = (zMin + zMax) / 2;
        float distance = Mathf.Sqrt(((xMax - xMin)*(xMax-xMin))+((zMax - zMin)*(zMax-zMin)));
        if (distance < minDistance)
        {
            distance = minDistance;
        }
        //if (xMiddle > zMiddle)
        //{
        //    XOffset = 0;
        //    ZOffset = savZoffset;
        //}
        //if (xMiddle < zMiddle)
        //{
        //    ZOffset = 0;
        //    XOffset= savXoffset;
        //}
           
       

       // this.camera.transform.LookAt(getCenterPoint(), Vector3.up);
        transform.position = new Vector3(xMiddle+XOffset, yMiddle + YOffset, zMiddle + ZOffset);
    }
    void OnDrawGizmosSelected()
    {
      
        
            // Draws a blue line from this transform to the target
            Gizmos.color = Color.blue;
         //   Gizmos.DrawLine(getCenterPoint(),transform.position);
    }



       
}
