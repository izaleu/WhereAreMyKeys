using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
using WhiteCatEditor;
#endif

namespace WhiteCat
{
	/// <summary>
	/// 视野探测器
	/// </summary>
	[AddComponentMenu("White Cat/Common/View Detector")]
	public class ViewDetector : ScriptableComponent
	{
		[SerializeField] [Min(0)]
		float _radius = 10f;

		[SerializeField] [Range(0f, 360f)]
		float _angle = 90f;


		public float radius
		{
			get { return _radius; }
			set { _radius = Mathf.Max(0f, value); }
		}


		public float angle
		{
			get { return _angle; }
			set { _angle = Mathf.Clamp(value, 0f, 360f); }
		}


		/// <summary>
		/// 检查一个目标点是否可见
		/// </summary>
		/// <param name="targetPosition"> 目标位置 </param>
		/// <param name="barrierLayerMask"> 障碍物层掩码 </param>
		/// <param name="queryTriggerInteraction"> 触发器处理模式, 默认忽略触发器类型的碰撞框 </param>
		/// <returns> 如果可以看到目标返回 true, 否则返回 false </returns>
		public bool IsVisible(
			Vector3 targetPosition,
			int barrierLayerMask,
			QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.Ignore)
		{
			Vector3 direction = targetPosition - transform.position;
			float distance = direction.magnitude;

			return distance <= _radius
				&& Vector3.Dot(transform.forward, direction) >= Mathf.Cos(0.5f * Mathf.Deg2Rad * _angle) * distance
				&& !Physics.Raycast(transform.position, direction, distance, barrierLayerMask, queryTriggerInteraction);
		}

#if UNITY_EDITOR

		void OnDrawGizmosSelected()
		{
			Vector3 position = transform.position;
			Vector3 up = transform.up;
			Vector3 right = transform.right;
			Vector3 forward = transform.forward;

			Vector3 circleCenter = _radius * Mathf.Cos(0.5f * Mathf.Deg2Rad * _angle) * forward + position;
			float circleRadius = _radius * Mathf.Sin(0.5f * Mathf.Deg2Rad * _angle);

			Vector3 circleUp = circleCenter + up * circleRadius;
			Vector3 circleLeft = circleCenter - right * circleRadius;

			EditorKit.RecordAndSetHandlesColor(Color.cyan);

			Handles.DrawLine(position + forward * _radius, position);
			if (_angle > 180f) Handles.DrawWireDisc(position, forward, _radius);
			Handles.DrawWireArc(position, up, circleLeft - position, _angle, _radius);
			Handles.DrawWireArc(position, right, circleUp - position, _angle, _radius);

			EditorKit.RestoreHandlesColor();

			EditorKit.RecordAndSetHandlesColor(Color.yellow);

			Handles.DrawWireDisc(circleCenter, forward, circleRadius);
			Handles.DrawLine(circleUp, position);
			Handles.DrawLine(circleLeft, position);
			Handles.DrawLine(circleCenter - up * circleRadius, position);
			Handles.DrawLine(circleCenter + right * circleRadius, position);

			EditorKit.RestoreHandlesColor();
		}

#endif

	} // class ViewDetector

} // namespace WhiteCat