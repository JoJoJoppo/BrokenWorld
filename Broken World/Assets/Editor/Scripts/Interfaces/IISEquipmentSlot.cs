using UnityEngine;
using System.Collections;

namespace BrokenWorld.ItemSystem
{
    public interface IISEquipmentSlot
    {
        string Name { get; set; }
        Sprite Icon { get; set; }
    }
}