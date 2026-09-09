using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class ShopItem<TTowerType> : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler where TTowerType : Tower
{
    [SerializeField] protected Image prefabImage;
    [SerializeField] protected TTowerType towerPrefab;
    protected static bool isHovering = false;

    protected static bool IsHovering
    {
        get => isHovering;
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
        if (isHovering) Instantiate(towerPrefab.gameObject, Vector3.zero, Quaternion.identity);
    }
    
}