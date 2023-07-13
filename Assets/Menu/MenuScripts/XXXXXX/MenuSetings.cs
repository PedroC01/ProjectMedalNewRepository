using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem.UI;
using TMPro;
using UnityEngine.UI;
using System;

public class MenuSetings : MonoBehaviour
{
    public static MenuSetings Instance;
    private void Awake()
    {
        Instance = this;
    }


    [Space(5)]
    public Toggle fullScreenToogle;
    public Slider menuVolumeSlider;
    public TMP_Dropdown qualityLevels;
    Resolution[] resolutions;
    public TMP_Dropdown dropdownResolution;
    //
    public Button bt_GoBack;
    public Button bt_Apply;

 

    private void Start()
    {
        StartQualityShow();
        // Change the first selected object in the MenuSelectedObject method
        //StartCoroutine(ChangeFirstSelectedObject());

        MenuSelectedObject(fullScreenToogle.gameObject);//troca 1º obj selected. //aqui nao faz nada?!?!?!?!?!?!?!?!?!?!?!?
        ResolutionInit();
        bt_GoBack.onClick.AddListener(() =>
        {
            MenuSelectedObject(MainMenus.instance.bt_VersusMode.gameObject);//troca 1º obj selected.
            //Turn this painel off, and show again 
            MainMenus.instance.SwitchPanels(MainMenus.instance.panelSettings, MainMenus.instance.panelStarMenuMode);
        });

        bt_Apply.onClick.AddListener(() =>
        {
            OnApplyButtonClick();
        });
    }


    public void FristSelectedSettings()
    {
        MenuSelectedObject(fullScreenToogle.gameObject);//troca 1º obj selected.
    }

    //Quando entramos em settings queremos um bt/ui selecionado para navegar.------------------------------------------------
    void MenuSelectedObject(GameObject firstSelected)
    {
        FindObjectOfType<MultiplayerEventSystem>().SetSelectedGameObject(firstSelected);
    }

    //------------------------------------------------------------------------------------------------
    //Volume:
    //Mudar o Volume de Algo com base em um valor, Ex index ou arrows up e down, a gosto...
    public void AudioSourceVolume(AudioSource audioSource, float volume)
    {
        audioSource.volume = volume;
    }

    //Mudar o volume do AudioMixer:
    public void SetVolume(float volume)
    {
       MainMenus.instance.audioMixer.SetFloat("volume", volume);
    }


    //------------------------------------------------------------------------------------------------
    //Video quality com UI DropDown 
    //edit-ProjectSettings-Quality:
    // Define your custom quality level names
    private string[] qualityLevelNames = { "High", "Medium", "Low" };//Nomes nossos
    private void StartQualityShow()
    {
        qualityLevels.ClearOptions();
        qualityLevels.AddOptions(new List<string>(qualityLevelNames));

        int qualityIndex = QualitySettings.GetQualityLevel();
        qualityLevels.value = qualityIndex;
        qualityLevels.RefreshShownValue();
    }
    public void SetVideoQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        qualityLevels.captionText.text = qualityLevelNames[qualityIndex];
    }



    //------------------------------------------------------------------------------------------------
    //FullScreen on/Off: Toogle []
    public void ToggleFullscreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }

    //------------------------------------------------------------------------------------------------
    //Resoluções:
    public void ResolutionInit()
    {
        resolutions = Screen.resolutions;
        dropdownResolution.ClearOptions();
        int currentResolutionIndex = 0;
        List<string> resolutionListString = new List<string>();

        // Add your logic here to filter and prioritize the resolutions based on your criteria
        // For example, you can prioritize resolutions with a specific aspect ratio or size

        // Sample logic to prioritize commonly used resolutions
        int maxResolutionsToShow = 5; // Number of resolutions to show
        List<Resolution> filteredResolutions = new List<Resolution>();

        // Sort resolutions based on width (descending order)
        Array.Sort(resolutions, (x, y) => y.width.CompareTo(x.width));

        // Add the most commonly used resolutions to the filtered list
        for (int i = 0; i < Mathf.Min(maxResolutionsToShow, resolutions.Length); i++)
        {
            filteredResolutions.Add(resolutions[i]);
        }

        // Create the dropdown options based on the filtered resolutions
        for (int i = 0; i < filteredResolutions.Count; i++)
        {
            string option = filteredResolutions[i].width + " x " + filteredResolutions[i].height;
            resolutionListString.Add(option);

            // Find the index of the current resolution
            if (filteredResolutions[i].width == Screen.currentResolution.width &&
                filteredResolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        dropdownResolution.AddOptions(resolutionListString);
        dropdownResolution.value = currentResolutionIndex;
        dropdownResolution.RefreshShownValue();
    }
    public void UpdateResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }


    //------------------------------------------------------------------------------------------------
    //Apply and Save Settings:
    private void OnApplyButtonClick()
    {
        SaveSettings();
        // Apply the settings
        ApplySettings();
    }
    private void SaveSettings()
    {
        // Save the settings using PlayerPrefs or any other preferred method

        // Save fullscreen toggle state
        PlayerPrefs.SetInt("Fullscreen", fullScreenToogle.isOn ? 1 : 0);

        // Save video quality index
        PlayerPrefs.SetInt("VideoQuality", qualityLevels.value);

        // Save audio volume
        PlayerPrefs.SetFloat("Volume", MainMenus.instance.audioSource.volume);
    }
    private void ApplySettings()
    {
        // Apply the settings based on the updated values

        AudioSourceVolume(MainMenus.instance.audioSource, PlayerPrefs.GetFloat("Volume", 0.5f)); // Example: Set audio source volume from saved value
        SetVolume(PlayerPrefs.GetFloat("Volume", 0.7f)); // Example: Set audio mixer volume from saved value
        SetVideoQuality(PlayerPrefs.GetInt("VideoQuality", 0)); // Example: Set video quality from saved value
        ToggleFullscreen(); // Example: Toggle fullscreen mode
    }

}
