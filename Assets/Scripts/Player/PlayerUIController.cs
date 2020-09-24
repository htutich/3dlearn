using UnityEngine;
using UnityEngine.UI;


public class PlayerUIController : MonoBehaviour
{
    #region PrivateData

    private int _kills = 0;
    private int _maxKills = 10;

    [SerializeField] private Text _score;

    #endregion


    #region UnityMethods

    private void OnEnable()
    {
        EnemyHealthManager.onEnemyDie += EnemyDie;
    }

    private void OnDisable()
    {
        EnemyHealthManager.onEnemyDie -= EnemyDie;
    }

    private void Start()
    {
        _score.text = $"Score: {_kills}";
    }

    #endregion


    #region Methods

    private void EnemyDie()
    {
        _kills++;
        _score.text = $"Score: {_kills} / {_maxKills}";
    }

    #endregion
}
