using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Reflection;
using UnityEngine.EventSystems;
using CommandConsole.Signals;

namespace CommandConsole
{
    public class Console
    {    
        private static Dictionary<string, Action<string[]>> _commands;

        public static bool IsActive { get { return ConsoleSignals.InvokeCheckConsoleActive(); } }

        private static void Execute(CommandInfo cmd)
        {
            if (_commands.ContainsKey(cmd.key))
            {
                _commands[cmd.key](cmd.args);
            }
            else
            {
                Log("err");
            }
        }

        private static CommandInfo Parse(string input)
        {
            var split = new ArraySegment<string>(input.Split(' '));
            return new CommandInfo()
            {
                key     = split.ElementAt(0),
                args    = split.Skip(1).ToArray<string>()
            };   
        }

        private static void OnInput(string input)
        {
            Log(input, "green");
            Execute(Parse(input));
        }

        public static void Initialize()
        {
            _commands = new Dictionary<string, Action<string[]>>();
            
            var prefab = Resources.Load<GameObject>("ConsoleCanvas");
            GameObject.Instantiate(prefab).GetComponentInChildren<ConsoleView>().Init();
            if (GameObject.FindObjectOfType<EventSystem>() == null)
            {
                var eventSystem = new GameObject("EventSystem");
                eventSystem.AddComponent<EventSystem>();
                eventSystem.AddComponent<StandaloneInputModule>();
            }
            
            ConsoleSignals.OnInputEvent += OnInput;

            Hide();
        }
        
        public static void Register(string key, Action<string[]> command)
        {
            _commands.Add(key, command);
        }

        public static void Show()
        {
            ConsoleSignals.InvokeDisplay(true);
        }

        public static void Hide()
        {
            ConsoleSignals.InvokeDisplay(false);
        }

        public static void Log(string message)
        {
            ConsoleSignals.InvokeLog(message);   
        }
    
        public static void Log(string message, string color)
        {
            Log("<color=\"" + color + "\">"+ message +"</color>");
        }
    }
}
