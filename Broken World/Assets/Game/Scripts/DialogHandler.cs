using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Collections;
using System.IO;
using System;

namespace BrokenWorld
{   
    [RequireComponent(typeof(AudioSource))]
    public class DialogHandler : MonoBehaviour 
    {
        

        Text instruction;
        string fullDialogTemp ="";
        string fullDialogOriginal = "";
        int dialogIndex;
        float tickLength = 1;
        float _scale;
        float roundTimeLeft;
        float startTime;
        float[] pitchSets = { 1, 0.95f, 0.90f, 0.85f, 1, 0.95f, 0.90f, 0.85f, 1.25f };

        [Range(-0.6f, 0.6f)]
        public float basePitchMod;

        [Range(0.005f,3)]
        public float roundTimeLength = 0.070f;//360f / 60f;

        AudioSource blipPlayer;
        AudioClip clip;

        const char COMMAND_CHARACTER = '#';
        char currentChar;
        string currentCommand;
        string currentCommandParameter;
	    // Use this for initialization
        bool dialogPaused;

        const string READ_FROM_FILE_TEST = @"Assets/Game/Dialog.txt";

        void Start ()
        {
            fullDialogOriginal = File.ReadAllLines(READ_FROM_FILE_TEST)[0];
            fullDialogTemp = fullDialogOriginal;
            dialogPaused = false;
            currentCommand = "";
            currentCommandParameter = "";
            blipPlayer = gameObject.GetComponent<AudioSource>();
            clip = blipPlayer.clip;

            MasterMixerControl.masterMixer = blipPlayer.outputAudioMixerGroup.audioMixer;

            dialogIndex = 0;
        
            instruction = GetComponent<Text>();
            instruction.text = "";

            roundTimeLeft = 0;
            startTime = 0;
        }
            
        
	    // Update is called once per frame
	    void Update () 
        {
            ReadDialog();

        }

        private void ReadDialog() 
        {

            if (dialogIndex < fullDialogTemp.Length)
            {
                roundTimeLeft = Time.time - startTime;
                if (roundTimeLeft >= roundTimeLength)
                {
                    startTime = Time.time;
                    roundTimeLeft = 0;

                    instruction.text = fullDialogTemp.Substring(0, dialogIndex);
                    currentChar = fullDialogTemp[dialogIndex];

                    if (currentChar == COMMAND_CHARACTER)
                    {
                        FetchDialogCommand(dialogIndex, out currentCommand, out currentCommandParameter);
                        HandleDialogCommand(currentCommand, currentCommandParameter);
                    }
                    else if (currentChar == ' ') { /*Do nothing*/ }
                    else
                    {
                        MasterMixerControl.SetDialogPitch(pitchSets[Mathf.RoundToInt(UnityEngine.Random.Range(0, pitchSets.Length))] + basePitchMod);
                        blipPlayer.PlayOneShot(clip);
                    }
                    
                    dialogIndex++;
                }
            }
            else
            {
                fullDialogTemp = fullDialogOriginal;
                dialogIndex = 0;
                startTime = 0;
            }

        }

        private void FetchDialogCommand(int currentIndex, out string command, out string parameter)
        {
            command = "";
            parameter = "";
            int commandLength = 0;
            int parameterLength = 0;
            int parameterStart = 0;

            if (fullDialogTemp[currentIndex + 1] == '[')
            {
                commandLength = fullDialogTemp.IndexOf(']', currentIndex + 2) - currentIndex ;                
                command = fullDialogTemp.Substring(currentIndex + 2, commandLength - 2);
                fullDialogTemp = fullDialogTemp.Remove(currentIndex, commandLength+1);
                
                if (command.Contains("(") && command.Contains(")"))
                {
                    parameterStart = command.IndexOf('(');
                    parameterLength = command.Length - parameterStart - 2;
                    parameter = command.Substring(parameterStart + 1, parameterLength);
                    command = command.Substring(0, parameterStart);
                }
                currentIndex--;
            }
            else
                Debug.LogError("Incorrect Start of Command");

        }

        private void HandleDialogCommand(string command)
        {
            HandleDialogCommand(command, "");
        }

        private void HandleDialogCommand(string command, string parameter)
        {
            bool drunkActive = false;
                
            

            switch (command.ToUpper())
            { 
                case "DRUNK":
                    if (parameter != "")
                        drunkActive = Boolean.Parse(parameter);
                    else
                        Debug.LogError("The " + command + " command require a parameter");

                    break;

                case "PAUSE": 
                    if (parameter != "")
                    {
                        Debug.Log("Pausat i " + parameter);
                    }
                    else
                        Debug.LogError("The " + command + " command require a parameter");
                    break;

                case "CLEAR":
                    fullDialogTemp = fullDialogTemp.Remove(0, dialogIndex-1);
                    dialogIndex = 0; 
                    break;

                case "CAMERA_SHAKE":

                    break;

                case "SET_LEFT_ACTIVE":
                    if (parameter != "")
                    {
                        Debug.Log("Set left portrait");
                    }
                    else
                        Debug.LogError("The " + command + " command require a parameter");
                    break;

                case "SET_RIGHT_ACTIVE":
                    if (parameter != "")
                    {
                        Debug.Log("Set right portrait");
                    }
                    else
                        Debug.LogError("The " + command + " command require a parameter");
                    break;

                default:
                    Debug.LogError("Unknown dialog: " + command);
                    break;
            }

            MasterMixerControl.PlayDrunknes(drunkActive);
           
        }

        public void SkippDialog()
        {
            if (dialogIndex == fullDialogTemp.Length)
            { 
                //Exit dialog or go to next state
            }
            else
            {
                dialogIndex = fullDialogTemp.Length;
            }
           
        }

    }
}