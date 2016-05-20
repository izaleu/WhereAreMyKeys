using UnityEngine;
using WhiteCat;

[RequireComponent(typeof(ViewDetector))]
public class UseViewDetector : MonoBehaviour
{
	public Transform target;
	public LayerMask barrier;

	ViewDetector view;
	Material material;


	void Awake()
	{
		view = GetComponent<ViewDetector>();
		material = GetComponent<MeshRenderer>().material;
	}


	void Update()
	{
		if (view.IsVisible(target.position, barrier))
		{
			material.color = Color.red;
			Debug.DrawLine(transform.position, target.position, Color.red);
		}
		else
		{
			material.color = Color.blue;
		}
	}
}