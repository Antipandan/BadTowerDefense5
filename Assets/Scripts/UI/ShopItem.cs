using System;
using UnityEngine;
using UnityEngine.UI;

public abstract class ShopItem<TTowerType> : MonoBehaviour where TTowerType : Tower
{
    [SerializeField] private Image prefabImage;
    [SerializeField] private TTowerType towerPrefab;
    
    protected void OnValidate()
    {
        if (towerPrefab is not null)
        {
            prefabImage.sprite = towerPrefab.gameObject.GetComponent<SpriteRenderer>().sprite;        
        }
    }
}