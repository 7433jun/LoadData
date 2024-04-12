using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CSVView : MonoBehaviour
{
    [SerializeField] LoadCSV loadCSV;

    [SerializeField] GameObject userView;
    [SerializeField] GameObject itemView;
    [SerializeField] GameObject infoView;

    [SerializeField] GameObject viewButton;
    [SerializeField] GameObject viewImage;
    [SerializeField] GameObject viewText;

    public void ViewUpdate()
    {
        foreach (User user in loadCSV.userData)
        {
            Button button = Instantiate(viewButton, userView.transform).GetComponent<Button>();
            button.onClick.AddListener(() => ViewInfo(user));
            button.GetComponentInChildren<TextMeshProUGUI>().text = user.name;
        }

        foreach (Item item in loadCSV.itemData)
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
            case User user:
                ViewInfoUser(user);
                break;
            case Item item:
                ViewInfoItem(item);
                break;
            default:
                Debug.Log("Not Avaliable Type");
                break;
        }
    }

    private void ViewInfoUser(User user)
    {
        Instantiate(viewText, infoView.transform).GetComponent<TextMeshProUGUI>().text = $"UserID : {user.userID}";
        Instantiate(viewText, infoView.transform).GetComponent<TextMeshProUGUI>().text = $"Name : {user.name}";
        Instantiate(viewText, infoView.transform).GetComponent<TextMeshProUGUI>().text = $"Level : {user.level}";
        Instantiate(viewText, infoView.transform).GetComponent<TextMeshProUGUI>().text = $"Money : {user.money}";

        // 인벤토리 리스트에서 유저와 일치하는 인벤토리 탐색
        List<Inventory> inventories = loadCSV.inventoryData.Where(inventory => inventory.userID == user.userID).ToList();
        foreach (var inventory in inventories)
        {
            GameObject icon = Instantiate(viewImage, infoView.transform);

            // 아이템 탐색
            Item item = loadCSV.itemData.FirstOrDefault(item => item.itemID == inventory.itemID);
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

    private void ViewInfoItem(Item item)
    {
        Instantiate(viewImage, infoView.transform).GetComponent<Image>().sprite = Resources.Load<Sprite>(item.iconPath);
        Instantiate(viewText, infoView.transform).GetComponent<TextMeshProUGUI>().text = $"ItemID : {item.itemID}";
        Instantiate(viewText, infoView.transform).GetComponent<TextMeshProUGUI>().text = $"Name : {item.name}";
        Instantiate(viewText, infoView.transform).GetComponent<TextMeshProUGUI>().text = $"Type : {item.type}";
        Instantiate(viewText, infoView.transform).GetComponent<TextMeshProUGUI>().text = $"Value : {item.value}";
    }
}
