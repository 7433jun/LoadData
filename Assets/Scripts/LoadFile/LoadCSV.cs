using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// 경로와 자료 타입을 이어주기 위함
public enum OBJTYPE
{
    USER,
    ITEM,
    INVENTORY,

}

public class LoadCSV : MonoBehaviour
{
    Dictionary<OBJTYPE, string> dPaths;

    StreamReader sr;

    public List<User> userData;
    public List<Item> itemData;
    public List<Inventory> inventoryData;

    void Start()
    {
        userData = new List<User>();
        itemData = new List<Item>();
        inventoryData = new List<Inventory>();
        dPaths = new Dictionary<OBJTYPE, string>();

        dPaths.Add(OBJTYPE.USER, "Assets/Resources/CSV/User.csv");
        dPaths.Add(OBJTYPE.ITEM, "Assets/Resources/CSV/Item.csv");
        dPaths.Add(OBJTYPE.INVENTORY, "Assets/Resources/CSV/Inventory.csv");

        foreach (var dPath in dPaths)
        {
            Load(dPath);
        }
    }

    public void Load(KeyValuePair<OBJTYPE, string> dPath)
    {
        sr = new StreamReader(dPath.Value);

        // 속성 줄 넘기기
        sr.ReadLine();

        while (true)
        {
            string line = sr.ReadLine();
            if (line == null)
            {
                break;
            }

            string[] strs = line.Split(',');

            switch(dPath.Key)
            {
                case OBJTYPE.USER:
                    userData.Add(new User(int.Parse(strs[0]), strs[1], int.Parse(strs[2]), float.Parse(strs[3])));
                    break;
                case OBJTYPE.ITEM:
                    itemData.Add(new Item(int.Parse(strs[0]), strs[1], strs[2], float.Parse(strs[3]), strs[4]));
                    break;
                case OBJTYPE.INVENTORY:
                    inventoryData.Add(new Inventory(int.Parse(strs[0]), int.Parse(strs[1]), int.Parse(strs[2]), int.Parse(strs[3])));
                    break;
                default:
                    break;
            }

        }
    }
}
