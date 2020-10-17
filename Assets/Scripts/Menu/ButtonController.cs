using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    #region Fields

    private Button _myButton;
    private Text _myButtonText;
    private Color _buttonDefaultColor;

    #endregion

    #region UnityMethods

    private void Start()
    {
        _myButton = GetComponent<Button>();
        _myButtonText = GetComponent<Text>();

        _buttonDefaultColor = _myButtonText.color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _myButtonText.color = Color.grey;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _myButtonText.color = _buttonDefaultColor;
    }

    #endregion
}
