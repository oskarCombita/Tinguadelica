using System.Collections.Generic;
using UnityEngine;

public class VFXManager : MonoBehaviour
{
    [SerializeField] private GameObject vfxCatchPrefab;
    [SerializeField] private GameObject vfxDamagePref;
    [SerializeField] private GameObject vfxLivePref;
    private int poolSize = 1;
    [SerializeField] private List<GameObject> vfxCatchList;
    [SerializeField] private List<GameObject> vfxDamageList;
    [SerializeField] private List<GameObject> vfxLiveList;


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
        AddVfxLive();
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

    private void AddVfxDamageToPool(int amout)
    {
        GameObject vfxDamage = Instantiate(vfxDamagePref);
        vfxDamage.SetActive(false);
        vfxDamageList.Add(vfxDamage);
        vfxDamage.transform.parent = transform;
    }

    private void AddVfxLive()
    {
        GameObject vfxLive = Instantiate(vfxLivePref);
        vfxLive.SetActive(false);
        vfxLiveList.Add(vfxLive);
        vfxLive.transform.parent = transform;

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
        vfxLiveList[0].SetActive(true);
        return vfxDamageList[0];
    }
}
