using IteratorTasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

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

    public static Task Jump(this IEnumerable<ILocomotor> locomotors, float force)
    {
        if (locomotors.CanJump())
        {
            var jumpTasks = locomotors.Select(x => x.Jump(force)).ToArray();
            return Task.WhenAll(jumpTasks);
        }
        else
        {
            return Task.CompletedTask;
        }
    }
    public static bool CanRest(this IEnumerable<ILocomotor> locomotors)
    {
        return locomotors.All(x => x.CanRest);
    }
    public static Task Rest(this IEnumerable<ILocomotor> locomotors)
    {
        if (locomotors.CanRest())
        {
            var restTasks = locomotors.Select(x => x.Rest()).ToArray();
            return Task.WhenAll(restTasks);
        }
        else
        {
            return Task.CompletedTask;
        }
    }
}