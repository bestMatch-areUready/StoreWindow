using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ResourcesManager : MonoBehaviour
{
    [SerializeField]
    List<Sprite> itemIcons = new List<Sprite>();

    [SerializeField]
    private Sprite coinImage;

    /// <summary>
    /// 图片索引配置
    /// </summary>
    Dictionary<string, int> keyValuePairs = new Dictionary<string, int>()
    {
        {"id1",0 },
        {"id2",1 },
        {"id3",2 },
        {"id4",3 },
        {"id5",4 },
    };

    Dictionary<string,Sprite> bgImages = new Dictionary<string,Sprite>();

    //Sprite chestBG = null;

    public static ResourcesManager Instance;
    public Sprite ChestBG(string name)
    {
        if (!bgImages.ContainsKey(name))
        {
            Sprite newImage = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/UI/Reference/" + name);
            bgImages.Add(name, newImage);
            if (newImage == null)
                Debug.Log("商品背景图片获取错误-图片 名称: " + name + "\n请检查名称准确性，或导入合适图片资源");
        }

        return bgImages[name];
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public Sprite CoinImage
    {
        get { return coinImage; }
    }
    public Sprite ItemIcon(int id)
    {
        if (id < itemIcons.Count)
        {
            return itemIcons[id];
        }
        return default;
    }
    public Sprite ItemIcon(string id)
    {
        if (keyValuePairs.ContainsKey(id))
        {
            return itemIcons[keyValuePairs[id]];
        }

        Debug.Log("缺少相关图片配置-物品名称: " + id+"\n请检查物品名称准确性，或于ResourcesManager内dictionary添加对应信息");
        return default;
    }

}
