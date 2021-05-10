using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlatformSpawner : MonoBehaviour
{

    #region Serializable Fields
    
    [Header("Initialize")]
    [SerializeField] private GameObject platform;
    [SerializeField] private PlatformContentGenerator platformContentGenerator;

    [Header("Random")] 
    [SerializeField] private StartVariables startVariables;
    
    [Tooltip("original: 2.9f")] [NonSerialized] public float distanceBetween;
    [Tooltip("X, original: 20")]  [NonSerialized] public float minLength, maxLength;
    [Tooltip("Y, original: 0.1f")]  [NonSerialized] public float minHeight, maxHeight;
    [Tooltip("Z, original: 2.1f")]  [NonSerialized] public float minWidth, maxWidth;
    [Tooltip("Elevation")] [NonSerialized] public float blockElevationAmplitude;
    
    [NonSerialized] public List<GameObject> currentPlatforms;
    #endregion
    
    #region Private Fields

    private float platformWidth;
    private GameObject previousPlatform;

    #endregion

    #region Monobehavoir Callbacks

    private void Awake()
    {
        distanceBetween = startVariables.distanceBetween;
        minLength = startVariables.minLength;
        maxLength = startVariables.maxLength;
        minWidth = startVariables.minWidth;
        maxWidth = startVariables.maxWidth;
        minHeight = startVariables.minHeight;
        maxHeight = startVariables.maxHeight;
        blockElevationAmplitude = startVariables.blockElevationAmplitude;
    }

    private void Start()
    {
        currentPlatforms = new List<GameObject> {platform};
        previousPlatform = platform;
        platformWidth = platform.GetComponent<BoxCollider>().size.x * currentPlatforms[0].transform.localScale.x;
    }


    private void Update()
    {
        //spawn
        if (platform.transform.position.x < transform.position.x - platformWidth)
        {
            GeneratePlatform();
            currentPlatforms.Add(platform);
            previousPlatform = platform;
            
            transform.position = new Vector3(transform.position.x, transform.position.y + Random.Range(-blockElevationAmplitude, blockElevationAmplitude), transform.position.z);
        }
        
        //destroy
        if (currentPlatforms[0].transform.position.x < -transform.position.x)
        {
            currentPlatforms[0].SetActive(false); // first platform is useless when not active (can be fixed in the future)
            ObjectPool.ReturnPlatform(currentPlatforms[0]);
            currentPlatforms.RemoveAt(0); 
        }

    }

    #endregion

    #region Private Methods

    private void GeneratePlatform()
    {
        platform = ObjectPool.GetPlatform();
        
        platform.transform.localScale = new Vector3(Random.Range(minLength, maxLength), Random.Range(minHeight, maxHeight), Random.Range(minWidth, maxWidth));
        platform.transform.rotation = Quaternion.identity;
        
        platformWidth = platform.transform.localScale.x;
        platform.transform.position = transform.position; 
        
        var prevBackTransform = previousPlatform.GetComponent<PlatformBlockScr>().back.position.x;
        var currentFrontTransform = platform.GetComponent<PlatformBlockScr>().front.position.x;
        var deltaPointsTransform = prevBackTransform - currentFrontTransform;

        platform.transform.position = new Vector3(platform.transform.position.x + deltaPointsTransform + Random.Range(0, distanceBetween), transform.position.y, transform.position.z);

        platformContentGenerator.Generate(platform);
        
        platform.SetActive(true);
    }

    #endregion

   
}
