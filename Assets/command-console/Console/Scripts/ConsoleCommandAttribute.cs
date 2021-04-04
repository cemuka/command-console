using System;

namespace CommandConsole
{
    [AttributeUsage(AttributeTargets.Method)]
    public class CommandAttribute : Attribute
    {
        public string key;

        public CommandAttribute()
        {
            
        }
        public CommandAttribute(string key)
        {
            this.key = key;
        }
    }
}