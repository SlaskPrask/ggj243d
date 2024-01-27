using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager instance { get; private set; }

    public static PlayerDetector playerDetector { get; private set; }

    public static PrinterManager printerManager { get; private set; }

    public static NPCManager npcManager { get; private set; }

    private void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this) {
            Destroy(gameObject);
            return;
        }

        playerDetector = GetComponentInChildren<PlayerDetector>().Initialize();
        printerManager = GetComponentInChildren<PrinterManager>().Initialize();
        npcManager = GetComponentInChildren<NPCManager>().Initialize();
    }
}
