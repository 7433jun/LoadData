using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LoadJson : MonoBehaviour
{
    string json;

    public List<User> userData;
    public List<Item> itemData;
    public List<Inventory> inventoryData;

    void Start()
    {
        userData = new List<User>();
        itemData = new List<Item>();
        inventoryData = new List<Inventory>();

        Load();
    }

    public void Load()
    {
        json = File.ReadAllText("Assets/Resources/Json/User.json");
        userData = JsonConvert.DeserializeObject<List<User>>(json);

        json = File.ReadAllText("Assets/Resources/Json/Item.json");
        itemData = JsonConvert.DeserializeObject<List<Item>>(json);

        json = File.ReadAllText("Assets/Resources/Json/Inventory.json");
        inventoryData = JsonConvert.DeserializeObject<List<Inventory>>(json);
    }
}
