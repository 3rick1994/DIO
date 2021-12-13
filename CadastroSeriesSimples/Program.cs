using System;

namespace CadastroDeSeriesSimples
{
    class Program
    {
        static SerieRepositorio repositorio = new SerieRepositorio();
        static void Main(string[] args)
        {
            string opcaousuario;

            do
            {
                opcaousuario = ObterOpcaoUsuario();
                switch (opcaousuario)
                {
                    case "1":
                        Console.Clear();
                        ListarSeries();
                        break;

                    case "2":
                        Console.Clear();
                        InserirSerie();
                        break;

                    case "3":
                        Console.Clear();
                        AtualizarSerie();
                        break;

                    case "4":
                        Console.Clear();
                        ExcluirSerie();
                        break;

                    case "5":
                        Console.Clear();
                        VisualizarSerie();
                        break;

                    case "7":
                        Console.Clear();
                        InserirSeriesAutomaticamente();
                        break;

                    default:
                        Console.Clear();
                        if(opcaousuario.ToUpper() != "X")
                            Console.WriteLine("Entre com uma das opcões acima");
                        break;
                }
            } while (opcaousuario.ToUpper() != "X");
        }

        private static void ListarSeries()
        {
            Console.WriteLine("Listar séries");

            var lista = repositorio.Lista();

            if (lista.Count == 0)
            {
                Console.WriteLine("Nenhuma série cadastrada");
                return;
            }

            foreach (var serie in lista)
            {
                var excluido = serie.RetornaExcluido();
                Console.WriteLine("#ID {0}: - {1} - {2}", serie.RetornaId(), serie.RetornaTitulo(), (excluido ? "Excluido" : ""));
            }
            Console.ReadKey();
        }

        private static void InserirSerie()
        {
            Console.WriteLine("Inserir nova série");

            foreach (int item in Enum.GetValues(typeof(Genero)))
            {
                Console.WriteLine("{0} - {1}", item, Enum.GetName(typeof(Genero), item));
            }
            Console.WriteLine("Digite o Genêro entre as opçoes acima:");
            int entradaGenero = int.Parse(Console.ReadLine());

            Console.WriteLine("Digite o Título da Série:");
            string entradaTitulo = Console.ReadLine();

            Console.WriteLine("Digite o Ano de Início da Série:");
            int entradaAno = int.Parse(Console.ReadLine());

            Console.WriteLine("Digite a Descriçao da Série:");
            string entradaDescricao = Console.ReadLine();

            Serie novaSerie = new Serie(id: repositorio.ProximoId(),
                genero: (Genero)entradaGenero,
                titulo: entradaTitulo,
                ano: entradaAno,
                descricao: entradaDescricao
                );

            repositorio.Insere(novaSerie);
        }

        private static void AtualizarSerie()
        {
            Console.WriteLine("Digite o id da série:");
            int indiceSerie = int.Parse(Console.ReadLine());

            foreach (int i in Enum.GetValues(typeof(Genero)))
            {
                Console.WriteLine("{0} - {1}", i, Enum.GetName(typeof(Genero), i));
            }

            Console.WriteLine("Digite o Genêro entre as opçoes acima:");
            int entradaGenero = int.Parse(Console.ReadLine());

            Console.WriteLine("Digite o Título da Série:");
            string entradaTitulo = Console.ReadLine();

            Console.WriteLine("Digite o Ano de Início da Série:");
            int entradaAno = int.Parse(Console.ReadLine());

            Console.WriteLine("Digite a Descriçao da Série:");
            string entradaDescricao = Console.ReadLine();

            Serie novaSerie = new Serie(id: indiceSerie,
                genero: (Genero)entradaGenero,
                titulo: entradaTitulo,
                ano: entradaAno,
                descricao: entradaDescricao
                );

            repositorio.Insere(novaSerie);
        }

        private static void ExcluirSerie()
        {
            Console.WriteLine("Digite o id da série:");
            int indiceSerie = int.Parse(Console.ReadLine());

            repositorio.Exclui(indiceSerie);
            Console.WriteLine("Série excluida");
            Console.ReadKey();
        }

        private static void VisualizarSerie()
        {
            Console.WriteLine("Digite o id da série:");
            int indiceSerie = int.Parse(Console.ReadLine());

            var serie = repositorio.RetornaPorId(indiceSerie);

            Console.WriteLine(serie);
            Console.ReadKey();
        }
        private static void InserirSeriesAutomaticamente()
        {
            Console.WriteLine("Quantas séries quer cadastrar automaticamente:");
            int indice = int.Parse(Console.ReadLine());

            Serie novaSerie;

            int entradaGenero;
            string entradaTitulo;
            int entradaAno;
            string entradaDescricao = "Descrição";
            var rand = new Random();

            for (int i = 0; i < indice; i++)
            {
                entradaGenero = rand.Next(0, Enum.GetValues(typeof(Genero)).Length);
                entradaTitulo = "Título " + i;
                entradaAno = rand.Next(1990, 2020);

                novaSerie = new Serie(id: i,
                genero: (Genero)entradaGenero,
                titulo: entradaTitulo,
                ano: entradaAno,
                descricao: entradaDescricao
                );

                repositorio.Insere(novaSerie);
            }
        }

        private static string ObterOpcaoUsuario()
        {
            Console.WriteLine();
            Console.WriteLine("Informe a opção desejada");

            Console.WriteLine("1- Listar séries");
            Console.WriteLine("2- Inserir nova série");
            Console.WriteLine("3- Atualizar série");
            Console.WriteLine("4- Excluir série");
            Console.WriteLine("5- Visualizar serie");
            Console.WriteLine("7- Inserir novas séries automaticamente");
            Console.WriteLine("x- Sair");

            string opcaoUsuario = Console.ReadLine().ToUpper();
            Console.WriteLine();
            return opcaoUsuario;
        }
    }
}
