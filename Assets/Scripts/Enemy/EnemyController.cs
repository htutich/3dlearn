using UnityEngine;


public class EnemyController : MonoBehaviour
{
    #region PrivateData

    private Rigidbody _myRB;
    private float _speedMove = 1.2f;
    private GameObject _thePlayer;

    #endregion


    #region Start

    private void Start()
    {
        _myRB = GetComponent<Rigidbody>();
    }

    #endregion


    #region Update

    private void Update()
    {
        if (_thePlayer != null)
        {
            transform.LookAt(_thePlayer.transform.position);

            var vectorX = _thePlayer.transform.position.x - transform.position.x;
            var vectorZ = _thePlayer.transform.position.z - transform.position.z;
            var vectorMove = new Vector3(vectorX, 0.0f, vectorZ);
            vectorMove.Normalize();
            _myRB.velocity = vectorMove * _speedMove;
        }
    }

    #endregion


    #region OnTriggerEnter

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _thePlayer = other.gameObject;
        }
    }

    #endregion

}

