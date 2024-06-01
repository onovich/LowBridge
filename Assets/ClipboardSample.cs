using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LowBridge {

    public class ClipboardSample : MonoBehaviour {

        void Start() {

        }

        void Update() {
            if (Input.GetKeyDown(KeyCode.V)) {
                string clipboardText = LowBridgeAPI.GetClipboardText();
                Debug.Log("Clipboard Text: " + clipboardText);
            }

            if (Input.GetKeyDown(KeyCode.B)) {
                string textToSet = "Hello from Unity!";
                bool success = LowBridgeAPI.SetClipboardText(textToSet);
                Debug.Log("Set Clipboard Text: " + (success ? "Success" : "Failed"));
            }
        }
    }

}