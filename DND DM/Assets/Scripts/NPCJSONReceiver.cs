using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class NPCJSONReceiver
{
	public string animation_name;
	public string reply_to_player;
	public string npcName;
	public bool optionMode;
	public bool isFinishedLore;
	public DNDOption[] options;
	public ChangeAttribute changeAttribute;
}

[Serializable]
public class DNDOption
{
	public string content;
	public string attribute;
	public int amount;
}

[Serializable]
public class ChangeAttribute
{
	public string attribute;
	public int changeAmount;
}
