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
        int hash = 0;
        string chave = nomeHotel + numeroQuarto.ToString();
        foreach (char c in chave)
        {
            hash = (hash * 31 + c) % tamanho;
        }
        return hash;
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
        var no = tabela[indice].First;
        while (no != null)
        {
            if (no.Value.NomeHotel == nomeHotel && no.Value.NumeroQuarto == numeroQuarto && no.Value.DataCheckIn == dataCheckIn)
            {
                tabela[indice].Remove(no);
                return true;
            }
            no = no.Next;
        }
        return false;
    }
}
