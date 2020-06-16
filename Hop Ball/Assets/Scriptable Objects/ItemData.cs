using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class ItemData : ScriptableObject
{
    [SerializeField] private Material material;
    [SerializeField] private int price;
    public bool isBought;

    public Material GetMaterial()
    {
        return material;
    }

    public int GetPrice()
    {
        return price;
    }
}
