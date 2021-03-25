using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Reflection;
using UnityEngine.EventSystems;

namespace CommandConsole
{
    public class Console
    {    
        private static Dictionary<string, Action<string[]>> _commands;
        private static string emptyKey = "empty_or_space";

        public void Initialize()
        {
            _commands = new Dictionary<string, Action<string[]>>();
            InitializeAttributes();
            
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


        private static void InitializeAttributes()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                var methods = assembly.GetTypes()
                        .SelectMany(t => t.GetMethods())
                        .Where(m => m.GetCustomAttributes(typeof(ConsoleCommandAttribute), false).Length > 0)
                        .ToArray();
                
                if (methods.Length > 0)
                {
                    foreach (var method in methods)
                    {
                        var target = method.DeclaringType;
                        object act = Activator.CreateInstance(target);
                        var action = (Action<string[]>) method.CreateDelegate(typeof(Action<string[]>), act);
                        
                        var attribute = method.GetCustomAttribute<ConsoleCommandAttribute>();
                        if (attribute != null)
                        {
                            if (string.IsNullOrEmpty(attribute.key) == false && 
                                string.IsNullOrWhiteSpace(attribute.key) == false)
                            {
                                _commands.Add(attribute.key, action);
                            }
                            else
                            {
                                _commands.Add(method.Name.ToLower(), action);   
                            }
                        }
                    }
                }
            }
        }

        private static void Execute(CommandInfo cmd)
        {
            if (cmd.key == emptyKey)
            {
                return;
            }

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
            if (string.IsNullOrEmpty(input) == false && string.IsNullOrWhiteSpace(input) == false)
            {
                var split = new ArraySegment<string>(input.Split(' '));
                return new CommandInfo()
                {
                    key     = split.ElementAt(0),
                    args    = split.Skip(1).ToArray<string>()
                };
            }
            else
            {
                return new CommandInfo()
                {
                    key = emptyKey
                };
            }
        }

        private static void OnInput(string input)
        {
            Execute(Parse(input));
        }


        public void Show()
        {
            ConsoleSignals.InvokeDisplay(true);
        }

        public void Hide()
        {
            ConsoleSignals.InvokeDisplay(false);
        }

        public static void Log(string message)
        {
            if (string.IsNullOrEmpty(message) == false && string.IsNullOrWhiteSpace(message) == false)
            {
                ConsoleSignals.InvokeLog(message);
            }
        }
    }
}
