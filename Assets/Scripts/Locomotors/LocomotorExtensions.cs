using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public static class LocomotorExtensions
{
    public static void Move(this IEnumerable<ILocomotor> locomotors, float velocity)
    {
        foreach (var locomotor in locomotors) { locomotor.Move(velocity); }
    }

    public static void Rotate(this IEnumerable<ILocomotor> locomotors, float velocity)
    {
        foreach (var locomotor in locomotors) { locomotor.Rotate(velocity); }
    }

    public static bool CanJump(this IEnumerable<ILocomotor> locomotors)
    {
        return locomotors.All(x => x.CanJump);
    }

    public static void Jump(this IEnumerable<ILocomotor> locomotors, float force)
    {
        if (locomotors.CanJump())
        {
            foreach (var locomotor in locomotors) { locomotor.Jump(force); }
        }
    }
}