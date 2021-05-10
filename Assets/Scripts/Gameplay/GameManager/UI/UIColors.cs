
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObjects/UI Colors")]
public class UIColors : ScriptableObject
{
    [Header("INFO-WIDGETS")] 
    public Color BossSpawned;
    public Color Boss_PlayerWin;
    public Color Boss_PlayerWinXp;
    public Color Boss_PlayerWinHp;
    public Color Boss_PlayerLose;
    public Color GameStarted;
    public Color NewRecord;
}
