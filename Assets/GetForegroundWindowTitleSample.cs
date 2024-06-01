using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Win32LowAPI {

    public class GetForegroundWindowTitleSample : MonoBehaviour {

        void Start() {

        }

        void Update() {
            if (Input.GetKeyDown(KeyCode.T)) {
                string windowTitle = LowLevelInput.GetForegroundWindowTitle();
                Debug.Log("Foreground Window Title: " + windowTitle);
            }
        }
    }

}