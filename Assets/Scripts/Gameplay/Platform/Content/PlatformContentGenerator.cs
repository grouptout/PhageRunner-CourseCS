
using UnityEngine;
using Random = UnityEngine.Random;

public class PlatformContentGenerator : MonoBehaviour
{
    #region Fields

    [Header("Initialize")]
    [SerializeField] private StartVariables startVariables;
    [SerializeField] private InfoWidgetsController infoWidgetsController;
    [SerializeField] private UIColors uiColors;
    [Space]
    
    [Header("current contentChances")]
    public float dnaXpChance;
    public float dnaHpChance;
    public float dnaInvChance;
    public float lavaChance;
    public float doubleJumpChance;
    public float timeDilationChance;
    public float timeAccelerationChance;
    
    private PlayerMovement _playerMovement;
    private PlatformSpawner _platformSpawner;
    public bool wannaGenerateBoss;

    #endregion

    #region MonoBehavoir Callbacks

    private void Awake()
    {
        dnaXpChance = startVariables.dnaXpChance;
        dnaHpChance = startVariables.dnaHpChance;
        dnaInvChance = startVariables.dnaInvChance;
        lavaChance = startVariables.lavaChance;
        doubleJumpChance = startVariables.doubleJumpChance;
        timeDilationChance = startVariables.timeDilationChance;
        timeAccelerationChance = startVariables.timeAccelerationChance;
    }

    private void Start()
    {
        _playerMovement = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
        _platformSpawner = GetComponent<PlatformSpawner>();
    }
    
    #endregion

    #region Public Methods

    public void Generate(GameObject platform)
    {
        //BUFFS
            //dna
                if (Random.Range(0, 100) < dnaXpChance)
                {
                    SpawnObj(platform, PooledObjectName.DnaXp, false);
                }
                
                if (Random.Range(0, 100) < dnaHpChance)
                {
                    SpawnObj(platform, PooledObjectName.DnaHp, false);
                }
                if (Random.Range(0, 100) < dnaInvChance)
                {
                    SpawnObj(platform, PooledObjectName.DnaInv, false);
                }
                
            //spheres
                if (Random.Range(0, 100) < doubleJumpChance)
                {
                    SpawnObj(platform, PooledObjectName.SphereDoubleJump, false);
                }
                if (Random.Range(0, 100) < timeDilationChance)
                {
                    SpawnObj(platform, PooledObjectName.SphereTimeDilation, false);
                }
    
        //DEBUFFS
            //obs
                if (Random.Range(0, 100) < lavaChance)
                {
                    SpawnObj(platform, PooledObjectName.Lava, true);
                }
            //spheres
                if (Random.Range(0, 100) < timeAccelerationChance)
                {
                    SpawnObj(platform, PooledObjectName.SphereTimeAcceleration, false);
                }
        //BOSS
             if (wannaGenerateBoss)
             {
                 wannaGenerateBoss = false;
                 SpawnObj(platform, PooledObjectName.BossNotInPool, true);
                 infoWidgetsController.CreateWidget("get ready to fight the boss !", uiColors.BossSpawned);
             }
    }

    #endregion

    #region Private Methods

    private void SpawnObj(GameObject platform, PooledObjectName spawnObjectName, bool isOnGround)
    {
        GameObject obj;
        if (spawnObjectName == PooledObjectName.BossNotInPool)
        {
            obj = Instantiate(Resources.Load<GameObject>("Prefabs/Player/BOSS"));
            obj.SetActive(false);
        }
        else
        {
            obj = ObjectPool.GetPooledObject(spawnObjectName);
        }
        
        float halfDistanceBetween = 0;
        float randomSpawnHeight;

        if (isOnGround)
        {
            randomSpawnHeight = platform.transform.localScale.y / 2;

            if (spawnObjectName == PooledObjectName.BossNotInPool)
            {
                randomSpawnHeight += obj.GetComponent<ExtendedColliders3D>().properties.size.y * obj.transform.localScale.y;
            }
        }
        else
        {
            halfDistanceBetween = _platformSpawner.distanceBetween / 2;
            randomSpawnHeight = Random.Range(platform.transform.localScale.y / 2, _playerMovement.jumpForce / _playerMovement.gravity);
        }
        
        obj.transform.position = new Vector3(Random.Range(platform.GetComponent<PlatformBlockScr>().back.position.x + halfDistanceBetween, platform.GetComponent<PlatformBlockScr>().front.position.x - halfDistanceBetween), 
                                            platform.transform.position.y + randomSpawnHeight, platform.transform.position.z);

        obj.transform.rotation = Quaternion.identity;
        
        obj.SetActive(true);
        
        
    }
    
    #endregion
    
}
