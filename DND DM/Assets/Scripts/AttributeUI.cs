using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AttributeUI : MonoBehaviour
{
    public string attributeName;
    [HideInInspector] public int amount;
    [SerializeField] private GameObject[] buttons;
    [SerializeField] TextMeshProUGUI amountText;
    [SerializeField] TextMeshProUGUI nameText;

    private void Awake()
    {
        nameText.text = attributeName;
    }
    public void Increment()
    {
        if (PlayerManager.instance.TotalPoints > 0 && amount < 10 && PlayerManager.instance.totalPointsUI.gameObject.activeSelf) 
        {
            PlayerManager.instance.Decrement();
            amount++;
            amountText.text = amount.ToString();
        }
    }

    public void Decrement()
    {
        if (PlayerManager.instance.TotalPoints < 60 && amount > 0 && PlayerManager.instance.totalPointsUI.gameObject.activeSelf) 
        {
            PlayerManager.instance.Increment();
            amount--;
            amountText.text = amount.ToString();
        }
    }

    public void ChangeAmount(int amount)
    {
        amount = Mathf.Clamp(amount+amount,0,10);
        amountText.text = amount.ToString();
    }

    public void HideButtons()
    {
        foreach (GameObject button in buttons)
        {
            button.SetActive(false);
        }
    }
}
