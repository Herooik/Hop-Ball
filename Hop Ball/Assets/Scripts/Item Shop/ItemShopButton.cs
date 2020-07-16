using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemShopButton : MonoBehaviour
{
    [SerializeField] private BoughtItems boughtItems;
    [SerializeField] private Material playerMaterial;
    [SerializeField] private IntReference pickUp;
    //[SerializeField] private ShopScrollList shopScrollList;
    [Header("This Button Reference")]
    [SerializeField] private Button thisButton;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private Image pickUpImage;
    [SerializeField] private Image colorImage;

    private ItemData _itemData;

    public void SetupButtonData(ItemData itemData)
    {
        colorImage.color = itemData.GetMaterial().color;
        priceText.text = itemData.GetPrice().ToString();
        _itemData = itemData;
    }
    
    private void Start()
    {
        SetButtonAtStart();

        SetOnClickListener();
    }

    private void SetButtonAtStart()
    {
        if (playerMaterial.color == colorImage.color)
        {
            thisButton.interactable = false;
        }
    }

    private void SetOnClickListener()
    {
        if (_itemData.isBought)
        {
            thisButton.onClick.AddListener(ChangeColor);
        }
        else
        {
            thisButton.onClick.AddListener(TryToBuyItem);
        }
    }

    private void ChangeColor()
    {
        playerMaterial.color = colorImage.color;
        ShopScrollList.Instance.SetButtonsInteractable();
        thisButton.interactable = false;
    }
    
    private void TryToBuyItem()
    {
        if (pickUp.Value >= _itemData.GetPrice())
        {
            pickUp.Value -= _itemData.GetPrice();
            UIManager.Instance.RefreshPlayerValues();
        }
        else
        {
            return;
        }

        boughtItems.AddToList(_itemData);

        SetBoughtButton(_itemData);

        playerMaterial.color = colorImage.color;
        
        ShopScrollList.Instance.SetButtonsInteractable();
        thisButton.interactable = false;
        
        thisButton.onClick.RemoveListener(TryToBuyItem);
        thisButton.onClick.AddListener(ChangeColor);
    }

    public void SetBoughtButton(ItemData itemData)
    {
        itemData.isBought = true;
        priceText.gameObject.SetActive(false);
        pickUpImage.gameObject.SetActive(false);
        colorImage.gameObject.SetActive(true);
    }
}
