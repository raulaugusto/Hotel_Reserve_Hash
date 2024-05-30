using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Reserve_Hash
{
    public class LerDados
    {

        public static void CarregarDados(string caminhoArquivo, ref HashMapReservas hashMap)
        {
            string[] linhas = File.ReadAllLines(caminhoArquivo);
            int tamanho = int.Parse(linhas[0]);
            hashMap = new HashMapReservas(tamanho);

            for (int i = 1; i < linhas.Length; i++)
            {
                string linha = linhas[i];
                string[] partes = linha.Split(':');
                string numeroReserva = partes[0];
                string[] dados = partes[1].Split(',');

                string nomeHotel = dados[0];
                int numeroQuarto = int.Parse(dados[1]);
                DateTime dataCheckIn = DateTime.Parse(dados[2]);
                DateTime dataCheckOut = DateTime.Parse(dados[3]);

                Reserva reserva = new Reserva(nomeHotel, numeroQuarto, dataCheckIn, dataCheckOut);
                bool sucesso = hashMap.Inserir(reserva);
                if (!sucesso)
                {
                    Console.WriteLine($"Conflito de reserva para {nomeHotel} quarto {numeroQuarto} nas datas {dataCheckIn.ToShortDateString()} a {dataCheckOut.ToShortDateString()}");
                }
            }
        }
    }

}

