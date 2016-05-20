using System;

namespace WhiteCat
{
	/// <summary>
	/// 执行次数受限的方法. 一般情况不应当使用默认构造方法
	/// </summary>
	public struct LimitingAction
	{
		int _remaining;
		Action _action;


		public LimitingAction(Action action, int triggerCount = 1)
		{
			_remaining = triggerCount;
			_action = action;
		}


		public void Invoke()
		{
			if (_remaining > 0)
			{
				_remaining--;
				_action();
			}
		}

	} // struct LimitingAction

} // namespace WhiteCat