using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LowBridge {

    public class GetForegroundWindowTitleSample : MonoBehaviour {

        void Start() {

        }

        void Update() {
            if (Input.GetKeyDown(KeyCode.T)) {
                string windowTitle = LowBridgeAPI.GetForegroundWindowTitle();
                Debug.Log("Foreground Window Title: " + windowTitle);
            }
        }
    }

}