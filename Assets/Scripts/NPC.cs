using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;

public class NPC : MonoBehaviour {
    private static readonly Random random = new Random();

    public Transform itemSpawn;
    public ItemCollector itemCollector;
    public Exclamation exclamation;

    public QuestList questList;

    public Action onQuestDone = null;

    public float spawnForce = 6.5f;
    public float spawnRotForce = 20f;

    private ActiveQuest quest;

    // Start is called before the first frame update
    void Start() {
        if (itemCollector == null) {
            itemCollector = GetComponentInChildren<ItemCollector>();
        }

        name = GameManager.npcManager.getName();

        itemCollector.listener += collected;
    }

    // Update is called once per frame
    void Update() {
    }

    public bool collected(ItemProperty property, Item item, bool isTarget) {
        if (!isTarget) {
            return false;
        }

        if (property == ItemProperty.CoffeeCup) {
            if (item is CoffeeCup coffee) {
                coffee.emptyCoffeeCup();
            }

            Debug.Log("Got Coffee");
        }

        itemCollector.setWants(ItemProperty.None);

        if (item) {
            item.target = null;
        }

        if (onQuestDone != null) {
            onQuestDone();
            onQuestDone = null;
        }

        quest = null;

        return true;
    }

    public ActiveQuest generateNewQuest() {
        int index = random.Next(questList.itemQuests.Count);
        QuestData questData = questList.itemQuests[index];
        setQuest(questData);
        return quest;
    }

    public void onTalk() {
        if (quest != null && quest.started == false) {
            quest.started = true;
            if (quest.quest.spawnItem) {
                GameObject obj = Instantiate(quest.quest.spawnItem, itemSpawn.position,
                    Quaternion.identity);

                obj.GetComponentInParent<NPCItem>()?.init(this);

                Rigidbody rb = obj.GetComponentInParent<Rigidbody>();
                rb.velocity = Vector3.up * spawnForce;
                rb.angularVelocity = new Vector3((float)random.NextDouble() * spawnRotForce,
                    (float)random.NextDouble() * spawnRotForce,
                    (float)random.NextDouble() * spawnRotForce);
            }
        }

        exclamation.showQuest(true);
    }

    public void leaveRange() {
        exclamation.showQuest(false);
    }

    public void OnEnable() {
        GameManager.npcManager.add(this);
    }

    public void OnDisable() {
        GameManager.npcManager.remove(this);
    }

    public bool hasQuest() {
        return quest != null;
    }

    private void setQuest(QuestData questData) {
        itemCollector.setWants(questData.wantedItem);

        switch (questData.wantedItem) {
            case ItemProperty.None:
                quest = null;
                break;
            case ItemProperty.Mail:
                itemCollector.setWants(ItemProperty.None);
                break;
            case ItemProperty.CoffeeCup:
                break;
            case ItemProperty.PaperStack:
                GameManager.printerManager.addRequest(this);
                itemCollector.setWants(ItemProperty.None);
                break;
            default:
                Debug.LogWarning("invalid quest item " + questData.wantedItem);
                throw new ArgumentOutOfRangeException();
        }

        string subtitles =
            GameManager.audioManager.PlayRequestSoundGetText(questData.wantedItem,
                transform.position);


        quest = new ActiveQuest(questData, this, this); //TODO: target

        exclamation.setSubs(subtitles);
        exclamation.showQuest(false);
    }

    public void setOnQuestDone(Action onQuestDone) {
        this.onQuestDone = onQuestDone;
    }

    public void OnValidate() {
        /*if (name == "NPC") {
            Debug.LogWarning("Give me a real name instead of 'NPC', maybe I want to be Margaret");
        }*/

        if (!itemSpawn) {
            Debug.LogWarning("NPC is missing an object (itemSpawn) where their mail spawns",
                gameObject);
        }
    }
}
