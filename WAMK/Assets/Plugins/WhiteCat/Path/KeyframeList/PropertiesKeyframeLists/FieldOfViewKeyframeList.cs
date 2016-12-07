using UnityEngine;

namespace WhiteCat
{
	/// <summary>
	/// FieldOfViewKeyframeList
	/// </summary>
	[AddComponentMenu("White Cat/Path/Field Of View Keyframe List")]
	public class FieldOfViewKeyframeList : Path.FloatKeyframeList<Camera>
	{
		public override string targetPropertyName
		{
			get { return "Field of View"; }
		}


		protected override void Apply(Camera target, float value, MoveAlongPath movingObject)
		{
			target.fieldOfView = value;
		}

	} // class FieldOfViewKeyframeList

} // namespace WhiteCat