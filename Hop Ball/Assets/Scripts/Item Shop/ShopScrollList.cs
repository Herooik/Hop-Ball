using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopScrollList : MonoBehaviour
{
    public static ShopScrollList Instance { get; private set; }
    [SerializeField] private BoughtItems boughtItems;
    [SerializeField] private List<ItemData> itemList;
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private Transform contentPanel;

    public List<Button> _buttons;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SpawnButtons();
        Debug.Log(_buttons.Count);
    }

    private void SpawnButtons()
    {
        for (int i = 0; i < itemList.Count; i++)
        {
            var item = itemList[i];
            var newButton = Instantiate(buttonPrefab);
            newButton.transform.SetParent(contentPanel);

            if (boughtItems.CheckList(item))
            {
                newButton.GetComponent<ItemShopButton>().SetBoughtButton(item);
            }

            newButton.GetComponent<ItemShopButton>().SetupButtonData(item);

            _buttons.Add(newButton.GetComponent<Button>());
        }
    }

    public void SetButtonsInteractable()
    {
        Debug.Log(_buttons.Count);
        foreach (var button in _buttons)
        {
            button.interactable = true;
        }
    }
}
