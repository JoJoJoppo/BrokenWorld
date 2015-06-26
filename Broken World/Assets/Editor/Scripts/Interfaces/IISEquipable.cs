using UnityEngine;
using System.Collections;

namespace BrokenWorld.ItemSystem
{
    public interface IISEquipable
    {
        ISEquipmentSlot EquipmentSlot { get; }
        bool Equip();
    }
}