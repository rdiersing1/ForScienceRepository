using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySpawner : MonoBehaviour { 

    public float monsterLife = 1.0f;
    public float chunkSize = 1.0f;
    public float nextChunckAt = 1.0f;
    public float renderDistFromPlayer = 1.0f;
    public float PDFModifierInverse = 1.0f;
    public float ground = 1.0f;
    public Rigidbody2D playerRB;
    public GameObject master;
    ScoreCounter sc;
    
    private GameObject monsterPrefab;

	void Start () {
        sc = master.GetComponent<ScoreCounter>();
        monsterPrefab = Resources.Load("Monster") as GameObject;
	}
	
	void Update () {
        if (nextChunckAt < playerRB.position.x + renderDistFromPlayer) {
            spawnChunck(nextChunckAt);
            nextChunckAt += chunkSize;
        }
	}

    // Used to spawn chucnk of enimies
    private void spawnChunck(float location) {
        int score = sc.getScore();

        // Assigns the number of monsters to spawn to an event from the pdf f(x) = x/(c * int((1/c) * s) ds from 0 to max)
        int maxNumMonsters = Mathf.CeilToInt(((Mathf.Sqrt(score) / 5) + 2));
        float M = (1 / (2 * PDFModifierInverse)) * Mathf.Pow(maxNumMonsters, 2);
        float rand = Random.Range(0.0f, 1.0f);
        int numMonstersToSpawn = Mathf.CeilToInt(Mathf.Sqrt(2 * PDFModifierInverse * M * rand));

        // Spawn Monsters
        float distBetweenMonsters = chunkSize / (numMonstersToSpawn + 1);
        for (int i = 0; i < numMonstersToSpawn; ++i) {
            GameObject monster = Instantiate(monsterPrefab) as GameObject;
            monster.GetComponent<Rigidbody2D>().transform.position = new Vector2(location + (distBetweenMonsters * i), ground);
        }

        print("score: " + score + "\n" +
              "max Monsters: " + maxNumMonsters + "\n" +
              "Spawn: " + numMonstersToSpawn);
    }
}
