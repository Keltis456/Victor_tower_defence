using UnityEngine;
using System.Collections;

public class MoveToWayPoints : MonoBehaviour {

	public float Speed;
	public Transform[] waypoints;
	int curWaypointIndex = 0;
	public GameObject hp;
	void Update () 
	{
	if(curWaypointIndex < waypoints.Length){
	transform.position = Vector3.MoveTowards(transform.position,waypoints[curWaypointIndex].position,Time.deltaTime*Speed);
	transform.LookAt(waypoints[curWaypointIndex].position);
	if(Vector3.Distance(transform.position,waypoints[curWaypointIndex].position) < 0.5f)
	{
		curWaypointIndex++;
	}
	}
	if(hp.GetComponent<HpBar>().CurHp <= 0)
	{
		Destroy(gameObject);
		Destroy(hp);
	}
	}
}
