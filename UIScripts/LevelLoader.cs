using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelLoader : MonoBehaviour
{
    public Animator transition;

    public float transitionTime = 1f;

    //name of next level
    public string levelName;

    //Sets Frame rate - Trevor
    private void Start()
    {
        Application.targetFrameRate = Screen.currentResolution.refreshRate;
       
    }
    // Update is called once per frame
    void Update()
    {
        
    }
       
    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void LoadNextLevelByName()
    {
        StartCoroutine(LoadLevel(levelName));
    }

    public void LoadSceneSingle()
    {

        StartCoroutine(LoadLevel(levelName, LoadSceneMode.Single));
    }
    public void LoadRandomScene()
    {
        //loads a random scene
        SceneManager.LoadScene(Random.Range(1, 100));
    }

    public void LoadMainMenu()
    {
        //loads main menu
       StartCoroutine(LoadLevel(0));
    }

    public void LoadSkillMenu()
    {
        //loads skill menu
        StartCoroutine(LoadLevel(7));
    }

    public void Quit()
    {
        Application.Quit();
    }

    public IEnumerator LoadLevel(int levelIndex)
    {
        //play animation
        transition.SetTrigger("Start");

        //wait
        yield return new WaitForSeconds(transitionTime);

        // load scene
        SceneManager.LoadScene(levelIndex);
    }
    //Overload of load level for string input
    public IEnumerator LoadLevel(string levelName, LoadSceneMode type)
    {
        //play animation
        transition.SetTrigger("Start");

        //wait
        yield return new WaitForSeconds(transitionTime);

        // load scene
        SceneManager.LoadScene(levelName, type);
    }

    //Overload of load level for string input
    public IEnumerator LoadLevel(string levelName)
    {
        //play animation
        transition.SetTrigger("Start");

        //wait
        yield return new WaitForSeconds(transitionTime);

        // load scene
        SceneManager.LoadScene(levelName);
    }
    
}
