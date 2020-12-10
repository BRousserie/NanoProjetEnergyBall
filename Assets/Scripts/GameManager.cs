﻿using HealthBarsPackage;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject levelPrefab;
    public Health health1, health2;
    public LevelManager level;
    public SendLog logger;

    public float timeBeforeRestart = 2;

    private bool restarting = false;

    // Start is called before the first frame update
    void Start()
    {
        if (level == null) {
            CreateLevel();
            SetHealthBars();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RestartLevel() {
        if (!restarting) {
            StartCoroutine(Restart(timeBeforeRestart));
            restarting = true;
        }
    }

    public void SetHealthBars() {
        Debug.Log(level.player1);
        health1.player = level.player1;
        Debug.Log(level.player1);
        health2.player = level.player2;
        Debug.Log(level.player1);
    }

    public void Win(string winner) {
        if (logger != null) {
            Debug.Log("send !");
            logger.Send(winner);
        }
    }

    private IEnumerator Restart(float time) {
        yield return new WaitForSeconds(time);
        level.Stop();
        Destroy(level.gameObject);
        CreateLevel();
        SetHealthBars();
        restarting = false;
        if (logger != null) {
            logger.Restart();
        }
    }

    private void CreateLevel() {
        level = Instantiate(levelPrefab).GetComponent<LevelManager>();
        level.gameManager = this;
    }
    
    
    public static void PauseGame()
    {
        Time.timeScale = 0;
    }

    public static void ResumeGame()
    {
        Time.timeScale = 1;
    }
}
