using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class StoryJSONReceiver
{
	public string animation_name;
	public string reply_to_player;
	public string npcName;
	public string chatType;
	public Growth[] growth;
	public DNDOption[] options;
}

[Serializable]
public class DNDOption
{
	public string content;
	public string attribute;
	public int amount;
}

[Serializable]
public class DiceCheckJSONReceiver
{
	public string result;
}

public class PlayerDiceInput
{
	public string attribute;
	public string inputAmount;
	public string requiredAmount;
}

[Serializable]
public class Growth
{
	public string attribute;
	public int amount;
}

[Serializable]
public class GuideJSONReceiver
{
	public string animation_name;
	public string reply_to_player;
	public string npcName;
	public bool isPanelOn;
	public string chatType;
}
