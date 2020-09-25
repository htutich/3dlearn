using UnityEngine;


public class EventManager : MonoBehaviour
{
    public static EventManager actions;

    public delegate void PlayerAction();
    public delegate void PlayerScoreAction(int minValue = 0, int maxValue = 0);
    public delegate void EnemyAction();

    public event PlayerAction onPlayerShoot;
    public event PlayerAction onPlayerEndGame;
    public event PlayerScoreAction onPlayerShowScore;
    public event EnemyAction onEnemyDie;


    #region UnityMethods

    private void Awake()
    {
        actions = this;
    }

    #endregion


    #region Methods

    public void PlayerShoot()
    {
        onPlayerShoot?.Invoke();
    }

    public void PlayerEndGame()
    {
        onPlayerEndGame?.Invoke();
    }

    public void PlayerShowScore(int minValue = 0, int maxValue = 0)
    {
        onPlayerShowScore?.Invoke(minValue, maxValue);
    }

    public void EnemyDie()
    {
        onEnemyDie?.Invoke();
    }

    #endregion
}
