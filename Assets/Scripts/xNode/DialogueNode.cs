using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEditor;
using UnityEngine;
using XNode;

public class DialogueNode : BaseNode
{
	[Input] public int entry;
	[Output] public int exit;
	[Output] public int decision1;
	[Output] public int decision2;
	[Output] public int decision3;
	[Output] public int decision4;
	[Output] public int decision5;



	// public LocalizedString speakerName;
	public LocalizedString dialogueLine;
	public Sprite background;
	public LocalizedString selection1;
	public LocalizedString selection2;
	public LocalizedString selection3;
	public LocalizedString selection4;
	public LocalizedString selection5;



	public override LocalizedString GetLocalizedString()
	{
		// return "DialogueNode/" + speakerName.key + "/" + dialogueLine.key;
		return dialogueLine;
	}

	public override LocalizedString GetResponseStrings(int response)
	{
		if (response == 1)
		{
			return selection1;
		}
		else if (response == 2)
		{
			return selection2;
		}
		else if (response == 3)
		{
			return selection3;
		}
		else if (response == 4)
		{
			return selection4;
		}
		else if (response == 5)
		{
			return selection5;
		}
		else
		{
			return null;
		}
	}


	public override Sprite GetSprite()
	{
		return background;
	}

	public override bool HasResponse(int decision)
	{
		if (decision == 1)
		{
			Debug.Log("selection1.Value: " + selection1.Value);
			return selection1.Value != string.Empty;
		}
		else if (decision == 2)
		{
			return selection2.Value != string.Empty;
		}
		else if (decision == 3)
		{
			return selection3.Value != string.Empty;
		}
		else if (decision == 4)
		{
			return selection4.Value != string.Empty;
		}
		else if (decision == 5)
		{
			return selection5.Value != string.Empty;
		}
		else
		{
			return false;
		}
	}
}