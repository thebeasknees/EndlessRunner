using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    [SerializeField] private GameObject platform;
    [SerializeField] private GameObject deathPlatform;
    [SerializeField] private GameObject obstacle;
    [SerializeField] private float distanceThreshold;
    GameObject player;
    private Vector3 nextPlatformPos = Vector3.zero;

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Instantiate(platform, nextPlatformPos, Quaternion.identity); // Gimbal Lock - Euler angles vs Quaternions
        nextPlatformPos += new Vector3(0, 0, 55);
    }

    // Update is called once per frame
    void Update()
    {
      //  Destroy(gameObject);
        if (Vector3.Distance(nextPlatformPos, player.transform.position) < distanceThreshold)
        {
            GameObject plat = Instantiate(platform, nextPlatformPos, Quaternion.identity);
            plat.GetComponent<MeshRenderer>().material.color = new Color(Random.Range(0f,1f), Random.Range(0f, 1f), Random.Range(0f,1f)); 
            for (int i = 0; i < 3; i++)
            {
                obstacle.transform.position = new Vector3(Random.Range(-2, 3), 1, Random.Range(-20, 21));
                GameObject obs = Instantiate(obstacle, nextPlatformPos + obstacle.transform.position, Quaternion.identity);
                obs.transform.parent = plat.transform;
            }
            for (int i = 0; i < 1; i++)
            {
                deathPlatform.transform.position = new Vector3(Random.Range(-2, 3), -10, Random.Range(-20, 21));
                GameObject dp = Instantiate(deathPlatform, nextPlatformPos + deathPlatform.transform.position, Quaternion.identity);
                dp.transform.parent = plat.transform;
            }
            nextPlatformPos += new Vector3(Random.Range(-2, 3), Random.Range(-2, 2), 55);
        }
    }
}
