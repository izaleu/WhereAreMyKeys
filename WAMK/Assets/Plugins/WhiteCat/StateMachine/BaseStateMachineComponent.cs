
namespace WhiteCat
{
	/// <summary>
	/// 状态机组件基类
	/// </summary>
	public class BaseStateMachineComponent : ScriptableComponentWithEditor
	{
		float _currentStateTime;
		IState _currentState;


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
		public IState currentState
		{
			get { return _currentState; }
			set
			{
				if (_currentState != null)
				{
					_currentState.OnExit(value);
				}

				_currentStateTime = 0;
				var prevState = _currentState;
				_currentState = value;

				if (_currentState != null)
				{
					_currentState.OnEnter(prevState);
				}

				OnStateChanged(prevState, _currentState);
			}
		}


		/// <summary>
		/// 当前状态组件对象. 用于序列化事件
		/// </summary>
		public BaseStateComponent currentStateComponent
		{
			get { return _currentState as BaseStateComponent; }
			set { currentState = value; }
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
		protected virtual void OnStateChanged(IState prevState, IState currentState)
		{
		}

	} // class BaseStateMachineComponent

} // namespace WhiteCat