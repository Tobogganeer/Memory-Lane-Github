
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
    FNAL, // Marksman Rifle
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

public enum SurfaceType : byte
{
    None,
    Default,
    Item,
    Ladder,
    Concrete,
    Brick,
    Rock,
    Barrel,
    Chainlink,
    Metal,
    MetalBox,
    MetalGrate,
    MetalPanel,
    MetalVent,
    MetalVehicle,
    MetalSmallProp,
    Wood,
    WoodCrate,
    WoodPlank,
    WoodPanel,
    WoodSolid,
    Dirt,
    Grass,
    Gravel,
    Mud,
    Sand,
    Water,
    WaterWade,
    WaterPuddle,
    Ice,
    Snow,
    Foliage,
    Flesh,
    Asphalt,
    Glass,
    Tile,
    Paper,
    Cardboard,
    Plaster,
    Plastic,
    PlasticBarrel,
    Rubber,
    Tire,
    Carpet,
    Ceiling,
    Pottery
}

public enum SurfaceFootstepType : byte
{
    None,
    DefaultConcrete,
    Wood,
    WoodCrate,
    Rubber,
    Snow,
    Dirt,
    Grass,
    Mud,
    GlassTile,
    Metal,
    MetalGrate,
    MetalChainlink,
    MetalHollow,
    Water
}

public enum HitboxRegion
{
    Chest,
    Abdomen,
    Arms,
    Legs,
    Head
}

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
    Fire_FNAL,
    Fire_Molkor,
    Fire_GR3_N,
    Fire_XRM,
    Hitmarker,
    Explosion,
    Cachunk,
    Chck,
    Shck,
    Chook
}

public enum PooledObject
{
    AudioSource
}

public enum VisualEffect
{
    None,
    Explosion
}

#endregion
