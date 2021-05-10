
public class SP_DoubleJump : SPHERE
{
    #region MonoBehavoir Callbacks

    private new void Awake()
    {
        base.Awake();
        
        unityEvents.Add(EventName.SpDoubleJumpEvent, new Sp_DoubleJumpEvent());
        EventManager.AddInvoker(EventName.SpDoubleJumpEvent, this);
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
        unityEvents[EventName.SpDoubleJumpEvent].Invoke();
        DestroyThis();
    }

    protected override void DestroyThis()
    {
        gameObject.SetActive(false);
        ObjectPool.ReturnDJ(gameObject);
    }

    #endregion
}
