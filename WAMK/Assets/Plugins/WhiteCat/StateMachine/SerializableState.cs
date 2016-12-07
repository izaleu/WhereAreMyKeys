using System;
using UnityEngine;
using UnityEngine.Events;

namespace WhiteCat
{
	/// <summary>
	/// 可序列化状态
	/// </summary>
	[Serializable]
	public class SerializableState : SerializableClassWithEditor, IState
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


		void IState.OnEnter(IState prevState)
		{
			_onEnter.Invoke(prevState);
		}


		void IState.OnExit(IState nextState)
		{
			_onExit.Invoke(nextState);
		}


		void IState.OnUpdate(float deltaTime)
		{
			if (onUpdate != null)
			{
				onUpdate(deltaTime);
			}
		}

	} // class SerializableState

} // namespace WhiteCat