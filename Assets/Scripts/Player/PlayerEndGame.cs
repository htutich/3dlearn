using System.Collections;
using UnityEngine;


public class PlayerEndGame : MonoBehaviour
{
    #region Fields

    [SerializeField] private GameObject _completeGameUI;

    #endregion


    #region UnityMethods

    private void OnEnable()
    {
        EventManager.StartListening("PlayerEndGame", EndGame);
    }

    private void OnDisable()
    {
        EventManager.StopListening("PlayerEndGame", EndGame);
    }

    private void Start()
    {
        _completeGameUI.SetActive(false);
    }

    #endregion


    #region Methods

    private void EndGame(EventParam eventParams)
    {
        _completeGameUI.SetActive(true);
    }

    #endregion
}
