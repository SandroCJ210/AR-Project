using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    public string ItemName;
    public Sprite ItemImage;
    public string ItemDescription;
    public float itemPrice;
    public GameObject Item3DModel;
}
