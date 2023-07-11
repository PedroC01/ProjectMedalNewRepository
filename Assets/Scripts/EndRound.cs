using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndRound : MonoBehaviour
{
    public float timeToEndRound;
    public float currentTime;//Time that will appear on battle clock
    [SerializeField]
    public TMP_Text currentTimeText;
    public bool battle;

    void Start()
    {
        currentTime = timeToEndRound;
        battle = true;
        StartCoroutine(TimeToEndRound());
    }

  IEnumerator TimeToEndRound()
    {
        while (battle)
        {
            currentTime=Mathf.Clamp(currentTime-Time.deltaTime,0,timeToEndRound);
           
            currentTime = (int)currentTime;
            currentTimeText.text = currentTime.ToString();
            if (currentTime <= 0)
            {
              //  Application.Quit();
            }
            yield return new WaitForSecondsRealtime(1);
        }
    
    }
}
