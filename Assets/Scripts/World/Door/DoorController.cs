using UnityEngine;


public class DoorController : MonoBehaviour
{
    #region Fields

    [SerializeField] private GameObject Door;

    #endregion


    #region UnityMethods

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(Door);
        }
    }

    #endregion
}
