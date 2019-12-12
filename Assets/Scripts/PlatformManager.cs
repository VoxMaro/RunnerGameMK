using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    [SerializeField] GameObject platformPrefab;

    List<GameObject> activePlatforms;

    RunnerGameplayFunctions funcref;

    float PlatformTimer;

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
            PlatformTimer += Time.deltaTime;
            if (PlatformTimer > 3f)
            {
                SpawnPlatform();
                PlatformTimer = 0f;
            }
            List<GameObject> toRemove = activePlatforms.FindAll(platform => platform.GetComponent<PlatformController>().PlatformIsOffscreen());
            foreach (GameObject platform in toRemove)
            {
                activePlatforms.Remove(platform);
                Destroy(platform);
            }
        }
    }

    void SpawnPlatform()
    {
        GameObject createdPlatform = Instantiate(platformPrefab, new Vector3(0,0,25), Quaternion.identity);
        activePlatforms.Add(createdPlatform);
    }
}
