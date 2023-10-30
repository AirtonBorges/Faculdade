// Resultado
uint numeroPagina;
uint numeroSubPagina;
uint deslocamentoPagina;

// Obter quantidade bits
Console.Write("- Digite a quantidade de bits: ");
var quantidadeBits = int.Parse(Console.ReadLine() ?? throw new Exception("Digite um valor válido"));

// Lógica 32 bits
if (quantidadeBits == 32)
{
    // Obter valores
    Console.Write("- Digite a tamanho do deslocamento de página: ");
    var tamanhoDeslocamentoPaginas = int.Parse(Console.ReadLine() ?? throw new Exception("Digite um valor válido"));
    Console.Write("- Digite a quantidade de bits reservados para as sub-páginas: ");

    var quantidadeBitsSubPagina = int.Parse(Console.ReadLine() ?? throw new Exception("Digite um valor válido"));
    var tamanhoEspacoBitsSubpaginas = Math.Pow(2, quantidadeBitsSubPagina);
    var quantidadeBitsPagina = (int)Math.Log2(tamanhoDeslocamentoPaginas);
    var tabelaPaginas = LerArquivo32b("data_memory.txt");

    var enderecos = new List<uint>();

    if (args.Length == 0)
    {
        Console.Write("- Digite o endereço virtual: ");
        enderecos.Add(uint.Parse(Console.ReadLine() ?? throw new Exception("Digite um valor válido")));
    }
    else
        enderecos = LerEnderecos("addresses_32b.txt");


    enderecos.ForEach(pEndereco =>
    {
        // Calcular endereço
        numeroPagina = ObterNumeroDaPagina((quantidadeBitsPagina + quantidadeBitsSubPagina), pEndereco);
        numeroSubPagina = ObterNumeroDaSubPagina(quantidadeBitsPagina, quantidadeBitsSubPagina, pEndereco);
        deslocamentoPagina = ObterDeslocamento(quantidadeBitsPagina, pEndereco);

        // Mostrar resultado
        Console.WriteLine("==================== RESULTADO ====================");
        var enderecoMemoria = tabelaPaginas[(int)numeroPagina][(int)numeroSubPagina][(int)deslocamentoPagina];
        var numeroLinha = (numeroPagina * tamanhoEspacoBitsSubpaginas * tamanhoDeslocamentoPaginas)
                          + (tamanhoDeslocamentoPaginas * numeroSubPagina + deslocamentoPagina);

        Console.WriteLine($"Endereço: {pEndereco}");
        Console.WriteLine($"Página: {numeroPagina}");
        Console.WriteLine($"Sub-página: {numeroSubPagina}");
        Console.WriteLine("Deslocamento de página: " + deslocamentoPagina);
        Console.WriteLine("Endereço de Memória:  " + enderecoMemoria);
        Console.WriteLine("Linha em que o endereço pode ser encontrado: " + numeroLinha);
        Console.WriteLine("===================================================");
    });

    // Ler arquivo e criar simulação da tabela de páginas
    List<List<List<string>>> LerArquivo32b(string pCaminhoArquivo)
    {
        var paginas = new List<List<List<string>>> {
            new()
            {
                new List<string>()
            }
        };

        try
        {
            using StreamReader streamReader = new StreamReader(pCaminhoArquivo);
            while (streamReader.Peek() >= 0)
            {
                paginas[^1][^1].Add(streamReader.ReadLine() ?? throw new InvalidOperationException("Caminho não encontrado"));

                if (paginas[^1].Count % tamanhoEspacoBitsSubpaginas == 0 && paginas[^1][^1].Count == tamanhoDeslocamentoPaginas)
                {
                    paginas.Add(new List<List<string>>()
                    {
                        new ()
                    });
                }
                if (paginas[^1][^1].Count % tamanhoDeslocamentoPaginas == 0 && paginas[^1][^1].Count != 0 )
                {
                    paginas[^1].Add(new List<string>());
                }
            }
        }
        catch (Exception xException)
        {
            Console.WriteLine("Erro: {0}", xException.Message);
        }

        return paginas;
    }
}

