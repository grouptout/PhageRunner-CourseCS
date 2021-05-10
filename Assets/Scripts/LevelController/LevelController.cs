using System;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;
using Random = UnityEngine.Random;

public class LevelController : EventInvoker
{
    #region Fields

    [Header("Initialize")] 
    [SerializeField] private StartVariables startVariables;
    [SerializeField] private GameObject platformManager;
    [SerializeField] private Text levelText, scoreText, hp, chances;
    [SerializeField] private Image levelAmount;
    
    
    
    [NonSerialized] public float score;
    [NonSerialized] public int bossesDefeated;
    [NonSerialized] public int level = 1;
    [NonSerialized] public float scoreMultiplier = 1;
    
    [NonSerialized] public PlayerMovement playerMovement;
    [NonSerialized] public Phage phageScript;
    
    private float _minAllAdd, _levelUpSpeedAdd, _levelUpJumpAdd, _minLevelWeight, _maxLevelWeight, _blockElevationAmplitudeAdd, _distanceBetweenAmplitudeAdd;
    private float _levelWeight;

    private GameObject _player;
    private float _highscore, _lastScore;
    private PlatformSpawner _platformSpawner;
    private PlatformContentGenerator _platformContentGenerator;
    private bool _recordBeat;

    #endregion
    
    #region MonoBehavoir Callbacks

    private void Awake()
    {
        _platformSpawner = platformManager.GetComponent<PlatformSpawner>();
        _platformContentGenerator = platformManager.GetComponent<PlatformContentGenerator>();
        
        _levelWeight = startVariables.levelWeight;
        _minAllAdd = startVariables.minAllAdd;
        _minLevelWeight = startVariables.minLevelWeight;
        _maxLevelWeight = startVariables.maxLevelWeight;
        _levelUpSpeedAdd = startVariables.levelUpSpeedAdd;
        _levelUpJumpAdd = startVariables.levelUpJumpAdd;
        _blockElevationAmplitudeAdd = startVariables.blockElevationAmplitudeAdd;
        _distanceBetweenAmplitudeAdd = startVariables.distanceBetweenAmplitudeAdd;

        _highscore = PlayerPrefs.GetFloat("highscore");
    }

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        playerMovement = _player.GetComponent<PlayerMovement>();
        phageScript = _player.GetComponent<Phage>();
        EventManager.AddListener(EventName.BossPlayerWinEvent, HandleBossPlayerWinEvent);
        unityEvents.Add(EventName.NewRecordEvent, new NewRecordEvent());
        EventManager.AddInvoker(EventName.NewRecordEvent, this);
        //ShowStats();
    }

    private void Update()
    {
        
        if (playerMovement.canMove)
        {
            hp.text = $"{phageScript.health * 100:N0}"; 
            
            score += scoreMultiplier * Time.deltaTime * playerMovement.speed / 3;
            scoreText.text = $"Score {score:N0}";

            if (score >= _levelWeight)
            {
                LevelUp();
            }

            levelAmount.fillAmount = Remap(score, _lastScore, _levelWeight, 0f, 1f);

            if (!_recordBeat && score >= _highscore)
            {
                _recordBeat = true;
                unityEvents[EventName.NewRecordEvent].Invoke();
            }
        }
        
    }

    #endregion

    #region Private Methods

    private static float Remap(float value,float min1,float max1,float min2,float max2)
    {
        return min2 + (value - min1) * (max2 - min2) / (max1 - min1);
    }
    
    private void LevelUp()
    {
        
            
        if (level >= 5)
        {
            _levelWeight += Random.Range(100, 200);
            level++;

            if ((level % 2) == 0 && Random.Range(0, 100) < startVariables.bossSpawnChance)
            {
                _platformContentGenerator.wannaGenerateBoss = true;
            }
        }
        else
        {
            _levelWeight *= Random.Range(_minLevelWeight, _maxLevelWeight);
            level++;
        }
        
        playerMovement.speed += Random.Range(_minAllAdd, _levelUpSpeedAdd);
        playerMovement.jumpForce += Random.Range(_minAllAdd, _levelUpJumpAdd);
        
        _platformSpawner.blockElevationAmplitude += Random.Range(_minAllAdd, _blockElevationAmplitudeAdd);
        _platformSpawner.distanceBetween += Random.Range(_minAllAdd - _distanceBetweenAmplitudeAdd,_minAllAdd + _distanceBetweenAmplitudeAdd);

        _platformContentGenerator.dnaXpChance = Mathf.Max(startVariables.minXpBuffChance, _platformContentGenerator.dnaXpChance - Random.Range(startVariables.minXpBuffGain, startVariables.maxXpBuffGain));
        _platformContentGenerator.lavaChance = Mathf.Min( _platformContentGenerator.lavaChance + Random.Range(startVariables.minLavaDbuffGain, startVariables.maxLavaDbuffGain), startVariables.maxLavaDbuffChance);
        
        _lastScore = score;
        levelText.text = $"Level {level}";

        //ShowStats();
    }

    private void HandleBossPlayerWinEvent()
    {
        bossesDefeated++;
    }
    
    private void ShowStats()
    {
        chances.text = $@"speed {playerMovement.speed}
jump force {playerMovement.jumpForce}
level weight {_levelWeight}
blockElevationAmplitude {_platformSpawner.blockElevationAmplitude}
distanceBetween {_platformSpawner.distanceBetween}
dnaXpChance {_platformContentGenerator.dnaXpChance}
lavaChance {_platformContentGenerator.lavaChance}
TIMESCALE {Time.timeScale}";
    }

    #endregion
}
