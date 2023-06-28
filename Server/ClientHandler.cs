using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Common;
using Domain;

namespace Server
{
    public class ClientHandler
    {
        public event EventHandler OdjavljenKlijent;
        Socket socket;
        List<ClientHandler> clients;
        CommunicationHelper helper;
        public User User { get; set; }
        List<Igra> lista = new List<Igra>();
        int brojPokusaja = 0;
        int brojPogodaka = 0;

        public ClientHandler(Socket socket, List<ClientHandler> clients)
        {
            this.socket = socket;
            this.clients = clients;
            helper = new CommunicationHelper(socket);
        }

        internal void Stop()
        {
            if (socket != null)
            {
                socket.Shutdown(SocketShutdown.Both);
                socket.Dispose();
                socket = null;
            }
            OdjavljenKlijent?.Invoke(this, EventArgs.Empty);
        }

        internal void HandleRequests()
        {
            try
            {
                Poruka poruka;
                while ((poruka = helper.Receive<Poruka>()).Operations != Operations.EndCommunication)
                {
                    try
                    {
                        poruka = CreateResponse(poruka);
                    }
                    catch (Exception ex)
                    {
                        poruka = new Poruka
                        {
                            isSuccessful = false,
                            MessageText = ex.Message
                        };
                    }
                    helper.Send(poruka);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            finally
            {
                Stop();
            }
        }

        private Poruka CreateResponse(Poruka poruka)
        {
            switch (poruka.Operations)
            {
                case Operations.Login:
                    poruka = Login((User)poruka.PorukaObject);
                    break;
                case Operations.Igra:
                    poruka = VratiIgru((Igra)poruka.PorukaObject);
                    brojPokusaja++;
                    if (((Igra)poruka.PorukaObject).Vrijednost == "*")
                        brojPogodaka++;
                    if (brojPogodaka == 3)
                        poruka.MessageText = "Pobjeda";
                    else if (brojPokusaja == User.Matrica.BrojPokusaja)
                        poruka.MessageText = "Poraz";
                    break;
            }
            return poruka;
        }

        private Poruka VratiIgru(Igra porukaObject)
        {
            return new Poruka
            {
                isSuccessful = true,
                Operations = Operations.Igra,
                PorukaObject = new Igra
                {
                    xDimension = porukaObject.xDimension,
                    yDimension = porukaObject.yDimension,
                    Vrijednost = lista.SingleOrDefault(x => x.xDimension == porukaObject.xDimension && x.yDimension == porukaObject.yDimension).Vrijednost
                }
            };
        }

        private Poruka Login(User porukaObject)
        {
            bool postoji = false;
            foreach (ClientHandler client in clients)
            {
                if(client.User != null)
                {
                    if(client.User.Email == porukaObject.Email)
                    {
                        postoji = true;
                        break;
                    }
                }
            }
            Poruka poruka = new Poruka();
            if (postoji)
            {
                poruka.isSuccessful = false;
                poruka.MessageText = "Korisnicko ime vec postoji.";
            }
            else
            {
                this.User = porukaObject;
                poruka.isSuccessful = true;
                poruka.PorukaObject = porukaObject;
                GenerisiBrojeve(User.Matrica);
            }
            return poruka;
        }

        private void GenerisiBrojeve(Matrica matrica)
        {
            for (int i = 0; i <= matrica.xDimension ; i++)
            {
                for (int j = 0; j <= matrica.yDimension ; j++)
                {
                    lista.Add(new Igra
                    {
                        xDimension = i,
                        yDimension = j,
                        Vrijednost = "-"
                    });
                }
            }
            Random random = new Random();
            int brojac = 0;
            while (true)
            {
                int index = random.Next(lista.Count);
                if(lista[index].Vrijednost != "*")
                {
                    lista[index].Vrijednost = "*";
                    brojac++;
                }
                if (brojac == 3)
                    break;
            }
        }
    }
}
