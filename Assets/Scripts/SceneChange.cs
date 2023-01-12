using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SceneChange : MonoBehaviour
{
    public List<Sprite> LoadingImg;
    public Image image;
    private int index = 0;
    public GameObject notLoading;
    public GameObject Loading;
    public AudioSource audio;
    // Start is called before the first frame update
    IEnumerator LoadSceneAsync(string Scene)
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(Scene);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone&&audio.isPlaying==false)
        {
            if (index >= 5) index = 0;
            image.sprite = LoadingImg[index];
            index += 1;
            yield return new WaitForSeconds(1f);
        }
    }
    public void StartGame(string side)
    {
        audio.loop = false;
        notLoading.SetActive(false);
        Loading.SetActive(true);
        PlayerPrefs.SetString("Side", side);
        StartCoroutine(LoadSceneAsync("HexPlay"));
    }
}
