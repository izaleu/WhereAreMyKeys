
namespace WhiteCat
{
	/// <summary>
	/// 栈状态组件基类
	/// </summary>
	public abstract class BaseStackStateComponent : BaseStateComponent, IStackState
	{
		public abstract void OnPush(IState prevState);
		public abstract void OnPop(IState nextState);

	} // class BaseStackStateComponent

} // namespace WhiteCat