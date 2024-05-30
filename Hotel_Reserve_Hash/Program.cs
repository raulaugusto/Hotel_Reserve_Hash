using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Reserve_Hash
{
    public class Program
    {
        public static void Main(string[] args)
        {
            HashMapReservas hashMap = null;
            string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            string relativePath = @"txt\teste.txt";
            string fullPath = Path.Combine(projectDirectory, relativePath);

            // Agora você pode usar fullPath para acessar o arquivo txt

            LerDados.CarregarDados(fullPath, ref hashMap);

            // Inserir uma nova reserva
            Reserva novaReserva1 = new Reserva("Hotel E", 505, new DateTime(2024, 7, 20), new DateTime(2024, 7, 25));
            bool sucesso = hashMap.Inserir(novaReserva1);
            Console.WriteLine(sucesso ? "Reserva inserida com sucesso." : "Conflito de datas, reserva não inserida.");

            // Inserir outra reserva que cause colisão
            Reserva novaReserva2 = new Reserva("Hotel E", 505, new DateTime(2024, 7, 22), new DateTime(2024, 7, 27));
            sucesso = hashMap.Inserir(novaReserva2);
            Console.WriteLine(sucesso ? "Reserva inserida com sucesso." : "Conflito de datas, reserva não inserida.");

            // Buscar uma reserva
            var reservasEncontradas = hashMap.Buscar("Hotel E", 505);
            foreach (var reserva in reservasEncontradas)
            {
                Console.WriteLine("Reserva encontrada: " + reserva);
            }

            // Remover uma reserva
            bool removido = hashMap.Remover("Hotel E", 505, new DateTime(2024, 7, 20));
            Console.WriteLine(removido ? "Reserva removida com sucesso." : "Reserva não encontrada.");
        }
    }

}
