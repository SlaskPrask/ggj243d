using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSuit : MonoBehaviour
{
    private void Awake() {
        GameManager.instance.playerSuit = GetComponent<SkinnedMeshRenderer>().materials[0];
        Destroy(this);
    }
}
