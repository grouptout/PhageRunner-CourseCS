
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    #region Fields

    [SerializeField] private Dropdown graphicsDropDown;
    [SerializeField] private Toggle fpsToggle;

    #endregion

    #region MonoBehavoir Callbacks

    private void Awake()
    {
        if (PlayerPrefs.HasKey("Graphics"))
        {
            SetQuality(PlayerPrefs.GetInt("Graphics"));
        }
    }

    private void OnEnable()
    {
        // graphics
        graphicsDropDown.ClearOptions();
        graphicsDropDown.AddOptions(QualitySettings.names.ToList());
        graphicsDropDown.value = QualitySettings.GetQualityLevel();

        // fps
        if (!PlayerPrefs.HasKey("fpsOn"))
        {
            PlayerPrefs.SetInt("fpsOn", 0);
        }
        fpsToggle.isOn = PlayerPrefs.GetInt("fpsOn") == 1;
    }
    

    #endregion

    #region Public Methods

    public void SetQuality(Int32 value)
    {
        QualitySettings.SetQualityLevel(value);
        PlayerPrefs.SetInt("Graphics", value);
    }

    public void ToggleFps(bool fpsOn)
    {
        PlayerPrefs.SetInt("fpsOn", fpsOn ? 1 : 0);
    }

    #endregion
}
