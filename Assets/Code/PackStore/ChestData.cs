using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestData
{
    public float scalar { get;set;}

    public ChestJsonData content { get; set; }
}

public class ChestJsonData
{
    public enum packType
    {
        realMoneyPayments,
        coinPayments
    }

    public string name;
    public string bg;
    [JsonProperty("value")]
    public int cost;
    [JsonProperty("VType")]
    public int type;
    [JsonProperty("packCnt")]
    public int PackCount;

    public int coins;
    public List<item> content;

    public ChestJsonData()
    {
        name = "default";
        bg = "Assets/UI/Reference/GameScreenRef.png";
        cost = 10;
        type = 0;
        PackCount = 1;

        coins = 100;

        content = new List<item>();
    }
}

public class item
{
    public string id;
    public int cnt;
}

public class JsonWrap
{
    public List<ChestJsonData> wrap;
}
