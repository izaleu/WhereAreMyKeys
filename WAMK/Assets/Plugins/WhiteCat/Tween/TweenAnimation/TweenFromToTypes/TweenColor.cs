using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace WhiteCat
{
	/// <summary>
	/// Color 类型的插值动画
	/// </summary>
	public abstract class TweenColor : TweenFromTo<Color>
	{
		/// <summary> R 通道开关 </summary>
		public bool toggleR = true;

		/// <summary> G 通道开关 </summary>
		public bool toggleG = true;

		/// <summary> B 通道开关 </summary>
		public bool toggleB = true;

		/// <summary> A 通道开关 </summary>
		public bool toggleA = true;


		// 根据插值系数更改插值状态
		public override void OnTween(float factor)
		{
			var temp = current;
			if (toggleR) temp.r = (_to.r - _from.r) * factor + _from.r;
			if (toggleG) temp.g = (_to.g - _from.g) * factor + _from.g;
			if (toggleB) temp.b = (_to.b - _from.b) * factor + _from.b;
			if (toggleA) temp.a = (_to.a - _from.a) * factor + _from.a;
			current = temp;
		}

#if UNITY_EDITOR

		SerializedProperty[] _toggleProperties;
		SerializedProperty[] _fromProperties;
		SerializedProperty[] _toProperties;
		SerializedProperty _fromProperty;
		SerializedProperty _toProperty;


		protected override void Editor_OnEnable()
		{
			base.Editor_OnEnable();

			_toggleProperties = new SerializedProperty[4];
			_toggleProperties[0] = editor.serializedObject.FindProperty("toggleR");
			_toggleProperties[1] = editor.serializedObject.FindProperty("toggleG");
			_toggleProperties[2] = editor.serializedObject.FindProperty("toggleB");
			_toggleProperties[3] = editor.serializedObject.FindProperty("toggleA");

			_fromProperty = editor.serializedObject.FindProperty("_from");
			_toProperty = editor.serializedObject.FindProperty("_to");

			_fromProperties = GetColorProperties(_fromProperty);
			_toProperties = GetColorProperties(_toProperty);
		}


		protected override void Editor_OnDisable()
		{
			base.Editor_OnDisable();
			_toggleProperties = null;
			_fromProperties = null;
			_toProperties = null;
			_fromProperty = null;
			_toProperty = null;
		}


		protected override void DrawExtraFields()
		{
			DrawFromToChannels();
        }


		protected void DrawFromToChannels()
		{
			AllColorChannelsField(_fromProperty, _toProperty);

			ColorChannelField(_toggleProperties[0], "R", _fromProperties[0], _toProperties[0]);
			ColorChannelField(_toggleProperties[1], "G", _fromProperties[1], _toProperties[1]);
			ColorChannelField(_toggleProperties[2], "B", _fromProperties[2], _toProperties[2]);
			ColorChannelField(_toggleProperties[3], "A", _fromProperties[3], _toProperties[3]);
		}

#endif // UNITY_EDITOR

	} // class TweenColor

} // namespace WhiteCat