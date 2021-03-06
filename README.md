![command-console](./images/console.gif)
### Command Console
Inspired by Quantum Console from unity asset store. It looks pretty awesome.


#### Usage
Simply log any string

```csharp
Console.Log("This is the way");
Console.Log("Also with text support for colors", "green");
```

Just call `Initialize` to setup. It will create the command view from `Resources`.
Add your commands with `Register`.

```csharp
private void Start()
{
    Console.Initialize();
    Console.Register("greet", Greet);
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
        Console.Log("Err, missing argument!");
    }
}
```

Easily show and hide.

```csharp
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
```

### LICENSE
MIT
Cem Ugur Karacam