using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Difficulty_Slider : MonoBehaviour
{

    public Image easysliderImage, normalSliderImage, mediumSliderImage,hardSliderImage,insaneSliderImage;

    public GameObject startPoint;

    public Image newSlide1,newSlide2,newSlide3;

    public int difficulty=1;

    private float distanceTraveled = 0;



    private void Start()
    {

        newSlide1 = Instantiate(easysliderImage,startPoint.transform.position,Quaternion.identity);
        newSlide1.transform.parent = transform;
        newSlide1.transform.position = startPoint.transform.position + new Vector3(-45, 0, 0);
        newSlide2 = Instantiate(normalSliderImage, startPoint.transform.position, Quaternion.identity);
        newSlide2.transform.parent = transform;
        newSlide2.transform.position = startPoint.transform.position + new Vector3(45, 0, 0);
        distanceTraveled = 1;
    }

    private void FixedUpdate()
    {
        distanceTraveled +=  Time.deltaTime;
        if (newSlide3 != null)
        {
            newSlide3.transform.position += (Vector3.left * Time.deltaTime);
        }
        newSlide2.transform.position += (Vector3.left * Time.deltaTime);
        newSlide1.transform.position += (Vector3.left * Time.deltaTime);

        if (distanceTraveled >= 90)
        {
            SpawnSlide();
            distanceTraveled = 0;
        }



    }


    private void SpawnSlide()
    {
        if (newSlide3 != null)
        {
            Destroy(newSlide1.gameObject);
            newSlide1 = newSlide2;
            newSlide2 = newSlide3;

        }
       

        switch (difficulty)
        {
            
            case 1:
                newSlide3 = Instantiate(mediumSliderImage, startPoint.transform.position, Quaternion.identity);
                newSlide3.transform.parent = transform;
                newSlide3.transform.position = newSlide2.transform.position + new Vector3(90, 0, 0);
                difficulty++;
                break;
            case 2:
                newSlide3 = Instantiate(hardSliderImage, startPoint.transform.position, Quaternion.identity);
                newSlide3.transform.parent = transform;
                newSlide3.transform.position = newSlide2.transform.position + new Vector3(90, 0, 0);
                difficulty++;
                break;
            case 3:
                newSlide3 = Instantiate(insaneSliderImage, startPoint.transform.position, Quaternion.identity);
                newSlide3.transform.parent = transform;
                newSlide3.transform.position = newSlide2.transform.position + new Vector3(90, 0, 0);
                break;
            default:
                break;
        }


    }


}
