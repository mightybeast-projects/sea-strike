using System.Numerics;

namespace SeaStrike.Core.Entity.Game.Utility;

public class ShotVector
{
    internal int x => (int)v.X;
    internal int y => (int)v.Y;
    internal int length = 1;
    internal bool isZero => v == Vector2.Zero;
    private Vector2 v;

    internal ShotVector() => v = Vector2.Zero;

    internal void Rotate()
    {
        Normalize();

        if (v == Vector2.Zero)
            v = Vector2.UnitY;
        else if (v == Vector2.UnitY)
            v = Vector2.UnitX;
        else if (v == Vector2.UnitX)
            v = -Vector2.UnitY;
        else if (v == -Vector2.UnitY)
            v = -Vector2.UnitX;
        else
            v = Vector2.Zero;
    }

    internal void Invert()
    {
        Normalize();

        v *= -1;
    }

    internal void Extend() => length++;

    internal void Normalize() => length = 1;
}