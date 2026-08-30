using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconScript : MonoBehaviour {
    [SerializeField] private Image icon;
    public void SetIconImage(Sprite iconSprite) {
        this.icon.sprite = iconSprite;
    }
}
