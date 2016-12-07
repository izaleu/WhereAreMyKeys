using System;
using UnityEngine.Events;

namespace WhiteCat
{
	/// <summary>
	/// 状态事件
	/// </summary>
	[Serializable]
	public class StateEvent : UnityEvent<IState>
	{
	}

} // namespace WhiteCat