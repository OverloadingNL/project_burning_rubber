using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GenerateWorld : MonoBehaviour
{
    static public GameObject dummyTraveller;
    static public GameObject lastPlatform;
    static public Platform lastPlatformSettings = null;

    [SerializeField] int startPosition = 60;

    void Awake()
    {
        dummyTraveller = new GameObject("dummy");
    }

    private void Start()
    {
        dummyTraveller.transform.position += Vector3.forward * startPosition;
    }

    public static void RunDummy(int index, bool useIndex)
    {
        GameObject p = Pool.singleton.GetRandom(index, useIndex, lastPlatformSettings == null ? 0 : lastPlatformSettings.endIndex);

        if (p == null) return;

        Platform pSettings = p.GetComponent<Platform>();

        if (lastPlatform != null)
        {
            dummyTraveller.transform.position = lastPlatform.transform.position + Vector3.forward * lastPlatformSettings.length;
        }

        lastPlatform = p;
        lastPlatformSettings = pSettings;

        p.SetActive(true);
        p.transform.position = dummyTraveller.transform.position;
        p.transform.rotation = dummyTraveller.transform.rotation;
    }

}
