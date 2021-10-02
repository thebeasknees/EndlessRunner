using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
     public int speed = 5;
     public float jumpIntensity = 7f;
     public int health = 50;
     public int timer = 10;
    [SerializeField] private Text timerText;
    [SerializeField] private GameObject healthBar;
    

    // Start is called before the first frame update
    void Start()
    {
        health = PlayerPrefs.GetInt("PlayerHealth", health = 50);
        speed = PlayerPrefs.GetInt("PlayerSpeed", 5);
        timer = PlayerPrefs.GetInt("PlayerTimer", 30);
        jumpIntensity = PlayerPrefs.GetFloat("PlayerJump", 7f);
        StartCoroutine("CountDown");
    }

    IEnumerator CountDown()
    {
        while(timer >= 0)
        {
            timerText.text = "Timer: " + timer;
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
        PlayerPrefs.SetInt("PlayerSpeed", speed + 10);
        PlayerPrefs.GetInt("PlayerTimer", timer + 10);
        SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1) % 3);
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.transform.localScale = new Vector3(health / 50f, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
    //    if (Input.GetKeyDown(KeyCode.Escape) && !isPaused){ }
            if (health <= 0)
                GameOver();
    }

    // Created for pausing gameplay
    public void PauseGame()
    {
        Time.timeScale = 0;
        GameObject.FindGameObjectWithTag("Player").GetComponent<EndlessCharacterController>().enabled = false;
    }

    // Created for resuming gameplay
    public void ResumeGame()
    {
        Time.timeScale = 1;
        GameObject.FindGameObjectWithTag("Player").GetComponent<EndlessCharacterController>().enabled = true;
    }

    public void GameOver()
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
        PlayerPrefs.DeleteKey("PlayerTimer");
    }
}
