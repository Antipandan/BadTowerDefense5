using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static Utility.Utility;

public abstract class ShopItem<TTowerType> : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler where TTowerType : Tower
{
    [SerializeField] protected Image prefabImage;
    [SerializeField] protected TTowerType towerPrefab;
    protected static Shop shop;
    protected static bool isHovering = false;
    protected static Camera mainCamera;

    protected static bool IsHovering
    {
        get => isHovering;
    }

    protected TTowerType TowerPrefab
    {
        get => towerPrefab;
    }

    protected void Awake()
    {
        mainCamera = Camera.main;
    }

    protected virtual void OnEnable()
    {
        shop ??= GetComponentInParent<Shop>();
        if (shop is null) LogNullReferenceError(nameof(shop), ErrorSeverity.Warning, this);
    }

    protected void OnValidate()
    {
        if (towerPrefab is null) return;
        if (prefabImage is not null) prefabImage.sprite = towerPrefab.gameObject.GetComponent<SpriteRenderer>().sprite;
        else Debug.Log($"null");
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
        if (!isHovering) return;
        TTowerType clickedGameObject = Instantiate(towerPrefab, 
            Utility.ConvertBetweenSpaces.ConvertScreenPointToWorldPoint(
                mainCamera, Input.mousePosition), Quaternion.identity);
        if (clickedGameObject == null) return;
        clickedGameObject.FollowMouse = true;
        List<Collider2D> colliders = shop.GameEvents.PublishOnGetMapCollider2Ds();
        // shop.GameEvents.PublishChangeMapCollider2DsState(true, colliders);
    }
}