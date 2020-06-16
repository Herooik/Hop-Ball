using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class BoughtItems : ScriptableObject
{
    [SerializeField] private List<ItemData> ballsDatas;

    public void AddToList(ItemData itemData)
    {
        ballsDatas.Add(itemData);
    }

    public bool CheckList(ItemData item)
    {
        if(ballsDatas.Contains(item))
        {
            return true;
        }

        return false;
    }
}