using UnityEngine;
using System.Collections;

namespace BrokenWorld.ItemSystem
{
    public interface IISObject
    {
        string Name { get; set; }
        int Value { get; set; }
        Sprite Icon { get; set; }
        int Burden { get; set; }
        ISQuality Quality { get; set; }


    }
}