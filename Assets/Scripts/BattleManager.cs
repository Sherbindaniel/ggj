using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public GameObject[] wave1Enemies;
    public GameObject drone;
    public GameObject[] wave2Enemies;
    public GameObject boss;

    private int phase = 0;
    private bool battleStarted = false;

    void Update()
    {
        if (!battleStarted) return;

        if (phase == 0 && AllEnemiesDead())
        {
            phase = 1;
            drone.SetActive(true);
        }
        else if (phase == 1 && AllEnemiesDead())
        {
            phase = 2;
            SpawnWave(wave2Enemies);
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
