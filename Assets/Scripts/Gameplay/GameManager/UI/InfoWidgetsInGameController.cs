
using System.Collections;
using UnityEngine;

public class InfoWidgetsInGameController : MonoBehaviour
{
    #region Fields

    [SerializeField] private StartVariables startVariables;
    [SerializeField] private InfoWidgetsController infoWidgetsController;
    [SerializeField] private UIColors uiColors;
    private LevelController _levelController;

    #endregion

    #region MonoBehavoir Callbacks

    private void Awake()
    {
        _levelController = GetComponent<LevelController>();
        EventManager.AddListener(EventName.BossPlayerLoseEvent, HandleBossPlayerLoseEvent);
        EventManager.AddListener(EventName.BossPlayerWinEvent, HandleBossPlayerWinEvent);
        EventManager.AddListener(EventName.StartEvent, HandleStartEvent);
        EventManager.AddListener(EventName.NewRecordEvent, HandleNewRecordEvent);
    }
    

    #endregion

    #region Private Methods

    private IEnumerator PlayerWin()
    {
        infoWidgetsController.CreateWidget("winner !", uiColors.Boss_PlayerWin);
        _levelController.score += 100;
        yield return new WaitForSecondsRealtime(0.6f);
        infoWidgetsController.CreateWidget($"+{startVariables.bossRewardXp} exp", uiColors.Boss_PlayerWinXp);
        infoWidgetsController.CreateWidget($"+{startVariables.bossRewardHp} hp", uiColors.Boss_PlayerWinHp);
    }

    #endregion

    #region Event System Support

    private void HandleBossPlayerLoseEvent()
    {
        infoWidgetsController.CreateWidget("you lost to the boss", uiColors.Boss_PlayerLose);
    } 
    
    private void HandleBossPlayerWinEvent()
    {
        StartCoroutine(PlayerWin());
    }
    
    private void HandleStartEvent()
    {
        infoWidgetsController.CreateWidget("run !", uiColors.GameStarted);
    }  
    private void HandleNewRecordEvent()
    {
        infoWidgetsController.CreateWidget("new record !", uiColors.NewRecord);
    }

    #endregion
    
}
