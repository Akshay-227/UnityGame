using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelToLoad : MonoBehaviour {

	// Use this for initialization
	public int SceneIndex;
	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.gameObject.tag=="Player")
		{
			GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().SaveGame();
			SceneManager.LoadScene(1);
		}
	}
}
