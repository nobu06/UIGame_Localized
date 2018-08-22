/*
 * Loads the next scene when DataController and LocalizationManager
 * scripts have finished loading the localized texts.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartupManager : MonoBehaviour {

	private IEnumerator Start()
	{
        while(!LocalizationManager.instance.GetIsReady() || !DataController.instance.GetIsReady())
        {
            yield return null;
        }

        SceneManager.LoadScene("MenuScreen");
	}

}
