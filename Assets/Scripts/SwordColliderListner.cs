using UnityEngine;
using System.Collections;

public class SwordColliderListner : MonoBehaviour 
{
	public SwordColliderListner targetListner = null;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D collidedObject)
	{
		//EnemyScript enemy = collidedObject.GetComponent<EnemyScript>();

		print ("hit");
	}
}
