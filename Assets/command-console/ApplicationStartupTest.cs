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

    [Command("welcome")]
    public void Greet(string[] args)
    {
        if (args != null && args.Length > 1)
        {
            var log = "Greetings, " + string.Join(" ", args);
            Console.Log(log);
        }
        else
        {
            Console.Log("Err, missing argument!");
        }
    }
}