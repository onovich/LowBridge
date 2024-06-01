using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LowBridge {

    public class GetUsernameSample : MonoBehaviour {

        void Start() {
            string username = LowBridgeAPI.GetUsername();
            Debug.Log("Current Username: " + username);
        }

        void Update() {

        }
    }

}