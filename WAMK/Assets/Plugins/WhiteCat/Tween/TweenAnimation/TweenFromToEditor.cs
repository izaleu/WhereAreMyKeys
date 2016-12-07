#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;

namespace WhiteCat
{
	/// <summary>
	/// TweenFromTo Editor
	/// </summary>
	public partial class TweenFromTo<T>
	{
		const float _toggleRatio = 0.1f;
		const float _intervalRatio = 0.1f;
		const float _fromLabelWidth = 35;
		const float _toLabelWidth = 20;

		static Rect _rect;
		static float _labelWidth;
		static float _lineWidth;
		static float _fieldWidth;


		SerializedProperty _tweenerProperty;


		protected override void Editor_OnEnable()
		{
			_tweenerProperty = editor.serializedObject.FindProperty("_tweener");
		}


		protected override void Editor_OnDisable()
		{
			_tweenerProperty = null;
		}


		protected sealed override void Editor_OnInspectorGUI()
		{
			editor.serializedObject.Update();

			EditorGUILayout.PropertyField(_tweenerProperty);
			EditorGUILayout.Space();
			DrawExtraFields();

			editor.serializedObject.ApplyModifiedProperties();
		}


		protected abstract void DrawExtraFields();


		protected static void FloatChannelField(SerializedProperty toggle, string label, SerializedProperty from, SerializedProperty to)
		{
			_labelWidth = EditorGUIUtility.labelWidth;

			_rect = EditorGUILayout.GetControlRect();
			_lineWidth = _rect.width;
			_fieldWidth = (_lineWidth * (1f - _intervalRatio - _intervalRatio - _toggleRatio) - _fromLabelWidth - _toLabelWidth) * 0.5f;

			_rect.width = _lineWidth * _toggleRatio;
			toggle.boolValue = EditorGUI.ToggleLeft(_rect, label, toggle.boolValue);

			EditorGUIUtility.labelWidth = _fromLabelWidth;
			_rect.x = _rect.xMax + _lineWidth * _intervalRatio;
			_rect.width = _fieldWidth + _fromLabelWidth;
			from.floatValue = EditorGUI.FloatField(_rect, "From", from.floatValue);

			EditorGUIUtility.labelWidth = _toLabelWidth;
			_rect.x = _rect.xMax + _lineWidth * _intervalRatio;
			_rect.width = _fieldWidth + _toLabelWidth;
			to.floatValue = EditorGUI.FloatField(_rect, "To", to.floatValue);

			EditorGUIUtility.labelWidth = _labelWidth;
		}


		protected static void ClampedFloatChannelField(SerializedProperty toggle, string label, SerializedProperty from, SerializedProperty to, float min, float max)
		{
			_labelWidth = EditorGUIUtility.labelWidth;

			_rect = EditorGUILayout.GetControlRect();
			_lineWidth = _rect.width;
			_fieldWidth = (_lineWidth * (1f - _intervalRatio - _intervalRatio - _toggleRatio) - _fromLabelWidth - _toLabelWidth) * 0.5f;

			_rect.width = _lineWidth * _toggleRatio;
			toggle.boolValue = EditorGUI.ToggleLeft(_rect, label, toggle.boolValue);

			EditorGUIUtility.labelWidth = _fromLabelWidth;
			_rect.x = _rect.xMax + _lineWidth * _intervalRatio;
			_rect.width = _fieldWidth + _fromLabelWidth;
			from.floatValue = Mathf.Clamp(EditorGUI.FloatField(_rect, "From", from.floatValue), min, max);

			EditorGUIUtility.labelWidth = _toLabelWidth;
			_rect.x = _rect.xMax + _lineWidth * _intervalRatio;
			_rect.width = _fieldWidth + _toLabelWidth;
			to.floatValue = Mathf.Clamp(EditorGUI.FloatField(_rect, "To", to.floatValue), min, max);

			EditorGUIUtility.labelWidth = _labelWidth;
		}


