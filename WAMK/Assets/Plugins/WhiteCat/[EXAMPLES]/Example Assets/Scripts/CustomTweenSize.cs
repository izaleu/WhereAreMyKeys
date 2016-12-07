using UnityEngine;
using WhiteCat;

public class CustomTweenSize : TweenFloat
{
	public override float current
	{
		get { return transform.localScale.x; }
		set { transform.localScale = new Vector3(value, value, value); }
	}
}