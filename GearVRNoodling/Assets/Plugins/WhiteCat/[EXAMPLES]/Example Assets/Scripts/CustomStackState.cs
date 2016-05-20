using UnityEngine;
using UnityEngine.UI;
using WhiteCat;

[DisallowMultipleComponent]
public class CustomStackState : BaseStackStateComponent
{
	public CanvasGroup canvasGroup;
	public float interactableAlpha = 1f;
	public float nonInteractableAlpha = 0.4f;

	public Image background;
	public Color interactableColor;
	public Color nonInteractableColor;

	public GameObject highlight;


	public override void OnPush(IState prevState)
	{
		gameObject.SetActive(true);
		if (highlight) highlight.SetActive(true);
	}


	public override void OnPop(IState nextState)
	{
		gameObject.SetActive(false);
		if (highlight) highlight.SetActive(false);
	}


	public override void OnEnter(IState prevState)
	{
		canvasGroup.interactable = true;
		canvasGroup.alpha = interactableAlpha;
		background.color = interactableColor;
	}


	public override void OnExit(IState nextState)
	{
		canvasGroup.interactable = false;
		canvasGroup.alpha = nonInteractableAlpha;
		background.color = nonInteractableColor;
	}


	public override void OnUpdate(float deltaTime)
	{
	}
}