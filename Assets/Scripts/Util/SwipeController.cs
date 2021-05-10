
using UnityEngine;

public class SwipeController : MonoBehaviour
{
    #region Fields

    private bool _isDragging, _isMobile;
    private Vector2 _tapPoint, _swipeDelta;
    private RectTransform _rt;
    
    public SwipeEvent swipeEvent;

    #endregion

    #region MonoBehavoir Callbacks

    private void Awake()
    {
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            _isMobile = true;
        }

        _rt = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (!_isMobile)
        {
            if (Input.GetMouseButtonDown(0) && MouseInObj(Input.mousePosition))
            {
                _isDragging = true;
                _tapPoint = Input.mousePosition;  
            }
            else if(Input.GetMouseButtonUp(0))
            {
                ResetSwipe();
            }
        }
        else
        {
            if (Input.touchCount > 0 && MouseInObj(Input.touches[0].position))
            {
                if (Input.touches[0].phase == TouchPhase.Began)
                {
                    _isDragging = true;
                    _tapPoint = Input.touches[0].position;
                }
                else if (Input.touches[0].phase == TouchPhase.Canceled || Input.touches[0].phase == TouchPhase.Ended)
                {
                    ResetSwipe();
                }
            }
        }
        
        CalculateSwipe();
    }

    #endregion

    #region Private Methods

    private void CalculateSwipe()
    {
        _swipeDelta = Vector3.zero;

        if (_isDragging)
        {
            if (!_isMobile && Input.GetMouseButton(0))
            {
                _swipeDelta = (Vector2) Input.mousePosition - _tapPoint;
            }
            else if (Input.touchCount > 0)
            {
                _swipeDelta = Input.touches[0].position - _tapPoint;
            }
            
            swipeEvent.Invoke(_swipeDelta.x);
        }
    }

    private void ResetSwipe()
    {
        swipeEvent.Invoke(0);
        _isDragging = false;
        _tapPoint = _swipeDelta = Vector3.zero;
    }

    private bool MouseInObj(Vector2 cursorPosition)
    {
        var position = _rt.transform.position;
        var rect = _rt.rect;
        
        return cursorPosition.x < position.x + rect.width &&
               cursorPosition.x > position.x - rect.width;
    }

    #endregion
}
