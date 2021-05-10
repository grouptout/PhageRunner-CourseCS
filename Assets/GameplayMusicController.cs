
using UnityEngine;
using Random = UnityEngine.Random;

public class GameplayMusicController : MonoBehaviour
{
    #region Fields

    [SerializeField] private AudioClip[] musics;

    #endregion

    #region MonoBehavoir Callbacks

    private void Awake()
    {
        EventManager.AddListener(EventName.StartEvent, HandleStartEvent);
    }
    
    private void Start()
    {
        GetComponent<AudioSource>().clip = musics[Random.Range(0, musics.Length)];
    }

    #endregion
    
    #region EventSystem Support

    private void HandleStartEvent()
    {
        GetComponent<AudioSource>().Play();
    }
    
    #endregion
}
