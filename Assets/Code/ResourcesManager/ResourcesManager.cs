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
    /// ͼƬ��������
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
                Debug.Log("��Ʒ����ͼƬ��ȡ����-ͼƬ ����: " + name + "\n��������׼ȷ�ԣ��������ͼƬ��Դ");
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

        Debug.Log("ȱ�����ͼƬ����-��Ʒ����: " + id+"\n������Ʒ����׼ȷ�ԣ�����ResourcesManager��dictionary��Ӷ�Ӧ��Ϣ");
        return default;
    }

}
