using UnityEngine;

[CreateAssetMenu(menuName = "Data/ItemData")]
public class ItemDataSO : ScriptableObject
{
    public int id;
    public string itemName;
    public Sprite icon;
    public ItemType type;    // enum { Cosmetic, Functional }
    public int price;
    public GameObject prefab; // Âø¿ë ¸ðµ¨ µî
}