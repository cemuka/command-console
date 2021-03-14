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
    public string Greet(string[] args)
    {
        return "Greetings, " + args[0];
    }
}