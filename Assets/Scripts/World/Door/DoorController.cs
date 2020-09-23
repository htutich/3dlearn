using UnityEngine;


public class DoorController : MonoBehaviour
{
    #region PrivateData

    [SerializeField] private GameObject Door;

    #endregion


    #region OnTriggerEnter

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(Door);
        }
    }

    #endregion
}
