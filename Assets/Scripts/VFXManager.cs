using System.Collections.Generic;
using UnityEngine;

public class VFXManager : MonoBehaviour
{
    [SerializeField] private GameObject vfxCatchPrefab;
    [SerializeField] private GameObject vfxDamagePref;
    [SerializeField] private GameObject vfxLivePref;
    [SerializeField] private GameObject vfxLoseMushPref;
    private int poolSize = 3;
    [SerializeField] private List<GameObject> vfxCatchList;
    [SerializeField] private List<GameObject> vfxDamageList;
    [SerializeField] private List<GameObject> vfxLiveList;
    [SerializeField] private List<GameObject> vfxLoseMushList;


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
        AddVfxCatchToPool(poolSize);
        AddVfxDamageToPool(poolSize);
        AddVfxLiveToPool(poolSize);
        AddVfxLoseMushToPool(poolSize);
    }

    private void AddVfxCatchToPool(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject vfxMush = Instantiate(vfxCatchPrefab);
            vfxMush.SetActive(false);
            vfxCatchList.Add(vfxMush);
            vfxMush.transform.parent = transform;
        }
    }

    private void AddVfxDamageToPool(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject vfxDamage = Instantiate(vfxDamagePref);
            vfxDamage.SetActive(false);
            vfxDamageList.Add(vfxDamage);
            vfxDamage.transform.parent = transform;
        }
    }

    private void AddVfxLiveToPool(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject vfxLive = Instantiate(vfxLivePref);
            vfxLive.SetActive(false);
            vfxLiveList.Add(vfxLive);
            vfxLive.transform.parent = transform;
        }
    }

    private void AddVfxLoseMushToPool(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject vfxLoseMush = Instantiate(vfxLoseMushPref);
            vfxLoseMush.SetActive(false);
            vfxLoseMushList.Add(vfxLoseMush);
            vfxLoseMush.transform.parent = transform;
        }
    }

    public GameObject RequestVfxCatch()
    {
        for (int i = 0; i < vfxCatchList.Count; i++)
        {
            if (!vfxCatchList[i].activeSelf)
            {
                vfxCatchList[i].SetActive(true);
                return vfxCatchList[i];
            }
        }

        AddVfxCatchToPool(1);
        vfxCatchList[vfxCatchList.Count - 1].SetActive(true);
        return vfxCatchList[vfxCatchList.Count - 1];
    }

    public GameObject RequestVfxDamage()
    {
        for (int i = 0; i < vfxDamageList.Count; i++)
        {
            if (!vfxDamageList[i].activeSelf)
            {
                vfxDamageList[i].SetActive(true);
                return vfxDamageList[i];
            }
        }

        AddVfxDamageToPool(1);
        vfxDamageList[vfxDamageList.Count - 1].SetActive(true);
        return vfxDamageList[vfxDamageList.Count - 1];
    }

    public GameObject RequestVfxLive()
    {
        for (int i = 0; i < vfxLiveList.Count; i++)
        {
            if (!vfxLiveList[i].activeSelf)
            {
                vfxLiveList[0].SetActive(true);
                return vfxDamageList[0];
            }            
        }

        AddVfxLiveToPool(1);
        vfxLiveList[vfxLiveList.Count - 1].SetActive(true);
        return vfxLiveList[vfxLiveList.Count - 1];
    }

    public GameObject RequestVfxLoseMush()
    {
        for (int i = 0; i < vfxLoseMushList.Count; i++)
        {
            if (!vfxLoseMushList[i].activeSelf)
            {
                vfxLoseMushList[i].SetActive(true);
                return vfxLoseMushList[i];
            }
        }

        AddVfxLoseMushToPool(1);
        vfxLoseMushList[vfxLoseMushList.Count - 1].SetActive(true);
        return vfxLoseMushList[vfxLoseMushList.Count - 1];
    }
}
