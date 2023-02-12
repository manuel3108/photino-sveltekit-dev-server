using System.Drawing;
using PhotinoNET;
using PhotinoNET.Server;

namespace PhotinoSveltekitDevServer;

class Program
{
    private const bool UseDevServerIfDebug = true;

    [STAThread]
    public static void Main(string[] args)
    {
        string windowTitle;
        var useDevServer = false;
        var isDebug = false;
        var devServer = new DevServer();
        var baseUrl = string.Empty;

#if DEBUG
        // mark a flag, that we are currently in debug mode.
        isDebug = true;
#endif

        if (isDebug && UseDevServerIfDebug)
        {
            // if we are in debug mode, and the user wants to start the dev server, save a flag
            useDevServer = true;
        }

        if (useDevServer)
        {
            windowTitle = "My Application (Debug)";
            var devServerTask = devServer.Start();

            // wait until we were able to read the dev-server url from the stdout of the npm run
            // so that we know were Photino should navigate.
            devServer.WaitUntilReady();
        }
        else
        {
            windowTitle = "My Application (Release)";

            // in release mode, we will need to create static file server, so that we can serve the static file different then index.html
            PhotinoServer
                .CreateStaticFileServer(args, out baseUrl)
                .RunAsync();
        }

        var window = new PhotinoWindow()
            .SetTitle(windowTitle)
            .SetUseOsDefaultSize(false)
            .SetSize(new Size(600, 400))
            .Center()
            .SetTopMost(true)
            .SetIconFile("wwwroot/favicon.ico")
            .SetResizable(true)
            .RegisterWebMessageReceivedHandler((object sender, string message) =>
            {
                var window = (PhotinoWindow)sender;
                string response = $"Received message: \"{message}\"";
                window.SendWebMessage(response);
            });

        if (useDevServer)
        {
            window.Load(devServer.GetUrl());
        }
        else
        {
            window.Load(baseUrl + "/index.html");
        }

        window.WaitForClose(); // Starts the application event loop

        if (useDevServer)
        {
            // if the dev server was used, make sure to terminate it correctly, so 
            // that the used ports etc are freed
            devServer.Stop();
        }

        Console.WriteLine("Closing");
    }
}
