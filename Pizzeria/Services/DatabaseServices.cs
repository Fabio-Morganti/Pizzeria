using System;
using Pizzeria.Models;

namespace Pizzeria.Services
{
	public class DatabaseServices
	{
		public DatabaseServices()
		{
		}

        //Simulo un'interrogazione al DB dove recupero la lista delle pizze
        public static List<Pizza> LoadPizzeFromDB()
        {
            List<Pizza> listPizze = new List<Pizza>();

            Pizza margherita = new Pizza("Margherita", 15.5);
            Pizza marinara = new Pizza("Marinara", 10.5);
            Pizza capricciosa = new Pizza("Capricciosa", 20);
            Pizza quattroformaggi = new Pizza("Quattro formaggi", 25.7);
            Pizza boscaiola = new Pizza("Boscaiola", 8);

            listPizze.Add(margherita);
            listPizze.Add(marinara);
            listPizze.Add(capricciosa);
            listPizze.Add(quattroformaggi);
            listPizze.Add(boscaiola);

            return listPizze;
        }

        //Simulo un'interrogazione al DB dove recupero la lista dei clienti
        public static List<Client> GetListClient()
        {
            List<Client> clients = new List<Client>();

            Client client1 = new Client("Mario", "123", false, 1992);
            Client client2 = new Client("Franco", "124", true, 2020);
            Client client3 = new Client("Mario", "125", false, 1960);

            clients.Add(client1);
            clients.Add(client2);
            clients.Add(client3);

            return clients;
        }
    }
}

