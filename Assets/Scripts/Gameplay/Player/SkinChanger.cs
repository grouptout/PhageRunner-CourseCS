using UnityEngine;

public class SkinChanger : MonoBehaviour
{
    #region Fields

    public SkinnedMeshRenderer body, head;
    public SkinnedMeshRenderer[] legs, arms;
    
    private int _control = 0;
    private bool _canChange;

    #endregion

    #region MonoBehavoir Callbacks

    private void Start()
    {
        SyncSavedSkin();
    }
    

    #endregion

    #region Public Methods

    public void Change(Color color)
    {
        if (_canChange)
        {
            switch (_control)
            {
                case 0:
                    ChangeHead(color);
                    break;
                case 1:
                    ChangeBody(color);
                    break;
                case 2:
                    ChangeArms(color);
                    break;
                case 3:
                    ChangeLegs(color);
                    break;
                default:
                    Debug.LogError("Skin Changer _control doesn't have a value of your change mode");
                    break;
            }
        }

        _canChange = true;
    }

    public void Toggle(int number)
    {
        _control = number;
    }

    #endregion

    #region Private Methods

    private void ChangeHead(Color color)
    {
        color.a =  0.6705883f;
        head.materials[0].color = color;
        
        SaveColor("color_head_", color, true);
    }

    private void ChangeBody(Color color)
    {
        body.materials[0].color = color;
        head.materials[1].color = color;
        
        SaveColor("color_body_", color, false);
    }
    
    private void ChangeArms(Color color)
    {
        foreach (var arm in arms)
        {
            arm.materials[0].color = color;
        }
        
        SaveColor("color_arms_", color, false);
    }
    
    private void ChangeLegs(Color color)
    {
        foreach (var leg in legs)
        {
            leg.materials[0].color = color;
        }

        SaveColor("color_legs_", color, false);
    }

    private void SaveColor(string part, Color color, bool hasAlpha)
    {
        var r = color.r;
        var g = color.g;
        var b = color.b;

        PlayerPrefs.SetFloat(string.Format(part + "R"), r);
        PlayerPrefs.SetFloat(string.Format(part + "G"), g);
        PlayerPrefs.SetFloat(string.Format(part + "B"), b);

        if (hasAlpha)
        {
            var a = color.a;
            PlayerPrefs.SetFloat(string.Format(part + "a"), a);
        }
    }
    private void SyncSavedSkin()
    {
        if (PlayerPrefs.HasKey("color_head_R"))
        {
            var r = PlayerPrefs.GetFloat("color_head_R");
            var g = PlayerPrefs.GetFloat("color_head_G");
            var b = PlayerPrefs.GetFloat("color_head_B");
            var a = PlayerPrefs.GetFloat("color_head_A");
            var buffCol = new Color(r, g, b, a);
            
            ChangeHead(buffCol);
        }

        if (PlayerPrefs.HasKey("color_body_R"))
        {
            var r = PlayerPrefs.GetFloat("color_body_R");
            var g = PlayerPrefs.GetFloat("color_body_G");
            var b = PlayerPrefs.GetFloat("color_body_B");
            var buffCol = new Color(r, g, b);
            
            ChangeBody(buffCol);
        }
        
        if (PlayerPrefs.HasKey("color_arms_R"))
        {
            var r = PlayerPrefs.GetFloat("color_arms_R");
            var g = PlayerPrefs.GetFloat("color_arms_G");
            var b = PlayerPrefs.GetFloat("color_arms_B");
            var buffCol = new Color(r, g, b);
            
            ChangeArms(buffCol);
        }
        
        if (PlayerPrefs.HasKey("color_legs_R"))
        {
            var r = PlayerPrefs.GetFloat("color_legs_R");
            var g = PlayerPrefs.GetFloat("color_legs_G");
            var b = PlayerPrefs.GetFloat("color_legs_B");
            var buffCol = new Color(r, g, b);
            
            ChangeLegs(buffCol);
        }
    }

    #endregion
}
