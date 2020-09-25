using UnityEngine;


public class DoorController : MonoBehaviour
{
    #region Fields

    [SerializeField] private GameObject _door;

    private float _doorOpenHeight = 2.0f;

    #endregion


    #region UnityMethods

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _door.transform.position = new Vector3(_door.transform.position.x, _door.transform.position.y + _doorOpenHeight, _door.transform.position.z);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _door.transform.position = new Vector3(_door.transform.position.x, _door.transform.position.y - _doorOpenHeight, _door.transform.position.z);
        }
    }

    #endregion
}
