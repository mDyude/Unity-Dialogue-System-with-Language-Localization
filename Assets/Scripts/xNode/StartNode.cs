using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using XNode;

public class StartNode : BaseNode
{
	[Output] public int exit;
	// LocalizedString localizedString = "Start";
	LocalizedString localizedStart = new LocalizedString("start");


	public override LocalizedString GetLocalizedString()
	{
		return localizedStart;
	}


}