using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Manager : MonoBehaviour
{
    private void Update()
    {
        StartCoroutine(ChangeScene());

    }

    IEnumerator ChangeScene()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (SceneManager.GetActiveScene().buildIndex == 2)
            {
                SceneManager.LoadScene(3, LoadSceneMode.Additive);
                yield return new WaitForSeconds(1);
                SceneManager.UnloadSceneAsync(2);
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (SceneManager.GetActiveScene().buildIndex == 3)
            {
                SceneManager.LoadScene(4, LoadSceneMode.Additive);
                yield return new WaitForSeconds(1);
                SceneManager.UnloadSceneAsync(3);
            }
            else
            {
                SceneManager.LoadScene(0);
            }
        }
    }
}
