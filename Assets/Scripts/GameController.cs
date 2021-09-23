using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
     public int speed = 5;
     public float jumpIntensity = 5f;
     public int health = 50;
     public int timer = 10;
    

    // Start is called before the first frame update
    void Start()
    {
        health = PlayerPrefs.GetInt("PlayerHealth", health = 50);
        speed = PlayerPrefs.GetInt("PlayerSpeed", 10);
        StartCoroutine("CountDown");
    }

    IEnumerator CountDown()
    {
        while(timer > 0)
        {
            // wait for 1 second
            yield return new WaitForSeconds(1);
            timer--;
        }
        // pause the player, load the UI message
        GameObject.FindGameObjectWithTag("Player").GetComponent<EndlessCharacterController>().enabled = false;
        yield return new WaitForSeconds(2);
        ChangeLevel();
    }

    private void ChangeLevel()
    {
        PlayerPrefs.SetInt("PlayerHealth", health);
        PlayerPrefs.SetInt("PlayerSpeed", speed + 5);
        SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1) % 3);
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
            GameOver();
    }

    private void GameOver()
    {
        Debug.Log("GAME OVER!!");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.DeleteKey("PlayerHealth");
        PlayerPrefs.DeleteKey("PlayerSpeed");
    }
}
