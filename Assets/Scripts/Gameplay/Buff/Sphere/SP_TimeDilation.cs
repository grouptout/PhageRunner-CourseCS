
public class SP_TimeDilation : SPHERE
{
    #region MonoBehavoir Callbacks

    private new void Awake()
    {
        base.Awake();
        
        unityEvents.Add(EventName.SpTimeDilationEvent, new Sp_TimeDilationEvent());
        EventManager.AddInvoker(EventName.SpTimeDilationEvent, this);
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
        unityEvents[EventName.SpTimeDilationEvent].Invoke();
        DestroyThis();
    }

    protected override void DestroyThis()
    {
        gameObject.SetActive(false);
        ObjectPool.ReturnTD(gameObject);
    }

    #endregion
}
