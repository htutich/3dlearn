using System;
using UnityEngine;


public class EnemyTurretController : MonoBehaviour
{
    #region Fields

    private GameObject _player;
    private RaycastHit _raycastHit;
    private Ray _ray;

    private string _uniqueID;
    private float _playerCheckArea = 5f;
    private bool _hasPlayer = true;

    #endregion

    #region UnityMethods

    private void Start()
    {
        _uniqueID = Guid.NewGuid().ToString();
        var EnemyTurretShootController = gameObject.GetComponentInChildren<EnemyTurretShootController>();
            EnemyTurretShootController.UniqueID = _uniqueID;
        _player = FindObjectOfType<PlayerController>().gameObject;
    }

    private void Update()
    {
        if (_hasPlayer)
        {
            var distancePlayerCheck = (_player.transform.position - transform.position).sqrMagnitude;
            if (distancePlayerCheck < _playerCheckArea * _playerCheckArea)
            {
                transform.LookAt(_player.transform.position);
                Debug.DrawLine(transform.position, _player.transform.position, Color.blue);

                var startRaycastPosition = transform.position;
                var playerPosition = _player.transform.position - startRaycastPosition;
                var rayCast = Physics.Raycast(startRaycastPosition, playerPosition, out _raycastHit, playerPosition.magnitude, ~ (1 << LayerMask.GetMask("Player")));
                
                if (rayCast && _raycastHit.collider.gameObject.CompareTag("Player"))
                {
                    EventParam currentParams = new EventParam();
                    currentParams.uniqueID = _uniqueID;

                    EventManager.TriggerEvent("TurretShoot", currentParams);
                }
            }
        }
    }

    #endregion
}
