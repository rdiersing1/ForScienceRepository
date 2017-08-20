using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaserSpawner : MonoBehaviour {

    public float laserSpeed = 1.0f;
    public float laserDespawnTime = 1.0f;
    public float fireAnimationLength = 1.0f;
    public float maxHeat = 1.0f;
    public float heatPerLaser = 1.0f;
    public float heatLossVelocity = 1.0f;
    public Rigidbody2D playerRB;
    public GameObject player;
    public Vector2 laserOffset = new Vector2(0, 0);
    public Vector3 firingLaserOffset = new Vector3(0, 0, 0);
    public Text heatText;

    private float currHeat = 0.0f;
    private GameObject laserPrefab;
    private GameObject firingLaserPrefab;

    void Start() {
        laserPrefab = Resources.Load("LaserBeam") as GameObject;
        firingLaserPrefab = Resources.Load("FiringLaser") as GameObject;
        heatText.text = "0";
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {

            if (currHeat <= maxHeat) {
                currHeat += heatPerLaser;
                GameObject laser = Instantiate(laserPrefab) as GameObject;
                laser.transform.position = playerRB.position + laserOffset;         // We are using the rigidbody2D because the players
                Rigidbody2D laserRB = laser.GetComponent<Rigidbody2D>();            // gameObject.transform.position is a Vector3
                laserRB.velocity = new Vector2(laserSpeed, 0);

                GameObject firingLaser = Instantiate(firingLaserPrefab) as GameObject;
                firingLaser.transform.parent = player.transform;
                firingLaser.transform.position = player.transform.position + firingLaserOffset;

                Destroy(firingLaser, fireAnimationLength);
                Destroy(laser, laserDespawnTime);
            }
        }

        heatText.text = currHeat.ToString();
        if (currHeat - heatLossVelocity * Time.deltaTime > 0) {
            currHeat -= heatLossVelocity * Time.deltaTime;
        }
    }
}
