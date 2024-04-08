using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    public Dictionary<string, int> characters = new Dictionary<string, int>();
    public static PlayerManager instance;
    [SerializeField] private AttributeUI[] attributes;
    [SerializeField] private GameObject startButton;
    public TextMeshProUGUI totalPointsUI;

    private int totalPoints = 30;
    public int TotalPoints
    {
        get => totalPoints;
        set
        {
            totalPoints = Mathf.Clamp(value,0,60);
        }
    }


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        totalPointsUI.text = totalPoints.ToString();

        foreach (AttributeUI attribute in attributes)
        {
            characters.Add(attribute.attributeName, 0);
        }
    }

    public void Increment()
    {
        totalPoints++;
        totalPointsUI.text = totalPoints.ToString();
    }

    public void Decrement()
    {
        totalPoints--;
        totalPointsUI.text = totalPoints.ToString();
    }

    public void Init()
    {
        foreach (AttributeUI attribute in attributes)
        {
            attribute.HideButtons();
        }
        startButton.SetActive(false);
        totalPointsUI.gameObject.SetActive(false);
        GameManager.instance.GPTs[1].SendToChatGPT("{\"player_said\":\"" + "I've finished establishing my character" + "}");
        GameManager.instance.chatBox.SetActive(true);
    }

    public void RefreshStats(Growth target)
    {
        foreach (AttributeUI attribute in attributes)
        {
            if(attribute.attributeName == target.attribute)
            {
                characters[attribute.attributeName] = Mathf.Clamp(target.amount + target.amount, 0, 10);
                attribute.amountText.text = characters[target.attribute].ToString();
            }
        }
    }
}
