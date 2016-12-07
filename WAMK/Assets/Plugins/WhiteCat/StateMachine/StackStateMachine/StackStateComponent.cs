using System;
using UnityEngine;
using UnityEngine.Events;

namespace WhiteCat
{
    /// <summary>
    /// 栈状态组件
    /// </summary>
    [AddComponentMenu("White Cat/State Machine/Stack State Component")]
    public class StackStateComponent : BaseStackStateComponent
    {
		[SerializeField]
		StateEvent _onPush = new StateEvent();

		[SerializeField]
		StateEvent _onPop = new StateEvent();

		[SerializeField]
		StateEvent _onEnter = new StateEvent();

		[SerializeField]
		StateEvent _onExit = new StateEvent();


		/// <summary>
		/// 添加或移除更新状态触发的事件
		/// </summary>
		public event Action<float> onUpdate;


		/// <summary>
		/// 添加或移除状态入栈触发的事件
		/// </summary>
		public event UnityAction<IState> onPush
        {
			add { _onPush.AddListener(value); }
			remove { _onPush.RemoveListener(value); }
		}


        /// <summary>
        /// 添加或移除状态出栈触发的事件
        /// </summary>
        public event UnityAction<IState> onPop
        {
			add { _onPop.AddListener(value); }
			remove { _onPop.RemoveListener(value); }
		}


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


		public override void OnPush(IState prevState)
		{
            _onPush.Invoke(prevState);
		}


		public override void OnPop(IState nextState)
		{
			_onPop.Invoke(nextState);
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

	} // class StackStateComponent

} // namespace WhiteCat