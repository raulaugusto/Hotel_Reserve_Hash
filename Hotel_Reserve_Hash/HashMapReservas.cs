using Hotel_Reserve_Hash;
using System;
using System.Collections.Generic;

public class HashMapReservas
{
    private int tamanho;
    private LinkedList<Reserva>[] tabela;

    public HashMapReservas(int tamanho)
    {
        this.tamanho = tamanho;
        tabela = new LinkedList<Reserva>[tamanho];
        for (int i = 0; i < tamanho; i++)
        {
            tabela[i] = new LinkedList<Reserva>();
        }
    }

    private int FuncaoHash(string nomeHotel, int numeroQuarto)
    {
        int hash = (nomeHotel.GetHashCode() * 31 + numeroQuarto) % tamanho;
        int indice = Math.Abs(hash);

        while (tabela[indice].First != null && tabela[indice].First.Value.NomeHotel != nomeHotel)
        {
            indice = (indice + 1) % tamanho;
        }

        return indice;
    }

    public bool Inserir(Reserva reserva)
    {
        int indice = FuncaoHash(reserva.NomeHotel, reserva.NumeroQuarto);
        foreach (var res in tabela[indice])
        {
            if (res.NomeHotel == reserva.NomeHotel && res.NumeroQuarto == reserva.NumeroQuarto)
            {
                if (!(reserva.DataCheckOut <= res.DataCheckIn || reserva.DataCheckIn >= res.DataCheckOut))
                {
                    Console.WriteLine($"Conflito de datas: {reserva.NomeHotel} quarto {reserva.NumeroQuarto} nas datas {reserva.DataCheckIn.ToShortDateString()} a {reserva.DataCheckOut.ToShortDateString()}");
                    break;
                }
            }
        }
        tabela[indice].AddLast(reserva);
        return true;
    }

    public List<Reserva> Buscar(string nomeHotel, int numeroQuarto)
    {
        int indice = FuncaoHash(nomeHotel, numeroQuarto);
        List<Reserva> reservasEncontradas = new List<Reserva>();
        foreach (var reserva in tabela[indice])
        {
            if (reserva.NomeHotel == nomeHotel && reserva.NumeroQuarto == numeroQuarto)
            {
                reservasEncontradas.Add(reserva);
            }
        }
        return reservasEncontradas;
    }

    public bool Remover(string nomeHotel, int numeroQuarto, DateTime dataCheckIn)
    {
        int indice = FuncaoHash(nomeHotel, numeroQuarto);
        LinkedListNode<Reserva> no = tabela[indice].First;
        Reserva reservaParaRemover = null;

        while (no != null)
        {
            if (no.Value.NomeHotel == nomeHotel && no.Value.NumeroQuarto == numeroQuarto && no.Value.DataCheckIn == dataCheckIn)
            {
                reservaParaRemover = no.Value;
                tabela[indice].Remove(no);
                break;
            }
            no = no.Next;
        }

        if (reservaParaRemover != null)
        {
            PromoverReservaMaisProxima(indice);
            return true;
        }

        return false;
    }


    public void ExibirTabela()
    {
        for (int i = 0; i < tamanho; i++)
        {
            Console.WriteLine($"Índice {i}:");
            foreach (var reserva in tabela[i])
            {
                Console.WriteLine($"  {reserva}");
            }
        }
    }

    private void PromoverReservaMaisProxima(int indice)
    {
        if (tabela[indice].Count > 0)
        {
            var reservasOrdenadas = new SortedList<DateTime, Reserva>();
            foreach (var reserva in tabela[indice])
            {
                reservasOrdenadas.Add(reserva.DataCheckIn, reserva);
            }

            tabela[indice].Clear();
            foreach (var reserva in reservasOrdenadas.Values)
            {
                tabela[indice].AddLast(reserva);
            }
        }
    }

}

