
using UnityEngine;
using UnityEngine.UI;

public class HelpMenu : MonoBehaviour
{
    #region Fields

    public StartVariables startVariables;
    [SerializeField] private Text txt;
    [SerializeField] private Text aboutTxt;

    #endregion

    #region MonoBehavoir Callbacks

    private void Start()
    {
        txt.text = $@"——————————————
CONTROLS:
Jump - Space or left mouse
Pause - Esc
Kill your character - K

——————————————

BUFFS:
***exp-dna***
Doubles the received exp
for {startVariables.buffDnaXpDuration} seconds
<doesn't stack>

***invulnerability-dna***
Disables incoming damage
for {startVariables.buffDnaInvDuration} seconds
<doesn't stack>

***hp-dna***
Heals you for {startVariables.buffHealPerSecond} hp points per second
over {startVariables.buffDnaHpDuration} seconds
<stacks>

——————————————

OBSTACLES:
***lava***
Burns you for {startVariables.debuffLavaDamagePerSecond} damage per second
over {startVariables.debuffLavaDuration} seconds
<stacks>

***boss***
Boss can spawn after 6 lvl
spawns every even level (6, 8, 10, ...)
with chance {startVariables.bossSpawnChance}%

Boss have {startVariables.minBossDmg}-{startVariables.maxBossDmg} hp
To beat him you must have more health than he has.

When in contact with him, the amount of health
is removed from you equal to the health of the boss.

The boss also loses health equal to yours.
If you are still alive you will receive a reward:
{startVariables.bossRewardHp}hp and {startVariables.bossRewardXp}exp

——————————————

SPHERES:
***extra jump***
Allows extra jump in air
<max {startVariables.maxExtraJumps} stacks>

***time dilation***
Slows down time during {startVariables.spTimeDilationDuration} seconds.
<stacks>

***time acceleration***
Accelerates time during {startVariables.spTimeAccelerationDuration} seconds.
<stacks>

——————————————
——————————————
——————————————
";
        aboutTxt.text = $@"
——— ABOUT———

{Application.productName} <{Application.companyName}>
Game version {Application.version}
Unity version {Application.unityVersion}


Programming, level | game design, etc : Andrew Spirin

Asset Store used:
AllSkyFree, AnotherColorPicker, Extended Colliders 3D, bl_HUD, I2, Le Tai's Asset, Modern UI Pack, SineVFX

3d models:
https:||www.turbosquid.com|ru|3d-models|phage-unity-3d-1341082
https:||www.turbosquid.com|ru|3d-model|dna

music | sounds
https:||freesound.org|people|deleted_user_6988258|sounds|399821|
https:||freesound.org|people|Andrewkn|sounds|527676|
https:||freesound.org|people|frankum|sounds|368743|
https:||freesound.org|people|PatrickLieberkind|sounds|396024|


not for sale
not for commercial purposes
made for educational purposes


";
    }
    

    #endregion
}
