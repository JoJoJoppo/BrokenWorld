using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;     

namespace BrokenWorld
{
    public class ScriptableObjectDatabase<T> : ScriptableObject where T: class
    {

        [SerializeField] List<T> database = new List<T>();

        #region Generic functions

        public void Add(T item)
        {
            database.Add(item);

            //Save data to disk
            EditorUtility.SetDirty(this);
        }

        public void Insert(int index, T item)
        {
            database.Insert(index, item);
            EditorUtility.SetDirty(this);
        }

        public void Remove(T item)
        {
            database.Remove(item);
            EditorUtility.SetDirty(this);
        }

        public void RemoveAt(int index)
        {
            database.RemoveAt(index);
            EditorUtility.SetDirty(this);
        }

        public void Clear(T item)
        {
            database.Clear();
            EditorUtility.SetDirty(this);
        }

        /// <summary>
        /// Return the number of elements in database
        /// </summary>
        public int Count
        {
            get { return database.Count; }
        }

        public T Get(int index)
        {
            return database.ElementAt(index);
        }

        public void Replace(int index, T item)
        {
            database[index] = item;
            EditorUtility.SetDirty(this);
        }

        #endregion

        public static U GetDatabase<U>(string dbPath, string dbName) where U : ScriptableObject
        {
            string dbFullPath = @"Assets/" + dbPath + "/" + dbName;

            //Load Database
            U db = AssetDatabase.LoadAssetAtPath(dbFullPath, typeof(U)) as U;

            //If database not found
            if (db == null)
            {
                //If folder not found, create folder
                if (!AssetDatabase.IsValidFolder(@"Assets/" + dbPath))
                    AssetDatabase.CreateFolder("Assets", dbPath);
                 
                //Generate a new ScriptableObject database
                try
                {
                    db = ScriptableObject.CreateInstance<U>() as U;

                    AssetDatabase.CreateAsset(db, dbFullPath);
                    AssetDatabase.SaveAssets();
                    AssetDatabase.Refresh();
                }
                catch (Exception e)
                {
                    Debug.LogError(e.Message);
                }
            }

            return db;
        }

    }
}