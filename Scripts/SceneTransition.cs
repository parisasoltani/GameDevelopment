﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour {



    private void Start()
    {

    }

    public void LoadScene(string sceneName) {
        StartCoroutine(Transition(sceneName));
    }

    IEnumerator Transition(string sceneName) {
        // transitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(sceneName);
    }

}