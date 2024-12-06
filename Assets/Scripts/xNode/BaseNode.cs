using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using XNode;

public class BaseNode : Node {

	public virtual LocalizedString GetLocalizedString()
	{
		return null;
	}
	public virtual LocalizedString GetSelectionsLocString(int decision) {
		return null;
	} 

	// public virtual Image GetImage()
	// {
	// 	return null;
	// }
	public virtual Sprite GetSprite()
	{
		return null;
	}
	
	public virtual LocalizedString GetResponseStrings(int decision) {
		return null;
	}

	public virtual bool HasResponse(int decision) {
		return false;
	}
}