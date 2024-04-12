using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using Newtonsoft.Json;



public class JsonExample : MonoBehaviour
{
    [SerializeField] LoadCSV loadCSV;

    void Start()
    {
        //JTestClass jtc = new JTestClass(true);
        //string jsonData = ObjectToJson(jtc);
        //CreateJsonFile(Application.dataPath, "JTestClass", jsonData);

        //var jtc2 = LoadJsonFile<JTestClass>(Application.dataPath, "JTestClass");
        //jtc2.Print();
    }

    string ObjectToJson(object obj)
    {
        return JsonUtility.ToJson(obj);
    }

    T JsonToObject<T>(string jsonData)
    {
        return JsonUtility.FromJson<T>(jsonData);
    }

    void CreateJsonFile(string createPath, string fileName, string jsonData)
    {
        FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", createPath, fileName), FileMode.Create);
        byte[] data = Encoding.UTF8.GetBytes(jsonData);
        fileStream.Write(data, 0, data.Length);
        fileStream.Close();
    }

    T LoadJsonFile<T>(string loadPath, string fileName)
    {
        FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", loadPath, fileName), FileMode.Open);
        byte[] data = new byte[fileStream.Length];
        fileStream.Read(data, 0, data.Length);
        fileStream.Close();
        string jsonData = Encoding.UTF8.GetString(data);
        return JsonUtility.FromJson<T>(jsonData);
    }

    public void jsonbutton()
    {
        //string userstr = JsonUtility.ToJson(loadCSV.userData);
        //CreateJsonFile("Assets/Resources/Json", "User", userstr);
        //
        //string itemstr = JsonUtility.ToJson(loadCSV.itemData);
        //CreateJsonFile("Assets/Resources/Json", "Item", itemstr);
        //
        //string inventorystr = JsonUtility.ToJson(loadCSV.inventoryData);
        //CreateJsonFile("Assets/Resources/Json", "Inventory", inventorystr);

        
        string userstr = JsonConvert.SerializeObject(loadCSV.userData, Formatting.Indented);
        File.WriteAllText("Assets/Resources/Json/User.json", userstr);
        
        string itemstr = JsonConvert.SerializeObject(loadCSV.itemData, Formatting.Indented);
        File.WriteAllText("Assets/Resources/Json/Item.json", itemstr);
        
        string inventorystr = JsonConvert.SerializeObject(loadCSV.inventoryData, Formatting.Indented);
        File.WriteAllText("Assets/Resources/Json/Inventory.json", inventorystr);

        Debug.Log("done");
    }
}

[System.Serializable]
public class JTestClass
{
    public int i;
    public float f;
    public bool b;
    public Vector3 v;
    public string str;
    public int[] iArray;
    public List<int> iList = new List<int>();

    public JTestClass() { }

    public JTestClass(bool isSet)
    {
        if (isSet)
        {
            i = 10;
            f = 99.9f;
            b = true;
            v = new Vector3(39.56f, 21.2f, 6.4f);
            str = "JSON Test String";
            iArray = new int[] { 1, 1, 3, 5, 8, 13, 21, 34, 55 };

            for (int idx = 0; idx < 5; idx++)
            {
                iList.Add(2 * idx);
            }
        }
    }

    public void Print()
    {
        Debug.Log("i = " + i);
        Debug.Log("f = " + f);
        Debug.Log("b = " + b);
        Debug.Log("v = " + v);
        Debug.Log("str = " + str);

        for (int idx = 0; idx < iArray.Length; idx++)
        {
            Debug.Log(string.Format("iArray[{0}] = [{1}]", idx, iArray[idx]));
        }

        for (int idx = 0; idx < iList.Count; idx++)
        {
            Debug.Log(string.Format("iList[{0}] =  [{1}]", idx, iList[idx]));
        }
    }

}