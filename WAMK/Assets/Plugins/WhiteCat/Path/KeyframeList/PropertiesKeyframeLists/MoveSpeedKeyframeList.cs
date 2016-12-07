using UnityEngine;

namespace WhiteCat
{
	/// <summary>
	/// MoveSpeedKeyframeList
	/// </summary>
	[AddComponentMenu("White Cat/Path/Move Speed Keyframe List")]
	public class MoveSpeedKeyframeList : Path.FloatKeyframeList<MoveAlongPath>
	{
		public override string targetPropertyName
		{
			get { return "Move Speed"; }
		}


		protected override void Apply(MoveAlongPath target, float value, MoveAlongPath movingObject)
		{
			target.speed = value;
		}

	} // class MoveSpeedKeyframeList

} // namespace WhiteCat