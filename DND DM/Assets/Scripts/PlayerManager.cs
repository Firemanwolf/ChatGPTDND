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
            characters.Add(attribute.attributeName, attribute.amount);
            attribute.HideButtons();
        }
        startButton.SetActive(false);
        totalPointsUI.gameObject.SetActive(false);
    }

    public void RefreshStats()
    {
        foreach (KeyValuePair<string,int> character in characters)
        {
            foreach (AttributeUI attribute in attributes)
            {
                if (character.Key.Equals(attribute.attributeName))
                {
                    attribute.ChangeAmount(character.Value);
                    break;
                }
            }
        }
    }
}
