using System;
using Console.Data;
using Microsoft.EntityFrameworkCore;
using static System.Console;
using System.Threading.Tasks;

namespace Console
{
    class Program
    {
        private static bool bibliotecario = false;
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
                        //await PesquisarLivroGenero();
                        return true;
                    case "3":
                        //await PesquisarLivroNome();
                        return true;
                    case "4":
                        //await CadastraReservaAsync();
                        return true;
                    case "5":
                        //await CalcularMulta();
                        return true;
                    case "6":
                        //await Login();
                        return true;
                    default:
                        return true;
                }
            }
            return true;
           
        }

        private static async Task TodosLivrosAsync()
        {
            var context = new AppDbContext();
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


    }
}
