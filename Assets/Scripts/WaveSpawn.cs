using UnityEngine;
using System.Collections;
public class WaveSpawn : MonoBehaviour {

	int waveSize;
	public GameObject EnemyPrefab;
	public Transform spawnPoint;
	public Transform[] WayPoints;
	public GameObject Hp;
    int currWaveHP;
	int enemyCount=0;
	public GameObject canvas;

    public void StartNextWave(int _enemyHP, int _waveSize, float _enemyInterval)
    {
        currWaveHP = _enemyHP;
        waveSize = _waveSize;
        InvokeRepeating("SpawnEnemy", 0f, _enemyInterval);

    }

	void Update()
	{
		if(enemyCount >= waveSize && GameManager.instance.isWaveStarted)
		{
			CancelInvoke("SpawnEnemy");
            enemyCount = 0;
            GameManager.instance.EndWave();
		}
	}

	void SpawnEnemy()
	{
		enemyCount++;
		GameObject enemy = GameObject.Instantiate(EnemyPrefab,spawnPoint.position,Quaternion.identity) as GameObject;
		enemy.GetComponent<MoveToWayPoints>().waypoints = WayPoints;
		GameObject hp = GameObject.Instantiate(Hp,Vector3.zero,Quaternion.identity) as GameObject;
		hp.transform.SetParent(canvas.transform);
		hp.GetComponent<HpBar>().enemy = enemy;
        hp.GetComponent<HpBar>().CurHp = currWaveHP;
		enemy.GetComponent<MoveToWayPoints>().hp = hp;
	}
}
