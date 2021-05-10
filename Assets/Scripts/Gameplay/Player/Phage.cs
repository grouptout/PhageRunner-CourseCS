
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Phage : EventInvoker
{
    #region Private Fields
    private Animator _animator;
    private bool _isDead;

    #endregion

    #region Public Fields
    
    [Range(0, 1)] public float health = 1;
    public UnityEvent playerWinSoundEvent;
    #endregion
    
    #region MonoBehaviour Callbacks

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        
        unityEvents.Add(EventName.DieEvent, new DieEvent());
        EventManager.AddInvoker(EventName.DieEvent, this);
        EventManager.AddListener(EventName.StartEvent, HandleStartEvent); 
        EventManager.AddListener(EventName.BossPlayerWinEvent, HandleBossPlayerWinEvent); 
        
        unityEvents.Add(EventName.ShowDieEvent, new ShowDieDialogEvent());
        EventManager.AddInvoker(EventName.ShowDieEvent, this);

    }

    private void Update()
    {
        if (health <= 0 && !_isDead)
        {
            StartCoroutine(Die());
        }
        
        if (Input.GetKeyDown(KeyCode.K))
        {
            health = 0;
        }
        
    }

    #endregion

    #region Private Methods

    private IEnumerator Die()
    {
        _isDead = true;
        _animator.SetTrigger("Die");
        unityEvents[EventName.DieEvent].Invoke();

        yield return new WaitForSeconds(1.5f);
        
        unityEvents[EventName.ShowDieEvent].Invoke();
        Destroy(gameObject);
    }

    #endregion

    #region Event System support

    private void HandleStartEvent()
    {
        transform.Rotate(0, -90, 0);
        GetComponent<Animator>().SetTrigger("Start");
    }

    private void HandleBossPlayerWinEvent()
    {
        playerWinSoundEvent.Invoke();
    }

    #endregion
}
