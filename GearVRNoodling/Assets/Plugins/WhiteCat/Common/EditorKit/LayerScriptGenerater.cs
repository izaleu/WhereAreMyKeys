#if UNITY_EDITOR

using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using WhiteCat;

namespace WhiteCatEditor
{
	class LayerScriptGenerater
	{
		[MenuItem("Assets/Create/Layer Script")]
		public static void CreateScript()
		{
			string path = EditorKit.activeDirectory + "/Layer.cs";
			using (var stream = new FileStream(path, FileMode.Create, FileAccess.Write))
			{
				using (var writer = new StreamWriter(stream))
				{
					writer.Write(
@"
namespace WhiteCat
{
	/// <summary> Constants of Layers. </summary>
	public struct Layer
	{"					);

					List<string> list = new List<string>(32);

					for (int i = 0; i < 32; i++)
					{
						var name = LayerMask.LayerToName(i);

						writer.Write(string.Format(
		@"
		/// <summary> Layer {0} / Original Layer Name: {1} </summary>
		public const int {2} = {0};
"							, i, string.IsNullOrEmpty(name) ? "(none)" : name, LayerNameToVariableName(name, i, list)));
					}

					writer.Write(
		@"

		/// <summary> Constants of LayerMasks. </summary>
		public struct Mask
		{"				);

					for (int i = 0; i < 32; i++)
					{
						var name = LayerMask.LayerToName(i);

						writer.Write(string.Format(
			@"
			/// <summary> LayerMask of Layer {0} / Original Layer Name: {1} </summary>
			public const int {2} = {3};
"							, i, string.IsNullOrEmpty(name) ? "(none)" : name, list[i], 1 << i));
					}

					writer.Write(
@"		}
	}
}"						);
				}
			}
		
			AssetDatabase.Refresh();
			Selection.activeObject = AssetDatabase.LoadAssetAtPath<MonoScript>(path);
		}


		// 层名转化为变量名
		// 仅支持下划线，英文字母 和 数字，其他字符被忽略
		// 如果无法获取有效的变量名，则使用 “Layer0” 的格式作为变量名
		static string LayerNameToVariableName(string name, int layer, List<string> list)
		{
			var chars = new List<char>(name);
			bool invalid;
			bool upper = true;

			for (int i = 0; i < chars.Count; i++)
			{
				var c = chars[i];

				if (i==0) invalid = (c != '_' && !Kit.IsLowerOrUpper(c));
				else invalid = (c != '_' && !Kit.IsDigit(c) && !Kit.IsLowerOrUpper(c));

				if (invalid)
				{
					chars.RemoveAt(i--);
					upper = true;
				}
				else if (upper)
				{
					if (Kit.IsLower(c))
					{
						chars[i] = (char)(c + 'A' - 'a');
					}
					upper = false;
				}
			}

			name = new string(chars.ToArray());
			if (name.Length == 0 || list.Contains(name))
			{
				name = string.Format("Layer{0}", layer);
			}

			list.Add(name);
			return name;
		}

	} // class LayerScriptGenerater

} // namespace WhiteCatEditor

#endif // UNITY_EDITOR