using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossPlatformInput : MonoBehaviour
{

	public const string AXIS_HORIZONTAL = "AXIS_HORIZONTAL";
	public const string AXIS_VERTICAL = "AXIS_VERTICAL";
	public const string JUMP = "JUMP";

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
