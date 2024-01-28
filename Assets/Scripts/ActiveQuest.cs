using UnityEngine;

public class ActiveQuest {
    public QuestData quest;
    public NPC from;
    public NPC to;

    public bool started = false;

    public ActiveQuest(QuestData quest, NPC from, NPC to) {
        this.quest = quest;
        this.from = from;
        this.to = to;
    }

    public string formatted() {
        return quest.formatted(from.name, to.name);
    }
}
