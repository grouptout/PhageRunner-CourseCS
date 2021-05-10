
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/StartVars", order = 3)]
public class StartVariables : ScriptableObject
{
    [Header("Phage")]
    public float speed;
    public float jumpForce;
    public float gravity;
    [Space]
    
    [Header("Buffs/Debuffs")]
    [Tooltip("Duration")][Range(1, 10)] public float buffDnaXpDuration;
    [Tooltip("Duration")][Range(1, 10)] public float buffDnaHpDuration;
    [Tooltip("Duration")][Range(1, 10)] public float buffDnaInvDuration;
    [Tooltip("Duration")][Range(1, 10)] public float spTimeDilationDuration;
    [Tooltip("Duration")][Range(1, 10)] public float spTimeAccelerationDuration;
    [Tooltip("999 = INF")][Range(1, 999)] public float spDoubleJumpDuration;
    
    [Tooltip("Duration")][Range(1, 10)] public float debuffLavaDuration;
    [Tooltip("Duration")][Range(1, 10)] public float debuffLavaDamagePerSecond;
    [Tooltip("Duration")][Range(1, 10)] public float buffHealPerSecond;
    [Space]
    
    [Tooltip("dna 4")] [Range(0, 100)] public int dnaXpChance, dnaHpChance, dnaInvChance;
    [Tooltip("spheres")] [Range(0, 100)] public int doubleJumpChance, timeDilationChance, timeAccelerationChance;
    [Header("Obs")]
    [Tooltip("obstacle")] [Range(0, 100)] public int lavaChance;
    [Space]
    
    [Header("Platforms")]
    [Tooltip("original: 2.9f")] public float distanceBetween;
    [Space]
    [Tooltip("X, original: 20")]  public float minLength, maxLength;
    [Tooltip("Y, original: 0.1f")]  public float minHeight, maxHeight;
    [Tooltip("Z, original: 2.1f")]  public float minWidth, maxWidth;
    [Space] 
    [Tooltip("Elevation")] public float blockElevationAmplitude;

    [Space] [Header("LevelUps")] 
    [Range(10, 30)] public float levelWeight;
    [Range(0, 2f)] public float minAllAdd;
    [Space] 
    [Range(1f, 8f)]public float levelUpSpeedAdd;
    [Range(1f, 8f)]public float levelUpJumpAdd;
    [Space] 
    [Range(1.1f, 3f)] public float minLevelWeight;
    [Range(1.1f, 3f)] public float maxLevelWeight;
    [Space] 
    [Range(0f, 1.5f)] public float blockElevationAmplitudeAdd;
    [Range(0f, 2f)] public float distanceBetweenAmplitudeAdd;
    [Space] 
    [Tooltip("Will not change")] [Range(0, 100)] public float minXpBuffChance, maxLavaDbuffChance;
    [Tooltip("Will not change")] [Range(0, 100)] public int bossSpawnChance;
    [Tooltip("Will not change")] public float minXpBuffGain, maxXpBuffGain;
    [Tooltip("Will not change")] public float minLavaDbuffGain, maxLavaDbuffGain;
    [Tooltip("Will not change")] public int maxExtraJumps;
    [Space] [Header("BOSS")] 
    [Range(0, 100)] public int minBossDmg, maxBossDmg;
    [Tooltip("HP")][Range(0, 100)] public float bossRewardHp;
    [Tooltip("HP")][Range(0, 1000)] public float bossRewardXp;
}
