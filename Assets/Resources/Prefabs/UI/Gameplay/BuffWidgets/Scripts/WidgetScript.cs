using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class WidgetScript : MonoBehaviour
{
    [SerializeField] private StartVariables startVariables;
    [SerializeField] private bool secondsAreRealtime;
    [SerializeField] private Image amount;

    private float _duration;
    private Animator _animator;   
    private float _timeElapsed;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        
        if (gameObject.name.Contains("DOUBLING_SCORE"))
        {
            _duration = startVariables.buffDnaXpDuration;
        }
        else if (gameObject.name.Contains("HEALING"))
        {
            _duration = startVariables.buffDnaHpDuration;
        }
        else if (gameObject.name.Contains("INVULNERABILITY"))
        {
            _duration = startVariables.buffDnaInvDuration;
        }
        else if (gameObject.name.Contains("DAMAGING_LAVA"))
        {
            _duration = startVariables.debuffLavaDuration;
        }
        else if (gameObject.name.Contains("TIME_DILATION"))
        {
            _duration = startVariables.spTimeDilationDuration;
        }
        else if (gameObject.name.Contains("TIME_ACCELERATION"))
        {
            _duration = startVariables.spTimeAccelerationDuration;
        }
    }
    

    private void Update()
    {
        if (secondsAreRealtime)
        {
            _timeElapsed += Time.unscaledDeltaTime;
        }
        else
        {
            _timeElapsed += Time.deltaTime;
        }
        
        amount.fillAmount = 1 - _timeElapsed / _duration;

        if (_timeElapsed >= _duration)
        {
            StartCoroutine(DestroyWidgetWithAnimation());
        }
    }

    private IEnumerator DestroyWidgetWithAnimation()
    {
        _animator.Play("Out");
        yield return new WaitForSeconds(_animator.runtimeAnimatorController.animationClips[0].length);
        Destroy(gameObject);
    }
}
