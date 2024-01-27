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

        return true;
    }

    public void generateNewQuest() {
        int index = random.Next(questList.itemQuests.Count);
        ItemProperty property = questList.itemQuests[index];
        setQuest(property);
    }

    public bool hasQuest() {
        return itemCollector.hasWants();
    }

    public void setQuest(ItemProperty property) {
        if (property == ItemProperty.None) {
            itemCollector.setWants(ItemProperty.None);
            return;
        }

        if (!itemCollector.hasWants()) {
            itemCollector.wants = property;
            Debug.Log($"got quest {property}", gameObject);
        }
    }
}
