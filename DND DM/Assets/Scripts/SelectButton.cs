using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using ChatGPTWrapper;

public class SelectButton : MonoBehaviour
{

	[SerializeField] TextMeshProUGUI contentText;
	[SerializeField] TextMeshProUGUI attributeText;
	[SerializeField] TextMeshProUGUI amountText;
	[SerializeField] ChatGPTConversation chatGPT;
	public void OptionInit(DNDOption option)
    {
		amountText.text = option.amount.ToString();
		contentText.text = option.content;
		attributeText.text = option.attribute;
    }

    public void SubmitChatMessage()
    {
		chatGPT.SendToChatGPT("{\"player_chose\":\"" + contentText.text + "\","
			+ "\"Player " + attributeText.text +":" + "\"" + PlayerManager.instance.characters[attributeText.text]
			+ "}") ;
    }
}
