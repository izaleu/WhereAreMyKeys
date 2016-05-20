
namespace WhiteCat
{
	/// <summary>
	/// 循环模式
	/// </summary>
	public enum WrapMode
	{
		Clamp = 0,					// 在到达端点时保持状态不变
		Loop = 1,					// 在到达端点时跳转到另一端
		PingPong = 2,				// 在到达端点时改变方向
	}


	/// <summary>
	/// 播放模式
	/// </summary>
	public enum PlayMode
	{
		PlayForever = 0,				// 永远播放
		StopWhenForwardToEnding = 1,	// 前进到终点时停止
		StopWhenBackToBeginning = 2,	// 后退到起点时停止
		BothStop = 3,					// 前进或后退到两端时都停止
	}


	/// <summary>
	/// 材质类型
	/// </summary>
	public enum MaterialType
	{
		Shared = 0,					// 共享材质
		UniqueForRenderer = 1,		// 渲染器独立材质
	}

} // namespace WhiteCat