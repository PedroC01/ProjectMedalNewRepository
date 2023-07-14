using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOnUI : MonoBehaviour
{
    public int UINumber;
    // Start is called before the first frame update
    public LockOn Lo;
    public GameObject headTargeted;
    public GameObject legsTargeted;
    public GameObject leftArmTargeted;
    public GameObject rightArmTargeted;

    void Start()
    {
        if (this.UINumber == 1)
        {
            
            Lo = FindObjectOfType<Player1>().GetComponent<LockOn>();
        }
        if (this.UINumber == 2)
        {
           
            Lo = FindObjectOfType<Player2>().GetComponent<LockOn>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Lo.pieceReference==1)
        {
            headTargeted.SetActive(true);
        }
        else
        {
            headTargeted.SetActive(false);
        }
        if (Lo.pieceReference==3)
        {
            leftArmTargeted.SetActive(true);
        }
        else
        {
            leftArmTargeted.SetActive(false);
        }
        if(Lo.pieceReference==2)
        {
            rightArmTargeted.SetActive(true);
        }
        else
        {
            rightArmTargeted.SetActive(false);
        }
        if (Lo.pieceReference==4)
        {
           legsTargeted.SetActive(true);
        }
        else
        {
            legsTargeted.SetActive(false) ;
        }
    }
}
