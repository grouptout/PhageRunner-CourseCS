using System;
using System.Collections;
using UnityEngine;

public class SPHERE : SpawnedObject
{
    #region Fields

    [SerializeField] protected AudioClip spherePickup, sphereActivated;
    protected AudioSource AudioSource;
    #endregion
    
    #region MonoBehavoir Callbacks

    protected new void Awake()
    {
        base.Awake();
        AudioSource = GameObject.FindGameObjectWithTag("EffectsAudioSource").GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerPickedUp();
        }
    }

    #endregion

    #region Protected Methods

    protected IEnumerator AnimEnd()
    {
        AudioSource.PlayOneShot(spherePickup);
        yield return new WaitForSeconds(animator.runtimeAnimatorController.animationClips[0].length);
        AudioSource.PlayOneShot(sphereActivated);
        DoAfterAnim();

    }

    #endregion

    #region Virtual Methods

    protected virtual void DoAfterAnim()
    { }

    #endregion
    
}
