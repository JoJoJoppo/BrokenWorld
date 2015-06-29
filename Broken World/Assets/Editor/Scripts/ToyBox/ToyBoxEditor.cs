using UnityEngine;
using UnityEditor;
using UnityEngine.Audio;
using System.Collections;

namespace BrokenWorld
{

    public class ToyBoxEditor : EditorWindow
    {
        float AudioTestVol;
        string fullDialog, subDialog;
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
 

            fullDialog = "Hello";
            subDialog = "";
        }

        public void OnGUI()
        {
            AudioControlTest();

            GUILayout.Space(25);

            DialogTest();
            
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

        private void DialogTest()
        {
            

        }
    }

}