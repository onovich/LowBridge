using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LowBridge {

    public class ToggleCapsLockSample : MonoBehaviour {

        void Start() {

        }

        void Update() {
            if (Input.GetKeyDown(KeyCode.C)) {
                bool capsLockState = LowBridgeAPI.ToggleCapsLock();
                Debug.Log("Caps Lock is now " + (capsLockState ? "ON" : "OFF"));
            }
        }
    }

}