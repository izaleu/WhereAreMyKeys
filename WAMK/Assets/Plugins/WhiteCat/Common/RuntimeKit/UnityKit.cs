using System;
using System.Collections;
using UnityEngine;

namespace WhiteCat
{
	/// <summary>
	/// Unity 相关方法
	/// </summary>
	public partial struct Kit
	{
		/// <summary>
		/// 同时设置 Unity 时间缩放和 FixedUpdate 频率
		/// </summary>
		/// <param name="timeScale"> 要设置的时间缩放 </param>
		/// <param name="fixedFrequency"> 要设置的 FixedUpdate 频率 </param>
		public static void SetTimeScaleAndFixedFrequency(float timeScale, float fixedFrequency = 50f)
		{
			Time.timeScale = timeScale;
			Time.fixedDeltaTime = timeScale / fixedFrequency;
		}


		/// <summary>
		/// 将 RGBA 格式的整数转换为 Color 类型
		/// </summary>
		public static Color IntRGBAToColor(int rgba)
		{
			return new Color(
				(rgba >> 24) / 255.0f,
				((rgba >> 16) & 0xFF) / 255.0f,
				((rgba >> 8) & 0xFF) / 255.0f,
				(rgba & 0xFF) / 255.0f);
		}


		/// <summary>
		/// 将 RGB 格式的整数转换为 Color 类型
		/// </summary>
		public static Color IntRGBToColor(int rgb)
		{
			return new Color(
				((rgb >> 16) & 0xFF) / 255.0f,
				((rgb >> 8) & 0xFF) / 255.0f,
				(rgb & 0xFF) / 255.0f);
		}


		/// <summary>
		/// 安全获取组件. 如果物体上没有组件则自动添加
		/// </summary>
		public static T SafeGetComponent<T>(GameObject gameObject) where T : Component
		{
			T component = gameObject.GetComponent<T>();
			if (!component) component = gameObject.AddComponent<T>();
			return component;
		}


		/// <summary>
		/// (深度优先)遍历 Transform 层级, 对每一个节点执行一个自定义的操作
		/// </summary>
		/// <param name="root"> 遍历开始的根部 Transform 对象 </param>
		/// <param name="operate"> 遍历到每一个节点时将调用此方法 </param>
		/// <param name="depthLimit"> 访问深度限制, 负值表示不限制, 0 表示只访问 root 本身而不访问其子级, 正值表示最多访问的子级层数 </param>
		public static void TraverseHierarchy(Transform root, Action<Transform> operate, int depthLimit = -1)
		{
			operate(root);
			if (depthLimit == 0) return;

			int count = root.childCount;

			for (int i = 0; i < count; i++)
			{
				TraverseHierarchy(root.GetChild(i), operate, depthLimit - 1);
			}
		}


		/// <summary>
		/// (深度优先)遍历 Transform 层级, 判断每一个节点是否为查找目标, 发现查找目标则立即终止查找
		/// </summary>
		/// <param name="root"> 遍历开始的根部 Transform 对象 </param>
		/// <param name="match"> 判断当前节点是否为查找目标 </param>
		/// <param name="depthLimit"> 遍历深度限制, 负值表示不限制, 0 表示只访问 root 本身而不访问其子级, 正值表示最多访问的子级层数 </param>
		/// <returns> 如果查找到目标则返回此目标; 否则返回 null </returns>
		public static Transform FindInHierarchy(Transform root, Predicate<Transform> match, int depthLimit = -1)
		{
			if (match(root)) return root;
			if (depthLimit == 0) return null;

			int count = root.childCount;
			Transform result = null;

			for (int i = 0; i < count; i++)
			{
				result = FindInHierarchy(root.GetChild(i), match, depthLimit - 1);
				if (result) break;
			}

			return result;
		}


		/// <summary>
		/// 重置 Transform 的 localPosition, localRotation 和 localScale
		/// </summary>
		/// <param name="transform"> 执行重置的对象 </param>
		public static void ResetTransform(Transform transform)
		{
			transform.localPosition = Vector3.zero;
			transform.localRotation = Quaternion.identity;
			transform.localScale = Vector3.one;
        }


		/// <summary>
		/// 延时调用指定的方法
		/// </summary>
		/// <param name="behaviour"> 协程附着的脚本对象 </param>
		/// <param name="delay"> 延迟时间(秒) </param>
		/// <param name="action"> 延时结束调用的方法 </param>
		public static void DelayedInvoke(MonoBehaviour behaviour, float delay, Action action)
		{
			behaviour.StartCoroutine(DelayedCoroutine(delay, action));
		}


		/// <summary>
		/// 延迟协程
		/// </summary>
		/// <param name="delay"> 延迟时间(秒) </param>
		/// <param name="action"> 延时结束调用的方法 </param>
		/// <returns> 协程迭代器 </returns>
		public static IEnumerator DelayedCoroutine(float delay, Action action)
		{
			yield return new WaitForSeconds(delay);
			action();
		}

	} // struct Kit

} // namespace WhiteCat