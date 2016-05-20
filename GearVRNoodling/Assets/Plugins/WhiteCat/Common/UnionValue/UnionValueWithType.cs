using System;

#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using WhiteCatEditor;
#endif

namespace WhiteCat
{
	/// <summary>
	/// 带类型的联合体
	/// </summary>
	[Serializable]
	public struct UnionValueWithType
	{
		public BaseValueTypes type;
		public UnionValue union;
	}


#if UNITY_EDITOR

	[CustomPropertyDrawer(typeof(UnionValueWithType))]
	class UnionValueWithTypeDrawer : BasePropertyDrawer
	{
		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			return EditorGUIUtility.singleLineHeight;
		}


		public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
		{
			var field = GetFieldValue<UnionValueWithType>(property);
			EditorGUI.BeginChangeCheck();

			float typeWidth = (rect.width - EditorGUIUtility.labelWidth - 4f) * 0.5f;
			rect.width -= typeWidth + 4f;

			switch (field.type)
			{
				case BaseValueTypes.Bool:
					field.union.boolValue = EditorGUI.Toggle(rect, label, field.union.boolValue);
					break;

				case BaseValueTypes.SByte:
					field.union.sbyteValue = (sbyte)Mathf.Clamp(EditorGUI.IntField(rect, label, field.union.sbyteValue), sbyte.MinValue, sbyte.MaxValue);
					break;

				case BaseValueTypes.Byte:
					field.union.byteValue = (byte)Mathf.Clamp(EditorGUI.IntField(rect, label, field.union.byteValue), byte.MinValue, byte.MaxValue);
					break;

				case BaseValueTypes.Char:
					var text = EditorGUI.TextField(rect, label, field.union.charValue.ToString());
					field.union.charValue = text.Length > 0 ? text[0] : '\0';
                    break;

				case BaseValueTypes.Short:
					field.union.shortValue = (short)Mathf.Clamp(EditorGUI.IntField(rect, label, field.union.shortValue), short.MinValue, short.MaxValue);
					break;

				case BaseValueTypes.UShort:
					field.union.ushortValue = (ushort)Mathf.Clamp(EditorGUI.IntField(rect, label, field.union.ushortValue), ushort.MinValue, ushort.MaxValue);
					break;

				case BaseValueTypes.Int:
					field.union.intValue = EditorGUI.IntField(rect, label, field.union.intValue);
					break;

				case BaseValueTypes.UInt:
					long longValue = EditorGUI.LongField(rect, label, field.union.uintValue);
					field.union.uintValue = longValue < uint.MinValue ? uint.MinValue : (longValue > uint.MaxValue ? uint.MaxValue : (uint)longValue);
                    break;

				case BaseValueTypes.Long:
					field.union.longValue = EditorGUI.LongField(rect, label, field.union.longValue);
					break;

				case BaseValueTypes.ULong:
					field.union.ulongValue = (ulong)EditorGUI.LongField(rect, label, (long)field.union.ulongValue);
					break;

				case BaseValueTypes.Float:
					field.union.floatValue = EditorGUI.FloatField(rect, label, field.union.floatValue);
					break;

				case BaseValueTypes.Double:
					field.union.doubleValue = EditorGUI.DoubleField(rect, label, field.union.doubleValue);
					break;
			}

			rect.x = rect.xMax + 4f;
			rect.width = typeWidth;

			Enum type = EditorGUI.EnumPopup(rect, GUIContent.none, field.type);

			if (EditorGUI.EndChangeCheck())
			{
				var target = property.serializedObject.targetObject;
				Undo.RecordObject(target, "UnionValueWithType");
				field.type = (BaseValueTypes)type;
                fieldInfo.SetValue(target, field);
				EditorUtility.SetDirty(target);
            }
		}

	} // UnionValueWithTypeDrawer

#endif // UNITY_EDITOR

} // namespace WhiteCat