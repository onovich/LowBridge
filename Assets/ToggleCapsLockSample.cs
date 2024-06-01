using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Win32LowAPI {

    public class ToggleCapsLockSample : MonoBehaviour {

        void Start() {

        }

        void Update() {
            if (Input.GetKeyDown(KeyCode.C)) {
                bool capsLockState = LowLevelInput.ToggleCapsLock();
                Debug.Log("Caps Lock is now " + (capsLockState ? "ON" : "OFF"));
            }
        }
    }

}