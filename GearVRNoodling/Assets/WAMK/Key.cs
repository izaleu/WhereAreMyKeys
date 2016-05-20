using UnityEngine;
using System.Collections;

public class Key : VRStandardAssets.Utils.VRInteractiveItem {

    [SerializeField] Material GlowMat;

    Material BaseMat;

    bool Glowing = false;

	// Use this for initialization
	void Start () {
        BaseMat = gameObject.GetComponent<Renderer>().material;
        base.OnOver += GlowOn;
        base.OnOut += GlowOff;
        base.OnClick += OnClick;
	}
	
	void GlowOn() {
        if (Glowing == false)
        {
            Glowing = true;
            gameObject.GetComponent<MeshRenderer>().material = GlowMat;
        }
        
	}

    void GlowOff()
    {
        if (Glowing)
        {
            gameObject.GetComponent<MeshRenderer>().material = BaseMat;
            Glowing = false;
        }
    }

    void OnClick()
    {
        if (Glowing)
        {
            Debug.Log("Clicked!");
            Timer.Instance.StopTimer();
        }
    }
}
