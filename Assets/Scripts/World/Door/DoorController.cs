using UnityEngine;


public class DoorController : MonoBehaviour
{
    #region PrivateData

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
