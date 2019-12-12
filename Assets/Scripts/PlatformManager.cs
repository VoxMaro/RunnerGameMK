using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    [SerializeField] GameObject platformPrefab = null;

    List<GameObject> activePlatforms;

    RunnerGameplayFunctions funcref;

    float PlatformTimer = 0f;
    bool firstPlatform = false;

    //currently creating/destroying objects, maybe use object pool instead


    // Start is called before the first frame update
    void Start()
    {
        funcref = transform.GetComponent<RunnerGameplayFunctions>();
        activePlatforms = new List<GameObject>();
    }
    // Update is called once per frame
    void Update()
    {
        if (funcref.GameStarted()) {

            if (firstPlatform == false)
            {
                SpawnFirstPlatform();
                PlatformTimer = 0f;
            }
            PlatformTimer += Time.deltaTime;
            if (PlatformTimer > 2f)
            {
                SpawnPlatform();
                PlatformTimer = 0f;
            }
            List<GameObject> offscreenPlatforms = activePlatforms.FindAll(platform => platform.GetComponent<PlatformController>().PlatformIsOffscreen());
            foreach (GameObject platform in offscreenPlatforms)
            {
                activePlatforms.Remove(platform);
                Destroy(platform);
            }
        }
    }
    void SpawnFirstPlatform()
    {
        GameObject createdPlatform = Instantiate(platformPrefab, new Vector3(0, -3, 0), Quaternion.identity);
        createdPlatform.GetComponent<PlatformController>().SetPlatformProperties(65f);
        activePlatforms.Add(createdPlatform);
        firstPlatform = true;
    }
    void SpawnPlatform()
    {
        GameObject createdPlatform = Instantiate(platformPrefab, new Vector3(0,0,20), Quaternion.identity);
        activePlatforms.Add(createdPlatform);
    }
}
