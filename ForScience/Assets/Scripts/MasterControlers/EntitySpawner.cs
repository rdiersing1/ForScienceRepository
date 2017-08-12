using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySpawner : MonoBehaviour { 

    public float entityLife = 1.0f;
    public float chunkSize = 1.0f;
    public float nextChunckAt = 1.0f;
    public float renderDistFromPlayer = 1.0f;
    public float trapRadius = 1.0f;
    public Rigidbody2D playerRB;
    public GameObject master;
    ScoreCounter sc;
    
    private GameObject monsterPrefab;
    private GameObject obsticalPrefab;
    private GameObject acidPrefab;
    private List<float> traps = new List<float>();

	void Start () {
        sc = master.GetComponent<ScoreCounter>();
        monsterPrefab = Resources.Load("Monster") as GameObject;
        obsticalPrefab = Resources.Load("obstical") as GameObject;
        acidPrefab = Resources.Load("acid") as GameObject;
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
        float M = 0.5f * Mathf.Pow(maxNumMonsters, 2);
        float rand = Random.Range(0.0f, 1.0f);
        int numMonstersToSpawn = Mathf.CeilToInt(Mathf.Sqrt(2 * M * rand));

        // Spawn Monsters
        spawner(location, numMonstersToSpawn, monsterPrefab);

        // spawn obsticals 
        int numObsticalsToSpawn = Mathf.FloorToInt(Random.Range(0, 3));
        spawner(location, numObsticalsToSpawn, obsticalPrefab);
        print("Attempting obsticals: " + numObsticalsToSpawn);

        // spawn acids
        int numAcidToSpawn = Mathf.FloorToInt(Random.Range(0, 2));
        spawner(location, numAcidToSpawn, acidPrefab);
        print("Attempting acids: " + numAcidToSpawn);
    }

    // Spawns certian number of objects, does not spawn an "impossible object combo"
    private void spawner(float location, int numObjects, GameObject objectPrefab) {
        for (int i = 0; i < numObjects; ++i) {
            float objPosX = Random.Range(location, location + chunkSize);
            if (objectPrefab.CompareTag("Trap")) {
                if (validTrapPos(objPosX)) {
                    traps.Add(objPosX);
                    spawnObject(objPosX, objectPrefab);
                } else {
                    print("invalid trap pos at: " + objPosX);
                }
            }
            else {
                spawnObject(objPosX, objectPrefab);
            }
        }
    }

    // Spawns a single object at objPosX for entityLife time
    private void spawnObject(float objPosX, GameObject objectPrefab) {
        GameObject obj = Instantiate(objectPrefab) as GameObject;
        float objPosY = obj.transform.position.y;
        obj.transform.position = new Vector3(objPosX, objPosY, 0);
        Destroy(obj, entityLife);
    }

    // Detects if the current trap is in range of another trap
    private bool validTrapPos(float position) {
        for (int i = 0; i < traps.Count; ++i) {
            if (position - traps[i] < trapRadius) {
                return false;
            }
        }
        return true;
    }
}
