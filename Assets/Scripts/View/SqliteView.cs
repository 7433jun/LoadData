using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SqliteView : MonoBehaviour
{
    [SerializeField] LoadSqlite loadSqlite;

    [SerializeField] GameObject userView;
    [SerializeField] GameObject itemView;
    [SerializeField] GameObject infoView;

    [SerializeField] GameObject viewButton;
    [SerializeField] GameObject viewImage;
    [SerializeField] GameObject viewText;

    public void ViewUpdate()
    {
        // LoadSqlite에서 정의한 User을 사용
        foreach (LoadSqlite.User user in loadSqlite.userData)
        {
            Button button = Instantiate(viewButton, userView.transform).GetComponent<Button>();
            button.onClick.AddListener(() => ViewInfo(user));
            button.GetComponentInChildren<TextMeshProUGUI>().text = user.name;
        }

        foreach (LoadSqlite.Item item in loadSqlite.itemData)
        {
            Button button = Instantiate(viewButton, itemView.transform).GetComponent<Button>();
            button.onClick.AddListener(() => ViewInfo(item));
            button.GetComponentInChildren<TextMeshProUGUI>().text = item.name;
        }
    }

    public void ViewInfo<T>(T data)
    {
        if (infoView.transform.childCount != 0)
        {
            foreach (Transform child in infoView.transform)
            {
                Destroy(child.gameObject);
            }
        }

        switch (data)
        {
            case LoadSqlite.User user:
                ViewInfoUser(user);
                break;
            case LoadSqlite.Item item:
                ViewInfoItem(item);
                break;
            default:
                Debug.Log("Not Avaliable Type");
                break;
        }
    }

    private void ViewInfoUser(LoadSqlite.User user)
    {
        Instantiate(viewText, infoView.transform).GetComponent<TextMeshProUGUI>().text = $"UserID : {user.userID}";
        Instantiate(viewText, infoView.transform).GetComponent<TextMeshProUGUI>().text = $"Name : {user.name}";
        Instantiate(viewText, infoView.transform).GetComponent<TextMeshProUGUI>().text = $"Level : {user.level}";
        Instantiate(viewText, infoView.transform).GetComponent<TextMeshProUGUI>().text = $"Money : {user.money}";

        // LoadSqlite에서 정의한 Inventory 테이블을 씀으로 리스트 안의 객체들의 타입이 다름
        // c#의 LINQ문법
        //List<LoadSqlite.Inventory> inventories = loadSqlite.inventoryData.Where(inventory => inventory.userID == user.userID).ToList();

        // db에 바로 쿼리를 던지는 문법
        List<LoadSqlite.Inventory> inventories = loadSqlite.connection.Query<LoadSqlite.Inventory>($"SELECT * FROM Inventory WHERE userID == {user.userID}").ToList();

        foreach (var inventory in inventories)
        {
            GameObject icon = Instantiate(viewImage, infoView.transform);

            // 마찬가지로 Item 테이블 객체를 사용
            LoadSqlite.Item item = loadSqlite.itemData.FirstOrDefault(item => item.itemID == inventory.itemID);
            if (item != null)
            {
                icon.GetComponent<Image>().sprite = Resources.Load<Sprite>(item.iconPath);
                icon.GetComponentInChildren<TextMeshProUGUI>().text = $"{inventory.itemStack}";
            }
            else
            {
                Debug.LogError("아이템을 찾을 수 없습니다.");
            }
        }

    }

    private void ViewInfoItem(LoadSqlite.Item item)
    {
        Instantiate(viewImage, infoView.transform).GetComponent<Image>().sprite = Resources.Load<Sprite>(item.iconPath);
        Instantiate(viewText, infoView.transform).GetComponent<TextMeshProUGUI>().text = $"ItemID : {item.itemID}";
        Instantiate(viewText, infoView.transform).GetComponent<TextMeshProUGUI>().text = $"Name : {item.name}";
        Instantiate(viewText, infoView.transform).GetComponent<TextMeshProUGUI>().text = $"Type : {item.type}";
        Instantiate(viewText, infoView.transform).GetComponent<TextMeshProUGUI>().text = $"Value : {item.value}";
    }
}
