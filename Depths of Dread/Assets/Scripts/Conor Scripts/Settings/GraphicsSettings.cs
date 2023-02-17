using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering.PostProcessing;

public class GraphicsSettings : MonoBehaviour
{
    public TMP_Dropdown QualityDropdown,ResolutionDropdown;
    public Slider BrightnessSlider;
    public PostProcessVolume PP;

    private ColorGrading cg;
    private Resolution[] resolutions;

    private void Start()
    {
        int qualityIndex = PlayerPrefs.GetInt("UnityGraphcsQuality");
        QualitySettings.SetQualityLevel(qualityIndex);
        QualityDropdown.value = qualityIndex;

        resolutions = Screen.resolutions;
        ResolutionDropdown.ClearOptions();
        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        foreach (Resolution resolution in resolutions)
        {
            string option = resolution.width + "x" + resolution.height;
            
            options.Add(option);

            if(!(resolution.width == Screen.currentResolution.width && resolution.height == Screen.currentResolution.height))
            { 
                currentResolutionIndex+=1;
            }
        }
        ResolutionDropdown.AddOptions(options);

        int resolutionIndex = PlayerPrefs.GetInt("UnityResolution");
        if(resolutionIndex==0)
        {
            ResolutionDropdown.value = currentResolutionIndex;
        }
        else
        {
            ResolutionDropdown.value = resolutionIndex;
        }
        ResolutionDropdown.RefreshShownValue();

        float brightness = PlayerPrefs.GetInt("GameBrightness");
        PP.profile.TryGetSettings(out cg);
        cg.postExposure.value = brightness;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        PlayerPrefs.SetInt("UnityResolution", resolutionIndex);
    }
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("UnityGraphcsQuality", qualityIndex);
    }

    private void Update()
    {
        PP.profile.TryGetSettings(out cg);
        cg.postExposure.value = BrightnessSlider.value;
        PlayerPrefs.SetFloat("GameBrightness",cg.postExposure.value);

    }
    public void SetBrightness()
    {
        
    }
}
