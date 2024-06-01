using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Win32LowAPI {

    public class CursorSample : MonoBehaviour {

        bool forceCursor = false;

        void Start() {

        }

        void Update() {

            if (Input.GetKeyDown(KeyCode.Space)) {
                forceCursor = !forceCursor;
                if (forceCursor) {
                    Debug.Log("Force Cursor");
                } else {
                    Debug.Log("Release Cursor");
                }
            }

            if (forceCursor) {
                LowLevelInput.MouseMove(100, 100);
            } else {

            }

        }

    }

}