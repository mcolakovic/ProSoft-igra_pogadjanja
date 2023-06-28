using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Domain;
using System.Net.Sockets;

namespace KorisnickiInterfejs
{
    public class Communication
    {
        static Communication instance;
        Socket socket;
        CommunicationHelper helper;
        private Communication()
        {
        }
        public static Communication Instance
        {
            get
            {
                if (instance == null)
                    instance = new Communication();
                return instance;
            }
        }

        public void Connect()
        {
            try
            {
                if(socket == null || !socket.Connected)
                {
                    socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    socket.Connect("127.0.0.1", 9999);
                    helper = new CommunicationHelper(socket);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void CloseConnection()
        {
            try
            {
                Poruka poruka = new Poruka
                {
                    Operations = Operations.EndCommunication
                };
                helper.Send(poruka);
                socket.Shutdown(SocketShutdown.Both);
                socket.Dispose();
                socket = null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void SendMessage<T>(T m) where T : class
        {
            try
            {
                helper.Send<T>(m);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public T ReadMessage<T>() where T : class
        {
            try
            {
                return helper.Receive<T>();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
