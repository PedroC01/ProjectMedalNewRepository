using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MedaHealthSlider : MonoBehaviour
{
    // Start is called before the first frame update
  //  public GameObject medapart;
    public float medaPartEnergy;
    public Slider partEnergySlider;
    public float percentageValue;
    public Gradient gradient;
    public float maxEnergy;
    public Image fill;
    public float current;
    private float next;
    public float sliderAnimationSpeed=1;
    private PlayerHealth ph;
    public int MedaPartNum;
    public GameObject Player;
    private GameObject Medapart;
    public  TMP_Text MedapartEnergyText;
    void Start()
    {
        
            ph=Player.GetComponent<PlayerHealth>();
        this.Medapart = ph.MedaParts[this.MedaPartNum];
        this.medaPartEnergy = ph.MedaParts[this.MedaPartNum].GetComponent<MedaPartScript>().partEnergy;
        //medaPartEnergy= medapart.GetComponent<MedaPartScript>().partEnergy;
        maxEnergy = (int)medaPartEnergy;
        this.partEnergySlider=GetComponent<Slider>();
        this.partEnergySlider.value = Medapart.GetComponent<MedaPartScript>().partEnergy;
        current = (int)maxEnergy;
        next = (int)maxEnergy;
       fill.color= gradient.Evaluate(1f);
        percentageValue = (this.partEnergySlider.value * 100) / maxEnergy;
    }

    // Update is called once per frame
    void Update()
    {
        //  SetEnergy();
        
        next = this.Medapart.GetComponent<MedaPartScript>().partEnergy;
        
      if (percentageValue != next)
        {
            percentageValue-= Time.deltaTime+sliderAnimationSpeed;
            percentageValue = Mathf.Clamp(percentageValue, 0, maxEnergy);
            this.partEnergySlider.value = Mathf.Lerp(percentageValue, next, 1f);
            if (percentageValue == next)
            {
                percentageValue = next;
            }

            this.MedapartEnergyText.text = Mathf.RoundToInt(percentageValue).ToString() + "%";
        }
        

        percentageValue = (this.partEnergySlider.value * 100) / maxEnergy;
        if(65 <= percentageValue)
        {
            fill.color = gradient.Evaluate(1f);
        }
        if (65 >= percentageValue && percentageValue >= 36)
        {
            fill.color = this.gradient.Evaluate(0.5f);
        }
        if (35 >= percentageValue)
        {
            fill.color = this.gradient.Evaluate(0f);
        }
    }
   

   /* public void SetEnergy() {

        next = medapart.GetComponent<MedaPartScript>().partEnergy;
        this.partEnergySlider.value = Mathf.Lerp(current--, next, 0.5f);
        current=next;
        percentageValue = (this.partEnergySlider.value * 100) / maxEnergy;
        if (65 >= percentageValue && percentageValue >= 36)
        {
            fill.color=this.gradient.Evaluate(0.5f);
        }
        if (35 >= percentageValue)
        {
            fill.color=this.gradient.Evaluate(0f);
        }
    }*/
}
