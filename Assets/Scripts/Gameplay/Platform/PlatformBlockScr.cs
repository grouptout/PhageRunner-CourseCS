
using UnityEngine;

public class PlatformBlockScr : MonoBehaviour
{

    #region Fields

    private PlayerMovement playerMovement;
    [Space]
    public Transform front;
    public Transform back;

    #endregion

    #region MonoBehavoir Callbacks

    private void Start()
    { 
        playerMovement = FindObjectOfType<Phage>().GetComponent<PlayerMovement>();
    }
    

    // Update is called once per frame
    private void Update()
    {    
        if (playerMovement.canMove)
        {
            transform.Translate(Vector3.left * (Time.deltaTime * playerMovement.speed));
        }
    }

    #endregion
}
