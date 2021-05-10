using UnityEngine;
using UnityEngine.UI;

public class HighscoresMenu : MonoBehaviour
{
    #region Fields

    [SerializeField] private Text txt;

    #endregion

    #region MonoBehavoir Callbacks

    private void OnEnable()
    {
        if (PlayerPrefs.HasKey("highscore"))
        {
            txt.text = $@"——————High scores——————
{PlayerPrefs.GetFloat("highscore"):N0} on lvl {PlayerPrefs.GetInt("highlvl")}
Bosses defeated: {PlayerPrefs.GetInt("bossesdefeated")}";
        }
    }

    #endregion
}
