
using UnityEngine;
using UnityEngine.UI;

public class ButtonWithEntityLink : MonoBehaviour
{
    [SerializeField]
    private Button _button;
    [SerializeField]
    private EntityLink _link;

    private void Awake()
    {
        _link.Init();
    }


    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        _link.Entity.isButtonClicked = true;
    }
}