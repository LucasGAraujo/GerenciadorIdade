
public class Entidades
{
    public String Nome { get; set; }
    public String SobreNome { get; set; }
    public DateTime Aniversario { get; set; }

    public int Idade { get { return CalcularIdade(); } }

    private int CalcularIdade()
    {
        DateTime dataAtual = DateTime.Now;
        int idade = dataAtual.Year - Aniversario.Year;
        if (dataAtual < Aniversario.AddYears(idade)) idade--;
        return idade;
    }

    public Entidades(string nome, string sobrenome, DateTime aniversario)
    {
        Nome = nome;
        SobreNome = sobrenome;
        Aniversario = aniversario;
    }
}

public class Adicionar
{
    private List<Entidades> entidades = new List<Entidades>();

    public void AdicionarPessoa(Entidades entidade)
    {
        entidades.Add(entidade);
    }

    public List<Entidades> ListarTodasAsPessoas()
    {
        return entidades;
    }

}

class Program
{
    public static void Main(string[] args)
    {
        bool continuar = true;
        Adicionar gerenciador = new Adicionar();

        while (continuar)
        {
            Console.WriteLine("Gerenciador de Aniversario -");
            Console.WriteLine("Selecione uma opção abaixo");
            Console.WriteLine("1-Adicionar nova pessoa");
            Console.WriteLine("2-Pesquisar pessoa");
            Console.WriteLine("3-Sair");

            int opcao = Convert.ToInt32(Console.ReadLine());

            switch (opcao)
            {
                case 1:
                    Console.WriteLine("Digite o nome da pessoa: ");
                    string nome = Console.ReadLine();
                    Console.WriteLine("Digite sobrenome: ");
                    string sobrenome = Console.ReadLine();
                    Console.WriteLine("Digite a data do seu nascimento no formato yyyy/00/00");
                    string dataNascimentoStr = Console.ReadLine();
                    if (DateTime.TryParseExact(dataNascimentoStr, "yyyy/MM/dd", null, System.Globalization.DateTimeStyles.None, out DateTime dataNascimento))
                    {
                        Entidades novaEntidade = new Entidades(nome, sobrenome, dataNascimento);
                        Console.WriteLine("Confirma 1-Sim 2-Não");
                        Console.WriteLine($"Nome: {novaEntidade.Nome} {novaEntidade.SobreNome} idade: {novaEntidade.Idade} Nascimento = {novaEntidade.Aniversario}");
                        int confirma = Convert.ToInt32(Console.ReadLine());
                        if (confirma == 1)
                        {
                            gerenciador.AdicionarPessoa(novaEntidade);
                            Console.WriteLine("Obrigado! Cadastrado com sucesso");
                        }
                        else
                        {
                            Console.WriteLine("Tudo bem, de volta ao menu inicial");
                        }
                    }
                    break;
                case 2:
                    ListarTodasAsPessoas(gerenciador);
                    break;
                case 3:
                    continuar = false;
                    break;
                default:
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    break;
            }
        }
    }
    private static void ListarTodasAsPessoas(Adicionar gerenciador)
    {
        List<Entidades> todasAsPessoas = gerenciador.ListarTodasAsPessoas();

        if (todasAsPessoas.Count > 0)
        {
            Console.WriteLine("Todas as pessoas cadastradas:");
            int i = 0;
            foreach (var pessoa in todasAsPessoas)
            {

                Console.WriteLine($"Nome: {i} {pessoa.Nome}");
                i++;
            }
            Console.WriteLine("Digite o indece da pessoa que deseja saber mais informe");
            int informa = Convert.ToInt32(Console.ReadLine());
            DateTime aniversarioEsteAno = new DateTime(DateTime.Now.Year, todasAsPessoas[informa].Aniversario.Month, todasAsPessoas[informa].Aniversario.Day);
            int diasFaltando = (aniversarioEsteAno - DateTime.Now).Days + 1;

            Console.WriteLine($"Nome: {todasAsPessoas[informa].Nome} {todasAsPessoas[informa].SobreNome} Idade: {todasAsPessoas[informa].Idade} Faltam {diasFaltando} dias para o próximo aniversário.");


            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine();
            Console.WriteLine();

        }
        else
        {
            Console.WriteLine("Nenhuma pessoa cadastrada ainda.");
        }
    }
}