
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/CameraPrefs", order = 1)]
public class CameraPrefs : ScriptableObject
{
    [Header("TTP default")]
    public float dumping;
    public Vector3 offset;
    [Space]
    [Header("before")]
    public float  dumpingBefore;
    public Vector3  offsetBefore;
}
