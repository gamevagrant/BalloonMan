using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossPlatformInput : MonoBehaviour
{

	public const string JUMP_LEFT = "jumpLeft";
	public const string JUMP_RIGHT = "jumpRight";


	void Awake () 
	{
#if UNITY_EDITOR
		gameObject.AddComponent<StandaloneInputHandle>();
#elif UNITY_ANDROID
		gameObject.AddComponent<MobileInputHandle>();
#elif UNITY_IPHONE
		gameObject.AddComponent<MobileInputHandle>();
#endif
	}

}
