using System.Collections.Generic;
using UnityEngine;

namespace WhiteCat
{
	/// <summary>
	/// 游戏对象池
	/// </summary>
	[AddComponentMenu("White Cat/Common/Game Object Pool")]
	public class GameObjectPool : ScriptableComponentWithEditor
	{
		[SerializeField] [Editable(true, false)]
		GameObject _originalObject;

		[SerializeField] [Editable(true, false)]
		int _startQuantity = 10;

		[SerializeField]
		float _recycleCountdown = 10f;

		Stack<GameObject> _storedObjects;
		Queue<ObjectRecycler> _aliveObjects;


		// 对象回收器
		class ObjectRecycler : ScriptableComponent
		{
			[HideInInspector] public GameObjectPool pool;		// 所属对象池
			[System.NonSerialized] public float beginTime;		// 开始激活的时间, -1 表示未激活
		}


		/// <summary>
		/// 自动回收倒计时. 仅当组件 enabled 才会自动回收
		/// </summary>
		public float recycleCountdown
		{
			get { return _recycleCountdown; }
			set { _recycleCountdown = value; }
		}


		/// <summary>
		/// 是否自动回收
		/// </summary>
		public bool autoRecycle
		{
			get { return enabled; }
			set { enabled = value; }
		}


		/// <summary>
		/// 添加对象
		/// </summary>
		/// <param name="number"> 添加的对象数量 </param>
		public void AddObject(int number = 1)
		{
			while (number > 0)
			{
				GameObject obj = Instantiate(_originalObject);

				obj.transform.SetParent(transform, false);
				var recycler = obj.AddComponent<ObjectRecycler>();
				recycler.pool = this;
				recycler.beginTime = -1f;
				obj.SetActive(false);

                _storedObjects.Push(obj);

				number--;
			}
		}


		/// <summary>
		/// 取出一个游戏对象
		/// </summary>
		/// <returns> 取出的对象 </returns>
		public GameObject TakeOut()
		{
			if (_storedObjects.Count == 0)
			{
				AddObject();
			}

			GameObject obj = _storedObjects.Pop();
			obj.transform.SetParent(null, false);
			obj.SetActive(true);

			var recycler = obj.GetComponent<ObjectRecycler>();
			recycler.beginTime = Time.time;
			if (enabled) _aliveObjects.Enqueue(recycler);

			return obj;
		}


		/// <summary>
		/// 回收游戏对象
		/// </summary>
		/// <param name="target"> 被回收的对象 </param>
		public static void Recycle(GameObject target)
		{
			var recycler = target.GetComponent<ObjectRecycler>();
			target.SetActive(false);
			target.transform.SetParent(recycler.pool.transform, false);
			recycler.pool._storedObjects.Push(target);
			recycler.beginTime = -1f;
		}


		void Awake()
		{
			_storedObjects = new Stack<GameObject>(_startQuantity < 16 ? 16 : _startQuantity);
			_aliveObjects = new Queue<ObjectRecycler>(_startQuantity < 16 ? 16 : _startQuantity);
			AddObject(_startQuantity);
		}


		void Update()
		{
			while (_aliveObjects.Count > 0)
			{
				var recycler = _aliveObjects.Peek();

				if (!recycler || recycler.beginTime < 0f)
				{
					_aliveObjects.Dequeue();
				}
				else if (recycler.beginTime + _recycleCountdown > Time.time)
				{
					return;
				}
				else
				{
					recycler.gameObject.SetActive(false);
					recycler.transform.SetParent(recycler.pool.transform, false);
					recycler.pool._storedObjects.Push(recycler.gameObject);
					recycler.beginTime = -1f;
					_aliveObjects.Dequeue();
				}
			}
		}


#if UNITY_EDITOR

		protected override void Editor_OnInspectorGUI()
		{
			editor.DrawDefaultInspector();

			editor.DrawToggleLayout(enabled, value => enabled = value, "Auto-Recycle");
		}

#endif

	} // class GameObjectPool

} // namespace WhiteCat