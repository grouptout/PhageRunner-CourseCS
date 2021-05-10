
using UnityEngine;

public class SpawnedObject : EventInvoker
{
    #region Fields

    protected Transform platformSpawner;
    protected Animator animator;

    #endregion

    #region MonoBehavoir Callbacks

    protected void Awake()
    {
        animator = GetComponent<Animator>();
        platformSpawner = FindObjectOfType<PlatformSpawner>().transform;
    }
    
    protected void Update()
    {
        if (transform.position.x < -platformSpawner.position.x)
        {
            DestroyThis();
        }
    }

    #endregion

    #region Virtual Methods

    protected virtual void DestroyThis()
    { }

    public virtual void PlayerPickedUp()
    { }    

    #endregion
}