		protected static void ColorChannelField(SerializedProperty toggle, string label, SerializedProperty from, SerializedProperty to)
		{
			_labelWidth = EditorGUIUtility.labelWidth;

			_rect = EditorGUILayout.GetControlRect();
			_lineWidth = _rect.width;
			_fieldWidth = (_lineWidth * (1f - _intervalRatio - _intervalRatio - _toggleRatio) - _fromLabelWidth - _toLabelWidth) * 0.5f;

			_rect.width = _lineWidth * _toggleRatio;
			toggle.boolValue = EditorGUI.ToggleLeft(_rect, label, toggle.boolValue);

			EditorGUIUtility.labelWidth = _fromLabelWidth;
			_rect.x = _rect.xMax + _lineWidth * _intervalRatio;
			_rect.width = _fieldWidth + _fromLabelWidth;
			from.floatValue = Mathf.Clamp(EditorGUI.IntField(_rect, "From", Mathf.RoundToInt(from.floatValue * 255f)), 0, 255) / 255f;

			EditorGUIUtility.labelWidth = _toLabelWidth;
			_rect.x = _rect.xMax + _lineWidth * _intervalRatio;
			_rect.width = _fieldWidth + _toLabelWidth;
			to.floatValue = Mathf.Clamp(EditorGUI.IntField(_rect, "To", Mathf.RoundToInt(to.floatValue * 255f)), 0, 255) / 255f;

			EditorGUIUtility.labelWidth = _labelWidth;
		}


		protected static void AllColorChannelsField(SerializedProperty from, SerializedProperty to)
		{
			_labelWidth = EditorGUIUtility.labelWidth;

			_rect = EditorGUILayout.GetControlRect();
			_lineWidth = _rect.width;
			_fieldWidth = (_lineWidth * (1f - _intervalRatio - _intervalRatio - _toggleRatio) - _fromLabelWidth - _toLabelWidth) * 0.5f;

			EditorGUIUtility.labelWidth = _fromLabelWidth;
			_rect.x += _lineWidth * (_intervalRatio + _toggleRatio);
			_rect.width = _fieldWidth + _fromLabelWidth;
			from.colorValue = EditorGUI.ColorField(_rect, "From", from.colorValue);

			EditorGUIUtility.labelWidth = _toLabelWidth;
			_rect.x = _rect.xMax + _lineWidth * _intervalRatio;
			_rect.width = _fieldWidth + _toLabelWidth;
			to.colorValue = EditorGUI.ColorField(_rect, "To", to.colorValue);

			EditorGUIUtility.labelWidth = _labelWidth;
		}


		protected static SerializedProperty[] GetVector2Properties(SerializedProperty vector2Property)
		{
			SerializedProperty[] properties = new SerializedProperty[2];
			properties[0] = vector2Property.FindPropertyRelative("x");
			properties[1] = vector2Property.FindPropertyRelative("y");
			return properties;
		}


		protected static SerializedProperty[] GetVector3Properties(SerializedProperty vector3Property)
		{
			SerializedProperty[] properties = new SerializedProperty[3];
			properties[0] = vector3Property.FindPropertyRelative("x");
			properties[1] = vector3Property.FindPropertyRelative("y");
			properties[2] = vector3Property.FindPropertyRelative("z");
			return properties;
		}


		protected static SerializedProperty[] GetVector4Properties(SerializedProperty vector4Property)
		{
			SerializedProperty[] properties = new SerializedProperty[4];
			properties[0] = vector4Property.FindPropertyRelative("x");
			properties[1] = vector4Property.FindPropertyRelative("y");
			properties[2] = vector4Property.FindPropertyRelative("z");
			properties[3] = vector4Property.FindPropertyRelative("w");
			return properties;
		}


		protected static SerializedProperty[] GetColorProperties(SerializedProperty colorProperty)
		{
			SerializedProperty[] properties = new SerializedProperty[4];
			properties[0] = colorProperty.FindPropertyRelative("r");
			properties[1] = colorProperty.FindPropertyRelative("g");
			properties[2] = colorProperty.FindPropertyRelative("b");
			properties[3] = colorProperty.FindPropertyRelative("a");
			return properties;
		}

	} // class TweenFromTo

} // namespace WhiteCatEditor

#endif // UNITY_EDITOR