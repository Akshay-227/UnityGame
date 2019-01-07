using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour {
	float timer = .15f;
	float specialTime=1f;
	public bool special;
	public GameObject SwordParticle;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		timer-=Time.deltaTime;
		if(timer<=0){
			GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetInteger("AttackDir",5);
		}
		if(!special)
		if(timer<=0){
			GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().canMove=true;
			GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().canAttack=true;
			Destroy(gameObject);
	    }
		specialTime-=Time.deltaTime;
		if(specialTime<=0){
			GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().canAttack=true;
			Instantiate(SwordParticle, transform.position, transform.rotation);
			Destroy(gameObject);
		}
	}
	public void CreateParticle(){
		Instantiate(SwordParticle, transform.position, transform.rotation);
	}
	void OnTriggerEnter2D(Collider2D col){
		if(col.gameObject.tag=="wall"){
			GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().canAttack=true;
			GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().canMove=true;
			Destroy(gameObject);
		}
	}
	
}
