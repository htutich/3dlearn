using UnityEngine;
using UnityEngine.UI;


public class PlayerUIVisualizer : MonoBehaviour
{
    #region Fields

    [SerializeField] private Text _score;

    #endregion


    #region UnityMethods

    private void OnEnable()
    {
        EventManager.actions.onPlayerShowScore += PlayerShowScore;
    }

    private void OnDisable()
    {
        EventManager.actions.onPlayerShowScore -= PlayerShowScore;
    }

    #endregion


    #region Methods

    private void PlayerShowScore(int minValue, int maxValue)
    {
        _score.text = $"Score: {minValue} / {maxValue}";
    }

    #endregion
}
