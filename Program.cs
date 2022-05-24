using System;
using System.Linq;
using Dio.Series;

namespace DIO
{
    class Program
    {

        static SerieRepositorio repositorio = new SerieRepositorio();
        static void Main(string[] args)
        {
            string opcaoUsuario = ObterOpcaoUsuario();

            
                while(opcaoUsuario.ToUpper() != "X"){
                switch (opcaoUsuario)
                {
                    case "1":
                        ListarSeries();
                    break;
                    case "2":
                        InserirSerie();
                    break;
                    case "3":
                        AtualizarSerie();
                    break;
                    case "4":
                        ExcluirSerie();
                    break;
                    case "5":
                        VisualizarSerie();
                    break;
                    case "C":
                    Console.Clear();
                    break;

                        default:
                        throw new ArgumentOutOfRangeException();
                        }
                        opcaoUsuario = ObterOpcaoUsuario();
                    } 
                    Console.WriteLine("Obrigado por utilizar nossos serviços");
                    Console.ReadLine();            
        }

        private static void ListarSeries(){
            Console.WriteLine("Listar séries");
            
            var lista = repositorio.Lista();

            if (lista.Count == 0)
            {
                Console.WriteLine("Nenhuma série cadastrada.");
                return;
            }
            foreach (var serie in lista)
            {
                var excluido = serie.retornaExcluido();
                Console.WriteLine("#ID {0}: - {1} - {2}", serie.retornaId(), serie.retornaTitulo(), (excluido ? "*Excluído*" : ""));
            }
        }
        
        private static void InserirSerie()
        {
            Console.WriteLine("Inserir nova série");

            int indiceEnum = 0;
            foreach (int i in Enum.GetValues(typeof(Genero)))
            {
                Console.WriteLine("{0}-{1}", i, Enum.GetName(typeof(Genero), i));
                indiceEnum = i;
            }

            Console.WriteLine("Digite o gênero entre as opções acima: ");
            // int entradaGenero = int.Parse(Console.ReadLine());
            string entradaGenero = Console.ReadLine();

            int numInt;
            bool isNumeric = int.TryParse(entradaGenero, out numInt);
            while (isNumeric is false || numInt > indiceEnum )
            {                
                Console.WriteLine("Informe um número válido.");
                entradaGenero = Console.ReadLine();   
                isNumeric = int.TryParse(entradaGenero, out numInt);
            } 

            Console.WriteLine("Digite o titulo da série: ");
            string entradaTitulo = Console.ReadLine();

            Console.WriteLine("Digite o ano de início da série: ");
            string entradaAno = Console.ReadLine();
            int ano = 0;
            isNumeric = int.TryParse(entradaAno, out ano);
            while (isNumeric is false )
            {                
                Console.WriteLine("Informe um ano válido.");
                entradaAno = Console.ReadLine();   
                isNumeric = int.TryParse(entradaAno, out ano);
            } 

            Console.WriteLine("Digite a descrição da série: ");
            string entradaDescricao = Console.ReadLine();

            Serie novaSerie = new Serie(id:repositorio.ProximoId(),
                                        genero: (Genero)numInt,
                                        titulo: entradaTitulo,
                                        descricao: entradaDescricao,
                                        ano: ano);

            repositorio.Insere(novaSerie);
        }

        public static bool VerificaNum(string entrada)
        {
            int numInt;
            bool isNumeric = int.TryParse(entrada, out numInt);

            return isNumeric;
        }
        private static void AtualizarSerie()
        {
            Console.WriteLine("Digite o ID da série");
            int indiceSerie = int.Parse(Console.ReadLine());
            
            int indiceEnum = 0;
            foreach (int i in Enum.GetValues(typeof(Genero)))
            {
                Console.WriteLine("{0}-{1}", i, Enum.GetName(typeof(Genero), i));
                indiceEnum = i;
            }

            Console.WriteLine("Digite o gênero entre as opções acima: ");
            // int entradaGenero = int.Parse(Console.ReadLine());
            string entradaGenero = Console.ReadLine();

            int numInt;
            bool isNumeric = int.TryParse(entradaGenero, out numInt);
            while (isNumeric is false || numInt > indiceEnum )
            {                
                Console.WriteLine("Informe um número válido.");
                entradaGenero = Console.ReadLine();   
                isNumeric = int.TryParse(entradaGenero, out numInt);
            } 

            Console.WriteLine("Digite o titulo da série: ");
            string entradaTitulo = Console.ReadLine();

            Console.WriteLine("Digite o ano de início da série: ");
            string entradaAno = Console.ReadLine();
            int ano = 0;
            isNumeric = int.TryParse(entradaAno, out ano);
            while (isNumeric is false )
            {                
                Console.WriteLine("Informe um ano válido.");
                entradaAno = Console.ReadLine();   
                isNumeric = int.TryParse(entradaAno, out ano);
            } 

            Console.WriteLine("Digite a descrição da série: ");
            string entradaDescricao = Console.ReadLine();

            Serie novaSerie = new Serie(id:indiceSerie,
                                        genero: (Genero)numInt,
                                        titulo: entradaTitulo,
                                        descricao: entradaDescricao,
                                        ano: ano);

            repositorio.Atualiza(indiceSerie ,novaSerie);

        }

        public static void ExcluirSerie()
        {
            Console.WriteLine("Digite o ID da série");
            int indiceSerie = int.Parse(Console.ReadLine());

            repositorio.Exclui(indiceSerie);
        }

        public static void VisualizarSerie()
        {
            Console.WriteLine("Digite o ID da série");
            int indiceSerie = int.Parse(Console.ReadLine());    

            var serie = repositorio.RetornaPorId(indiceSerie);
            Console.WriteLine(serie);        
        }

        public static void opcoesMenu()
        {
            Console.WriteLine("--------------------------------");
            Console.WriteLine("Sérios DIO!");
            Console.WriteLine("Informe a opção desejada:");
            Console.WriteLine("1 - Listar Séries"); 
            Console.WriteLine("2 - Inserir Nova Série");
            Console.WriteLine("3 - Atualizar Série");
            Console.WriteLine("4 - Excluír Série");
            Console.WriteLine("5 - Vizualizar Série");
            Console.WriteLine("C - Limpar Tela");
            Console.WriteLine("X - Sair");
            Console.WriteLine("--------------------------------");
        }
        private static string ObterOpcaoUsuario()
        {
            opcoesMenu();

            string opcaoUsuario = Console.ReadLine().ToUpper();

            while (opcaoUsuario != 1.ToString() && opcaoUsuario != 2.ToString() && opcaoUsuario != 3.ToString()
            && opcaoUsuario != 4.ToString() && opcaoUsuario != 5.ToString() && opcaoUsuario != "C" && opcaoUsuario != "X" )
            {
                Console.WriteLine("Opção inválida! Favor selecione uma das opçoes disponíveis: ");
                opcoesMenu();
                opcaoUsuario = Console.ReadLine().ToUpper();
            }         
            Console.WriteLine("");
            return opcaoUsuario;
           
        }
    }
}
