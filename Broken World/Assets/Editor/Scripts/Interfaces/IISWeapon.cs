using UnityEngine;
using System.Collections;

namespace BrokenWorld.ItemSystem
{
    public interface IISWeapon
    {
        int MinDamage { get; set; }
        int Attack();

        
    }
}