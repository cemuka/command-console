using System;
using CommandConsole.Signals;
using UnityEngine;
using UnityEngine.UI;

namespace CommandConsole
{
    public class ConsoleView : MonoBehaviour
    {
        public Text consoleText;
        public InputField inputField;

        public void Init()
        {
            inputField.onEndEdit.AddListener(input => OnSubmit(input));
            ConsoleSignals.OnDisplayEvent   += OnDisplay;
            ConsoleSignals.OnLogEvent       += Log;
            ConsoleSignals.OnCheckConsoleActiveEvent += CheckViewActive;
        }

        private void OnSubmit(string input)
        {
            if (string.IsNullOrEmpty(input) == false && string.IsNullOrWhiteSpace(input) == false)
            {
                ConsoleSignals.InvokeOnInput(input);
            }
            inputField.ActivateInputField();
            inputField.text = "";
        }

        private void OnDisplay(bool enabled)
        {
            if (enabled)
            {
                inputField.text = "";
                gameObject.SetActive(true);
                inputField.ActivateInputField();
            }
            else
            {
                gameObject.SetActive(false);
                inputField.text = "";
            }
        }

        private void Log(string log)
        {
            consoleText.text += log +"\n";
        }
    
        private bool CheckViewActive()
        {
            return gameObject.activeSelf;
        }
    }
}