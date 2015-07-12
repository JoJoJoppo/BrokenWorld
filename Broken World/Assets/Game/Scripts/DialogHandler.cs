using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Collections;
using System.IO;
using System;
using BrokenWorld.MasterMixer;

namespace BrokenWorld
{   
    [RequireComponent(typeof(AudioSource))]
    public class DialogHandler : MonoBehaviour 
    {
        
        [Range(-0.6f, 0.6f)]
        public float basePitchMod;

        [Range(0.005f, 3)]
        public float RoundTimeLength = 0.07f;
        public bool Loop;
        public string fullDialogOriginal = "";
        
        private float roundTimeLeft;
        private float startTime;
        private int dialogIndex;
        private Text instruction;
        private AudioSource blipPlayer;
        private AudioClip clip;
        private char currentChar;
        private string currentCommand;
        private string currentCommandParameter;
        private bool dialogPlaying;

        private const char COMMAND_START_CHARACTER = '[';

        void Start ()
        {
            Loop = false;
            fullDialogOriginal = "[S(0.07)]What happend? [PAUSE][S(0.08)]He is [S(1)]dead[S(0.07)]...";

            dialogPlaying = true;
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

        public void ReadDialog() 
        {
            float[] pitchSets = { 1, 0.95f, 0.93f, 0.96f, 1, 0.95f, 0.98f, 0.94f, 1.10f };


            if (dialogPlaying)
            {
                if (dialogIndex < fullDialogOriginal.Length)
                {
                    roundTimeLeft = Time.time - startTime;
                    if (roundTimeLeft >= RoundTimeLength)
                    {
                        startTime = Time.time;
                        roundTimeLeft = 0;

                        currentChar = fullDialogOriginal[dialogIndex];

                        if (currentChar == COMMAND_START_CHARACTER)
                        {
                            FetchDialogCommand(dialogIndex, out currentCommand, out currentCommandParameter);
                            HandleDialogCommand(currentCommand, currentCommandParameter);

                            dialogIndex += currentCommand.Length + 1;

                            if (currentCommandParameter.Length > 0)
                                dialogIndex += currentCommandParameter.Length + 2;
                        }
                        else if (currentChar == ' ')
                        {
                            instruction.text += currentChar;
                        }
                        else
                        {
                            instruction.text += currentChar;
                            MasterMixerControl.SetDialogPitch(pitchSets[Mathf.RoundToInt(UnityEngine.Random.Range(0, pitchSets.Length))] + basePitchMod);
                            blipPlayer.PlayOneShot(clip);
                        }

                        dialogIndex++;
                    }
                }
                else if (Loop)
                {
                    ResettDialog();
                }
            }
            else 
            {
                if (Input.GetKeyDown("space"))
                    dialogPlaying = true;
            }
        }

        public void SkippDialog()
        {


        }

        public void PlayDialog()
        {
            dialogPlaying = true;
        }

        public void PauseDialog()
        {
            dialogPlaying = false;
        }

        public void ResettDialog()
        {
            instruction.text = "";
            dialogIndex = 0;
            startTime = 0;
        }

        private void FetchDialogCommand(int currentIndex, out string command, out string parameter)
        {
            command = "";
            parameter = "";
            int commandLength = 0;
            int parameterLength = 0;
            int parameterStart = 0;

            commandLength = fullDialogOriginal.IndexOf(']', currentIndex + 1) - currentIndex;
            command = fullDialogOriginal.Substring(currentIndex + 1, commandLength - 1);
                
            if (command.Contains("(") && command.Contains(")"))
            {
                parameterStart = command.IndexOf('(');
                parameterLength = command.Length - parameterStart - 2;
                parameter = command.Substring(parameterStart + 1, parameterLength);
                command = command.Substring(0, parameterStart);
            }

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
                case "S":   //Read speed
                    if (parameter != "")
                    {
                        RoundTimeLength = float.Parse(parameter);
                    }
                    else
                        Debug.LogError("HandleDialogCommand: The " + command + " command require a parameter");
                    break;

                case "DRUNK":
                    if (parameter != "")
                    {
                        drunkActive = Boolean.Parse(parameter);

                        if (drunkActive)
                            RoundTimeLength = 0.09f;
                        else
                            RoundTimeLength = 0.07f;

                        MasterMixerControl.PlayDrunknes(drunkActive);
                    }
                        
                    else
                        Debug.LogError("HandleDialogCommand: The " + command + " command require a parameter");

                    break;

                case "PAUSE": 
                    dialogPlaying = false;

                    break;

                case "CLEAR":
                    instruction.text = "";
                    break;

                case "CAMERA_SHAKE":

                    break;

                case "SET_LEFT_ACTIVE":
                    if (parameter != "")
                    {
                        Debug.Log("Set left portrait");
                    }
                    else
                        Debug.LogError("HandleDialogCommand: The " + command + " command require a parameter");
                    break;

                case "SET_RIGHT_ACTIVE":
                    if (parameter != "")
                    {
                        Debug.Log("Set right portrait");
                    }
                    else
                        Debug.LogError("HandleDialogCommand: The " + command + " command require a parameter");
                    break;

                default:
                    Debug.LogError(" HandleDialogCommand: Unknown dialog: " + command);
                    break;
            }

            
           
        }

        

        
    }
}