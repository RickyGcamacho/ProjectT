using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsOptions : MonoBehaviour
{
    public bool inspect, drop, equip;

    public void Inspectionar()
    {
        inspect = true;
    }

    public void Dropear()
    {
        drop = true;
    }

    public void Equipar()
    {
        equip = true;
    }

}
