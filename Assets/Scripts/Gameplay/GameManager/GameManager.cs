using System;
using System.Collections;
using System.Collections.Generic;
using Michsky.UI.ModernUIPack;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : EventInvoker
{
    #region Fields
    
    [Space]
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject pauseMenu, otherMenus, scoreText, settingsMenu, helpMenu, fps;
    [SerializeField] private GameObject dieDialog;
    [SerializeField] private GameObject tip;
    [SerializeField] private GameObject hud;
    [SerializeField] private GameObject bars;

    private LevelController _levelController;
    private StartEvent startEvent = new StartEvent();
    private bool _gameStarted;
    private Animator _pauseAnimator;

    #endregion

    #region MonoBehavoir Callbacks

    private void Awake()
    {
        Instantiate(Resources.Load("Prefabs/Player/Phage"), spawnPoint.position, spawnPoint.rotation);

        _levelController = gameObject.GetComponent<LevelController>();
        _pauseAnimator = pauseMenu.GetComponent<Animator>();
        _pauseAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;
        
        if (!PlayerPrefs.HasKey("highscore"))
        {
            PlayerPrefs.SetFloat("highscore", 0);
            PlayerPrefs.SetInt("highlvl", 0);
            PlayerPrefs.SetInt("bossesdefeated", 0);
        }

        if (PlayerPrefs.HasKey("fpsOn"))
        {
            fps.SetActive(PlayerPrefs.GetInt("fpsOn") == 1);
        }
    }

    private void Start()
    {
        ObjectPool.Initialize();
        unityEvents.Add(EventName.StartEvent, new StartEvent());
        EventManager.AddInvoker(EventName.StartEvent, this);
        
        EventManager.AddListener(EventName.ShowDieEvent, HandleShowDieEvent);

    }


    private void Update()
    {
        if (Input.anyKeyDown && _gameStarted == false)
        {
            _gameStarted = true;
            StartGame();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }
    
    private void OnApplicationQuit()
    {
        SaveRecord();
    }

    #endregion
    
    #region Public Methods

    public void RestartGame()
    {
        SaveRecord();
        SceneManager.LoadScene("Gameplay");
        Time.timeScale = 1;
    }
    
    public void MainMenu()
    { 
        SaveRecord();
        SceneManager.LoadScene("MainMenu");
    }
    
    public void SettingsMenu()
    {
        otherMenus.SetActive(true);
        settingsMenu.SetActive(true);
        helpMenu.SetActive(false);
    }
    
    public void HelpMenu()
    {
        otherMenus.SetActive(true);
        helpMenu.SetActive(true);
        settingsMenu.SetActive(false);
    }
    
    public void BackFromMenuToPause()
    {
        if (PlayerPrefs.HasKey("fpsOn"))
        {
            fps.SetActive(PlayerPrefs.GetInt("fpsOn") == 1);
        }
        
        settingsMenu.SetActive(false);
        helpMenu.SetActive(false);
        otherMenus.SetActive(false);
    }

    public void PauseGame()
    {
        BackFromMenuToPause();
        _pauseAnimator.Play("Show");
        if (Time.timeScale > 0) // If playing
        {
            _pauseAnimator.SetTrigger("Show");
            Time.timeScale = 0;
        }
        else
        {
            _pauseAnimator.SetTrigger("Close");
            
            Time.timeScale = 1;
        }
    }



    #endregion

    #region Private Methods

    private void StartGame()
    {
        unityEvents[EventName.StartEvent].Invoke();
        StartCoroutine(hideTip());
        hud.SetActive(true);
        bars.SetActive(true);
    }
    private IEnumerator hideTip()
    {
        tip.GetComponent<Animator>().SetTrigger("FadeOut");
        yield return new WaitForSeconds(1);
        tip.SetActive(false);
    }
    
    private void SaveRecord()
    {
        if (_levelController.score > PlayerPrefs.GetFloat("highscore") || (Math.Abs(_levelController.score - PlayerPrefs.GetFloat("highscore")) < 0.00001 && _levelController.level > PlayerPrefs.GetInt("highlvl")))
        {
            PlayerPrefs.SetFloat("highscore", _levelController.score);
            PlayerPrefs.SetInt("highlvl", _levelController.level);
        }

        if (PlayerPrefs.GetInt("bossesdefeated") < _levelController.bossesDefeated)
        {
            PlayerPrefs.SetInt("bossesdefeated", _levelController.bossesDefeated);
        }
    }


    #endregion
    
    
    
    #region Event System support
    
    public void AddStartEventListener(UnityAction listener)
    {
        startEvent.AddListener(listener);
    }

    private void HandleShowDieEvent()
    {
        SaveRecord();
        
        scoreText.GetComponent<Text>().text = $@"
—————Your score—————
{_levelController.score:N0} on lvl {_levelController.level}
Bosses defeated: {_levelController.bossesDefeated}

—————High score—————
{PlayerPrefs.GetFloat("highscore"):N0} on lvl {PlayerPrefs.GetInt("highlvl")}
Bosses defeated: {PlayerPrefs.GetInt("bossesdefeated")}
";
        
        
        dieDialog.SetActive(true);
    }

    #endregion
}
