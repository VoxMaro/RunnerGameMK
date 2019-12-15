using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    [SerializeField] GameObject platformPrefab = null;
    [SerializeField] GameObject projectilePrefab = null;
    [SerializeField] float projectileTimerStart = 0f;

    List<MovingObjectController> activeMovingObjects;

    RunnerGameplayFunctions GameplayManager;

    float PlatformTimer = 0f;
    float ProjectileTimer = 0f;
    bool firstPlatform = false;
    bool endStop = false;
    bool restart = false;

    Vector2 lastVector;
    float randomDistMod;

    //currently creating/destroying objects, maybe use object pool instead


    // Start is called before the first frame update
    void Start()
    {
        GameplayManager = transform.GetComponent<RunnerGameplayFunctions>();
        activeMovingObjects = new List<MovingObjectController>();
        ProjectileTimer = projectileTimerStart;
    }
    // Update is called once per frame

    void RestartGame()
    {
        foreach(MovingObjectController obj in activeMovingObjects)
        {
            Destroy(obj.gameObject);
        }
        activeMovingObjects.Clear();
        ProjectileTimer = projectileTimerStart;
        PlatformTimer = 0f;
        SpawnFirstPlatform();
        restart = false;
        endStop = false;
    }
    void Update()
    {
        switch (GameplayManager.GetState())
        {
            case RunnerGameplayFunctions.GameState.Running:
                if(restart == true)
                {
                    RestartGame();

                }
                PlatformTimer += Time.deltaTime;
                ProjectileTimer += Time.deltaTime;
                //Debug.Log(PlatformTimer);
                if (!firstPlatform)
                {
                    SpawnFirstPlatform();
                    PlatformTimer = 0f;
                }
                
                if (PlatformTimer > 2f)
                {
                    SpawnPlatform();
                }
                if(ProjectileTimer > 3f)
                {
                    SpawnProjectile();
                }

                List<MovingObjectController> offscreenPlatforms = activeMovingObjects.FindAll(objects => objects.IsOffscreen());

                foreach (MovingObjectController platforms in offscreenPlatforms)
                {
                    activeMovingObjects.Remove(platforms);
                    Destroy(platforms.gameObject);
                }
                break;
            case RunnerGameplayFunctions.GameState.Dead:
                if (!endStop)
                {
                    foreach (MovingObjectController objects in activeMovingObjects)
                    {
                        objects.SetSpeed(0);
                    }
                    endStop = true;
                    restart = true;
                }
                break;
            default:
                break;
        }
    }
    void SpawnFirstPlatform()
    {
        GameObject createdPlatform = Instantiate(platformPrefab, new Vector3(0, -2, 0), Quaternion.identity);
        createdPlatform.GetComponent<PlatformController>().SetLength(35f);
        //createdPlatform.GetComponent<PlatformController>().SetSpeed(0f);
        activeMovingObjects.Add(createdPlatform.GetComponent<PlatformController>());
        firstPlatform = true;
    }
    void SpawnPlatform()
    {
        Vector2 vector = RandomRelativeVectorNormalized();
        //GameObject createdPlatform = Instantiate(platformPrefab, new Vector3(0,( (-6) + vector.y)*1.5f /*+ lastVector.y*/, 25 + (vector.x - (randomDistMod / 5))/ distHeightModFactor), Quaternion.identity);
        randomDistMod = Random.Range(0f, 50f);
        PlatformController createdPlatform = Instantiate(platformPrefab, new Vector3(0,(-3) + vector.y -((lastVector.y+2<vector.y) ? 1 : 0), 25+vector.x), Quaternion.identity).GetComponent<PlatformController>();
           
        //note, make it so platforms can't spawn lower or higher than -6/6
        createdPlatform.SetLength(10 - randomDistMod / 10);
            
        activeMovingObjects.Add(createdPlatform);
        if (randomDistMod < 20) createdPlatform.AddItem();
        lastVector = vector;
        Debug.Log(new Vector3(vector.x,vector.y, 10-randomDistMod/10));
        PlatformTimer = randomDistMod / (80);
    }

    void SpawnProjectile()
    {
        float VarianceNearPlayer = GameplayManager.GetPlayerPosition().y + Random.Range(-4f, 4f);
        activeMovingObjects.Add(Instantiate(projectilePrefab, new Vector3(0, VarianceNearPlayer, 25), Quaternion.identity).GetComponent<ProjectileController>());
        ProjectileTimer = 0f;
    }

    //Add randomization functions here

    Vector2 RandomRelativeVectorNormalized()
    {
        float angle = Random.Range(0f, 70f) * Mathf.Deg2Rad;
        return new Vector2(Mathf.Cos(angle)*3, Mathf.Sin(angle)*5);
    }

}
