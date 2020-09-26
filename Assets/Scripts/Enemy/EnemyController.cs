using UnityEngine;


public class EnemyController : MonoBehaviour
{
    #region Fields

    private Rigidbody _myRigidbody;
    private GameObject _player;
    private float _speedMove = 0.6f;
    private float _playerCheckArea = 7.0f;

    #endregion


    #region UnityMethods

    private void Start()
    {
        _myRigidbody = GetComponent<Rigidbody>();
        _player = FindObjectOfType<PlayerController>().gameObject;
    }

    private void Update()
    {
        var distanceToPlayer = (_player.transform.position - transform.position).sqrMagnitude;
        if (distanceToPlayer < _playerCheckArea * _playerCheckArea)
        {
            transform.LookAt(_player.transform.position);

            var vectorX = _player.transform.position.x - transform.position.x;
            var vectorZ = _player.transform.position.z - transform.position.z;
            var vectorMove = new Vector3(vectorX, 0.0f, vectorZ);
            vectorMove.Normalize();
            _myRigidbody.velocity = vectorMove * _speedMove;
        }
    }

    #endregion
}

