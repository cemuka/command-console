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
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Tab))
        {
            Console.Show();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Console.Hide();
        }
    }

    private void Greet(string[] args)
    {
        if (args != null && args.Length >= 1)
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