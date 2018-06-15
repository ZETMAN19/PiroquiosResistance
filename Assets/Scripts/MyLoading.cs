using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MyLoading : MonoBehaviour {
    static string nextScene;
    AsyncOperation op;
    public TextMesh txt;
    float percent;
    public GameObject bar;
    public static string NextScene
    {
        get
        {
            return nextScene;
        }

        set
        {
            nextScene = value;
        }
    }

	// Use this for initialization
	void Start () {
        op = SceneManager.LoadSceneAsync(NextScene);
        op.allowSceneActivation = false;
	}
	
	// Update is called once per frame
	void Update () {
        percent = Mathf.MoveTowards(percent, (op.progress + 0.1f), Time.deltaTime);
        txt.text = percent.ToString("000.00%");
        bar.transform.localScale = new Vector3(percent, 1, 1);
        

        if (percent == 1)
        {
            op.allowSceneActivation = true;
        }
	}
}
