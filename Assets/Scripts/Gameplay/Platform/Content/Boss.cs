
using UnityEngine;
using Random = UnityEngine.Random;

public class Boss : EventInvoker
{
    #region Fields

    [SerializeField] private ClampText clampText;
    [SerializeField] private StartVariables startVariables;
    private bool _yetDamaged;
    private float _damage;
    private Animator _animator;
    private BuffController _buffController;
    private Transform _platformSpawnerTransform;
    private PlatformSpawner _platformSpawnerScr;
    

    #endregion

    #region MonoBehavoir Callbacks

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _buffController = GameObject.FindGameObjectWithTag("GameManager").GetComponent<BuffController>();
        _platformSpawnerTransform = FindObjectOfType<PlatformSpawner>().transform;
        _platformSpawnerScr = _platformSpawnerTransform.gameObject.GetComponent<PlatformSpawner>();
        
        unityEvents.Add(EventName.BossPlayerLoseEvent, new StartEvent());
        unityEvents.Add(EventName.BossPlayerWinEvent, new StartEvent());
        EventManager.AddInvoker(EventName.BossPlayerLoseEvent, this);
        EventManager.AddInvoker(EventName.BossPlayerWinEvent, this);
    }
    
    private void Start()
    {
        _damage = Random.Range(startVariables.minBossDmg, startVariables.maxBossDmg);
        transform.localScale *= Random.Range(1f, 2.5f);
        _damage /= 100;
        clampText.SetText($"{_damage * 100:N0}");
    }
    private void Update()
    {
        if (transform.position.x < -_platformSpawnerTransform.position.x || transform.position.y + 100 < _platformSpawnerScr.currentPlatforms[0].transform.position.y)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (!_yetDamaged && other.collider.CompareTag("Player"))
        {
            _yetDamaged = true;
            var lastPlayerHp = other.gameObject.GetComponent<Phage>().health;

            if (_buffController.canTakeDmg)
            {
                other.gameObject.GetComponent<Phage>().health -= _damage;
            }
            _damage -= lastPlayerHp;
            clampText.SetText($"{_damage * 100:N0}");

            if (other.gameObject.GetComponent<Phage>().health <= 0)
            {
                unityEvents[EventName.BossPlayerLoseEvent].Invoke();
            }
            else
            {
                unityEvents[EventName.BossPlayerWinEvent].Invoke();
                _animator.Play("Die");
                other.gameObject.GetComponent<Phage>().health = Mathf.Min(1, other.gameObject.GetComponent<Phage>().health + startVariables.bossRewardHp / 100);
            }
        }
    }

    #endregion
}
