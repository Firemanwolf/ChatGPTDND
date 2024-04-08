using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AttributeUI : MonoBehaviour
{
    public string attributeName;
    [SerializeField] private GameObject[] buttons;
    public TextMeshProUGUI amountText;
    [SerializeField] TextMeshProUGUI nameText;

    private void Awake()
    {
        nameText.text = attributeName;
    }
    public void Increment()
    {
        if (PlayerManager.instance.TotalPoints > 0 && int.Parse(amountText.text) < 10 && PlayerManager.instance.totalPointsUI.gameObject.activeSelf) 
        {
            PlayerManager.instance.Decrement();
            PlayerManager.instance.characters[attributeName]++;
            amountText.text = PlayerManager.instance.characters[attributeName].ToString();
        }
    }

    public void Decrement()
    {
        if (PlayerManager.instance.TotalPoints < 60 && int.Parse(amountText.text) > 0 && PlayerManager.instance.totalPointsUI.gameObject.activeSelf) 
        {
            PlayerManager.instance.Increment();
            PlayerManager.instance.characters[attributeName]--;
            amountText.text = PlayerManager.instance.characters[attributeName].ToString();
        }
    }

    public void HideButtons()
    {
        foreach (GameObject button in buttons)
        {
            button.SetActive(false);
        }
    }
}
