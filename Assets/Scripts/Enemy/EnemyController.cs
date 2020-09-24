using UnityEngine;


public class EnemyController : MonoBehaviour
{
    #region Fields

    private Rigidbody _myRigidbody;
    private GameObject _player;
    private float _speedMove = 1.2f;

    #endregion


    #region UnityMethods

    private void Start()
    {
        _myRigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (_player != null)
        {
            transform.LookAt(_player.transform.position);

            var vectorX = _player.transform.position.x - transform.position.x;
            var vectorZ = _player.transform.position.z - transform.position.z;
            var vectorMove = new Vector3(vectorX, 0.0f, vectorZ);
            vectorMove.Normalize();
            _myRigidbody.velocity = vectorMove * _speedMove;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _player = other.gameObject;
        }
    }

    #endregion
}

