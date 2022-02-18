using System;

namespace CommandConsole.Signals
{
    public static class ConsoleSignals
    {
        public static event Action<bool>    OnDisplayEvent;
        public static event Action<string>  OnInputEvent;
        public static event Action<string>  OnLogEvent;
        public static event Func<bool>      OnCheckConsoleActiveEvent;

        public static void InvokeDisplay(bool enabled)
        {
            OnDisplayEvent?.Invoke(enabled);
        }
        public static void InvokeOnInput(string input)
        {
            OnInputEvent?.Invoke(input);
        }
        public static void InvokeLog(string message)
        {
            OnLogEvent?.Invoke(message);
        }

        public static bool InvokeCheckConsoleActive()
        {
            return OnCheckConsoleActiveEvent.Invoke();
        }
    }
}