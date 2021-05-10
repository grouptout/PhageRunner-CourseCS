
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    #region Fields

    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Image loadingBar;
    
    private Animator _anim;
    private static SceneTransition _instance;
    private AsyncOperation _loadingSceneOperation;
    private static bool _shouldPlayFinishAnimation;

    #endregion

    #region MonoBehavoir Callbacks

    private void Awake()
    {
        _instance = this;
        _anim = GetComponent<Animator>();
    }

    private void Start()
    {
        if (_shouldPlayFinishAnimation)
        {
            _anim.SetTrigger("Finish");
            _shouldPlayFinishAnimation = false; 
        }
    }

    private void Update()
    {
        if(_loadingSceneOperation == null) return;

        var progress = _loadingSceneOperation.progress; 
        
        text.text = $"{Mathf.RoundToInt(progress * 100)}%";
        loadingBar.fillAmount = Mathf.Lerp(loadingBar.fillAmount, progress, Time.deltaTime * 5);

    }

    #endregion
    
    #region Public Methods

    public static void SwitchToSceneWithAnimation(string sceneName)
    {
        _instance._anim.SetTrigger("Start");
        _instance._loadingSceneOperation = SceneManager.LoadSceneAsync(sceneName);
        _instance._loadingSceneOperation.allowSceneActivation = false;

    }
    
    public void OnAnimationOver()
    {
        _shouldPlayFinishAnimation = true;
        _instance._loadingSceneOperation.allowSceneActivation = true;
    }

    #endregion
    
}
