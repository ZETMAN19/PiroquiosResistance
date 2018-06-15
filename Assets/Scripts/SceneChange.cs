using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour {
    static SceneChange instance;
    public bool AutoLoad;
    public string scene;
    public static SceneChange Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogError("SceneChange nao esta na cena!");
            }
            return instance;
        }

    }

	// Use this for initialization
	void Awake () {

        instance = this;
	}

    void Start()
    {
        if (AutoLoad)
            Invoke("MyLoadScene", 5);
    }

    public void MyLoadScene(string scene)
    {
        MyLoading.NextScene = scene;
        SceneManager.LoadScene("Loading");
    }

    public void MyLoadScene()
    {
        MyLoading.NextScene = scene;
        SceneManager.LoadScene("Loading");
    }

}
