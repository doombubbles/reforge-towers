using Il2CppAssets.Scripts.Models.TowerSets;

namespace ReforgeTowers.Reforges;

public class Keen : ModReforge
{
    public override TowerSet TowerSet => TowerSet.None;

    public override int Pierce => 1;
}

public class Superior : ModReforge
{
    public override TowerSet TowerSet => TowerSet.None;

    public override int Damage => 1;

    public override int Pierce => 1;

    public override float Cash => .1f;
}

public class Forceful : ModReforge
{
    public override TowerSet TowerSet => TowerSet.None;

    public override float Range => .15f;
}

/* These ones seem weirder when not talking about inanimate objects lol
public class Broken : ModReforge
{
    public override TowerSet TowerSet => TowerSet.None;

    public override int Damage => -1;

    public override float Range => -.2f;
}

public class Damaged : ModReforge
{
    public override TowerSet TowerSet => TowerSet.None;

    public override int Pierce => 1;
}
*/

public class Shoddy : ModReforge
{
    public override TowerSet TowerSet => TowerSet.None;

    public override int Pierce => -1;

    public override float Range => -.15f;

    public override float Cash => -.05f;
}

public class Strong : ModReforge
{
    public override TowerSet TowerSet => TowerSet.None;

    public override float Range => .15f;

    public override float Cash => .05f;
}

public class Unpleasant : ModReforge
{
    public override TowerSet TowerSet => TowerSet.None;

    public override int Pierce => 1;

    public override float Range => .15f;
}

public class Weak : ModReforge
{
    public override TowerSet TowerSet => TowerSet.None;

    public override float Range => -.2f;
}

public class Ruthless : ModReforge
{
    public override TowerSet TowerSet => TowerSet.None;

    public override int Damage => 2;

    public override float Range => .1f;
}

public class Godly : ModReforge
{
    public override TowerSet TowerSet => TowerSet.None;

    public override int Damage => 1;

    public override int Pierce => 1;

    public override float Range => .15f;

    public override float Cash => .15f;
}

public class Demonic : ModReforge
{
    public override TowerSet TowerSet => TowerSet.None;

    public override int Damage => 1;

    public override int Pierce => 2;
}

public class Zealous : ModReforge
{
    public override TowerSet TowerSet => TowerSet.None;

    public override int Pierce => 2;
}

public class Quick : ModReforge
{
    public override TowerSet TowerSet => TowerSet.None;

    public override float AttackSpeed => .1f;
}

public class Deadly : ModReforge
{
    public override TowerSet TowerSet => TowerSet.None;

    public override int Damage => 1;

    public override float AttackSpeed => .1f;
}

public class Agile : ModReforge
{
    public override TowerSet TowerSet => TowerSet.None;

    public override int Pierce => 1;

    public override float AttackSpeed => .1f;
}

public class Nimble : ModReforge
{
    public override TowerSet TowerSet => TowerSet.None;

    public override float AttackSpeed => .05f;
}

public class Murderous : ModReforge
{
    public override TowerSet TowerSet => TowerSet.None;

    public override int Pierce => 2;

    public override float AttackSpeed => .05f;
}

public class Slow : ModReforge
{
    public override TowerSet TowerSet => TowerSet.None;

    public override float AttackSpeed => -.15f;
}

public class Sluggish : ModReforge
{
    public override TowerSet TowerSet => TowerSet.None;

    public override float AttackSpeed => -.2f;
}

public class Lazy : ModReforge
{
    public override TowerSet TowerSet => TowerSet.None;

    public override float AttackSpeed => -.08f;
}

public class Annoying : ModReforge
{
    public override TowerSet TowerSet => TowerSet.None;

    public override int Pierce => -1;

    public override float AttackSpeed => -.15f;
}

public class Nasty : ModReforge
{
    public override TowerSet TowerSet => TowerSet.None;

    public override int Pierce => 1;

    public override float AttackSpeed => .1f;

    public override float Range => -.1f;
}