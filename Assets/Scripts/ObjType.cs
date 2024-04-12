[System.Serializable]
public class User
{
    public int userID;
    public string name;
    public int level;
    public float money;

    public User(int _userID, string _name, int _level, float _money)
    {
        userID = _userID;
        name = _name;
        level = _level;
        money = _money;
    }
}

[System.Serializable]
public class Item
{
    public int itemID;
    public string name;
    public string type;
    public float value;
    public string iconPath;

    public Item(int _itemID, string _name, string _type, float _value, string _iconPath)
    {
        itemID = _itemID;
        name = _name;
        type = _type;
        value = _value;
        iconPath = _iconPath;
    }
}

[System.Serializable]
public class Inventory
{
    public int userID;
    public int inventoryOrder;
    public int itemID;
    public int itemStack;

    public Inventory(int _userID, int _inventoryOrder, int _itemID, int _itemStack)
    {
        userID = _userID;
        inventoryOrder = _inventoryOrder;
        itemID = _itemID;
        itemStack = _itemStack;
    }
}