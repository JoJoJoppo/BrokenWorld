using UnityEngine;
using System.Collections;

namespace BrokenWorld.ItemSystem
{
    public interface IISQuality
    {
        string Name { get; set; }
        Sprite Icon { get; set; }
    }
}