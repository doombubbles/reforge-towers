using Il2CppAssets.Scripts.Models.TowerSets;

namespace ReforgeTowers.Reforges;

public class Dangerous : ModReforge
{
    public override TowerSet TowerSet => TowerSet.Primary;

    public override int Pierce => 1;
}

public class Savage : ModReforge
{
    public override TowerSet TowerSet => TowerSet.Primary;

    public override float Range => .1f;

    public override int Pierce => 2;
}

public class Dull : ModReforge
{
    public override TowerSet TowerSet => TowerSet.Primary;

    public override int Pierce => -1;
}

public class Terrible : ModReforge
{
    public override TowerSet TowerSet => TowerSet.Primary;

    public override int Pierce => -1;

    public override float Range => -.15f;
}

public class Unhappy : ModReforge
{
    public override TowerSet TowerSet => TowerSet.Primary;

    public override float AttackSpeed => -.1f;

    public override float Range => -.1f;
}

public class Shameful : ModReforge
{
    public override TowerSet TowerSet => TowerSet.Primary;

    public override int Pierce => -1;

    public override float Range => -.2f;
}

public class Legendary : ModReforge
{
    public override TowerSet TowerSet => TowerSet.Primary;

    public override int Damage => 1;

    public override float Range => .15f;

    public override int Pierce => 2;

    public override float AttackSpeed => .1f;
}