using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Item",menuName ="Inventory/New Item")]
public class NewBehaviourScript : MonoBehaviour
{
    public string itemName;
    public Sprite itemImage;
    public int itemHeld;
    public string itemInfo;
}
