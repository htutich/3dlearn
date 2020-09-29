using UnityEngine;
using UnityEngine.UI;


public class PlayerUIVisualizer : MonoBehaviour
{
    #region Fields

    [SerializeField] private Text _score;
    [SerializeField] private GameObject _canvas;

    #endregion


    #region UnityMethods

    private void OnEnable()
    {
        EventManager.StartListening("PlayerShowScore", PlayerShowScore);
    }

    private void OnDisable()
    {
        EventManager.StopListening("PlayerShowScore", PlayerShowScore);
    }

    private void Start()
    {
        _canvas.SetActive(true);
    }

    #endregion


    #region Methods

    private void PlayerShowScore(EventParam eventParam)
    {
        _score.text = $"Score: {eventParam.kills} / {eventParam.maxKills}";
    }

    #endregion
}
