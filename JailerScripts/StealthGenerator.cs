using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealthGenerator : Ability
{
    private GameObject playerGameObject;

    public PlayerLocomotion currentPlayerMove;
    //How fast player can stealth again

    
    
    //how long stealth lasts
    public float stealthDuration = 2f;
    //Checks if player can stealth
    public float speedMult = 0;
    

    //checks if player is in stealth
    public bool isStealthed;

    private GameObject enemyCollision;

    //special Ability
    public bool emergeFromShadow = false;

    //refrence to weapon
    public ConsecutiveShots weapon;

    //audio for stealth gen
    public AudioSource stealthFiz;

    //materials for stealth
    public List<GameObject> bodyParts;

    public List<Material> opaque,
        transparent;

   
    // Update is called once per frame
    void Update()
    {
        //if player presses leftctrl
        if(Input.GetKeyDown(KeyCode.LeftControl) && gameObject.GetComponent<PlayerLocomotion>().canMove)
        {
            //start stealth
            StartCoroutine(Stealth());
        }
    }

    public IEnumerator Stealth()
    {
        //if player can stealth
        if(canUse)
        {
            //play audio
            stealthFiz.Play();

            //play animation
            gameObject.GetComponent<PlayerLocomotion>().characterRig.GetComponent<Animator>().SetTrigger("Stealth");

            //cannot stealth
            canUse = false;

            //variable for enemies to check if they can attack this player
            isStealthed = true;

            //turn materials transparent
            for (int i = 0; i < bodyParts.Count; i++)
            {
                bodyParts[i].GetComponent<SkinnedMeshRenderer>().material = transparent[i%3];
            }
            

            //Ignore enemy collision
            Physics.IgnoreLayerCollision(8, 9, true);

            //Wait until stealth is over
            StartCoroutine(stealthTime());

           
            //wait for cooldown
            yield return new WaitForSeconds(cooldown);

            //can stealth again
            canUse = true;

        }
    }

    public IEnumerator stealthTime()
    {
        currentPlayerMove.movementSpeedModifier += speedMult;
        yield return new WaitForSeconds(stealthDuration);
        currentPlayerMove.movementSpeedModifier -= speedMult;
        //restore enemy collision
        Physics.IgnoreLayerCollision(8, 9, false);

        //variable for enemies to check if they can attack this player
        isStealthed = false;
        //set transparency back to normal
        for (int i = 0; i < bodyParts.Count; i++)
        {
            bodyParts[i].GetComponent<SkinnedMeshRenderer>().material = opaque[i % 3];
        }

        if (emergeFromShadow)
        {
            weapon.emerge = true;
        }
        

    }
   
}
