using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;

public class NPC : MonoBehaviour {
    private static readonly Random random = new Random();


    public ItemCollector itemCollector;
    public QuestList questList;
    public Action onQuestDone = null;

    private ActiveQuest quest;

    // Start is called before the first frame update
    void Start() {
        if (itemCollector == null) {
            itemCollector = GetComponentInChildren<ItemCollector>();
        }

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
            item.emptyCoffeeCup();
            Debug.Log("Got Coffee");
        }

        itemCollector.setWants(ItemProperty.None);

        item.target = null;

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

    public bool hasQuest() {
        return quest != null;
    }

    private void setQuest(QuestData questData) {
        itemCollector.setWants(questData.wantedItem);

        if (questData.wantedItem == ItemProperty.None) {
            quest = null;
        }
        else {
            quest = new ActiveQuest(questData, this, this); //TODO: target
        }
    }

    public void setOnQuestDone(Action onQuestDone) {
        this.onQuestDone = onQuestDone;
    }
}
