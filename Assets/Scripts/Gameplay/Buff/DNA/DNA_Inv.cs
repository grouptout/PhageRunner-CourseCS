
using UnityEngine;

public class DNA_Inv : DNA
{
    #region MonoBehavoir Callbacks

    private new void Awake()
    {
        base.Awake();
        unityEvents.Add(EventName.BuffDnaInvEvent, new Buff_DnaInvEvent());
        EventManager.AddInvoker(EventName.BuffDnaInvEvent, this);
    }
    private new void OnEnable()
    {
        base.OnEnable();
    }
    private new void Update()
    {
        parts.transform.Rotate(0, 0, rndSpeed);
        base.Update();
    }
    

    #endregion

    #region Override Methods

    public override void PlayerPickedUp()
    {
        AudioSource.PlayOneShot(dnaPickup);
        unityEvents[EventName.BuffDnaInvEvent].Invoke();
        DestroyThis();
    }

    protected override void DestroyThis()
    {
        gameObject.SetActive(false);
        ObjectPool.ReturnDnaInv(gameObject);
    }
    
    #endregion
}
