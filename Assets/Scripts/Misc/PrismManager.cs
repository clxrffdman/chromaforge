using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrismManager : MonoBehaviour
{
    public static PrismManager Instance;

    public List<PrismTower> allPrismTowers;
    public List<TransTower> allTransTowers;
    public GameObject mainCore;


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach(TransTower t in allTransTowers)
        {
            t.tp[0] = 0;
            t.tp[1] = 0;
            t.tp[2] = 0;
            t.tp[3] = 0;
        }

        foreach(TransTower t in allTransTowers)
        {
            t.throughputList.Clear();
        }
    }
}
