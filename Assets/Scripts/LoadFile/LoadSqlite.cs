using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLite4Unity3d;
using System.Linq;

public class LoadSqlite : MonoBehaviour
{
    public SQLiteConnection connection;

    public List<User> userData;
    public List<Item> itemData;
    public List<Inventory> inventoryData;

    void Start()
    {
        userData = new List<User>();
        itemData = new List<Item>();
        inventoryData = new List<Inventory>();

        // 데이터베이스 파일 경로 설정
        string dbPath = Application.dataPath + "mydb.db";

        // 데이터베이스 연결
        connection = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);

        userData = connection.Table<User>().ToList();
        itemData = connection.Table<Item>().ToList();
        inventoryData = connection.Table<Inventory>().ToList();
    }

    #region Table

    [Table("User")]
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int userID { get; set; }
        public string name { get; set; }
        public int level { get; set; }
        public float money { get; set; }
    }

    [Table("Item")]
    public class Item
    {
        [PrimaryKey, AutoIncrement]
        public int itemID { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public float value { get; set; }
        public string iconPath { get; set; }
    }

    [Table("Inventory")]
    public class Inventory
    {
        public int userID { get; set; }
        public int inventoryOrder { get; set; }
        public int itemID { get; set; }
        public int itemStack { get; set; }
    }

    #endregion


}
