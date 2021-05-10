
using UnityEngine;

public class CameraMovement : EventInvoker
{
    #region Private Fields

    [Header("Settings")]
    [SerializeField] private CameraPrefs cameraPrefs;
    
    private Transform player;
    
    private float dumping;
    private Vector3 offset;

    private float _ypos;
    private float _zpos;

    #endregion
    
    #region MonoBehavoir Callbacks

    private void Awake()
    {
        EventManager.AddListener(EventName.StartEvent, HandleStartEvent);

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        dumping = cameraPrefs.dumpingBefore;
        offset = cameraPrefs.offsetBefore;
    }
    private void Start()
    {
        try
        {
            transform.position = new Vector3(player.position.x - offset.x, player.position.y - offset.y, player.position.z - offset.z);
            _ypos = transform.position.y;
            _zpos = transform.position.z;
        }
        catch
        {
            // ignored
        }
    }
    
    private void LateUpdate()
    {
        try
        {
            _ypos = Mathf.Lerp(_ypos, player.position.y - offset.y, dumping * Time.deltaTime);
            _zpos = Mathf.Lerp(_zpos, player.position.z - offset.z, dumping * Time.deltaTime);
            transform.position = new Vector3(player.position.x - offset.x, _ypos, _zpos);
        }
        catch 
        {
            // ignored
        }
    }

    #endregion
    
    #region EventSystem Support

    private void HandleStartEvent()
    {
        dumping = cameraPrefs.dumping;
        offset = cameraPrefs.offset;
    }
    
    #endregion
}
