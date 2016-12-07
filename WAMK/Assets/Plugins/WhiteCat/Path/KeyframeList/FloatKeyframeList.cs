using UnityEngine;
using System;

#if UNITY_EDITOR
using UnityEditor;
using WhiteCatEditor;
#endif

namespace WhiteCat
{
	public partial class Path
	{
		/// <summary>
		/// FloatKeyframeList
		/// </summary>
		public abstract class FloatKeyframeList<Component> : KeyframeList<float, FloatKeyframe, Component>
			where Component : UnityEngine.Component
		{
			protected override float Lerp(float from, float to, float t)
			{
				return from + (to - from) * t;
			}
			 
#if UNITY_EDITOR

			static GUIStyle _labelStyle;


			protected override void DrawKeyValueInScene(Vector3 position, float value, float handleSize)
			{
				if (_labelStyle == null)
				{
					_labelStyle = new GUIStyle();
					_labelStyle.normal.textColor = Color.white;
					_labelStyle.alignment = TextAnchor.LowerCenter;
				}

				var point = HandleUtility.WorldToGUIPoint(position);
				_rect.Set(point.x - 100f, point.y - 30f, 200f, 20f);

				Handles.BeginGUI();
				GUI.Label(_rect, value.ToString(), _labelStyle);
				Handles.EndGUI();
			}


			protected override float DrawKeyValueHandle(Vector3 position, float value, float handleSize)
			{
				return value;
			}


			protected override float keyValueWidth
			{
				get { return 88f; }
			}


			protected override float DrawKeyValue(float value, Rect rect)
			{
				EditorKit.RecordAndSetLabelWidth(38f);
				value = EditorGUI.FloatField(rect, "Value", value);
				EditorKit.RestoreLabelWidth();
				return value;
			}

#endif // UNITY_EDITOR

		} // class FloatKeyframeList

	} // class Path

} // namespace WhiteCat