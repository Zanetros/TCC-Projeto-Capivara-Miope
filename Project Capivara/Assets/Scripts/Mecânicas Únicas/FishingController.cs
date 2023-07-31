using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FishingController : MonoBehaviour
{
    public bool fishing = false;
    public float minTimeToWait;
    public float maxTimeToWait;
    public bool onCatch = false;
    private float actualTimeToWait1;
    private float actualTimeToWait2;

    public float timeToCatch1;
    public float timeToCatch2;
    private float actualTimeToCatch;
        
    public ItemContainer fishes;
    public GameManager gameManager;
    public GameObject signal;
    public Transform fishSpawn;

    public Item[] fishToSpawn;

    void Start()
    {
        fishing = false;
        onCatch = false;
    }
    
    void Update()
    {
        if (fishing && !onCatch)
        {
            if (actualTimeToWait1 > 0)
            {
                actualTimeToWait1 -= Time.deltaTime;
                if (actualTimeToWait1 <= 0)
                {
                    onCatch = true;
                    actualTimeToCatch = timeToCatch1;
                }
            }
            if (actualTimeToWait1 <= 0)
            {
                actualTimeToWait2 -= Time.deltaTime;
                if (actualTimeToWait2 <= 0)
                {
                    onCatch = true;
                    actualTimeToCatch = timeToCatch2;
                }
            }
        }
        if (onCatch)
        {
            signal.SetActive(true);
            actualTimeToCatch -= Time.deltaTime;
            if (VerifyCatch())
            {
                signal.SetActive(false);
                onCatch = false;
                if (actualTimeToWait1 <= 0 && actualTimeToWait2 <= 0)
                {
                    GetFish();
                    Conclude();
                }
            }
            if (actualTimeToCatch <= 0)
            {
                signal.SetActive(false);
                print("Que pena, você não pegou nada...");
                Conclude();
            }
        }
        
    }

    private bool VerifyCatch()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            return true;
        }
        return false;
    }

    private void GetFish()
    {
        var TypeofFishToSpawn = fishToSpawn[Random.Range(0, fishToSpawn.Length)];
        Vector3 position = fishSpawn.transform.position;
        DropedItemSpawner.instance.SpawnItem(position, TypeofFishToSpawn, 1);
    }
    
    public void Initialize(GameObject s)
    {
        fishing = true;
        onCatch = false;
        gameManager.ControlCharacterControls(false, false);
        actualTimeToWait1 = Random.Range(minTimeToWait, maxTimeToWait);
        actualTimeToWait2 = actualTimeToWait1 / 2;
        signal = s;
    }
    
    void Conclude()
    {
        fishing = false;
        onCatch = false;
        print("The fishing has ended");
        gameManager.ControlCharacterControls(true, true);
    }

    private void PlaySound()
    {
        
    }
    
}
