using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UISelectableButton : UIButton
{
    [SerializeField] private Image m_selectImage;

    public UnityEvent OnSelect;

    public UnityEvent OnUnSelect;

    public override void SetFocuse()
    {
        base.SetFocuse();

        m_selectImage.enabled = true;

        OnSelect?.Invoke();
    }

    public override void SetUnFocuse()
    {
        base.SetUnFocuse();

        m_selectImage.enabled = false;

        OnUnSelect?.Invoke();
    }
}
