
using Random = UnityEngine.Random;

public class DNA_MultiXP : DNA
{
    #region MonoBehavoir Callbacks

    private new void OnEnable()
    {
        base.OnEnable();
        parts.transform.Rotate(Random.Range(minRotPos, maxRotPos), 0, Random.Range(minRotPos, maxRotPos), 0); 
    }

    private new void Awake()
    {
        base.Awake();
        unityEvents.Add(EventName.BuffDnaXpEvent, new Buff_DnaXpEvent());
        EventManager.AddInvoker(EventName.BuffDnaXpEvent, this);
    }

    private new void Update()
    {
        parts.transform.Rotate(0, rndSpeed, 0);
        base.Update();
    }

    #endregion

    #region Override Methods

    public override void PlayerPickedUp()
    {
        AudioSource.PlayOneShot(dnaPickup);
        unityEvents[EventName.BuffDnaXpEvent].Invoke();
        DestroyThis();
    }
    
    protected override void DestroyThis()
    {
        gameObject.SetActive(false);
        ObjectPool.ReturnDnaXp(gameObject);
    }
    

    #endregion

}
