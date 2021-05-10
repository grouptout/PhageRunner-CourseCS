using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaticWidgetScript : MonoBehaviour
{
    private Animator _animator; 
    public Text countText;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }
    
    
    public IEnumerator DestroyWidgetWithAnimation()
    {
        _animator.Play("Out");
        yield return new WaitForSeconds(_animator.runtimeAnimatorController.animationClips[0].length);
        Destroy(gameObject);
    }
}
