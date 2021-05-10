
using UnityEngine;

public class ColliderManager : MonoBehaviour
{
   #region Fields

   [SerializeField] private DNA parent;

   #endregion

   #region MonoBehavoir Callbacks

   private void OnTriggerEnter(Collider other)
   {
      if (other.CompareTag("Player"))
      {
         parent.PlayerPickedUp();
      }
   }

   #endregion
}
