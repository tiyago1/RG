using System.Collections.Generic;

/*
public enum SortingType
{
    all,
    altimages,
    blurb,
    enemytips,
    image,
    info,
    lore,
    partype,
    passive,
    recommended,
    skins,
    spells,
    stats,
    tags
}
*/
public class ChampionDto
{
    public int id;
    public string key;
    public string name;
    public string title;

    public List<string> tags;
    public string partype;
}

public class ChampionListDto
{
    public string type;
    public string version;
    public Dictionary<string, ChampionDto> data;
}
