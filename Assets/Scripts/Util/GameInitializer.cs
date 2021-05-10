
using UnityEngine;

/// <summary>
/// Initializes the game
/// </summary>
public class GameInitializer : MonoBehaviour
{
    #region MonoBehavoir Callbacks

    private void Awake()
    {
        EventManager.Initialize();
    }

    #endregion
}