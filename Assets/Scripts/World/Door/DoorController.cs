using UnityEngine;


public class DoorController : MonoBehaviour
{
    #region Fields

    [SerializeField] private GameObject _door;
    private float _doorOpenHeight = 2.0f;
    private bool _canPlayerOpenDoor = true;

    #endregion


    #region UnityMethods

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (_canPlayerOpenDoor)
            {
                _door.SetActive(false);
                _canPlayerOpenDoor = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!_canPlayerOpenDoor)
            {
                _door.SetActive(true);
                _canPlayerOpenDoor = true;
            }
        }
    }

    #endregion
}
