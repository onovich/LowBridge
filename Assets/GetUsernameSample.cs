using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Win32LowAPI {

    public class GetUsernameSample : MonoBehaviour {

        void Start() {
            string username = LowLevelInput.GetUsername();
            Debug.Log("Current Username: " + username);
        }

        void Update() {

        }
    }

}