using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Reserve_Hash
{
    public class Reserva
    {
        public string NomeHotel { get; set; }
        public int NumeroQuarto { get; set; }
        public DateTime DataCheckIn { get; set; }
        public DateTime DataCheckOut { get; set; }

        public Reserva(string nomeHotel, int numeroQuarto, DateTime dataCheckIn, DateTime dataCheckOut)
        {
            NomeHotel = nomeHotel;
            NumeroQuarto = numeroQuarto;
            DataCheckIn = dataCheckIn;
            DataCheckOut = dataCheckOut;
        }

        public override string ToString()
        {
            return $"{NomeHotel}, Quarto: {NumeroQuarto}, Check-In: {DataCheckIn.ToShortDateString()}, Check-Out: {DataCheckOut.ToShortDateString()}";
        }
    }

}
