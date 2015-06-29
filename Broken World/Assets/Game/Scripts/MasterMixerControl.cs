using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

namespace BrokenWorld
{
    public static class MasterMixerControl
    {
        public static AudioMixer masterMixer;

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

        public static void SetDialogPitch(float pitch)
        { 
             if (masterMixer != null)
                 masterMixer.SetFloat("DialogPitch", pitch);
        }

        public static void PlayDrunknes(bool drunkActive)
        {
            if (masterMixer != null)
            {
                if (drunkActive)
                {
                    masterMixer.SetFloat("MasterVol", 5);
                    masterMixer.SetFloat("MasterPitch", 0.80f);
                    masterMixer.SetFloat("MasterDryMix", 0.57f);
                    masterMixer.SetFloat("MasterWetMix", 0.42f);
                }
                else
                {
                    masterMixer.SetFloat("MasterVol", 0);
                    masterMixer.SetFloat("MasterPitch", 1);
                    masterMixer.SetFloat("MasterDryMix", 1);
                    masterMixer.SetFloat("MasterWetMix", 0);
                }
            }
            else
            {
                Debug.LogError("MasterMixer not fetched");
            }

           
        }
    }
}