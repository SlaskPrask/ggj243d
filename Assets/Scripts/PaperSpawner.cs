using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class PaperSpawner : MonoBehaviour {
    private static readonly Random random = new Random();

    public GameObject paperPrefab;

    public float spawnForceY = 5;
    public float spawnForceXZ = 5;
    public float spawnRotForce = 20;

    public void Start() {
        GameManager.printerManager.addSpawner(this);
    }

    public void spawn() {
        GameObject obj = Instantiate(paperPrefab, transform.position,
            Quaternion.identity);

        Rigidbody rb = obj.GetComponentInParent<Rigidbody>();
        rb.velocity = new Vector3(spawnForceXZ / 2 + (float)random.NextDouble() * spawnForceXZ,
            spawnForceXZ / 2 + (float)random.NextDouble() * spawnForceXZ);
        rb.angularVelocity = new Vector3((float)random.NextDouble() * spawnRotForce,
            (float)random.NextDouble() * spawnRotForce,
            (float)random.NextDouble() * spawnRotForce);
    }
}
