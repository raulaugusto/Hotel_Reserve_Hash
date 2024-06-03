using System;
using System.Collections.Generic;
using System.IO;

namespace Hotel_Reserve_Hash
{
    public class Program
    {
        public static void Main(string[] args)
        {
            HashMapReservas hashMap = null;
            string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            string relativePath = @"txt\reservas.txt";
            string fullPath = Path.Combine(projectDirectory, relativePath);

            LerDados.CarregarDados(fullPath, ref hashMap);

            string opc = "1";
            ExibirMenu(hashMap, opc);
        }

        public static void ExibirMenu(HashMapReservas hashMap, string opc)
        {
            while (opc != "0" && opc != null)
            {
                switch (opc)
                {
                    case "1":
                        Console.Clear();
                        hashMap.ExibirTabela();
                        break;
                    case "2":
                        Reserva res = ColetarDadosParaInsercao();
                        if (hashMap.Inserir(res))
                        {
                            Console.WriteLine("Reserva inserida com sucesso.");
                        }
                        else
                        {
                            Console.WriteLine("Conflito de datas, reserva não inserida.");
                        }
                        break;
                    case "3":
                        ColetarDadosParaBusca(hashMap);
                        break;
                    case "4":
                        ColetarDadosParaRemocao(hashMap);
                        break;
                    default:
                        Console.WriteLine("Opção inválida.");
                        break;
                }
                Console.WriteLine("\nEscolha uma opção: \n" +
                    "0: Sair \n" +
                    "1: Exibir Tabela \n" +
                    "2: Inserir Reserva \n" +
                    "3: Buscar Reserva \n" +
                    "4: Remover Reserva \n");
                opc = Console.ReadLine();
            }
        }

        public static Reserva ColetarDadosParaInsercao()
        {
            Console.Clear();
            Console.WriteLine("Digite o nome do hotel:");
            string nome = Console.ReadLine();
            Console.WriteLine("Digite o número do quarto:");
            int num = int.Parse(Console.ReadLine());
            Console.WriteLine("Digite a data de check-in (AAAA-MM-DD):");
            DateTime checkIn = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Digite a data de check-out (AAAA-MM-DD):");
            DateTime checkOut = DateTime.Parse(Console.ReadLine());

            return new Reserva(nome, num, checkIn, checkOut);
        }

        public static void ColetarDadosParaBusca(HashMapReservas hashMap)
        {
            Console.Clear();
            Console.WriteLine("Digite o nome do hotel:");
            string nome = Console.ReadLine();
            Console.WriteLine("Digite o número do quarto:");
            int num = int.Parse(Console.ReadLine());

            var reservas = hashMap.Buscar(nome, num);
            if (reservas.Count == 0)
            {
                Console.WriteLine("Nenhuma reserva encontrada.");
            }
            else
            {
                Console.WriteLine("Reservas encontradas:");
                foreach (var reserva in reservas)
                {
                    Console.WriteLine(reserva);
                }
            }
        }

        public static void ColetarDadosParaRemocao(HashMapReservas hashMap)
        {
            Console.Clear();
            Console.WriteLine("Digite o nome do hotel:");
            string nome = Console.ReadLine();
            Console.WriteLine("Digite o número do quarto:");
            int num = int.Parse(Console.ReadLine());
            Console.WriteLine("Digite a data de check-in (AAAA-MM-DD) da reserva a ser removida:");
            DateTime checkIn = DateTime.Parse(Console.ReadLine());

            if (hashMap.Remover(nome, num, checkIn))
            {
                Console.WriteLine("Reserva removida com sucesso. Reserva mais próxima assumiu a posição.");
            }
            else
            {
                Console.WriteLine("Reserva não encontrada.");
            }
        }
    }
}