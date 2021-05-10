
public class SP_TimeAcceleration : SPHERE
{
    #region MonoBehavoir Callbacks

    private new void Awake()
    {
        base.Awake();
        
        unityEvents.Add(EventName.SpTimeAccelerationEvent, new Sp_TimeAccelerationEvent());
        EventManager.AddInvoker(EventName.SpTimeAccelerationEvent, this);
    }
    
    private new void Update()
    {
        base.Update();
    }

    #endregion

    #region Override Methods

    public override void PlayerPickedUp()
    {
        animator.Play("DelSp");
        StartCoroutine(AnimEnd());
    }
    
    protected override void DoAfterAnim()
    {
        unityEvents[EventName.SpTimeAccelerationEvent].Invoke();
        DestroyThis();
    }
    
    protected override void DestroyThis()
    {
        gameObject.SetActive(false);
        ObjectPool.ReturnTA(gameObject);
    }

    #endregion
}
