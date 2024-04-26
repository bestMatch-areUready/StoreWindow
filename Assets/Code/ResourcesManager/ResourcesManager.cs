using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesManager : MonoBehaviour
{
    [SerializeField]
    List<Sprite> itemIcons=new List<Sprite>();

    public static ResourcesManager Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    public Sprite ItemIcon(int id)
    {
        if (id < itemIcons.Count)
        {
            return itemIcons[id];
        }
        return default;
    }

}
