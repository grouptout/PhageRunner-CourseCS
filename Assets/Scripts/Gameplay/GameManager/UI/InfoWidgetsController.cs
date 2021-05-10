
using UnityEngine;
using UnityEngine.UI;

public class InfoWidgetsController : MonoBehaviour
{
    #region Fields

    [SerializeField] private GameObject infoTextWidget;
    [SerializeField] private GameObject parentObj;
    

    #endregion

    #region Public Methods

    public void CreateWidget(string str, Color color)
    {
        var go = Instantiate(infoTextWidget, parentObj.transform, false);
        go.GetComponent<Text>().color = color;
        go.GetComponent<Text>().text = str;
        
    }

    #endregion
    
}
