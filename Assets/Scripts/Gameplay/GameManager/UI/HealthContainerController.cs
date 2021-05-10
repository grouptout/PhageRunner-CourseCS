
using UnityEngine;

public class HealthContainerController : EventInvoker
{
    #region Fields

    [SerializeField] private ContainerManager healthContainer;
    private Phage phage;

    private bool _started;
    private bool _autoColorChange = true;
    private float _a, _b; // colors rgBA
    #endregion

    #region MonoBehavoir Callbacks

    private void Awake()
    {
        EventManager.AddListener(EventName.StartEvent, HandleStartEvent);
        phage = GameObject.FindGameObjectWithTag("Player").GetComponent<Phage>();
        
        _b = healthContainer.Colors.Liquid.Color.b;
        _a = healthContainer.Colors.Liquid.Color.a;
    }

    private void Update()
    {
        if (_started && _autoColorChange)
        {
            healthContainer.LiquidBehavior.Amount = Mathf.Lerp(healthContainer.LiquidBehavior.Amount, phage.health, Time.deltaTime * 2);

            if (phage.health > .5)
            {
               healthContainer.Colors.Liquid.Color.r = (0.8392157f - healthContainer.LiquidBehavior.Amount * 0.8392157f) * 2;
               healthContainer.Colors.Liquid.Color.g = 0.8392157f;
            }
            else if(phage.health < .5)
            {
                healthContainer.Colors.Liquid.Color.r = 0.8392157f;
                healthContainer.Colors.Liquid.Color.g =  healthContainer.LiquidBehavior.Amount * 0.8392157f * 2;
            }
            else
            {
                healthContainer.Colors.Liquid.Color.r = 0.8392157f;
                healthContainer.Colors.Liquid.Color.g = 0.8392157f;
            }
            
        }
    }

    public void SetColor(Color color)
    {
        _autoColorChange = false;
        healthContainer.Colors.Liquid.Color = color;
    }

    public void ResetColor()
    {
        healthContainer.Colors.Liquid.Color.b = _b;
        healthContainer.Colors.Liquid.Color.a = _a;
        _autoColorChange = true;
    }

    #endregion
    
    #region EventSystem Support

    private void HandleStartEvent()
    {
        _started = true;
    }

    #endregion
}
