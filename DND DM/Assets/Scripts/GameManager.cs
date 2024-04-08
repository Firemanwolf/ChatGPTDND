using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using ChatGPTWrapper;
using System;
using UnityEngine.SceneManagement;
using SimpleJSON;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField]
    TMP_InputField iF_PlayerTalk;
    [SerializeField]
    TextMeshProUGUI tX_AIReply;
    [SerializeField]
    NPCController npc;

    [SerializeField] public GameObject chatBox;
    [SerializeField] private GameObject optionsBox;

    [SerializeField] private GameObject playerAttributes;

    [SerializeField] private SelectButton[] options;

    public ChatGPTConversation[] GPTs;

    private string currentState;

    string npcName;
    string playerName = "Player";

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        //chatGPT._initialPrompt = string.Format(chatGPT._initialPrompt, npcName, playerName) + initialPrompt_CommonPart;

        //Enable ChatGPT
        foreach (ChatGPTConversation GPT in GPTs)
        {
            GPT.Init();
        }

    }

    private void Start()
    {
        GPTs[0].SendToChatGPT("{\"player_said\":" + "\"Hello! Please introduce yourself and help me to start the adventure\"}");

    }
    // Update is called once per frame
    void Update()
    {
		if (Input.GetButtonUp("Submit") && chatBox.activeSelf)
		{
            SubmitReply();
		}
	}

    public void ReceiveGuiderReply(string message)
    {
        print(message);
        //try
        //{
        message = JSONMessageConversion(message);
        GuideJSONReceiver npcJSON = JsonUtility.FromJson<GuideJSONReceiver>(message);
        currentState = npcJSON.chatType;
        string talkLine = npcJSON.reply_to_player;
        npcName = npcJSON.npcName;
        npc.ShowAnimation(npcJSON.animation_name);
        playerAttributes.SetActive(npcJSON.isPanelOn);
        chatBox.SetActive(!npcJSON.isPanelOn);
        tX_AIReply.text = "<color=#ff7082>" + npcName + ": </color>" + talkLine;
        /*
        catch (Exception e)
        {
            Debug.Log(e.Message);
            string talkLine = "Don't say that!";
            tX_AIReply.text = "<color=#ff7082>" + npcName + ": </color>" + talkLine;
        }
        */
    }

    public void ReceiveTellerReply(string message)
    {
        print(message);
        //try
        //{
        message = JSONMessageConversion(message);
        StoryJSONReceiver npcJSON = JsonUtility.FromJson<StoryJSONReceiver>(message);
        currentState = npcJSON.chatType;
        string talkLine = npcJSON.reply_to_player;
        npcName = npcJSON.npcName;
        npc.ShowAnimation(npcJSON.animation_name);
        tX_AIReply.text = "<color=#ff7082>" + npcName + ": </color>" + talkLine;
        if (npcJSON.options != null && npcJSON.options.Length != 0)
        {
            chatBox.SetActive(false);
            optionsBox.SetActive(true);
            for (int i = 0; i < npcJSON.options.Length; i++)
            {
                options[i].OptionInit(npcJSON.options[i]);
            }
        }
        else { 
            chatBox.SetActive(true);
            optionsBox.SetActive(false);
        }

        if (npcJSON.growth != null&& npcJSON.growth.Length != 0)
        foreach (Growth change in npcJSON.growth)
        {
            PlayerManager.instance.RefreshStats(change);
        }
        /*
        catch (Exception e)
        {
            Debug.Log(e.Message);
            string talkLine = "Don't say that!";
            tX_AIReply.text = "<color=#ff7082>" + npcName + ": </color>" + talkLine;
        }
        */
    }

    public void ReceiveDiceCheckerReply(string message)
    {
        print(message);
        message = JSONMessageConversion(message);
        DiceCheckJSONReceiver npcJSON = JsonUtility.FromJson<DiceCheckJSONReceiver>(message);
        GPTs[1].SendToChatGPT(npcJSON.result);
    }

    string JSONMessageConversion(string message)
    {
        if (!message.EndsWith("}"))
        {
            if (message.Contains("}"))
            {
                message = message.Substring(0, message.LastIndexOf("}") + 1);
            }
            else
            {
                message += "}";
            }
        }
        message = message.Replace("\\", "\\\\");
        return message;
    }
    /*
    public void SubmitChatMessage()
    {
        if (iF_PlayerTalk.text != "")
        {
            Debug.Log("Message sent: " + iF_PlayerTalk.text);
            currentChatBot.SendToChatGPT("{\"player_said\":\"" + iF_PlayerTalk.text + "}");
            ClearText();
        }
    }
    */
    void ClearText()
    {
        iF_PlayerTalk.text = "";
    }

    public void SubmitReply()
    {
        switch (currentState)
        {
            case "guider":
                if(iF_PlayerTalk.text != null)
                    GPTs[0].SendToChatGPT("{\"player_said\":\"" + iF_PlayerTalk.text + "}");
                ClearText();
                break;
            case "storyTeller":
                if (iF_PlayerTalk.text != "")
                {
                    Debug.Log("Message sent: " + iF_PlayerTalk.text);
                    if (iF_PlayerTalk.text != null)
                        GPTs[1].SendToChatGPT("{\"player_said\":\"" + iF_PlayerTalk.text + "}");
                    ClearText();
                }
                break;
        }
    }
}
