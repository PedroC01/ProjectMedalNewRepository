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
    private float current;
    private float next;
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

        next = medapart.GetComponent<MedaPartScript>().partEnergy;
        this.partEnergySlider.value = Mathf.Lerp(current, next, 0.5f);
        current = next;
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

        next = medapart.GetComponent<MedaPartScript>().partEnergy;
        this.partEnergySlider.value = Mathf.Lerp(current, next, 0.5f);
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
    }
}
