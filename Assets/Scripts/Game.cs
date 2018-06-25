using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {

    private void Start()
    {
        StartPrototype();
    }

    void StartPrototype()
    {
        StartCoroutine(StartPrototypeRoutine());
    }

    IEnumerator StartPrototypeRoutine()
    {
        yield return new WaitForEndOfFrame();

        SocketClient.Instance.ConnectToGame();
    }
}
