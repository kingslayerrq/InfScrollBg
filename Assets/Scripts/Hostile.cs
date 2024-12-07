using System;
using UnityEngine;

public abstract class Hostile: Entity
{
    public HostileType type;
    public enum HostileType
    {
        DMG,
        SLOW,
        DEFAULT
    }

 
}

