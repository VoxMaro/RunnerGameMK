using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MovingObjectController
{
    // Start is called before the first frame update


    [SerializeField] GameObject itemPrefab = null;

    public void SetProperties(float Length = 10f, float Speed = -8f,  bool Breakable = false)
    {
        objectSpeed = Speed;
        objectLength = Length;
        transform.localScale = new Vector3(1, 1, objectLength);
        isBreakable = Breakable;
    }


    public void AddItem()
    {
        float RandomSpawnDistance = Random.Range(0f, 100f) / 100;
        float RandomSpawnHeight = Random.Range(10f, 40f) / 10;
        float RandomShieldChance = Random.Range(0f, 1f);
        GameObject item = Instantiate(itemPrefab, transform.position+new Vector3(0,RandomSpawnHeight,objectLength*RandomSpawnDistance),transform.rotation);
        item.transform.SetParent(transform);
        if(RandomShieldChance > 0.8f)
        item.GetComponent<PickupItem>().CurrentItemType = PickupItem.ItemTypes.Shield;
    }
    
}
