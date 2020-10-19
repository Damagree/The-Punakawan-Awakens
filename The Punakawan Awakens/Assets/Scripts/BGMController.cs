using System;
using System.Collections.Generic;
using TechnomediaLabs;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zetcil;

[Serializable]
public class BGM
{
    public string sceneName;
    public AudioClip theBgm;
};

public class BGMController : MonoBehaviour
{
    [Space(10)]
    [Header("BGM")]
    public BGM[] bgms;

    [Space(10)]
    [Header("Settings")]
    public static bool isDisabled;
    private bool currentState;
    public string settingSceneName;
    public List<string> openScenes = new List<string>();

    [Space(20)]
    public bool debug;
    [ConditionalField("debug")] public string currentScene;
    [ConditionalField("debug")] public bool isMore;


    private void Start()
    {
        gameObject.GetComponent<FindController>().InvokeFindController();
        GameObject[] soundObject = gameObject.GetComponent<FindController>().findingObjectTag;
        if (soundObject.Length > 1)
        {
            Destroy(soundObject[1]);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }

        currentScene = SceneManager.GetActiveScene().name;
        UpdateScene();
    }

    private void FixedUpdate()
    {
        Debug.Log("current scene: " + SceneManager.GetActiveScene().name);
        if (SceneManager.GetActiveScene().name != currentScene)
        {
            UpdateScene();
            for (int i = 0; i < bgms.Length; i++)
            {
                if (openScenes[0] == bgms[i].sceneName)
                {
                    if (gameObject.GetComponent<AudioSource>().clip != bgms[i].theBgm)
                    {
                        gameObject.GetComponent<AudioSource>().clip = bgms[i].theBgm;
                        gameObject.GetComponent<AudioSource>().Play();
                    }
                }
            }
        }

        if (SceneManager.sceneCount > 1 && !isMore)
        {
            UpdateScene();

            for (int i = 0; i < openScenes.Count; i++)
            {
                if (openScenes[i] != currentScene)
                {
                    for (int j = 0; j < bgms.Length; j++)
                    {
                        if (openScenes[i] == bgms[j].sceneName && gameObject.GetComponent<AudioSource>().clip != bgms[j].theBgm)
                        {
                            gameObject.GetComponent<AudioSource>().clip = bgms[j].theBgm;
                            gameObject.GetComponent<AudioSource>().Play();
                        }
                    }
                }
            }
            isMore = true;
        }
        else if (SceneManager.sceneCount < 2 && isMore)
        {

            UpdateScene();
            for (int i = 0; i < bgms.Length; i++)
            {
                if (openScenes[0] == bgms[i].sceneName)
                {
                    if (gameObject.GetComponent<AudioSource>().clip != bgms[i].theBgm)
                    {
                        gameObject.GetComponent<AudioSource>().clip = bgms[i].theBgm;
                        gameObject.GetComponent<AudioSource>().Play();
                    }
                }
            }

            isMore = false;
        }
        if (currentState != isDisabled)
        {
            if (isDisabled)
            {
                gameObject.GetComponent<AudioSource>().Stop();
            }
            else
            {
                gameObject.GetComponent<AudioSource>().Play();
            }

            currentState = isDisabled;   
        }

    }

    void UpdateScene()
    {
        int countLoaded = SceneManager.sceneCount;
        
        if (SceneManager.GetActiveScene().name != currentScene && SceneManager.sceneCount == 1)
        {
            currentScene = SceneManager.GetActiveScene().name;
        }

        openScenes.Clear();
        for (int i = 0; i < countLoaded; i++)
        {
            openScenes.Add(SceneManager.GetSceneAt(i).name);
        }

        //if (openScenes.Count > 1)
        //{
        //    if (openScenes[1] != settingSceneName)
        //    {
        //        gameObject.GetComponent<AudioSource>().Play();
        //    }
        //}
    }

    public void SetMusic()
    {
        isDisabled = !isDisabled;
    }
}
