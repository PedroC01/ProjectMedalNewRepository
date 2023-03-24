using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MedaHealthSlider : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject medapart;
    public float medaPartEnergy;
    public Slider partEnergySlider;
    public float percentageValue;
    public Gradient gradient;
    public float maxEnergy;
    public Image fill;
    void Start()
    {
        medaPartEnergy= medapart.GetComponent<MedaPartScript>().partEnergy;
        maxEnergy = medaPartEnergy;
        this.partEnergySlider=GetComponent<Slider>();
        this.partEnergySlider.value = medapart.GetComponent<MedaPartScript>().partEnergy;
       fill.color= gradient.Evaluate(1f);
    }

    // Update is called once per frame
    void Update()
    {
        //  SetEnergy();
      
        this.partEnergySlider.value = medapart.GetComponent<MedaPartScript>().partEnergy;
        percentageValue = (this.partEnergySlider.value * 100) / maxEnergy;
        if (65 >= percentageValue && percentageValue >= 36)
        {
            fill.color = this.gradient.Evaluate(0.5f);
        }
        if (35 >= percentageValue)
        {
            fill.color = this.gradient.Evaluate(0f);
        }
    }
   

    public void SetEnergy() {

        this.partEnergySlider.value = medapart.GetComponent<MedaPartScript>().partEnergy;
        percentageValue = (this.partEnergySlider.value * 100) / maxEnergy;
        if (65 >= percentageValue && percentageValue >= 36)
        {
            fill.color=this.gradient.Evaluate(0.5f);
        }
        if (35 >= percentageValue)
        {
            fill.color=this.gradient.Evaluate(0f);
        }
    }
}
