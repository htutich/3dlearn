using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    #region Fields

    [SerializeField] private AudioClip _hoverMusic;
    [SerializeField] private AudioClip _clickMusic;

    private Button _myButton;
    private Text _myButtonText;
    private Color _buttonDefaultColor;
    private AudioSource _btnMusic;

    #endregion

    #region UnityMethods

    private void Start()
    {
        _myButton = GetComponent<Button>();
        _myButtonText = GetComponent<Text>();
        _btnMusic = GetComponent<AudioSource>();

        _buttonDefaultColor = _myButtonText.color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    { 
        _btnMusic.clip = _hoverMusic;
        _btnMusic.Play();

        _myButtonText.color = Color.grey;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _myButtonText.color = _buttonDefaultColor;
    }

    #endregion


    #region UnityMethods

    public void OnClickMusic()
    {
        _btnMusic.clip = _clickMusic;
        _btnMusic.Play();
    }

    #endregion
}
