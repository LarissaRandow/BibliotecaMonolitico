﻿using System;
using Console.Data;
using Microsoft.EntityFrameworkCore;
using static System.Console;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace Console
{
    class Program
    {
        private static bool bibliotecario = false;
        private static AppDbContext context = new AppDbContext();

        static void Main(string[] args)
        {
            bool showMenu = true;
            Task<bool> task;
            while (showMenu)
            {
                task = MainMenuAsync();
                task.Wait();
                showMenu = task.Result;
            }

        }

        private static async Task<bool> MainMenuAsync()
        {
            //Console.Clear();
            if (!bibliotecario)
            {
                WriteLine("\r\nChoose an option:");
                WriteLine("1) Exibir Todos Os Livros");
                WriteLine("2) Pesquisar Por Gênero");
                WriteLine("3) Pesquisar Por Nome");
                WriteLine("4) Reservar Um Livro");
                WriteLine("5) Calcular Multa");
                WriteLine("6) Realizar Login");


                Write("\r\nSelect an option: ");

                switch (ReadLine())
                {
                    case "1":
                        await TodosLivrosAsync();
                        return true;
                    case "2":
                        await PesquisarLivroGenero();
                        return true;
                    case "3":
                        await PesquisarLivroNome();
                        return true;
                    case "4":
                       await CadastraReservaAsync();
                        return true;
                    case "5":
                        await CalcularMulta();
                        return true;
                    case "6":
                        //await Login();
                        bibliotecario = true;
                        return true;
                    default:
                        return true;
                }
            }
            else
            {
                WriteLine("\r\nChoose an option:");
                WriteLine("1) Cadastrar Gênero");
                WriteLine("2) Pesquisar Gêneros");
                WriteLine("3) Atualizar Gênero");
                WriteLine("\r");
                WriteLine("4) Cadastrar Livros");
                WriteLine("5) Atualizar Livro");
                WriteLine("6) Deletar Livro");
                WriteLine("\r");
                WriteLine("7) Exibir Todos Os Livros");
                WriteLine("8) Pesquisar Por Gênero");
                WriteLine("9) Pesquisar Por Nome");
                WriteLine("\r");
                WriteLine("10) Pesquisar Reservas");
                WriteLine("11) Atualizar Reserva");
                WriteLine("12) Deletar Reserva");
                WriteLine("\r");
                WriteLine("13) Calcular Multa");

                Write("\r\nSelect an option: ");

                switch (ReadLine())
                {
                    case "1":
                        await CadastraGeneroAsync();
                        return true;
                    case "2":
                        await TodosGenerosAsync();
                        return true;
                    case "3":
                        //await AtualizarGeneroAsync();
                        return true;
                    case "4":
                        await CadastraLivroAsync();
                        return true;
                    case "5":
                        //await AtualizarLivroAsync();
                        return true;
                    case "6":
                        await DeletarLivroAsync();
                        return true;
                    case "7":
                        await TodosLivrosAsync();
                        return true;
                    case "8":
                        await PesquisarLivroGenero();
                        return true;
                    case "9":
                        await PesquisarLivroNome();
                        return true;
                    case "10":
                        await TodosReservasAsync();
                        return true;
                    case "11":
                        //await AtualizarReservaAsync();
                        return true;
                    case "12":
                        await DeletarReservaAsync();
                        return true;
                    case "13":
                        await CalcularMulta();
                        return true;
                    default:
                        return true;
                }
            }

        }
        
        private static async Task TodosLivrosAsync()
        {
            
            var livroTask = context.Livros.ToListAsync();

            WriteLine("\r");
            WriteLine("--------Lista----------");
            await livroTask.ContinueWith(task =>
            {
                var livros = task.Result;
                foreach (var p in livros)
                    WriteLine(p.ToString());
            },
            TaskContinuationOptions.OnlyOnRanToCompletion
            );
            WriteLine("-----------------------");

        }

        private static async Task PesquisarLivroGenero()
        {
            Write("\r\nGenero Id: ");
            int generoId = Convert.ToInt32(ReadLine());

            var livroTask = context.Livros.Where(p => p.Genero == generoId).ToListAsync();

            WriteLine("\r");
            WriteLine("--------Lista----------");
            await livroTask.ContinueWith(task =>
            {
                var livros = task.Result;
                foreach (var p in livros)
                    WriteLine(p.ToString());
            },
            TaskContinuationOptions.OnlyOnRanToCompletion
            );
            WriteLine("-----------------------");
        }

        private static async Task PesquisarLivroNome()
        {
            Write("\r\nNome: ");
            string nome = ReadLine();

            var livros = await context.Livros.ToListAsync();
            List<Livro> lista = new List<Livro>();

            WriteLine("\r");
            WriteLine("--------Lista----------");
            foreach (var livro in livros)
            {
                if (livro.Nome.Contains(nome, StringComparison.OrdinalIgnoreCase))
                {
                    WriteLine(livro.ToString());
                }
            }
            WriteLine("-----------------------");
        }

        private static async Task CadastraGeneroAsync()
        {
            Write("\r\nNome: ");
            string nome = ReadLine();

            Genero genero = new Genero
            {
                Id = 0,
                Nome = nome
            };

            context.Genero.Add(genero);
            await context.SaveChangesAsync();
        }

        private static async Task TodosGenerosAsync()
        {
            var generoTask = context.Genero.ToListAsync();
            WriteLine("\r");
            WriteLine("--------Lista----------");
            await generoTask.ContinueWith(task =>
            {
                var generos = task.Result;
                foreach (var p in generos)
                    WriteLine(p.ToString());
            },
            TaskContinuationOptions.OnlyOnRanToCompletion
            );
            WriteLine("-----------------------");
        }

        private static async Task CadastraLivroAsync()
        {
            Write("\r\nNome: ");
            string nome = ReadLine();

            Write("\r\nGenero Id: ");
            int generoId = Convert.ToInt32(ReadLine());

            Livro livro = new Livro
            {
                Id = 0,
                Nome = nome,
                Reservado = false,
                Genero = generoId
            };

            context.Livros.Add(livro);
            await context.SaveChangesAsync();
        }

        private static async Task CadastraReservaAsync()
        {
            Write("\r\nCPF: ");
            string cpf = ReadLine();

            Write("\r\nLivro Id: ");
            int livroId = Convert.ToInt32(ReadLine());

            Reserva reserva = new Reserva
            {
                Id = 0,
                Cpf = cpf,
                Data = DateTime.Now.AddDays(14),
                Livro = livroId
            };

            var livro = await context.Livros.FindAsync(reserva.Livro);
            if (!livro.Reservado)
            {
                livro.Reservado = true;
                context.Livros.Update(livro);

                context.Reserva.Add(reserva);
                await context.SaveChangesAsync();
            }
        }

        private static async Task TodosReservasAsync()
        {
            var reservaTask = context.Reserva.ToListAsync();
            WriteLine("\r");
            WriteLine("--------Lista----------");
            await reservaTask.ContinueWith(task =>
            {
                var reservas = task.Result;
                foreach (var p in reservas)
                    WriteLine(p.ToString());
            },
            TaskContinuationOptions.OnlyOnRanToCompletion
            );
            WriteLine("-----------------------");
        }

        private static async Task DeletarLivroAsync()
        {
            Write("\r\nLivro ID: ");
            int id = Convert.ToInt32(ReadLine());

            var reservas = context.Reserva.Where(l => l.Livro == id).ToList();

            foreach (var reserva in reservas)
            {
                context.Reserva.Remove(reserva);
            }
            await context.SaveChangesAsync();

            var livro = await context.Livros.FindAsync(id);
            if (livro == null)
            {
                WriteLine("Not Found");
            }

            context.Livros.Remove(livro);
            await context.SaveChangesAsync();
        }

        private static async Task DeletarReservaAsync()
        {
            Write("\r\nReserva ID: ");
            int id = Convert.ToInt32(ReadLine());

            var reserva = await context.Reserva.FindAsync(id);

            if (reserva == null)
            {
                WriteLine("Not Found");
            }

            var livro = await context.Livros.FindAsync(reserva.Livro);
            livro.Reservado = false;
            context.Livros.Update(livro);

            context.Reserva.Remove(reserva);
            await context.SaveChangesAsync();
        }

        private static async Task CalcularMulta()
        {
            Write("\r\nReserva ID: ");
            int id = Convert.ToInt32(ReadLine());

            var reserva = await context.Reserva.FindAsync(id);

            if (reserva == null)
            {
                WriteLine("Not Found");
            }

            var mensagem = "Reserva dentro do prazo";
            var diferença = DateTime.Today.Subtract(reserva.Data).TotalDays;
            if (diferença > 0)
            {
                mensagem = "Multa: R$" + Math.Floor(diferença);
            }

            WriteLine(mensagem);

        }

        //private static async Task AtualizarGeneroAsync()
        //{
        //    Write("\r\nId: ");
        //    int id = Convert.ToInt32(ReadLine());
        //    Write("\r\nNome: ");
        //    string nome = ReadLine();

        //    Genero genero = new Genero
        //    {
        //        Id = id,
        //        Nome = nome
        //    };

        //    //try
        //    //{
        //    //    context.Entry(genero).State = EntityState.Modified;
        //    //    await context.SaveChangesAsync();
        //    //}
        //    //catch(Exception ex)
        //    //{
        //    //    WriteLine(ex);
        //    //}

        //    var generos = await context.Genero.FindAsync(id);
        //    if(generos != null)
        //    {
        //       context.Genero.Update(genero);
        //    }
        //}
    }
}
