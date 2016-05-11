using UnityEngine;
using System.Collections;

public class LoadingScreen : MonoBehaviour {

	void Update () {
        if (Application.GetStreamProgressForLevel("Scene") == 1)
            {
                Application.LoadLevel("Scene");
            }
	}
}
