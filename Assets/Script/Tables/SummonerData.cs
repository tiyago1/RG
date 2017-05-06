using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Runtime.Serialization;

public class SummonerData  
{
    public int id { get; set; }
    public string name { get; set; }
    public int profileIconId { get; set; }
    public long revisionDate { get; set; }
    public int summonerLevel { get; set; }
}

