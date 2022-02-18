using CommandConsole;
using UnityEngine;

public class ApplicationStartupTest : MonoBehaviour
{
    private void Start()
    {
        Console.Initialize();
        Console.Register("greet", Greet);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.DoubleQuote))
        {
            if(Console.IsActive) 
            {
                Console.Hide();
            }
            else
            {
                Console.Show();
            }
        }
    }

    private void Greet(string[] args)
    {
        if (args != null && args.Length >= 1)
        {
            var log = "Greetings, " + string.Join(" ", args);
            Console.Log(log, "green");
        }
        else
        {
            Console.Log("Err, missing argument!", "red");
        }
    }
}