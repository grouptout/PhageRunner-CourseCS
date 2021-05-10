
using UnityEngine;

public class PlayerPrewRotator : MonoBehaviour
{
    #region Fields

    [Range(50, 300)]public float rotatorSensivity;
    [Range(1, 10)] public float  rotatorAmplitude;
    private float _rotatingSpeed;
    private float _smoothSpeed;
    

    #endregion

    #region MonoBehavoir Callbacks

    private void Update()
    {
        _smoothSpeed = _rotatingSpeed != 0 ? _rotatingSpeed : Mathf.Lerp(_smoothSpeed, _rotatingSpeed, Time.deltaTime * rotatorAmplitude);
        transform.Rotate(0,_smoothSpeed, 0);
    }

    #endregion

    #region Public Methods

    public void ChangeSpeed(float mouseSpeed)
    {
        _rotatingSpeed = -mouseSpeed / rotatorSensivity;
    }

    #endregion
}


