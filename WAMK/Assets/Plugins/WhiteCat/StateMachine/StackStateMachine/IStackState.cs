
namespace WhiteCat
{
	/// <summary>
	/// 栈状态接口
	/// </summary>
	public interface IStackState : IState
	{
        /// <summary>
        /// 状态入栈时触发
        /// </summary>
        void OnPush(IState prevState);


        /// <summary>
        /// 状态出栈时触发
        /// </summary>
        void OnPop(IState nextState);
    }

} // namespace WhiteCat