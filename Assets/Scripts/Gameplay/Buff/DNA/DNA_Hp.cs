
public class DNA_Hp : DNA
{
    #region MonoBehavoir Callbacks

    private new void Awake()
    {
        base.Awake();
        
        unityEvents.Add(EventName.BuffDnaHpEvent, new Buff_DnaHpEvent());
        EventManager.AddInvoker(EventName.BuffDnaHpEvent, this);
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
        unityEvents[EventName.BuffDnaHpEvent].Invoke();
        DestroyThis();
    }

    protected override void DestroyThis()
    {
        gameObject.SetActive(false);
        ObjectPool.ReturnDnaHp(gameObject);
    }

    #endregion
    
}
