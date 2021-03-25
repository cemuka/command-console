using CommandConsole;
using UnityEngine;

public class ApplicationStartupTest : MonoBehaviour
{
    private Console _console;

    private void Start()
    {
        _console = new Console();
        _console.Initialize();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Tab))
        {
            _console.Show();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _console.Hide();
        }
    }

    [ConsoleCommand("welcome")]
    public void Greet(string[] args)
    {
        var log = "Greetings, " + args[0];
        Console.Log(log);
    }
}