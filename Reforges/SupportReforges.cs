using Il2CppAssets.Scripts.Models.TowerSets;

namespace ReforgeTowers.Reforges;

// A couple extra custom ones to throw a bone to Banana Farms

public class Fresh : ModReforge
{
    public override TowerSet TowerSet => TowerSet.Support;

    public override float Cash => .05f;
}

public class Healthy : ModReforge
{
    public override TowerSet TowerSet => TowerSet.Support;

    public override float Cash => .1f;
}