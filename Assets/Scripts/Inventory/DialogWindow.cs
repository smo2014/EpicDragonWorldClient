using System;
using UnityEngine;
using UnityEngine.UI;

public class DialogWindow : MonoBehaviour
{
    public Text titleText;
    public Text dialogText;

    public Image Window;
    public Image dialogImage;

    public Button yesButton;
    public Button noButton;
    public Button okButton;

    public event Action OnYesEvent;
    public event Action OnNoEvent;
    public event Action OnOkEvent;

    public void Show()
    {
        gameObject.SetActive(true);
        OnYesEvent = null;
        OnNoEvent = null;
        OnOkEvent = null;
    }

    internal void RewardWindow(int windowWidth, int windowHeight, int itemid)
    {
        Show();
        Window.rectTransform.sizeDelta = new Vector2 (windowWidth, windowHeight);
        yesButton.gameObject.SetActive(false);
        noButton.gameObject.SetActive(false);
        Item item = Inventory.Instance.itemDatabase.GetItemId(itemid);
        titleText.text = item.ItemName;
        dialogImage.enabled = true;
        dialogImage.sprite = item.Icon;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void OnYesButtonClick()
    {
        if (OnYesEvent != null)
            OnYesEvent();
        Hide();
    }

    public void OnNoButtonClick()
    {
        if (OnNoEvent != null)
            OnNoEvent();
        Hide();
    }

    public void OnOkButtonClick()
    {
        if (OnOkEvent != null)
            OnOkEvent();
        Hide();
    }
}
