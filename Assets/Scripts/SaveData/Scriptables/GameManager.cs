using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameStateManager stateManager;

    private void Awake() {
        stateManager.Init();
        stateManager.Save();
    }
}
