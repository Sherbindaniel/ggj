using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public GameObject[] wave1Enemies;     // 3 soldiers
    public GameObject[] wave2Drones;      // 2 drones
    public GameObject[] wave3Enemies;     // 2 soldiers
    public GameObject[] wave3Drones;      // 2 drones
    public GameObject boss;

    public int phase = 0;
    private bool battleStarted = false;

    void Update()
    {
        if (!battleStarted) return;

        if (phase == 0 && AllEnemiesDead())
        {
            phase = 1;
            SpawnWave(wave2Drones);
        }
        else if (phase == 1 && AllEnemiesDead())
        {
            phase = 2;
            SpawnWave(wave3Enemies);
            //SpawnWave(wave3Drones);
        }
        else if (phase == 2 && AllEnemiesDead())
        {
            phase = 3;
            boss.SetActive(true);
        }
    }

    public void StartBattle()
    {
        battleStarted = true;
        SpawnWave(wave1Enemies);
    }

    bool AllEnemiesDead()
    {
        return GameObject.FindGameObjectsWithTag("Enemy").Length == 0;
    }

    void SpawnWave(GameObject[] enemies)
    {
        foreach (GameObject e in enemies)
            e.SetActive(true);
    }
}
