
namespace WhiteCat
{
	/// <summary>
	/// 状态组件基类
	/// </summary>
	public abstract class BaseStateComponent : ScriptableComponentWithEditor, IState
	{
		public abstract void OnEnter(IState prevState);
		public abstract void OnExit(IState nextState);
		public abstract void OnUpdate(float deltaTime);

	} // class BaseStateComponent

} // namespace WhiteCat