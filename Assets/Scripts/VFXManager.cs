using System.Collections.Generic;
using UnityEngine;

public class VFXManager : MonoBehaviour
{
    [SerializeField] private GameObject[] vfxPrefabs;
    private int poolSize = 1;
    [SerializeField] private List<GameObject> vfxList;

    private static VFXManager instance;

    public static VFXManager Instance { get { return instance; } }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        AddVfxToPool(poolSize);
    }

    private void AddVfxToPool(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject vfxMush = Instantiate(vfxPrefabs[0]);
            vfxMush.SetActive(false);
            vfxList.Add(vfxMush);
            vfxMush.transform.parent = transform;
        }
    }

    public GameObject RequestVfx()
    {
        for (int i = 0; i < vfxList.Count; i++)
        {
            if (!vfxList[i].activeSelf)
            {
                vfxList[i].SetActive(true);
                return vfxList[i];
            }
        }

        AddVfxToPool(1);
        vfxList[vfxList.Count - 1].SetActive(true);
        return vfxList[vfxList.Count - 1];
    }
}
