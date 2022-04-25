using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Entities;
using TMPro;

public class GameState : MonoBehaviour
{
    
    public bool isDay { get; private set; }
    
    [SerializeField] private List<Enemy> listOfEnemies;
    public int currentWave;
    public int currentTime;
    public int currentDay;
    private TMP_Text waveCountUI;
    private TMP_Text dayCountUI;

    [SerializeField] private Light directionalLight;
    [SerializeField, Range(0, 24)] private float timeOfDay;

    [SerializeField] private Player player;
    

    private void Start()
    {
        isDay = true;
        currentWave = 0;
        currentDay = 1;
        waveCountUI = GameObject.Find("WaveCount").GetComponent<TMP_Text>();
        waveCountUI.text = "WaveCount: " + currentWave;

        dayCountUI = GameObject.Find("DayCount").GetComponent<TMP_Text>();
        dayCountUI.text = "DayCount: " + currentDay;
        // LoadPlayers(GameObject.Find("PlayerSpawn").transform);
    }
    
    private void Update()
    {
        if (Application.isPlaying)
        {
            UpdateLighting(isDay, ref directionalLight);
        }

        if(listOfEnemies.Count <= 0)
        {
            isDay = true;
            currentDay += 1;
        }
    }

    public void AddEnemy(Enemy newEnemy)
    {
        listOfEnemies.Add(newEnemy);
        Debug.Log("Amount of enemies: " + listOfEnemies.Count());
    }

    public void RemoveEnemy(Enemy enemy)
    {
        listOfEnemies.Remove(enemy);
        Debug.Log("Amount of enemies: " + listOfEnemies.Count());
    }
    
    public void ToggleDay()
    {
        isDay = !isDay;
    }
    
    // Spawn in a player at the given position (A gameobject transform)
    private void LoadPlayers(Transform arg)
    {
        var position = arg.position;
        Debug.Log("Player Spawned.");
        Instantiate(player, new Vector3(position.x, position.y, position.z), Quaternion.identity);
    }


    // Check if light is valid - if true: set time based on bool:
    private static void UpdateLighting(bool isDayArg, ref Light lightArg)
    {
        if (!lightArg)
        {
            lightArg = RenderSettings.sun;
            if(!lightArg) throw new Exception("Could not instantiate directional Light");
        }
        // Change time of day based on bool:
        float timeArg = (isDayArg ? 13.0f : 0.0f) / 24f;
        
        // Rotate the directional light:
        lightArg.transform.localRotation = Quaternion.Euler
            (new Vector3((timeArg * 360f) - 90f, 170f, 0));
    }

    public void UpdateWaveCount()
    {
        currentWave += 1;
        waveCountUI.text = "WaveCount: " + currentWave;
    }

}