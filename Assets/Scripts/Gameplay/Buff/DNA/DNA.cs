
using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class DNA : SpawnedObject
{
    #region Fields
    protected AudioSource AudioSource;
    [SerializeField] protected AudioClip dnaPickup;
    [SerializeField] protected GameObject parts;
    [Header("Rotaing speed")]
    [SerializeField] protected float minRotSpeed = -3;
    [SerializeField] protected float maxRotSpeed = 3;
    [Tooltip("Will be changed in script")][SerializeField] protected float rndSpeed;

    [Space] [Header("Random rotate")]
    [Range(-90, 90)] [SerializeField] protected int minRotPos = -50; 
    [Range(-90, 90)] [SerializeField] protected int  maxRotPos = 50;
    

    #endregion

    #region MonoBehavoir Callbacks

    protected new void Awake()
    {
        base.Awake();
        AudioSource = GameObject.FindGameObjectWithTag("EffectsAudioSource").GetComponent<AudioSource>();
    }
    
    protected void OnEnable()
    {
        rndSpeed = Random.Range(minRotSpeed, maxRotSpeed);
    }

    

    #endregion
}
