using Il2CppAssets.Scripts.Models.TowerSets;

namespace ReforgeTowers.Reforges;

public class Mystic : ModReforge
{
    public override TowerSet TowerSet => TowerSet.Magic;

    public override int Damage => 1;

    public override float Range => .15f;
}

public class Adept : ModReforge
{
    public override TowerSet TowerSet => TowerSet.Magic;

    public override float Range => .15f;
}

public class Masterful : ModReforge
{
    public override TowerSet TowerSet => TowerSet.Magic;

    public override int Damage => 1;

    public override int Pierce => 1;

    public override float Range => .15f;
}

public class Inept : ModReforge
{
    public override TowerSet TowerSet => TowerSet.Magic;

    public override float Range => -.1f;
}

public class Ignorant : ModReforge
{
    public override TowerSet TowerSet => TowerSet.Magic;

    public override int Pierce => -1;

    public override float Range => -.1f;
}

public class Deranged : ModReforge
{
    public override TowerSet TowerSet => TowerSet.Magic;

    public override int Pierce => -1;
}

public class Intense : ModReforge
{
    public override TowerSet TowerSet => TowerSet.Magic;

    public override int Damage => 1;

    public override int Pierce => -1;
}

public class Taboo : ModReforge
{
    public override TowerSet TowerSet => TowerSet.Magic;

    public override int Pierce => 1;

    public override float AttackSpeed => .1f;

    public override float Range => -.1f;
}

public class Celestial : ModReforge
{
    public override TowerSet TowerSet => TowerSet.Magic;

    public override int Damage => 1;

    public override int Pierce => 1;

    public override float AttackSpeed => -.1f;
}

public class Furious : ModReforge
{
    public override TowerSet TowerSet => TowerSet.Magic;

    public override int Damage => 1;

    public override int Pierce => 1;

    public override float AttackSpeed => -.1f;
}

public class Manic : ModReforge
{
    public override TowerSet TowerSet => TowerSet.Magic;

    public override int Pierce => -1;

    public override float AttackSpeed => .1f;

    public override float Range => .1f;
}

public class Mythical : ModReforge
{
    public override TowerSet TowerSet => TowerSet.Magic;

    public override int Damage => 1;

    public override float AttackSpeed => .1f;

    public override int Pierce => 2;

    public override float Range => .15f;
}