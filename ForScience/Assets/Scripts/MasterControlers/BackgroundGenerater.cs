using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;

public class BackgroundGenerater : MonoBehaviour {

    public float nextSpawn;
    public float spawnIncrement;
    public float spawnDistFromPlayer = 1f;
    public float backgroundPannleLife = 1f;
    public Rigidbody2D playerRB;
    public GameObject backgroundParent;

    private GameObject backgroundMidPrefab;

	// Use this for initialization
	void Start () {
        backgroundMidPrefab = Resources.Load("BackgroundMid") as GameObject;
	}
	
	// Update is called once per frame
	void Update () {
        float playerPosX = playerRB.position.x;
        if (playerPosX + spawnDistFromPlayer >= nextSpawn) {
            GameObject backgroundMid = Instantiate(backgroundMidPrefab) as GameObject;
            backgroundMid.transform.position = new Vector2(nextSpawn, 0);
            backgroundMid.transform.parent = backgroundParent.transform;
            nextSpawn += spawnIncrement;
            Destroy(backgroundMid, backgroundPannleLife);
        }
	}
}
