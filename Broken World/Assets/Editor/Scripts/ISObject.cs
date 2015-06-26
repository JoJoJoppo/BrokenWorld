using UnityEngine;
using UnityEditor;
using System.Collections;

namespace BrokenWorld.ItemSystem
{
    [System.Serializable]
    public class ISObject : IISObject
    {
        [SerializeField] string _name;
        [SerializeField] int _value;
        [SerializeField] Sprite _icon;
        [SerializeField] int _burden;
        [SerializeField] ISQuality _quality;

        const int MAX_VALUE = 1000000;
        const int MAX_BURDEN = 1000;

       
        #region Getters & Setters

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public int Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public Sprite Icon
        {
            get { return _icon; }
            set { _icon = value;}
        }

        public int Burden
        {
            get { return _burden; }
            set { _burden = value; }
        }

        public ISQuality Quality
        {
            get { return _quality; }
            set { _quality = value; }
        }

        #endregion

        public virtual void OnGUI()
        {
            _name = EditorGUILayout.TextField("Name: ", _name);
            _value = Mathf.Clamp(EditorGUILayout.IntField("Value:", _value), 0, MAX_VALUE);
            _burden = Mathf.Clamp(EditorGUILayout.IntField("Burden:", _burden),0,MAX_BURDEN);
            _icon = EditorGUILayout.ObjectField("Icon", _icon, typeof(Sprite), false) as Sprite;
            DisplayQuality();

            GUILayout.Space(10);
        }

        






        public int SelectedQualityID
        {
            get { return qualitySelectedIndex; }
        }

        ISQualityDatabase qdb;
        int qualitySelectedIndex = 0;
        string[] option;


        public ISObject()
        {
            string DATABASE_NAME = @"bwQualityDatabase.asset";
            string DATABASE_PATH = @"Database";
            qdb = ISQualityDatabase.GetDatabase<ISQualityDatabase>(DATABASE_PATH, DATABASE_NAME);

            option = new string[qdb.Count];
            for (int x = 0; x < qdb.Count; x++)
                option[x] = qdb.Get(x).Name;

        }


        public void DisplayQuality()
        {
            qualitySelectedIndex = EditorGUILayout.Popup("Quality", qualitySelectedIndex, option);
            //_quality = qdb.Get(SelectedQualityID);
        }

    }
}