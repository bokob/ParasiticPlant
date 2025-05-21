using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResolutionManager : MonoBehaviour
{
    public TMP_Dropdown resolutionDropdown;
    List<Resolution> resolutionList = new List<Resolution>();   // 해상도 리스트
    int _optimalResolutionIndex = 0;                            // 가장 적합한 해상도 인덱스

    void Start()
    {
        resolutionList.Add(new Resolution { width = 1280, height = 720 });
        resolutionList.Add(new Resolution { width = 1280, height = 800 });
        resolutionList.Add(new Resolution { width = 1440, height = 900 });
        resolutionList.Add(new Resolution { width = 1600, height = 900 });
        resolutionList.Add(new Resolution { width = 1680, height = 1050 });
        resolutionList.Add(new Resolution { width = 1920, height = 1080 });
        resolutionList.Add(new Resolution { width = 1920, height = 1200 });
        resolutionList.Add(new Resolution { width = 2048, height = 1280 });
        resolutionList.Add(new Resolution { width = 2560, height = 1440 });
        resolutionList.Add(new Resolution { width = 2560, height = 1600 });
        resolutionList.Add(new Resolution { width = 2880, height = 1800 });
        resolutionList.Add(new Resolution { width = 3480, height = 2160 });

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        for (int i = 0; i < resolutionList.Count; i++)
        {
            string option = resolutionList[i].width + " x " + resolutionList[i].height;

            // 가장 적합한 해상도에 별표를 표기
            if (resolutionList[i].width == Screen.currentResolution.width && resolutionList[i].height == Screen.currentResolution.height)
            {
                _optimalResolutionIndex = i;
                option += " *";
            }
            options.Add(option);
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = _optimalResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        resolutionDropdown.onValueChanged.AddListener(delegate{
                SetResolution(resolutionDropdown.value);
            });

        // 가장 적합한 해상도로 시작되도록 설정
        SetResolution(_optimalResolutionIndex);
    }

    public void Init()
    {

    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutionList[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);

        Debug.LogWarning(resolutionIndex + " : " + resolution.width + " x " + resolution.height);
    }
}