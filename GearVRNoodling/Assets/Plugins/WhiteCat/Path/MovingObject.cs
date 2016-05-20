using System;
using UnityEngine;

namespace WhiteCat
{
	// Path
	public partial class Path
	{
		/// <summary>
		/// 关键帧列表与目标组件对
		/// </summary>
		[Serializable]
		public class KeyframeListTargetComponentPair
		{
			// 关键帧列表
			public KeyframeList keyframeList;

			// 关键帧插值应用的目标组件
			public Component targetComponent;

			// 上一次移动后, 移动对象右边最近的 Key 索引. 下一次搜索时作为起点以提高效率
			public int lastIndex;
		}

	} // class Path

} // namespace WhiteCat