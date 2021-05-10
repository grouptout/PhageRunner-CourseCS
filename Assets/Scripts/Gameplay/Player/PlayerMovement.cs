using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class PlayerMovement : EventInvoker
{
    #region Fields

    [Header("Important")] 
    [SerializeField] private CapsuleCollider capsuleCollider;
    [SerializeField] private Transform[] rayFloors;
    [FormerlySerializedAs("_canMove")] public bool canMove;

    [Header("Stats")] 
    [SerializeField] private StartVariables startVariables;
    
     public float speed;
    [NonSerialized] public float jumpForce;
    [NonSerialized] public float gravity;
    [NonSerialized] public int extraJumpsAvailable;
    public UnityEvent extraJumpSoundEvent, landSoundEvent, dieSoundEvent;
    
    private Animator _animator;
    private Rigidbody _rb;
    private PlatformSpawner _platformSpawner;

    private bool _wannaJump, _land = true; 
    private bool _wannaExtraJump; 

    #endregion

    #region MonoBehavoir Callbacks

    private void Awake()
    {
        speed = startVariables.speed;
        jumpForce = startVariables.jumpForce;
        gravity = startVariables.gravity;
        
        _rb = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        _animator = GetComponent<Animator>();
        _platformSpawner = FindObjectOfType<PlatformSpawner>();
        
        EventManager.AddListener(EventName.StartEvent, HandleStartEvent);
        EventManager.AddListener(EventName.DieEvent, HandleDieEvent);
        
        unityEvents.Add(EventName.DecreaseExtraJumpWidgetEvent, new DecreaseExtraJumpWidgetEvent());
        EventManager.AddInvoker(EventName.DecreaseExtraJumpWidgetEvent, this);
    }
    
    private void FixedUpdate()
    {
        _rb.AddForce(new Vector3(0, Physics.gravity.y * gravity, 0), ForceMode.Acceleration);  // Gravity

        if (!canMove) return;
        
        if (_wannaJump && IsGrounded())
        {
            landSoundEvent.Invoke();
            Jump();
            _wannaJump = false;
            _land = false;
        }
        if (_wannaExtraJump && extraJumpsAvailable > 0)
        {
            Jump();
            _wannaExtraJump = false;
            extraJumpsAvailable--;
            unityEvents[EventName.DecreaseExtraJumpWidgetEvent].Invoke();
            extraJumpSoundEvent.Invoke();
        }
        

    }

    private void Update()
    {
        if (IsGrounded())
        {
            _animator.SetBool("Jumping", false);

            if (!_land)
            {
                _land = true;
                landSoundEvent.Invoke();
            }

            if (JumpButtonPressed())
            {
                _wannaJump = true;
            }
        }
        else
        {
            if (JumpButtonPressed() && extraJumpsAvailable > 0)
            {
                _wannaExtraJump = true;
            }
            
            if (transform.position.y + 100 < _platformSpawner.currentPlatforms[0].transform.position.y)
            {
                gameObject.GetComponent<Phage>().health = 0;
            }
        }
        
    }
#endregion
    
    #region Private Methods

    private bool IsGrounded()
    {
         return rayFloors.Any(floorPoint => Physics.Raycast(floorPoint.position, Vector3.down, 0.17f));
    }

    private bool JumpButtonPressed()
    {
        return (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0)) && Time.timeScale > 0;
    }

    private bool JumpButtonReleased()
    {
        return (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.Mouse0)) && Time.timeScale > 0;
    }

    private void Jump()
    {
        _animator.SetBool("Jumping", true);
        _rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse); // Impulse jumping
    }

    
    #endregion    
    
    #region Event System support
    
    private void HandleStartEvent()
    {
        canMove = true;
    }
    
    private void HandleDieEvent()
    {
        dieSoundEvent.Invoke();
        canMove = false;
    }
    
    #endregion
    
}
