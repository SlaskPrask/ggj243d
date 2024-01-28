using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public class QuestManager : MonoBehaviour {
    private static readonly Random random = new Random();
    public BinderUI binder;

    private List<NPC> npcs;
    public List<float> newQuestTimers;

    public void Start() {
        npcs = FindObjectsOfType<NPC>().ToList();

        if (newQuestTimers.Count == 0) {
            newQuestTimers.Add(5);
        }
    }

    public void startGivingQuests() {
        StartCoroutine(giveNewQuest());
    }

    private IEnumerator giveNewQuest() {
        WaitForSeconds oneSecond = new WaitForSeconds(1);

        WaitForSeconds wait = new WaitForSeconds(newQuestTimers[0]);

        for (int timerIndex = 0;;) {
            yield return wait;
            if (timerIndex < newQuestTimers.Count - 1) {
                timerIndex++;
                wait = new WaitForSeconds(newQuestTimers[timerIndex]);
            }

            List<NPC> available = npcs.Where(n => !n.hasQuest()).ToList();
            while (available.Count == 0) {
                yield return oneSecond;
                available = npcs.Where(n => !n.hasQuest()).ToList();
            }

            int index = random.Next(available.Count);

            NPC npc = available[index];

            ActiveQuest quest = npc.generateNewQuest();
            if (quest == null) {
                Debug.LogWarning("no quest, why");
                continue;
            }

            BinderListEntry binderEntry = binder.addQuest(quest.formatted());

            npc.setOnQuestDone(() => {
                GameManager.goalsManager.taskCompleted();
                binder.finishQuest(binderEntry);
            });
        }
    }
}
