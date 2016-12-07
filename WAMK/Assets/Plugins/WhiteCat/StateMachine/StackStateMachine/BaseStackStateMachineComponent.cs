using System.Collections.Generic;

namespace WhiteCat
{
	/// <summary>
	/// 栈状态机组件基类
	/// </summary>
	public class BaseStackStateMachineComponent : ScriptableComponentWithEditor
	{
		float _currentStateTime;
		IStackState _currentState;
		List<IStackState> _states = new List<IStackState>(8);


		/// <summary>
		/// 当前状态持续时间
		/// </summary>
		public float currentStateTime
		{
			get { return _currentStateTime; }
		}


		/// <summary>
		/// 当前状态
		/// </summary>
		public IStackState currentState
		{
			get { return _currentState; }
		}


		/// <summary>
		/// 栈中状态的总数. 包括压入的空状态
		/// </summary>
		public int stateCount
		{
			get { return _states.Count; }
		}


		/// <summary>
		/// 将新状态压入栈
		/// </summary>
		public void PushState(IStackState newState)
		{
			if (_currentState != null)
			{
				_currentState.OnExit(newState);
			}

			_currentStateTime = 0;
			var prevState = _currentState;
			_currentState = newState;
			_states.Add(newState);

			if (_currentState != null)
			{
				_currentState.OnPush(prevState);
                _currentState.OnEnter(prevState);
			}

			OnStateChanged(prevState, _currentState);
		}


		/// <summary>
		/// 将新状态压入栈. 用于序列化事件
		/// </summary>
		public void PushStateComponent(BaseStackStateComponent newState)
		{
			PushState(newState);
        }


		/// <summary>
		/// 将当前状态弹出栈
		/// </summary>
		public void PopState()
		{
			int stateCount = _states.Count;
			if (stateCount > 0)
			{
				IStackState newState = stateCount == 1 ? null : _states[stateCount - 2];

				if (_currentState != null)
				{
					_currentState.OnExit(newState);
					_currentState.OnPop(newState);
				}

				_currentStateTime = 0;
				var prevState = _currentState;
				_currentState = newState;
				_states.RemoveAt(stateCount - 1);

				if (_currentState != null)
				{
					_currentState.OnEnter(prevState);
				}

				OnStateChanged(prevState, _currentState);
			}
		}


		/// <summary>
		/// 连续将多个状态弹出栈
		/// </summary>
		public void PopStates(int count)
		{
			while (count > 0)
			{
				PopState();
				count--;
            }
		}


		/// <summary>
		/// 手动调用以更新状态
		/// </summary>
		protected void UpdateState(float deltaTime)
		{
			_currentStateTime += deltaTime;
			if (_currentState != null)
			{
				_currentState.OnUpdate(deltaTime);
			}
		}


		/// <summary>
		/// 状态变化后触发的事件
		/// </summary>
		protected virtual void OnStateChanged(IStackState prevState, IStackState currentState)
		{
		}

	} // class BaseStackStateMachineComponent

} // namespace WhiteCat