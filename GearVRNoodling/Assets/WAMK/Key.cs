using UnityEngine;
using System.Collections;

public class Key : VRStandardAssets.Utils.VRInteractiveItem {

    [SerializeField] Material GlowMat;

	// Use this for initialization
	void Start () {
        base.OnOver += Glow;
	}
	
	void Glow () {
        gameObject.GetComponent<MeshRenderer>().material = GlowMat;
        base.OnOver -= Glow;
	}
}
