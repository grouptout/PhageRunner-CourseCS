using System.Collections;
using I2.TextAnimation;
using UnityEngine;

public class InfoWidgetPrefScr : MonoBehaviour
{
    #region Fields

    private bool _canDestroy;

    #endregion

    #region MonoBehavoir Callbacks

    private void Start()
    {
        StartCoroutine(Ie());
    }

    #endregion

    #region Methods

    private IEnumerator Ie()
    {
        GetComponent<TextAnimation>().PlayAnimation("Circles");
        yield return new WaitForSeconds(2);
        _canDestroy = true;
        GetComponent<TextAnimation>().PlayAnimation("Fade Out");
        
    }

    public void DestroyWidget()
    {
        if (_canDestroy)
        {
            Destroy(gameObject);
        }
    }

    #endregion
}
