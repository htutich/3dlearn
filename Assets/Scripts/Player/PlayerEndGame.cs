using UnityEngine;


public class PlayerEndGame : MonoBehaviour
{
    #region PrivateData

    [SerializeField] private GameObject _completeGameUI;

    #endregion


    #region OnEnable

    private void OnEnable()
    {
        PlayerController.onPlayerEndGame += EndGame;
    }

    #endregion


    #region OnDisable

    private void OnDisable()
    {
        PlayerController.onPlayerEndGame -= EndGame;
    }

    #endregion


    #region Start

    private void Start()
    {
        _completeGameUI.SetActive(false);
    }

    #endregion


    #region Methods

    private void EndGame()
    {
        _completeGameUI.SetActive(true);
    }

    #endregion
}
