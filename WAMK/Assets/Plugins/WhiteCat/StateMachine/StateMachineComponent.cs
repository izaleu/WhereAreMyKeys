using UnityEngine;

namespace WhiteCat
{
	/// <summary>
	/// 状态机组件
	/// </summary>
	[AddComponentMenu("White Cat/State Machine/State Machine Component")]
	public class StateMachineComponent : BaseStateMachineComponent
	{
		public UpdateMode updateMode = UpdateMode.Update;
		[SerializeField] BaseStateComponent _defaultState;


		/// <summary>
		/// 设置默认状态
		/// </summary>
		void Start()
		{
			if (_defaultState)
			{
				currentStateComponent = _defaultState;
			}
		}


		/// <summary>
		/// Update 更新状态
		/// </summary>
		void Update()
		{
			if (updateMode == UpdateMode.Update)
			{
				UpdateState(Time.deltaTime);
			}
		}


		/// <summary>
		/// LateUpdate 更新状态
		/// </summary>
		void LateUpdate()
		{
			if (updateMode == UpdateMode.LateUpdate)
			{
				UpdateState(Time.deltaTime);
			}
		}


		/// <summary>
		/// FixedUpdate 更新状态
		/// </summary>
		void FixedUpdate()
		{
			if (updateMode == UpdateMode.FixedUpdate)
			{
				UpdateState(Time.deltaTime);
			}
		}

	} // class StateMachineComponent

} // namespace WhiteCat