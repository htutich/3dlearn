using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuController : MonoBehaviour
{
    #region UnityMethods
    #endregion


    #region Methods

    public void LoadLevelMenu()
    {
        Debug.Log("ToMenu");
        SceneManager.LoadScene("Menu");
    }

    public void LoadLevelStart()
    {
        Debug.Log("ToStart");

        SceneManager.LoadScene("RoomPlacerTest");
    }

    public void Exit()
    {
        Debug.Log("ToExit");
        Application.Quit();
    }
    
    #endregion
}
