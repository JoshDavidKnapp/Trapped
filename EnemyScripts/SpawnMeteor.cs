using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMeteor : MonoBehaviour
{
    public GameObject meteorPrefab; 

    public float meteorCooldown;

    private GameObject playerGameObject;

    public float howCloseToPlayer;
    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine(Wait());

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator Wait()
    {
        yield return new WaitForSeconds(10);

        playerGameObject = GameObject.FindGameObjectWithTag("Player");

        StartCoroutine(Spawn());
    }

    public IEnumerator Spawn()
    {

        Vector3 playerLocal = new Vector3(playerGameObject.transform.position.x + Random.Range(-howCloseToPlayer, howCloseToPlayer), 0, playerGameObject.transform.position.z + Random.Range(-howCloseToPlayer, howCloseToPlayer));
        Instantiate(meteorPrefab, playerLocal, Quaternion.identity);

        yield return new WaitForSeconds(meteorCooldown);
        StartCoroutine(Spawn());
    }
}
