using System;
using UnityEngine;


namespace learn3d
{
    public class EnemyTurretController : MonoBehaviour
    {
        #region Fields

        [SerializeField] private LayerMask _checkLayerMask;

        private GameObject _player;
        private RaycastHit _raycastHit;
        private Ray _ray;

        private string _uniqueID;
        private float _playerCheckArea = 10.0f;
        private bool _hasPlayer = true;

        #endregion


        #region UnityMethods

        private void Start()
        {
            _uniqueID = Guid.NewGuid().ToString();
            var EnemyTurretShootController = gameObject.GetComponentInChildren<EnemyTurretShootController>();
                EnemyTurretShootController.UniqueID = _uniqueID;
            _player = FindObjectOfType<PlayerAnimationMovement>()?.gameObject;
        }

        private void Update()
        {
            if (_hasPlayer)
            {
                var distancePlayerCheck = (_player.transform.position - transform.position).sqrMagnitude;
                if (distancePlayerCheck < _playerCheckArea * _playerCheckArea)
                {
                    var lookAtPlayer = _player.transform.position;
                    lookAtPlayer.y += 0.5f;
                    transform.LookAt(lookAtPlayer);
                    Debug.DrawLine(transform.position, _player.transform.position, Color.blue);

                    var startRaycastPosition = transform.position;
                    var playerPosition = _player.transform.position - startRaycastPosition;
                    var vectorPlayerPosition = new Vector3(playerPosition.x, playerPosition.y + 0.5f, playerPosition.z);
                    var rayCast = Physics.Raycast(startRaycastPosition, vectorPlayerPosition, out _raycastHit, playerPosition.magnitude, _checkLayerMask);

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
}
