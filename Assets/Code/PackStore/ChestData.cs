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
    public string name;
    public string bg;
    [JsonProperty("value")]
    public int cost;
    [JsonProperty("VType")]
    public int type;

    public int coins;
    //public int item1;
    //public int item2;
    //public int item3;
    //public int item4;
    //public int item5;

    public List<item> content;

    public ChestJsonData()
    {
        name = "default";
        bg = "Assets/UI/Reference/GameScreenRef.png";
        cost = 10;
        type = 0;

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
