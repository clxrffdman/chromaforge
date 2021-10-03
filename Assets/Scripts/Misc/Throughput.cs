using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Throughput 
{
    public int throughput;
    public LaserProfile host;

    public Throughput(int tp, LaserProfile lp)
    {
        throughput = tp;
        host = lp;
    }
}
