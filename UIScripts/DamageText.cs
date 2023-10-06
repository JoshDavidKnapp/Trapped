using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    private GameObject playerGameObject;

    public float movementSpeed;
    // Start is called before the first frame update
    void Start()
    {
        playerGameObject = GameObject.FindGameObjectWithTag("Player");
        //this.gameObject.transform.LookAt(playerGameObject.transform);

        StartCoroutine(DestroyThisObject());
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.LookAt(playerGameObject.transform);
        this.gameObject.transform.Rotate(0, 180, 0);
        //this.gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);

    }

    private void FixedUpdate()
    {
        transform.position += transform.up * Time.deltaTime * movementSpeed;

    }

    public IEnumerator DestroyThisObject()
    {

        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);
    }
}
