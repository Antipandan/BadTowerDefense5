using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static Utility.Logging;

public abstract class ShopItem<TTowerType> : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler where TTowerType : Tower
{
    [Tooltip("Image of the item that is to be purchased. Reference required to display the item")]
    [SerializeField] protected Image prefabImage;
    [Tooltip("Tower to be instantiated when bought")]
    [SerializeField] protected TTowerType towerPrefab;
    protected static Shop shop;
    protected static bool isHovering = false;
    protected Color originalColor;

    protected static bool IsHovering
    {
        get => isHovering;
    }

    protected TTowerType TowerPrefab
    {
        get => towerPrefab;
    }

    protected virtual void Awake()
    {
        shop ??= GetComponentInParent<Shop>();
        originalColor = gameObject.GetComponent<Image>().color;
        SubscribeEvents();
    }

    protected virtual void SubscribeEvents()
    {
        shop.GameEvents.onMoneyChanged += CheckIfCanAfford;
    }

    protected virtual void UnsubscribeEvents()
    {
        shop.GameEvents.onMoneyChanged -= CheckIfCanAfford;
    }

    protected virtual void OnEnable()
    {
        shop ??= GetComponentInParent<Shop>();
        if (shop is null) LogNullReferenceError(nameof(shop), ErrorSeverity.Warning, this);
    }

    protected virtual void OnValidate()
    {
        if (towerPrefab is null) return;
        if (prefabImage is not null) prefabImage.sprite = towerPrefab.gameObject.GetComponent<SpriteRenderer>().sprite;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;
        gameObject.transform.localScale = GameConstants.ShopItemOnHoverScale;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
        gameObject.transform.localScale = Vector3.one;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isHovering || (Economy.Instance is null || Economy.Instance.CurrentMoney < towerPrefab.TowerCost)) return;
        TTowerType clickedGameObject = Instantiate(towerPrefab, 
            new Vector3(100, 100, 0), Quaternion.identity);
        if (clickedGameObject == null) return;
        clickedGameObject.FollowMouse = true;
    }

    protected void CheckIfCanAfford()
    {
        Image image = gameObject.GetComponent<Image>();
        if (Economy.Instance is not null && towerPrefab.TowerCost > Economy.Instance.CurrentMoney)
        {
            if (image is not null) image.color = Color.red * originalColor;
        }
        else image.color = Color.white * originalColor;
        
    }
}