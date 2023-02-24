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
    void Start()
    {
        medaPartEnergy= medapart.GetComponent<MedaPartScript>().partEnergy;
        this.partEnergySlider=GetComponent<Slider>();
        this.partEnergySlider.value = medapart.GetComponent<MedaPartScript>().partEnergy;
    }

    // Update is called once per frame
    void Update()
    {
        //  SetEnergy();
        this.partEnergySlider.value = medapart.GetComponent<MedaPartScript>().partEnergy;
    }
   

    public void SetEnergy() {

        this.partEnergySlider.value = medapart.GetComponent<MedaPartScript>().partEnergy;
       
    }
}
