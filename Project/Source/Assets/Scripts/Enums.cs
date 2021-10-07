
#region Complete

public enum Foot
{
    Left,
    Right
}

public enum Specialty
{
    Mobility,
    Combat,
    Mind
}

public enum WeaponType
{
    P3K, // Pistol
    Nateva, // Revolver
    MK_X, // Marksman Rifle
    Molkor, // LMG
    GR3_N, // Grenade Launcher
    XRM, // Shotgun
}

public enum Level
{
    MainMenu,
    MemoryLane,
    CargoShip,
    OilRig,
    City,
    Cove
}

//public enum SurfaceType : byte
//{
//    NONE,
//    ITEM,
//    LADDER,
//    CONCRETE,
//    BRICK,
//    ROCK,
//    BARREL,
//    CHAINLINK,
//    METAL,
//    METAL_BOX,
//    METAL_GRATE,
//    METAL_PANEL,
//    METAL_VENT,
//    METAL_VEHICLE,
//    METAL_SMALL_PROP,
//    WOOD,
//    WOOD_CRATE,
//    WOOD_PLANK,
//    WOOD_PANEL,
//    WOOD_SOLID,
//    DIRT,
//    GRASS,
//    GRAVEL,
//    MUD,
//    SAND,
//    WATER,
//    WATER_WADE,
//    WATER_PUDDLE,
//    ICE,
//    SNOW,
//    FOLIAGE,
//    FLESH,
//    ASPHALT,
//    GLASS,
//    TILE,
//    PAPER,
//    CARDBOARD,
//    PLASTER,
//    PLASTIC,
//    PLASTIC_BARREL,
//    RUBBER,
//    TIRE,
//    CARPET,
//    CEILING,
//    POTTERY
//}
//
//public enum SurfaceFootstepType : byte
//{
//    DEFAULT_CONCRETE,
//    WOOD,
//    WOOD_CRATE,
//    RUBBER,
//    SNOW,
//    DIRT,
//    GRASS,
//    MUD,
//    GLASS_TILE,
//    METAL,
//    METAL_GRATE,
//    METAL_CHAINLINK,
//    WATER
//}

#endregion

#region Being Added To

public enum AudioArray
{
    Null,
    RightFoot,
    LeftFoot,
    Jump,
    Fire_P3K,
    Fire_Nateva,
    Fire_MK_X,
    Fire_Molkor,
    Fire_GR3_N,
    Fire_XRM
}

public enum PooledObject
{
    AudioSource
}

//public enum HitEffect : byte
//{
//    PLAYER_BLOOD,
//    PLAYER_SPARKS,
//    WALL_ROCK,
//    METAL,
//    BARREL
//}

#endregion
