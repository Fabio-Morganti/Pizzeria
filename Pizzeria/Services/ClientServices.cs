using System;
using Pizzeria.Models;
using static Pizzeria.Constants.Constants;

namespace Pizzeria.Services
{
	public class ClientServices
	{
        public ClientServices()
		{
        }

        //Recupero il cliente corrispondente all'id della Fedelity Card
        public static Client GetClient(string idFedelity)
        {
            if(!string.IsNullOrEmpty(idFedelity))
            {
                List<Client> Clients = DatabaseServices.GetListClient()
                            .Where(client => client.FedelityId == idFedelity)
                            .ToList();
                if(Clients.Count() > 0)
                {
                    return Clients.First();
                }
            }

            return null;
        }

        //applico uno sconto al prezzo della pizza e restituisco il prezzo finale
        private static double ApplyDiscount(int discount, double price)
        {
            if(discount != 0)
            {
                double newPrice = price - price * discount / 100;
                if (newPrice < 5) newPrice = 5;
                return Math.Round(newPrice, 2);
            }

            return price;
        }

        //Restituisco il prezzo finale in base al tipo di sconto da applicare
        public static Tuple<TypeDiscount, double> GetPrice(string fedelity, bool disable, int eta, int group, double price)
        {
            if (disable)
            {
                return new (TypeDiscount.Disability, ApplyDiscount(90, price));
            }
            else if (eta >= 60)
            {
                return new (TypeDiscount.Age60, ApplyDiscount(70, price));
            }
            else if(group != -1 && group != 0)
            {
                switch (group)
                {
                    case 1:
                        return new(TypeDiscount.Group1, ApplyDiscount(20, price));

                    case 2:
                        return new(TypeDiscount.Group2, ApplyDiscount(30, price));

                    case 3:
                        return new(TypeDiscount.Group3, ApplyDiscount(50, price));
                }
            }
            else if (eta < 4 && eta != 0)
            {
                return new(TypeDiscount.Age4, ApplyDiscount(50, price));
            }
            else if (eta < 12 && eta != 0)
            {
                return new(TypeDiscount.Age12, ApplyDiscount(20, price));
            }
            else if (GetClient(fedelity) != null)
            {
                return new(TypeDiscount.Fedelity, ApplyDiscount(15, price));
            }
            else if(DateTime.Now.DayOfWeek == DayOfWeek.Saturday || DateTime.Now.DayOfWeek == DayOfWeek.Sunday || DateTime.Now >= DateTime.Today.AddHours(20))
            {
                return new(TypeDiscount.WeekEnd, ApplyDiscount(10, price));
            }

            return new(TypeDiscount.NoDiscount, price);
        }
    }
}

