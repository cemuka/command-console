using System;

namespace CommandConsole
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ConsoleCommandAttribute : Attribute
    {
        public string key;

        public ConsoleCommandAttribute()
        {
            
        }
        public ConsoleCommandAttribute(string key)
        {
            this.key = key;
        }
    }
}