using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using ChatGPTWrapper;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField]
    ChatGPTConversation chatGPT;

    [SerializeField]
    TMP_InputField iF_PlayerTalk;
    [SerializeField]
    TextMeshProUGUI tX_AIReply;
    [SerializeField]
    NPCController npc;

    [SerializeField] private GameObject chatBox;
    [SerializeField] private GameObject optionsBox;

    [SerializeField] private GameObject playerAttributes;

    [SerializeField] private SelectButton[] options;

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
        chatGPT.Init();

    }

    private void Start()
    {
        chatGPT.SendToChatGPT("{\"player_said\":" + "\"Hello! Who are you?\"}");

    }
    // Update is called once per frame
    void Update()
    {
		if (Input.GetButtonUp("Submit") && chatBox.activeSelf)
		{
			SubmitChatMessage();
		}
	}

    public void ReceiveChatGPTReply(string message)
    {
        print(message);
        try
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
            NPCJSONReceiver npcJSON = JsonUtility.FromJson<NPCJSONReceiver>(message);
            string talkLine = npcJSON.reply_to_player;
            npcName = npcJSON.npcName;
            chatBox.SetActive(!npcJSON.optionMode);
            optionsBox.SetActive(npcJSON.optionMode);
            if (npcJSON.optionMode)
            {
                for (int i = 0; i < npcJSON.options.Length; i++)
                {
                    options[i].OptionInit(npcJSON.options[i]);
                }
            }
            tX_AIReply.text = "<color=#ff7082>" + npcName + ": </color>" + talkLine;
            if (npcJSON.isFinishedLore) playerAttributes.SetActive(true);
            ChangeAttribute change = npcJSON.changeAttribute;
            if (change != null) PlayerManager.instance.characters[change.attribute] += change.changeAmount;
            npc.ShowAnimation(npcJSON.animation_name);
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            string talkLine = "Don't say that!";
            tX_AIReply.text = "<color=#ff7082>" + npcName + ": </color>" + talkLine;
        }
    }

    public void SubmitChatMessage()
    {
        if (iF_PlayerTalk.text != "")
        {
            Debug.Log("Message sent: " + iF_PlayerTalk.text);
            chatGPT.SendToChatGPT("{\"player_said\":\"" + iF_PlayerTalk.text + "\"" +
                "attribute:" +
                "" +
                "}");
            ClearText();
        }
    }

    void ClearText()
    {
        iF_PlayerTalk.text = "";
    }
}
