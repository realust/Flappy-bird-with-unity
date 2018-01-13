using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
    enum State {
        Ready,
        Play,
        GameOver
    }

    State state;
    int score;
    public int best_score;

    public BirdController bird;
    public ScrollObject[] scroll;
    public GameObject blocks;
    public Text scoreLabel;
    public Text bestscoreLabel;
    public Text stateLabel;

	// Use this for initialization
	void Start () {
        Ready();	
	}
	
	// Update is called once per frame
	void LateUpdate () {
        switch (state) {
            case State.Ready:
                if (Input.GetButtonDown("Fire1")) GameStart();
                break;
            case State.Play:
                if (bird.IsDead()) GameOver();
                break;
            case State.GameOver:
                if (Input.GetButtonDown("Fire1")) Reload();
                break;
        }
	}

    void Ready() {
        state = State.Ready;

        bird.SetSteerAction(false);
        blocks.SetActive(false);

        score = 0;
        scoreLabel.text = "Score : " + 0;

        stateLabel.gameObject.SetActive(true);
        stateLabel.text = "Ready";
    }

    void GameStart() {
        state = State.Play;

        bird.SetSteerAction(true);
        blocks.SetActive(true);

        bird.Flap();

        stateLabel.gameObject.SetActive(false);
        stateLabel.text = "";
    }

    void GameOver() {
        state = State.GameOver;

        ScrollObject[] scrollObjects = GameObject.FindObjectsOfType<ScrollObject>();

        foreach (ScrollObject so in scrollObjects) so.enabled = false;

        best_score = PlayerPrefs.GetInt("BestScore");

        if (best_score < score)
            best_score = score;

        PlayerPrefs.SetInt("BestScore", best_score);

        bestscoreLabel.gameObject.SetActive(true);
        bestscoreLabel.text = "Best score : " + best_score;

        stateLabel.gameObject.SetActive(true);
        stateLabel.text = "Game Over";
    }

    void Reload() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void IncreaseScore() {
        if (score >= 99999999)
            score = 99999999;
        else
            score++;

        scoreLabel.text = "Score : " + score;

        if (score % 5 == 0) {
            for(int i = 0; i < 7; i++)
                scroll[i].speed += (float)0.1;
        }
    }
}
