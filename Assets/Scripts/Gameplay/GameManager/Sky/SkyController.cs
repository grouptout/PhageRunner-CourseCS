using UnityEngine;
using Random = UnityEngine.Random;
using RenderSettings = UnityEngine.RenderSettings;

public class SkyController : EventInvoker
{
    #region Fields

    [SerializeField] private float rotationSpeed = 5;
    [SerializeField] private Light directionalLight;
    [SerializeField] private Material[] skyBoxes;
    [SerializeField] private Color[] lightColors;
    private bool _started;
    
    #endregion

    #region MonoBehavoir Callbacks

    private void Awake()
    {
        var rndSky = Random.Range(0, skyBoxes.Length);
        RenderSettings.skybox = skyBoxes[rndSky];
        directionalLight.color = lightColors[rndSky];
        
        EventManager.AddListener(EventName.StartEvent, HandleStartEvent);
        EventManager.AddListener(EventName.DieEvent, HandleDieEvent);
    }
    
    private void Update()
    {
        if (_started)
        {
            RenderSettings.skybox.SetFloat("_Rotation", Time.time * rotationSpeed); 
        }
         
    }

    #endregion

    #region Event System Support

    private void HandleStartEvent()
    {
        _started = true;
    }
    
    private void HandleDieEvent()
    {
        _started = false;
    }

    #endregion
}
