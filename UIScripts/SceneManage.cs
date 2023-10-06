using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum DebugLevel
{
    FrostedPeaks,
    IslandCity,
    MidnightSpires,
    OceanSanctuary,
    SwampsOfFear

}
public class SceneManage : MonoBehaviour
{
    [Header("Debug Level Choice")]
    [Tooltip("set debugMode to true")]
    public bool debugMode;
    [Tooltip("set which level to test")]
    public DebugLevel level;

    public CharType charType;

    

    public GameObject sliderButton;
    public GameObject jailerButton;
    public GameObject spitterButton;
    public GameObject rocketButton;

    public GameObject prefabToSpawn;

    public GameObject sliderPrefab;
    public GameObject jailerPrefab;
    public GameObject spitterPrefab;
    public GameObject rocketPrefab;

    //temp for next build
    public float totalTime;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    public void CharSelect()
    {

        if (sliderButton.activeInHierarchy)
        {
            prefabToSpawn = sliderPrefab;
            charType = CharType.inmate001;
        }
        else if (jailerButton.activeInHierarchy)
        {
            prefabToSpawn = jailerPrefab;
            charType = CharType.MPGuard;
        }
        else if (spitterButton.activeInHierarchy)
        {
            prefabToSpawn = spitterPrefab;
            charType = CharType.croc;
        }
        else if (rocketButton.activeInHierarchy)
        {
            prefabToSpawn = rocketPrefab;
            charType = CharType.SLB;
        }
        else
        {

            prefabToSpawn = sliderPrefab;


        }


    }
    public void DestroyThis()
    {
        Destroy(gameObject);


    }
}
