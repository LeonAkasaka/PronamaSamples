using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class SplashScreen : MonoBehaviour
{
	void NextLevel ()
	{
		Application.LoadLevel (Application.loadedLevel + 1);
	}
}
