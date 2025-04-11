public class Territory
{
    public string TerritoryID;
    public string TerritoryDescription;
    public string RegionID;

    public Territory(string id, string desc, string regionId)
    {
        TerritoryID = id;
        TerritoryDescription = desc;
        RegionID = regionId;
    }
}