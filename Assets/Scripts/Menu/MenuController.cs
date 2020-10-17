using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuController : MonoBehaviour
{
    #region Fields

    [SerializeField] private GameObject _pauseGameUI;
    [SerializeField] private GameObject _menuSettingsModal;
    [SerializeField] private GameObject _menuSelectLevelModal;
    [SerializeField] private bool _isMainMenu = true;
    
    private AudioMenager _audioMenager;
    private float _defaultTimeScale = 1.0f;
    private bool _isMenuPauseActive = false;

    #endregion


    #region UnityMethods

    private void Start()
    {
        Time.timeScale = _defaultTimeScale;
        _audioMenager = FindObjectOfType<AudioMenager>();
    }

    private void Update()
    {
        if (!_isMainMenu)
        {
            if (!_isMenuPauseActive && Input.GetKeyDown(KeyCode.Escape))
            {
                EnablePause();
                _isMenuPauseActive = true;
            }
            else if(_isMenuPauseActive && Input.GetKeyDown(KeyCode.Escape))
            {
                DisablePause();
                _isMenuPauseActive = false;
            }
        }
    }

    #endregion


    #region Methods

    private void EnablePause()
    {
        Time.timeScale = 0.0f;
        _pauseGameUI.SetActive(true);
    }

    public void DisablePause()
    {
        Time.timeScale = _defaultTimeScale;
        _pauseGameUI.SetActive(false);
    }

    public void ShowSettings()
    {
        _menuSettingsModal.SetActive(true);
    }

    public void CloseSettings()
    {
        _menuSettingsModal.SetActive(false);
    }

    public void ShowSelectLevel()
    {
        _menuSelectLevelModal.SetActive(true);
    }

    public void CloseSelectLevel()
    {
        _menuSelectLevelModal.SetActive(false);
    }

    public void LoadLevelMenu()
    {
        _audioMenager.SetIsMenuValue(true);
        SceneManager.LoadScene("Menu");
    }

    public void LoadLevelDungeon()
    {
        _audioMenager.SetIsMenuValue(false);
        SceneManager.LoadScene("Dungeon");
    }

    public void LoadLevelPoligon()
    {
        _audioMenager.SetIsMenuValue(false);
        SceneManager.LoadScene("Poligon");
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Exit()
    {
        Application.Quit();
    }
    
    #endregion
}
