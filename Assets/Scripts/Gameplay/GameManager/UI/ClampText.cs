using UnityEngine;
using UnityEngine.UI;

public class ClampText : MonoBehaviour
{
    #region Fields

    private Text _text;

    #endregion

    #region MonoBehavoir Callbacks

    private void Awake()
    {
        _text = Instantiate(Resources.Load<GameObject>("Prefabs/UI/Gameplay/BOSSTEXT")).GetComponentInChildren<Text>();
    }

    private void Update()
    {
        _text.transform.position = Camera.main.WorldToScreenPoint(transform.position);
    }
    

    #endregion

    #region Public Methods

    public void SetText(string txt)
    {
        _text.text = txt;
    }

    #endregion
}
