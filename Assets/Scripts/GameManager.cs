using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager instance { get; private set; }

    public static PlayerDetector playerDetector { get; private set; }

    public static PrinterManager printerManager { get; private set; }

    public static NPCManager npcManager { get; private set; }

    public static GoalsManager goalsManager { get; private set; }
    public static QuestManager questManager { get; private set; }
    public static AudioManager audioManager { get; private set; }

    public static Camera camera { get; private set; }

    
    public float gameTime = 1200;
    public float switchTime = 30f;
    public Material playerSuit;

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
        questManager = GetComponentInChildren<QuestManager>();
        goalsManager = GetComponentInChildren<GoalsManager>().Initialize();
        audioManager = GetComponentInChildren<AudioManager>();

        camera = Camera.main;
    }

    public void StartGame() {
        StartCoroutine(GameTimer());
    }

    public void SwitchPlayers() {
        playerDetector.SwitchPlayer(playerSuit);
    }

    IEnumerator GameTimer() {
        float time = 0;
        float sTime = 0;
        
        while (time < gameTime) {
            yield return null;
            float delta = Time.deltaTime;
            time += delta;
            sTime += delta;
            if (sTime >= switchTime) {
                sTime -= switchTime;
                SwitchPlayers();
            }
        }
        SceneManager.LoadScene("EndOfDay");
    }
}
