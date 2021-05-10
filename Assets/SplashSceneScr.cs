using System.Collections;
using UnityEngine;

public class SplashSceneScr : MonoBehaviour
{
    #region MonoBehavoir Callbacks

    private void Start()
    {
        StartCoroutine(StartGame());
    }

    #endregion

    #region Private Methods

    private static IEnumerator StartGame()
    {
        yield return new WaitForSeconds(1);
        SceneTransition.SwitchToSceneWithAnimation("MainMenu");
    }

    #endregion
}
