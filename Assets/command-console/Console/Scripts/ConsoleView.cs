using TMPro;
using UnityEngine;

namespace CommandConsole
{
    public class ConsoleView : MonoBehaviour
    {
        public TMP_Text consoleText;
        public TMP_InputField inputField;

        public void Init()
        {
            inputField.onSubmit.AddListener(input => OnSubmit(input));
            ConsoleSignals.OnDisplayEvent   += OnDisplay;
            ConsoleSignals.OnLogEvent       += Log;
        }

        private void OnSubmit(string input)
        {
            inputField.ActivateInputField();
            ConsoleSignals.InvokeOnInput(input);
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
            consoleText.text += log + "\n";
        }
    }
}