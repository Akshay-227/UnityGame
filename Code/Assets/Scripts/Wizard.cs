using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Wizard : MonoBehaviour {
	Animator anim;
	public Transform Reward;
	public GameObject potion;
	public float speed;
	public int dir;
	float dirTimer=1.2f;
	public int health;
	public GameObject deathParticle;
	bool canAttack;
	float attackTimer=2f;
	public GameObject projectile;
	public float thrusPower;
	float changeTimer=.2f;
	bool shouldChange;
	float specialTimer=.5f;
	// Use this for initialization
	void Start () {
		anim=GetComponent<Animator>();
		dir=Random.Range(0,4);
		canAttack=false;
		shouldChange=false;
	}
	
	// Update is called once per frame
	void Update () {

		specialTimer-=Time.deltaTime;
			if(specialTimer<=0)
			{
				SpecialAttack();
				SpecialAttack();
				specialTimer=.5f;
			}

		dirTimer-=Time.deltaTime;
		if(dirTimer<=0){
			dirTimer=1.2f;
			switch (dir)
			{
				case 1: dir=0;
						break;
				case 2: dir=1;
						break;
				case 3: dir=2;
						break;
				case 0: dir=3;
						break; 						
				default: dir=1;
						break;
			}
			
		}
		Movement();
		attackTimer-=Time.deltaTime;
		if(attackTimer<=0){
			attackTimer=2f;
			canAttack=true;
		}
		Attack();
		if(shouldChange){
			changeTimer-=Time.deltaTime;
			if(changeTimer<=0){
				changeTimer=.2f;
				shouldChange=false;
			}
		}
	}
	void Attack(){
		if(!canAttack){
			return;
		}
		canAttack=false;
		if(dir==0){
			GameObject newProjectile=Instantiate(projectile, transform.position, transform.rotation);
			newProjectile.GetComponent<Rigidbody2D>().AddForce(Vector2.up*thrusPower);
		}
		else if(dir==1){
			GameObject newProjectile=Instantiate(projectile, transform.position, transform.rotation);
			newProjectile.GetComponent<Rigidbody2D>().AddForce(Vector2.right*-thrusPower);
		}
		else if(dir==2){
			GameObject newProjectile=Instantiate(projectile,transform.position, transform.rotation);
			newProjectile.GetComponent<Rigidbody2D>().AddForce(Vector2.up*-thrusPower);
		}
		else if(dir==3){
			GameObject newProjectile=Instantiate(projectile,transform.position, transform.rotation);
			newProjectile.GetComponent<Rigidbody2D>().AddForce(Vector2.right*thrusPower);
		}

	}
	void Movement(){
		if(dir==0){
			transform.Translate(0, speed*Time.deltaTime,0);
			anim.SetInteger("dir",dir);
		}
		else if(dir==2)
		{
			transform.Translate(0,-speed*Time.deltaTime,0);
			anim.SetInteger("dir",dir);
		}
		else if(dir==1)
		{
			transform.Translate(-speed*Time.deltaTime,0,0);
			anim.SetInteger("dir",dir);
		}
		else if(dir==3)
		{
			transform.Translate(speed*Time.deltaTime,0,0);
			anim.SetInteger("dir",dir);
		}
	}
	void OnTriggerEnter2D(Collider2D col){
		if(col.gameObject.tag=="Sword"){
			health--;
			col.gameObject.GetComponent<Sword>().CreateParticle();
			GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().canAttack=true;
			GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().canMove=true;
			Destroy(col.gameObject);
			if(health<=0){
				Instantiate(deathParticle,transform.position, transform.rotation);
				Instantiate(potion, Reward.position, potion.transform.rotation );
				Destroy(gameObject);
			
			}
		}
	}
	void OnCollisionEnter2D(Collision2D col){
		if(col.gameObject.tag=="Player"){
			
			if(!col.gameObject.GetComponent<Player>().iniFrames){
				
				col.gameObject.GetComponent<Player>().currentHealth--;
				col.gameObject.GetComponent<Player>().iniFrames=true;
			
			}
			if(col.gameObject.GetComponent<Player>().currentHealth<=0)
			{
				SceneManager.LoadScene(0);
			}
			
		}
		if(col.gameObject.tag=="wall"){
			if(shouldChange)
				return;
			if(dir==0)
				dir=2;
			else if(dir==1)
				dir=3;
			else if(dir==2)
				dir=0;
			else if(dir==3)
				dir=1;
			shouldChange=true;
		}
	}
	
	void SpecialAttack(){
		GameObject newProjectile= Instantiate(projectile, transform.position, transform.rotation);
		int randomDir=Random.Range(0,4);
		switch(randomDir){
			case 0: newProjectile.GetComponent<Rigidbody2D>().AddForce(Vector2.right*thrusPower);
				break;
			case 1: newProjectile.GetComponent<Rigidbody2D>().AddForce(Vector2.up*thrusPower);
				break;
			case 2: newProjectile.GetComponent<Rigidbody2D>().AddForce(Vector2.right*-thrusPower);
				break;
			case 3: newProjectile.GetComponent<Rigidbody2D>().AddForce(Vector2.up*-thrusPower);
				break;
		}
		
	}
}
