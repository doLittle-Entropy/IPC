using System;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;



namespace NetCorePipes
{

    class Program
    {

        static void Main(string[] args)
        {
            try
            {
                if( File.Exists("/tmp/dolittle.sock"))
                    File.Delete("/tmp/dolittle.sock");
                Server.Start();
            }
            finally
            {
                File.Delete("/tmp/dolittle.sock");

            }

            //Task.Delay(1000).Wait();
            //Client.Start();


            //StartServer();
            //Task.Delay(1000).Wait();

            // http://stackoverflow.com/questions/40195290/how-to-connect-to-a-unix-domain-socket-in-net-core-in-c-sharp

            // https://gist.github.com/leandrosilva/656054            

            //new UnixDomainSocketEndPoint("");




            //Client
            /*/
            var client = new NamedPipeClientStream("dolittle");
            client.Connect();
            StreamReader reader = new StreamReader(client);
            StreamWriter writer = new StreamWriter(client);

            while (true)
            {
                string input = Console.ReadLine();
                if (String.IsNullOrEmpty(input)) break;
                writer.WriteLine(input);
                writer.Flush();
                Console.WriteLine(reader.ReadLine());
            }*/
        }

        static void StartServer()
        {
            Task.Factory.StartNew(() =>
            {
                var server = new NamedPipeServerStream("dolittle");
                server.WaitForConnection();
                StreamReader reader = new StreamReader(server);
                StreamWriter writer = new StreamWriter(server);
                while (true)
                {
                    var line = reader.ReadLine();
                    writer.WriteLine(String.Join("", line.Reverse()));
                    writer.Flush();
                }
            });
        }
    }
}
