using UnityEngine;
using System.Collections;
public class WaveSpawn : MonoBehaviour {

	public int WaveSize;
	public GameObject EnemyPrefab;
	public float EnemyInterval;
	public Transform spawnPoint;
	public float startTime;
	public Transform[] WayPoints;
	public GameObject Hp;
	int enemyCount=0;
	public GameObject canvas;
	void Start () {
	 InvokeRepeating("SpawnEnemy",startTime,EnemyInterval);
	}
	void Update()
	{
		if(enemyCount == WaveSize)
		{
			CancelInvoke("SpawnEnemy");
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
		enemy.GetComponent<MoveToWayPoints>().hp = hp;
	}
}
