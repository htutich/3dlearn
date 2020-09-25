using UnityEngine;


public class PlayerEndGame : MonoBehaviour
{
    #region Fields

    [SerializeField] private GameObject _completeGameUI;

    #endregion


    #region UnityMethods

    private void OnEnable()
    {
        //PlayerController.onPlayerEndGame += EndGame;
    }

    private void OnDisable()
    {
        //PlayerController.onPlayerEndGame -= EndGame;
    }

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
