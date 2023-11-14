
//아이템 클래스
public class Item
{
    public string ItemDescription { get; set; } //아이템 상세설명
    public string ItemName { get; } //아이템 이름
    public int ItemAtk { get; } //아이템 공격력
    public int ItemDef { get; } //아이템 방어력
    public int ItemGold { get; } //아이템 가격
    public int ItemType { get; } //아이템 형태. 0: 장신구, 1: 무기, 2: 방어구
    public bool IsEquipped { get; set; } //아이템 착용여부

    public Item(string itemName, int itemAtk, int itemDef, int itemGold, int itemtype, string itemDescription, bool IsEquipped = false)
    {
        ItemName = itemName;
        ItemAtk = itemAtk;
        ItemDef = itemDef;
        ItemGold = itemGold;
        ItemType = itemtype;
        ItemDescription = itemDescription;
    }
}
