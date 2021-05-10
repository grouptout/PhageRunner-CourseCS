using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffController : MonoBehaviour
{
    #region Fields

    [SerializeField] private StartVariables startVariables;
    [SerializeField] private HealthContainerController healthContainerController;
    [SerializeField] private GameObject buffWidgets;

    [NonSerialized] public float debuffLavaDamagePerSecond;
    [NonSerialized] public float buffHealPerSecond;
    public bool canTakeDmg = true;
    
    private LevelController _levelController;
    private GameObject _extraJumpWidget;
    private bool _canHeal = true;
    
    private Dictionary<string, int> _coroutineCounter = new Dictionary<string, int>();
    

    #endregion
    
    #region MonoBehavoir Callbacks

    private void Awake()
    {
        _levelController = gameObject.GetComponent<LevelController>();
        
        debuffLavaDamagePerSecond = startVariables.debuffLavaDamagePerSecond;
        buffHealPerSecond = startVariables.buffHealPerSecond;
    }

    private void Start()
    {
        EventManager.AddListener(EventName.BuffDnaXpEvent, HandleBuffDnaXpEvent);
        EventManager.AddListener(EventName.BuffDnaHpEvent, HandleBuffDnaHpEvent);
        EventManager.AddListener(EventName.BuffDnaInvEvent, HandleBuffDnaInvEvent);
        EventManager.AddListener(EventName.SpDoubleJumpEvent, HandleSpDoubleJumpEvent);
        EventManager.AddListener(EventName.SpTimeAccelerationEvent, HandleSpTimeAccelerationEvent);
        EventManager.AddListener(EventName.SpTimeDilationEvent, HandleSpTimeDilationEvent);
        EventManager.AddListener(EventName.ObsLavaEvent, HandleObsLavaEvent);
        EventManager.AddListener(EventName.DecreaseExtraJumpWidgetEvent, HandleDecreaseExtraJumpWidgetEvent);
        
        
        _coroutineCounter.Add("BuffDnaXpCoroutine", 0);
        _coroutineCounter.Add("BuffDnaHpCoroutine", 0);
        _coroutineCounter.Add("BuffDnaInvCoroutine", 0);
        _coroutineCounter.Add("DebuffLavaCoroutine", 0);
    }
    
    private void Update()
    {
        //buffs
            //heals
                if (_canHeal)
                {
                    Healing();
                }
                

        
        //debuffs
            //dmg
                if (canTakeDmg)
                {
                    LavaDmg();
                }
        
    }

    #endregion
    
    #region Buffs/Debuffs Methods

    private void Healing()
    {
        if (_coroutineCounter["BuffDnaHpCoroutine"] != 0 && _levelController.phageScript.health < 1)
        {
            _levelController.phageScript.health += Time.deltaTime * buffHealPerSecond / 100 * _coroutineCounter["BuffDnaHpCoroutine"];
        }
    }
    
    private void LavaDmg()
    {
        if (_coroutineCounter["DebuffLavaCoroutine"] != 0)
        {
            _levelController.phageScript.health -= Time.deltaTime * debuffLavaDamagePerSecond / 100 * _coroutineCounter["DebuffLavaCoroutine"];
        }
    }

    #endregion
    
    #region BuffEvents Support

    private void HandleBuffDnaXpEvent()
    {
        StartCoroutine(BuffDnaXpCoroutine());
        _coroutineCounter["BuffDnaXpCoroutine"]++;
    } 
    private void HandleBuffDnaHpEvent()
    {
        StartCoroutine(BuffDnaHpCoroutine());
        _coroutineCounter["BuffDnaHpCoroutine"]++;
    }
    private void HandleBuffDnaInvEvent()
    {
        StartCoroutine(BuffDnaInvCoroutine());
        _coroutineCounter["BuffDnaInvCoroutine"]++;
    }

    private void HandleObsLavaEvent()
    {
        StartCoroutine(DebuffLavaCoroutine());
        _coroutineCounter["DebuffLavaCoroutine"]++;
    }

    private void HandleSpTimeAccelerationEvent()
    {
        StartCoroutine(SpTimeAccelerationCoroutine());
    }

    private void HandleSpTimeDilationEvent()
    {
        StartCoroutine(SpTimeDilationCoroutine());
    }
    
    private void HandleSpDoubleJumpEvent()
    {
        if (_levelController.playerMovement.extraJumpsAvailable < startVariables.maxExtraJumps)
        {
            if (_levelController.playerMovement.extraJumpsAvailable <= 0)
            {
                _extraJumpWidget = Instantiate(Resources.Load<GameObject>("Prefabs/UI/Gameplay/BuffWidgets/EXTRA_JUMP"), buffWidgets.transform, false);
            }
            
            _levelController.playerMovement.extraJumpsAvailable++;
            _extraJumpWidget.GetComponent<StaticWidgetScript>().countText.text = $"{_levelController.playerMovement.extraJumpsAvailable} | {startVariables.maxExtraJumps}";
        }
        
    }

    private void HandleDecreaseExtraJumpWidgetEvent()
    {
        if (_levelController.playerMovement.extraJumpsAvailable <= 0)
        {
            //Destroy(_extraJumpWidget.gameObject);
            _extraJumpWidget.GetComponent<StaticWidgetScript>().countText.text = $"{_levelController.playerMovement.extraJumpsAvailable} | {startVariables.maxExtraJumps}";
            StartCoroutine(_extraJumpWidget.GetComponent<StaticWidgetScript>().DestroyWidgetWithAnimation());
            _extraJumpWidget = null;
        }
        else
        {
            _extraJumpWidget.GetComponent<StaticWidgetScript>().countText.text = $"{_levelController.playerMovement.extraJumpsAvailable} | {startVariables.maxExtraJumps}";
        }
    }

    #endregion    
    
    #region Buff Coroutines

    private IEnumerator BuffDnaXpCoroutine() //doesnt stack
    {
        _levelController.scoreMultiplier = 2;
        
       Instantiate(Resources.Load<GameObject>("Prefabs/UI/Gameplay/BuffWidgets/DOUBLING_SCORE"), buffWidgets.transform, false);

        yield return new WaitForSeconds(startVariables.buffDnaXpDuration);
        
        _coroutineCounter["BuffDnaXpCoroutine"]--;

        if (_coroutineCounter["BuffDnaXpCoroutine"] == 0)
        {
            _levelController.scoreMultiplier = 1;
        }
    }
    
    
    private IEnumerator BuffDnaHpCoroutine() //stacks
    {
        Instantiate(Resources.Load<GameObject>("Prefabs/UI/Gameplay/BuffWidgets/HEALING"), buffWidgets.transform, false);

        yield return new WaitForSeconds(startVariables.buffDnaHpDuration);
        
        _coroutineCounter["BuffDnaHpCoroutine"]--;
        
    }
    private IEnumerator BuffDnaInvCoroutine() //doesnt stack
    {
        canTakeDmg = false;
        
        var widget = Instantiate(Resources.Load<GameObject>("Prefabs/UI/Gameplay/BuffWidgets/INVULNERABILITY"), buffWidgets.transform, false);
        healthContainerController.SetColor(widget.GetComponentInChildren<Text>().color);
        
        yield return new WaitForSeconds(startVariables.buffDnaInvDuration);
        
        _coroutineCounter["BuffDnaInvCoroutine"]--;
        
        if (_coroutineCounter["BuffDnaInvCoroutine"] == 0)
        {
            healthContainerController.ResetColor();
            canTakeDmg = true;
        }
    }

    private IEnumerator SpTimeDilationCoroutine() //stacks
    {
        Time.timeScale /= 2;
        Instantiate(Resources.Load<GameObject>("Prefabs/UI/Gameplay/BuffWidgets/TIME_DILATION"), buffWidgets.transform, false);
        yield return new WaitForSecondsRealtime(startVariables.spTimeDilationDuration);
        Time.timeScale *= 2;
    }
    
    #endregion

    #region Debuff Coroutines

    private IEnumerator DebuffLavaCoroutine() //stacks
    {
        Instantiate(Resources.Load<GameObject>("Prefabs/UI/Gameplay/BuffWidgets/DAMAGING_LAVA"), buffWidgets.transform, false);

        yield return new WaitForSeconds(startVariables.debuffLavaDuration);
        
        _coroutineCounter["DebuffLavaCoroutine"]--;
        
    }
    
    private IEnumerator SpTimeAccelerationCoroutine() //stacks
    {
        Time.timeScale *= 2;
        Instantiate(Resources.Load<GameObject>("Prefabs/UI/Gameplay/BuffWidgets/TIME_ACCELERATION"), buffWidgets.transform, false);
        yield return new WaitForSecondsRealtime(startVariables.spTimeAccelerationDuration);
        Time.timeScale /= 2;
    }

    #endregion
    
}
