using System;
using System.ServiceModel;
using Lab4;

namespace Lab4Server
{
    class ClientConnection
    {
        private int id;

        public ClientConnection(int id)
        {
            this.id = id;
        }

        public void Update()
        {
            using (ServiceHost host = new ServiceHost(typeof(PathControllerActions), new Uri("net.pipe://localhost")))
            {
                NetNamedPipeBinding netNamedPipeBinding = new NetNamedPipeBinding();

                host.AddServiceEndpoint(typeof(IPathControllerActions), new NetNamedPipeBinding(), "pathController/" + id.ToString());
                host.Open();
                Console.WriteLine("Press any key to exit.");
                Console.ReadKey();
                host.Close();
            }
        }
    }
}
