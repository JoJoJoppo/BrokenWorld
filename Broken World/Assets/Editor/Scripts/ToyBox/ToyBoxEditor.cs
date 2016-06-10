using UnityEngine;
using UnityEditor;
using UnityEngine.Audio;
using System.Collections;
using BrokenWorld.MasterMixer;

namespace BrokenWorld.ToyBox
{

    public class ToyBoxEditor : EditorWindow
    {
        float AudioTestVol;
        bool drunkActive;

        [MenuItem("BrokenWorld/Toy Box")]
        public static void ShowMapEditorWindow()
        {
            var window = GetWindow<ToyBoxEditor>();
            window.titleContent.text = "Toy Box";
        }

        void OnEnable()
        {
            drunkActive = false;
            AudioTestVol = 0;
        }

        public void OnGUI()
        {
            AudioControlTest();

            GUILayout.Space(25);

        }

        private void AudioControlTest()
        {
            MasterMixerControl.masterMixer = EditorGUILayout.ObjectField("Target mixer", MasterMixerControl.masterMixer, typeof(AudioMixer), true) as AudioMixer;

            AudioTestVol = Mathf.Clamp(EditorGUILayout.FloatField("AudioVolume", AudioTestVol), -80, 20);
            if (MasterMixerControl.masterMixer != null)
                MasterMixerControl.SetMusicVol(AudioTestVol);


            drunkActive = EditorGUILayout.Toggle("Drunkness Active: ", drunkActive);

            MasterMixerControl.PlayDrunknes(drunkActive);
        }

    }
}