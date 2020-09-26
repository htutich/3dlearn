using System.Collections;
using UnityEngine;


public class PlayerEndGame : MonoBehaviour
{
    #region Fields

    [SerializeField] private GameObject _winGameUI;
    [SerializeField] private GameObject _loseGameUI;

    #endregion


    #region UnityMethods

    private void OnEnable()
    {
        EventManager.StartListening("PlayerEndGame", WinGame);
        EventManager.StartListening("PlayerDie", PlayerDie);
    }

    private void OnDisable()
    {
        EventManager.StopListening("PlayerEndGame", WinGame);
        EventManager.StopListening("PlayerDie", PlayerDie);
    }
    
    #endregion


    #region Methods

    private void WinGame(EventParam eventParams)
    {
        _winGameUI.SetActive(true);
    }

    private void PlayerDie(EventParam eventParams)
    {
        _loseGameUI.SetActive(true);
    }
    #endregion
}
