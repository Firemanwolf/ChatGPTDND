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
	public void OptionInit(DNDOption option)
    {
		amountText.text = option.amount.ToString();
		contentText.text = option.content;
		attributeText.text = option.attribute;
    }

    public void SubmitChatMessage()
    {
		GameManager.instance.GPTs[2].SendToChatGPT("{\"attribute\":" + "\""+ attributeText.text + "\"" + "," + "\"playerInput\":"
			+ "\"" + PlayerManager.instance.characters[attributeText.text] + "\"" + ","
			+ "\"requirement\":" + "\"" + amountText.text + "\""
			+ "}") ;
    }
}