if (quantidadeBits >= 16 && quantidadeBits < 32)
{
    Console.Write("- Digite a tamanho do deslocamento de página: ");
    var tamanhoDeslocamentoPaginas = int.Parse(Console.ReadLine() ?? throw new Exception("Digite um valor válido"));
    Console.Write("- Digite o endereço virtual: ");
    var quantidadeBitsPagina = (int)Math.Log2(tamanhoDeslocamentoPaginas);

    var paginas = LerArquivo16b("data_memory.txt");
    var enderecos = new List<uint>();

    if (args.Length == 0)
    {
        Console.Write("- Digite o endereço virtual: ");
        enderecos.Add(uint.Parse(Console.ReadLine() ?? throw new Exception("Digite um valor válido")));
    }
    else
        enderecos = LerEnderecos("addresses_32b.txt");

    enderecos.ForEach(pEndereco =>
    {
        numeroPagina = ObterNumeroDaPagina(quantidadeBitsPagina, pEndereco);
        deslocamentoPagina = ObterDeslocamento(quantidadeBitsPagina, pEndereco);

        Console.WriteLine("==================== RESULTADO ====================");
        var enderecoMemoria = paginas[(int)numeroPagina][(int)deslocamentoPagina];
        var numeroLinha = tamanhoDeslocamentoPaginas * numeroPagina + deslocamentoPagina;

        Console.WriteLine($"Endereço: {pEndereco}");
        Console.WriteLine($"Página: {numeroPagina}");
        Console.WriteLine("Deslocamento de página: " + deslocamentoPagina);
        Console.WriteLine("Endereço de Memória:  " + enderecoMemoria);
        Console.WriteLine("Linha em que o endereço pode ser encontrado: " + numeroLinha);
        Console.WriteLine("===================================================");
    });

    // Ler arquivo e criar simulação da tabela de páginas
    List<List<string>> LerArquivo16b(string pCaminhoArquivo)
    {
        var paginas = new List<List<string>> {
            new()
        };

        try
        {
            using StreamReader streamReader = new StreamReader(pCaminhoArquivo);
            while (streamReader.Peek() >= 0)
            {
                paginas[^1].Add(streamReader.ReadLine()
                                   ?? throw new InvalidOperationException("Caminho não encontrado"));

                if (paginas[^1].Count % tamanhoDeslocamentoPaginas == 0 && paginas[^1].Count != 0)
                {
                    paginas.Add(new List<string>());
                }
            }
        }
        catch (Exception xException)
        {
            Console.WriteLine("Erro: {0}", xException.Message);
        }

        return paginas;
    }
}

uint ObterDeslocamento(int pQuantidadeBits, uint pInput)
{
    var mascara = uint.MaxValue << pQuantidadeBits;
    var retorno = pInput & (~mascara);
    return retorno;
}

uint ObterNumeroDaSubPagina(int pDeslocamentoPagina, int pQuantidadeBitsSubPagina, uint pInput)
{
    var mascaraPagina = uint.MaxValue << pDeslocamentoPagina + pQuantidadeBitsSubPagina;
    var mascaraSubPaginaComDeslocamento = ~mascaraPagina;
    var mascaraSubPagina = (uint.MaxValue << pDeslocamentoPagina) & mascaraSubPaginaComDeslocamento;

    var numeroFinalSubPagina = (mascaraSubPagina & pInput) >> pDeslocamentoPagina;
    return numeroFinalSubPagina;
}

uint ObterNumeroDaPagina(int pQuantidadeBits, uint pInput)
{
    var mascara = uint.MaxValue << pQuantidadeBits;
    var bitsMaisSignificativos = mascara & pInput;
    var retorno = bitsMaisSignificativos >> pQuantidadeBits;
    return retorno;
}

List<uint> LerEnderecos(string pCaminho)
{
    var enderecos = new List<uint>();
    try
    {
        using StreamReader streamReader = new StreamReader(pCaminho);
        while (streamReader.Peek() >= 0)
        {
            enderecos.Add(uint.Parse(streamReader.ReadLine()
                                     ?? throw new InvalidOperationException("Caminho não encontrado")));
        }
    }
    catch (Exception xException)
    {
        Console.WriteLine("Erro: {0}", xException.Message);
    }

    return enderecos;
}