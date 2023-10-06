using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorObject : MonoBehaviour
{
	public Transform target;	

	private Rigidbody rb;

	public ParticleSystem explosion;

	public float meteorDamage;

	private GameObject playerGameObject;

	public float meteorStunDuration;

	public float shadowOpacity;

	[Range(0f, 1f)] public float speed = 1f;
	[Range(0f, 1f)] public float rotation = 1f;

	void Awake()
	{
		rb = GetComponent<Rigidbody>();
		rb.useGravity = false;
		rb.maxAngularVelocity = 30f; //set it to something pretty high so it can actually follow properly!
	}
    private void Start()
    {
		transform.position = new Vector3(target.position.x + Random.Range(-25,25), 50, target.position.z + Random.Range(-25, 25));
		target.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
	}

	void FixedUpdate()
	{
		shadowOpacity++;
		target.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, shadowOpacity/300);

		if (target == null) return;

		Vector3 deltaPos = target.position - transform.position;
		rb.velocity = 1f / Time.fixedDeltaTime * deltaPos * Mathf.Pow(speed, 90f * Time.fixedDeltaTime);

		Quaternion deltaRot = target.rotation * Quaternion.Inverse(transform.rotation);

		float angle;
		Vector3 axis;

		deltaRot.ToAngleAxis(out angle, out axis);

		if (angle > 180.0f) angle -= 360.0f;

		if (angle != 0) rb.angularVelocity = (1f / Time.fixedDeltaTime * angle * axis * 0.01745329251994f * Mathf.Pow(rotation, 90f * Time.fixedDeltaTime));
	}

    private void OnCollisionEnter(Collision other)
    {
		if(other.gameObject.tag == "Player")
        {
			playerGameObject = other.gameObject;

			if (playerGameObject.GetComponent<PlayerStats>().canTakeDamage)
            {
				float enemyLevel = GameObject.Find("EnemyMaster").GetComponent<EnemyMaster>().enemyLevel;
				other.gameObject.GetComponent<PlayerStats>().TakeDamage(meteorDamage+enemyLevel);
				StartCoroutine(Stun());
			}
			
        }

		if(other.gameObject.tag != "Player")
        {
			Delete();
		}

		GetComponent<SphereCollider>().enabled = false;
		GetComponent<MeshRenderer>().enabled = false;
		explosion.Play();
    }

	public void Delete()
    {
		Destroy(transform.parent.gameObject);
    }

	public IEnumerator Stun()
    {
		playerGameObject.GetComponent<PlayerLocomotion>().canMove = false;
		yield return new WaitForSeconds(meteorStunDuration);
		playerGameObject.GetComponent<PlayerLocomotion>().canMove = true;
		Destroy(transform.parent.gameObject);


	}


}
