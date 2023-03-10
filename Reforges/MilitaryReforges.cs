using Il2CppAssets.Scripts.Models.TowerSets;

namespace ReforgeTowers.Reforges;

public class Sighted : ModReforge
{
    public override TowerSet TowerSet => TowerSet.Military;

    public override int Damage => 1;
}

public class Rapid : ModReforge
{
    public override TowerSet TowerSet => TowerSet.Military;

    public override float AttackSpeed => .15f;
}

public class Intimidating : ModReforge
{
    public override TowerSet TowerSet => TowerSet.Military;

    public override int Pierce => 1;
}

public class Staunch : ModReforge
{
    public override TowerSet TowerSet => TowerSet.Military;

    public override int Damage => 1;

    public override int Pierce => 1;
}

public class Awful : ModReforge
{
    public override TowerSet TowerSet => TowerSet.Military;

    public override int Pierce => -2;
}

public class Lethargic : ModReforge
{
    public override TowerSet TowerSet => TowerSet.Military;

    public override float AttackSpeed => -.15f;
}

public class Awkward : ModReforge
{
    public override TowerSet TowerSet => TowerSet.Military;

    public override int Pierce => -1;

    public override float AttackSpeed => -.1f;
}

public class Powerful : ModReforge
{
    public override TowerSet TowerSet => TowerSet.Military;

    public override int Damage => 1;

    public override int Pierce => 1;

    public override float AttackSpeed => -.1f;
}

public class Frenzying : ModReforge
{
    public override TowerSet TowerSet => TowerSet.Military;

    public override int Pierce => -1;

    public override float AttackSpeed => .15f;
}

public class Unreal : ModReforge
{
    public override TowerSet TowerSet => TowerSet.Military;

    public override int Damage => 1;

    public override float Range => .15f;

    public override int Pierce => 2;

    public override float AttackSpeed => .1f;
}