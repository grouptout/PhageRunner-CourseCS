using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    #region Fields

    [Header("Menus")]
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject highscoresMenu;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject helpMenu;
    [SerializeField] private GameObject quitMenu;
    [SerializeField] private GameObject skinChangerImg, skinChanger;
    
    [Header("Buttons")]
    [SerializeField] private Button startButton;
    [SerializeField] private GameObject quitButton;

    #endregion

    #region MonoBehavoir Callbacks

    private void Awake()
    {
        Time.timeScale = 1;
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            quitButton.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (mainMenu.activeSelf)
            {
                PreQuitButton();
            }
            else
            {
                MainButton();
            }
        }
    }

    #endregion

    #region Public Methods

    public void MainButton()
    {
        settingsMenu.SetActive(false);
        helpMenu.SetActive(false);
        highscoresMenu.SetActive(false);
        quitMenu.SetActive(false);

        ShowMainMenu(true);
    }

    public void PlayButton()
    {
        startButton.interactable = false;
        SceneTransition.SwitchToSceneWithAnimation("Gameplay");
    }

    public void HighscoresButton()
    {
        ShowMainMenu(false);
        highscoresMenu.SetActive(true);
    }
    
    public void SettingsButton()
    {
        ShowMainMenu(false);
        settingsMenu.SetActive(true);
    }

    public void HelpButton()
    {
        ShowMainMenu(false);
        helpMenu.SetActive(true);
    }

    public void PreQuitButton()
    {
        ShowMainMenu(false);
        quitMenu.SetActive(true);
    }
    public void QuitButton()
    {
        Application.Quit();
    }

    #endregion

    #region Private Methods

    private void ShowMainMenu(bool stat)
    {
        mainMenu.SetActive(stat);
        skinChangerImg.SetActive(stat);
        skinChanger.SetActive(stat);
    }

    #endregion
}
