using UnityEngine;

public class PlayerScoreController : MonoBehaviour
{
    #region Fields

    private int _kills = 0;
    private int _maxKills = 10;

    #endregion


    #region UnityMethods
    private void OnEnable()
    {
        EventManager.actions.onEnemyDie += EnemyDie;
    }

    private void OnDisable()
    {
        EventManager.actions.onEnemyDie -= EnemyDie;
    }

    private void Start()
    {
        EventManager.actions.PlayerShowScore(_kills, _maxKills);
    }

    #endregion


    #region Methods

    private void EnemyDie()
    {
        _kills++;

        if (_kills >= _maxKills)
        {
            EventManager.actions.PlayerEndGame();
        } else
        {
            EventManager.actions.PlayerShowScore(_kills, _maxKills);
        }
    }

    #endregion
}
