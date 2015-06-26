using UnityEngine;

using System.Collections;

namespace BrokenWorld.ItemSystem
{
    [System.Serializable]
    public class ISQuality : IISQuality
    {
        [SerializeField]
        string _name;
        [SerializeField]
        Sprite _icon;

        public ISQuality()
        {
            _name = "";
            _icon = new Sprite();
        }

        public ISQuality(string name, Sprite icon)
        {
            _name = name;
            _icon = icon;
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public Sprite Icon
        {
            get { return _icon; }
            set { _icon = value; }
        }
    }
}