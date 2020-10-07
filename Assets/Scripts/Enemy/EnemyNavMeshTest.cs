using UnityEngine;
using UnityEngine.AI;

public class EnemyNavMeshTest : MonoBehaviour
{
    #region Fields

    [SerializeField] private Transform _target;

    private NavMeshAgent _navMeshAgent;
    private Vector3 _currentTargetPosition;
    private Vector3 _lastTargetPosition;

    private float _checkError = 5.0f;

    #endregion


    #region UnityMethods

    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _currentTargetPosition = _target.position;
        _lastTargetPosition = _currentTargetPosition;
    }

    private void Update()
    {
        _currentTargetPosition = _target.position;

        var distanceTargetCheck = (_target.transform.position - transform.position).sqrMagnitude;
        if ((_currentTargetPosition != _lastTargetPosition) && (distanceTargetCheck > _checkError * _checkError))
        {
            _navMeshAgent.SetDestination(_currentTargetPosition);
        }
    }

    #endregion
}
