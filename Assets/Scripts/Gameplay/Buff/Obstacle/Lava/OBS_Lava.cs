
using UnityEngine;

public class OBS_Lava : OBSTACLE
{
    #region Fields

    private bool _isDamaging;

    #endregion

    #region MonoBehavoir Callbacks
    
    private new void Awake()
    {
        base.Awake();
        
        unityEvents.Add(EventName.ObsLavaEvent, new Obs_LavaEvent());
        EventManager.AddInvoker(EventName.ObsLavaEvent, this);
    }

    private void OnEnable()
    {
        _isDamaging = false;
    }
    
    #endregion

    #region Override Methods

    protected override void DestroyThis()
    {
        gameObject.SetActive(false);
        ObjectPool.ReturnLava(gameObject);
    }

    #endregion

    #region MonoBehavoir Callbacks

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !_isDamaging)
        {
            PlayerDebuffed();
            _isDamaging = true;
        }
    }

    #endregion

    #region EventSystem Support

    private void PlayerDebuffed()
    {
        unityEvents[EventName.ObsLavaEvent].Invoke();
    }

    #endregion
}
