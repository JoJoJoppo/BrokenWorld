using UnityEngine;
using UnityEditor;
using System.Collections;

namespace BrokenWorld.ItemSystem
{
    [System.Serializable]
    public class ISWeapon : ISObject, IISWeapon, IISDestructable, IISGameObject
    {
        [SerializeField] int _minDamage;
        [SerializeField] int _durability;
        [SerializeField] int _maxDurability;
        [SerializeField] ISEquipmentSlot _equipmentSlot;
        [SerializeField] GameObject _prefab;

        public EquipmentSlot equipmentSlot;

        const int MAX_DURABILITY = 1000;
        const int MIN_DAMAGE = 10000;

        #region Constructors

        public ISWeapon()
        {
            _equipmentSlot = new ISEquipmentSlot();
        }

        public ISWeapon(int durability, int maxDurability, ISEquipmentSlot equipmentSlot, GameObject prefab)
        {
            _durability = durability;
            _maxDurability = maxDurability;
            _equipmentSlot = equipmentSlot;
            _prefab = prefab;
        }

        public ISWeapon(ISWeapon originalWeapon)
        {
            _minDamage = originalWeapon.MinDamage;
            _durability = originalWeapon.Durability;
            _maxDurability = originalWeapon.MaxDurability;
            _equipmentSlot = originalWeapon.EquipmentSlot;
            _prefab = originalWeapon.Prefab;
        }

        #endregion

        #region Getters & Setters

        public int MinDamage
        {
            get { return _minDamage; }
            set { _minDamage = value; }
        }
        
        public int Durability
        {
            get { return _durability; }
        }

        public int MaxDurability
        {
            get { return _maxDurability; }
        }

        public ISEquipmentSlot EquipmentSlot
        {
            get { return _equipmentSlot; }
        }

        public GameObject Prefab
        {
            get { return _prefab; }
        }

        #endregion


        public int Attack()
        {
            throw new System.NotImplementedException();
        }

        public void TakeDamage(int amount)
        {
            _durability -= amount;

            if (_durability < 0)
                Break();
            
        }

        public void Repair()
        {
            _maxDurability--;

            if (_maxDurability > 0)
                _durability = _maxDurability;
        }

        /// <summary>
        /// Reduce the durability to zero
        /// Beta
        /// </summary>
        public void Break()
        {
            _durability = 0;
        }

        public override void OnGUI()
        {
            base.OnGUI();

            _maxDurability = Mathf.Clamp(EditorGUILayout.IntField("Max Durability:", _maxDurability), 0, MAX_DURABILITY);
            _durability = Mathf.Clamp(EditorGUILayout.IntField("Current Durrability:", _durability), 0, _maxDurability);
            _minDamage = Mathf.Clamp(EditorGUILayout.IntField("Min Damage:", _minDamage), 0, MIN_DAMAGE);
            equipmentSlot = (EquipmentSlot)EditorGUILayout.EnumPopup("Equipment Slot", equipmentSlot);
            _prefab = EditorGUILayout.ObjectField("Prefab", _prefab, typeof(GameObject), false) as GameObject;
        }
  
    }
}