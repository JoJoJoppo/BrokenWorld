using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

namespace BrokenWorld
{
    public static class MasterMixerControl
    {
        public static AudioMixer masterMixer;
        public static bool Drunk;


        public static void SetMaserVol(float volume)
        {
            if (masterMixer != null)
                masterMixer.SetFloat("MaserVol", volume);
        }

        public static void SetAmbientVol(float volume)
        {
            if (masterMixer != null)
                masterMixer.SetFloat("AmbientVol", volume);
        }

        public static void SetMusicVol(float volume)
        {
            if (masterMixer != null)
                masterMixer.SetFloat("MusicVol", volume);
        }

        public static void PlayDrunknes()
        {
            if (masterMixer != null)
            {
                if (Drunk)
                {
                    Debug.Log("Drunk!");
                    masterMixer.SetFloat("MusicVol", 5);
                    masterMixer.SetFloat("MusicPitch", 0.60f);
                    masterMixer.SetFloat("MusicDryMix", 0.57f);
                    masterMixer.SetFloat("MusicWetMix", 0.42f);
                }
                else
                {
                    Debug.Log("Sober...");
                    masterMixer.SetFloat("MusicVol", 0);
                    masterMixer.SetFloat("MusicPitch", 1);
                    masterMixer.SetFloat("MusicDryMix", 1);
                    masterMixer.SetFloat("MusicWetMix", 0);
                    
                }
            }

           
        }
    }
}