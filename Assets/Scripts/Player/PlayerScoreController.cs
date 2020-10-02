using UnityEngine;

namespace learn3d
{
    public class PlayerScoreController : MonoBehaviour
    {
        #region Fields

        private int _kills = 0;
        private int _maxKills = 50;

        #endregion


        #region UnityMethods
        private void OnEnable()
        {
            EventManager.StartListening("EnemyDie", EnemyDie);
        }

        private void OnDisable()
        {
            EventManager.StopListening("EnemyDie", EnemyDie);
        }

        private void Start()
        {
            EventParam currentParams = new EventParam();
            currentParams.kills = _kills;
            currentParams.maxKills = _maxKills;

            EventManager.TriggerEvent("PlayerShowScore", currentParams);
        }

        #endregion


        #region Methods

        private void EnemyDie(EventParam eventParam)
        {
            _kills++;

            if (_kills >= _maxKills)
            {
                EventManager.TriggerEvent("PlayerEndGame");
            }
            else
            {
                EventParam currentParams = new EventParam();
                currentParams.kills = _kills;
                currentParams.maxKills = _maxKills;
                EventManager.TriggerEvent("PlayerShowScore", currentParams);
            }
        }

        #endregion
    }
}
