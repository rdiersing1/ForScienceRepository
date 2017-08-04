using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour {

    public float distPerScore = 1f;
    public Text scoreText;
    public Vector2 playerStart;
    public Rigidbody2D playerRB;

    private int score;

    public int getScore() {
        return score;
    }

    void Start() {
        scoreText.text = "0";
    }

    // Update is called once per frame
    void Update () {

        // Score Controler
        float newScoreFloat = (playerRB.position.x - playerStart.x) / distPerScore;
        int newScore = (int)newScoreFloat;
        if (newScore != score) {
            score = newScore;
            scoreText.text = score.ToString();
        }
	}
}
