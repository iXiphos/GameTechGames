using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour {

	const float minPathUpdateTime = .2f;
	const float pathUpdateMoveThreshold = .5f;

	public Transform target;
	public float speed = 20;

    public GameObject manager;

    bool dead;


	void Start() {
        dead = false;
        target = GameObject.Find("Player").transform;
	}

    private void Update()
    {
        if(target != null)
            target = GameObject.Find("Player").transform;
    }

    public void FixedUpdate()
    {
        //If the enemy is still alive, move towards the player
        if(!dead) MoveTowardsPlayer();
        if(Vector3.Distance(target.position, gameObject.transform.position) <= 1f && target != null)
        {
            target.gameObject.GetComponent<PlayerHealth>().doDamage();
            manager.GetComponent<GameManager>().RemoveEnemy(gameObject);
            Destroy(gameObject);
        }
    }

    //Destroy enemy and all the components to avoid errors, and remove from active enemy list
    public void DestroyEnemy()
    {
        StopCoroutine("FollowPath");
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<EnemyStatus>().enabled = false;
        gameObject.GetComponent<PolygonCollider2D>().enabled = false;
        manager.GetComponent<GameManager>().RemoveEnemy(gameObject);
        Destroy(gameObject);
        dead = true;
    }

    //Move towardst the player
    void MoveTowardsPlayer()
    {
        //If player is alive and this gameobject is still alive
        if (transform != null && target != null)
        {
            //Rotate towards the player and slowly move towards them
            Vector3 targetDir = target.position - transform.position;
            float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg - 90f;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, q, 180f);
            transform.Translate(Vector3.up * Time.deltaTime * speed);
        }
    }

}
