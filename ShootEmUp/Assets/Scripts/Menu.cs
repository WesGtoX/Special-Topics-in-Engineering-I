using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour {

    public Slider slider;
    public GameObject loadingPanel;

    private AsyncOperation async = null;
    
    void Update() {
        if (async == null) {
            return;
        }

        slider.value = async.progress;
    }

    public void LoadGame() {
        loadingPanel.SetActive(true);
        StartCoroutine(Loading());
    }


    public void Quit() {
        Application.Quit();
    }

    IEnumerator Loading() {
        async = SceneManager.LoadSceneAsync("GameScene");
        yield return async;
    }
}
