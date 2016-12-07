using System;
using UnityEngine;
using UnityEngine.Events;

namespace WhiteCat
{
	/// <summary>
	/// 状态组件
	/// </summary>
	[AddComponentMenu("White Cat/State Machine/State Component")]
	public class StateComponent : BaseStateComponent
	{
		[SerializeField]
		StateEvent _onEnter = new StateEvent();

		[SerializeField]
		StateEvent _onExit = new StateEvent();


		/// <summary>
		/// 添加或移除更新状态触发的事件
		/// </summary>
		public event Action<float> onUpdate;


		/// <summary>
		/// 添加或移除进入状态触发的事件
		/// </summary>
		public event UnityAction<IState> onEnter
		{
			add { _onEnter.AddListener(value); }
			remove { _onEnter.RemoveListener(value); }
		}


		/// <summary>
		/// 添加或移除离开状态触发的事件
		/// </summary>
		public event UnityAction<IState> onExit
		{
			add { _onExit.AddListener(value); }
			remove { _onExit.RemoveListener(value); }
		}


		public override void OnEnter(IState prevState)
		{
			_onEnter.Invoke(prevState);
		}


		public override void OnExit(IState nextState)
		{
			_onExit.Invoke(nextState);
		}


		public override void OnUpdate(float deltaTime)
		{
			if (onUpdate != null)
			{
				onUpdate(deltaTime);
			}
		}

	} // class StateComponent

} // namespace WhiteCat