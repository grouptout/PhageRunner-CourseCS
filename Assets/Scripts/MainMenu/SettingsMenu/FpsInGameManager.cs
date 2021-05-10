
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class FpsInGameManager : MonoBehaviour
{
    #region Fields

    [SerializeField] private Text fpstxt;
    private float _deltaTime;
    private float _interval = 1;

    #endregion

    #region MonoBehavoir Callbacks

    private void Update()
    {
        // calculate fps
		        _deltaTime += (Time.unscaledDeltaTime - _deltaTime) * 0.1f;
		        var fps = 1.0f / _deltaTime;
        
        // show fps
        _interval -= Time.unscaledDeltaTime;
        if (_interval < 0)
        {
            _interval = 1;
            fpstxt.text = $"fps: {Mathf.Ceil (fps).ToString(CultureInfo.CurrentCulture)}";
        }
        
    }

    #endregion
}
