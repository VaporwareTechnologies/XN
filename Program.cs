using System;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Beta_1
{
    class Program
    {
        static bool ext = false;
        private static string navigated;

        static void Main()
        {
            Console.WriteLine(@" ¬--¬      ¬--¬    ¬-----¬      ¬--¬                             ");
            Console.WriteLine(@"  \  \     /  /    |      \     |  |           Vaporware Terminal");
            Console.WriteLine(@"   \  \   /  /     |   \   \    |  |           v0.5              ");
            Console.WriteLine(@"    \  \_/  /      |   |\   \   |  |                             ");
            Console.WriteLine(@"     |  _  /       |   | \   \  |  |                             ");
            Console.WriteLine(@"    /  / \  \      |   |  \   \ |  |                             ");
            Console.WriteLine(@"   /  /   \  \     |   |   \   \|  |                             ");
            Console.WriteLine(@"  /  /     \  \    |   |    \   |  |                             ");
            Console.WriteLine(@"[___/       \___]  |___|     \_____|                             ");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            while (ext==false)
            {
                Console.Write("$ ");
                Commands();
            }
        }

        private static void Commands()
        {
            string arg = Console.ReadLine().ToLower();

            switch (arg)
            {
                case "help":
                    help();
                    break;

                case "help exit":
                    helpExit();
                    break;

                case "help ping":
                    helpPing();
                    break;

                case "exit":
                    exit();
                    break;

                case "exit y":
                    Environment.Exit(0);
                    break;

                case "exit r":
                    Process.Start("shutdown", "/r");
                    break;

                case "exit s":
		            exits();
                    break;

                case "clear":
                    Console.Clear();
                    Console.Write("$ ");
                    Commands();
                    break;

                case "xn":
                    Console.Clear();
                    Main();
                    break;

                default:
                    string[] tokens = arg.Split();

                    try
                    {
                        if (tokens[0] == "ping" && (tokens[2] == "true" || tokens[2] == "false"))
                        {
                            try
                            {
                                if (tokens[2] == "false")
                                {
                                    ping(tokens[1], false);
                                }
                                else if (tokens[2] == "true")
                                {
                                    ping(tokens[1], true);
                                }
                            }
                            catch (IndexOutOfRangeException)
                            {
                                Console.WriteLine("Please use \"ping <url>\" format.\nFor example -->     ping google.com");
                            }
                        }

                        else if (tokens[0] == "ping")
                        {
                            try
                            {
                                ping(tokens[1]);
                            }
                            catch (IndexOutOfRangeException)
                            {
                                Console.WriteLine("Please use \"ping <url>\" format.\nFor example -->     ping google.com");
                            }
                        }

                        else if (tokens[0] == "search")
                        {
                            try
                            {
                                new Program().search(tokens[1]);
                            }
                            catch (IndexOutOfRangeException)
                            {
                                Console.WriteLine("Please use \"search <term>\" format.\nFor example -->     search america");
                            }
                        }

                        else if (arg.Trim() == "")
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine(arg + ": command not recognized");
                        }
                    }
                    catch (Exception)
                    {
                        Console.WriteLine(arg + ": bad comand format. Please use help <command> to read about its sintax");
                    }
                    
                    break;
            }

            Console.WriteLine();
        }

        private static void exit()
        {
            Console.Write("Exit current session? <y/n> ");

            switch (Console.ReadLine().ToLower())
            {
                case "y":
                    ext = true;
                    break;

                default:
                    break;
            }
        }

        private static void help()
        {
            Console.WriteLine("Please use help <command> to have a complete explanation about it and its extensions\n");
            Console.WriteLine("help              --Shows a list with all commands, explained");
            Console.WriteLine("exit              --Exits current session");
            Console.WriteLine("clear             --Clears the terminal");
            Console.WriteLine("xn                --Restarts XN Terminal");
            Console.WriteLine("ping <url>        --Pings at a specified URL");
            Console.WriteLine("search <term>     --Performs a search on Internet for given term");
        }

        private static void ping(string input)
        {
            //Ping pingClass = new Ping();
            //byte[] packet = new byte[500];
            try
            {
                Ping pingSender = new Ping();

                // Create a buffer of 32 bytes of data to be transmitted.
                string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
                byte[] buffer = Encoding.ASCII.GetBytes(data);

                // Wait 1 second for a reply.
                int timeout = 1000;

                // Set options for transmission:
                // The data can go through 64 gateways or routers
                // before it is destroyed, and the data packet
                // cannot be fragmented.
                PingOptions options = new PingOptions(64, true);

                // Send the request.
                PingReply reply = pingSender.Send(input, timeout, buffer, options);

                if (reply.Status == IPStatus.Success)
                {
                    Console.WriteLine("Pinging  " + reply.Address.ToString() + "  with  " + reply.Buffer.Length + 
                        "  bytes of data.\nTotal time: " + reply.RoundtripTime + "   TTL: " + reply.Options.Ttl + 
                        "   Don't fragment option: " + reply.Options.DontFragment);
                }
                else
                {
                    Console.WriteLine("Error: " + reply.Status);
                }
            }
            catch (PingException)
            {
                // In case of a wrong URL input, a unexisting webpage 
                // or no Internet connection.
                Console.WriteLine("The specified URL appears to be not a valid one. It may not exist, be temporarily down or " +
                    "your computer may not be connected to the Internet.\nTry to use something like -->     ping google.com");
            }
        }

        private static void ping(string input, bool dontfragment)
        {
            //Ping pingClass = new Ping();
            //byte[] packet = new byte[500];
            try
            {
                Ping pingSender = new Ping();

                // Create a buffer of 32 bytes of data to be transmitted.
                string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
                byte[] buffer = Encoding.ASCII.GetBytes(data);

                // Wait 1 second for a reply.
                int timeout = 1000;

                // Set options for transmission:
                // The data can go through 64 gateways or routers
                // before it is destroyed, and the data packet
                // cannot be fragmented.
                PingOptions options = new PingOptions(64, dontfragment);

                // Send the request.
                PingReply reply = pingSender.Send(input, timeout, buffer, options);

                if (reply.Status == IPStatus.Success)
                {
                    Console.WriteLine("Pinging  " + reply.Address.ToString() + "  with  " + reply.Buffer.Length +
                        "  bytes of data.\nTotal time: " + reply.RoundtripTime + "   TTL: " + reply.Options.Ttl +
                        "   Don't fragment option: " + dontfragment);
                }
                else
                {
                    Console.WriteLine("Error: " + reply.Status);
                }
            }
            catch (PingException)
            {
                // In case of a wrong URL input, a unexisting webpage 
                // or no Internet connection.
                Console.WriteLine("The specified URL appears to be not a valid one. It may not exist, be temporarily down or " +
                    "your computer may not be connected to the Internet.\nTry to use something like -->     ping google.com");
            }
        }

        private void search(string phrase)
        {
            Console.WriteLine("Loading search... This can take from a few secons to almost one minute.");

            Thread thread = new Thread(new ParameterizedThreadStart(extractText));
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start(phrase);
            thread.Join();
            Console.WriteLine();
            Console.WriteLine(navigated);
            Console.WriteLine();

        }

        private void extractText(object phrase)
        {
            // Initializes new variables and
            // new WebBrowser for searching.
            string uri = "";
            WebBrowser _webBrowser = new WebBrowser();

            // Searches in BBC News, using Google,
            // and waits until all it's loaded.
            _webBrowser.Url = new Uri(string.Format(@"http://www.google.com/search?as_q={0}&as_sitesearch=www.bbc.co.uk/news", phrase));
            while (_webBrowser.ReadyState != WebBrowserReadyState.Complete) Application.DoEvents();

            foreach (HtmlElement a in _webBrowser.Document.GetElementsByTagName("A"))
            {
                uri = a.GetAttribute("href");
                if (uri.StartsWith("http://www.bbc.co.uk/news")) break;
            }

            StringBuilder sb = new StringBuilder();
            WebBrowser webBrowser = new WebBrowser();
            webBrowser.Url = new Uri(uri);
            while (webBrowser.ReadyState != WebBrowserReadyState.Complete) Application.DoEvents();

            // Pick out the main heading.
            foreach (HtmlElement h1 in webBrowser.Document.GetElementsByTagName("H1"))
                sb.Append(h1.InnerText + ". ");

            // Select only the article text, ignoring everything else.
            foreach (HtmlElement div in webBrowser.Document.GetElementsByTagName("DIV"))
                if (div.GetAttribute("classname") == "story-body")
                    foreach (HtmlElement p in div.GetElementsByTagName("P"))
                    {
                        string classname = p.GetAttribute("classname");
                        if (classname == "introduction" || classname == "")
                            sb.Append(p.InnerText + " ");
                    }

            webBrowser.Dispose();
            navigated = sb.ToString();
        }

        private static void helpExit()
        {
            Console.WriteLine("\"exit\" command v0.9\nUse this command for close all processes and exit your current " +
                "session on XN. After pressing enter, it will ask for confirmation to close. Answer with y/n (Yes/No).\n" +
                "EXTENSIONS:     exit y   --> leaves inmediatly, without confirmation\n" +
                "                exit r   --> leaves inmediatly and restarts your computer\n" +
                "                exit s   --> leaves inmediatly and shuts down your computer");
        }

        private static void helpPing()
        {
            Console.WriteLine("\"ping\" command v0.8\nUse this command for pinging any Internet adress. This can be " +
                "useful for knowing if you are really connected to the Internet, which broadband speed you have, etc. " +
                "It must be used in the \"ping google.com\" example, and after that, it will print all the details of " +
                "your pinging: the IP adress of pinged server, the time it has taken to ping it, TTL (the amount of time " +
                "given to ping the server (default is 64ms), and if sent packets can be fragmented or not (default is false).\n" +
                "EXTENSIONS:     ping <url> <false>   --> sent packets can't be fragmented. This is the default option\n" +
                "                ping <url> <true>    --> sent packets can fragment");
        }

        private static void exits()
        {
            var psi = new ProcessStartInfo("shutdown", "/s /t 0");

            // Avoids creating new window.
            psi.CreateNoWindow = true;
            psi.UseShellExecute = false;

            Process.Start(psi);
        }
    }
}
