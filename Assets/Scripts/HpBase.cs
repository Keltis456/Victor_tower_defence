using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class HpBase : MonoBehaviour {

	public int HP = 150;
	public  Text HPtext;

	// Use this for initialization
	void Update () 
	{
		HPtext.text = "Current health : " + HP.ToString();
	}
	
	void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("enemyBug"))
		{
			HP -= 10;
			Destroy(other.gameObject);
			Destroy(other.GetComponent<MoveToWayPoints>().hp);
            if (HP <= 0)
                GameManager.instance.LoseGame();
		}
	}
}
