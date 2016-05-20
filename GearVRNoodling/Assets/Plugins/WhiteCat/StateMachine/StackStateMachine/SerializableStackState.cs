using System;
using UnityEngine;
using UnityEngine.Events;

namespace WhiteCat
{
	/// <summary>
	/// 可序列化栈状态
	/// </summary>
	[Serializable]
	public class SerializableStackState : SerializableState, IStackState
    {
		[SerializeField]
		StateEvent _onPush = new StateEvent();

		[SerializeField]
		StateEvent _onPop = new StateEvent();


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


		void IStackState.OnPush(IState prevState)
		{
            _onPush.Invoke(prevState);
		}


		void IStackState.OnPop(IState nextState)
		{
			_onPop.Invoke(nextState);
		}

    } // class SerializableStackState

} // namespace WhiteCat