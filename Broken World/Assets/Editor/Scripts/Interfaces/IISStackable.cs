using UnityEngine;
using System.Collections;

namespace BrokenWorld.ItemSystem
{
    public interface IISStackable
    {
        int MaxStack { get; }
        int Stack(int amount);

    }
}