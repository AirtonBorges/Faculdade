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
    Console.Write("- Digite o endereço fisíco: ");
    var input = uint.Parse(Console.ReadLine() ?? throw new Exception("Digite um valor válido"));

    var quantidadeBitsPagina = (int)Math.Log2(tamanhoDeslocamentoPaginas);
    var tamanhoEspacoBitsSubpaginas = Math.Pow(2, quantidadeBitsSubPagina);

    var tlb = LerArquivo32b("addresses_32b.txt");

    // Calcular endereço
    numeroPagina = ObterNumeroDaPagina((quantidadeBitsPagina + quantidadeBitsSubPagina), input);
    numeroSubPagina = ObterNumeroDaSubPagina(quantidadeBitsPagina, quantidadeBitsSubPagina, input);
    deslocamentoPagina = ObterDeslocamento(quantidadeBitsPagina, input);

    // Mostrar resultado
    Console.WriteLine("==================== RESULTADO ====================");
    var enderecoMemoria = tlb[(int)numeroPagina][(int)numeroSubPagina][(int)deslocamentoPagina];
    var numeroLinha = (numeroPagina * tamanhoEspacoBitsSubpaginas * tamanhoDeslocamentoPaginas)
                      + (tamanhoDeslocamentoPaginas * numeroSubPagina + deslocamentoPagina);

    Console.WriteLine($"Página: {numeroPagina}");
    Console.WriteLine($"Sub-página: {numeroSubPagina}");
    Console.WriteLine("Deslocamento de página: " + deslocamentoPagina);
    Console.WriteLine("Endereço de Memória:  " + enderecoMemoria);
    Console.WriteLine("Linha em que o endereço pode ser encontrado: " + numeroLinha);
    Console.WriteLine("===================================================");

    // Ler arquivo e criar simulação do TLB
    List<List<List<string>>> LerArquivo32b(string pCaminhoArquivo)
    {
        var paginasTlb = new List<List<List<string>>> {
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
                paginasTlb[^1][^1].Add(streamReader.ReadLine() ?? throw new InvalidOperationException("Caminho não encontrado"));

                if (paginasTlb[^1].Count % tamanhoEspacoBitsSubpaginas == 0 && paginasTlb[^1][^1].Count == tamanhoDeslocamentoPaginas)
                {
                    paginasTlb.Add(new List<List<string>>()
                    {
                        new ()
                    });
                }
                if (paginasTlb[^1][^1].Count % tamanhoDeslocamentoPaginas == 0 && paginasTlb[^1][^1].Count != 0 )
                {
                    paginasTlb[^1].Add(new List<string>());
                }
            }
        }
        catch (Exception xException)
        {
            Console.WriteLine("Erro: {0}", xException.Message);
        }

        return paginasTlb;
    }
}

if (quantidadeBits == 16)
{
    Console.Write("- Digite a tamanho do deslocamento de página: ");
    var tamanhoDeslocamentoPaginas = int.Parse(Console.ReadLine() ?? throw new Exception("Digite um valor válido"));
    Console.Write("- Digite o endereço fisíco: ");
    var input = uint.Parse(Console.ReadLine() ?? throw new Exception("Digite um valor válido"));
    var quantidadeBitsPagina = (int)Math.Log2(tamanhoDeslocamentoPaginas);

    numeroPagina = ObterNumeroDaPagina(quantidadeBitsPagina, input);
    deslocamentoPagina = ObterDeslocamento(quantidadeBitsPagina, input);

    var tlb = LerArquivo16b("addresses_16b.txt");

    Console.WriteLine("==================== RESULTADO ====================");
    var enderecoMemoria = tlb[(int)numeroPagina][(int)deslocamentoPagina];
    var numeroLinha = tamanhoDeslocamentoPaginas * numeroPagina + deslocamentoPagina;

    Console.WriteLine($"Página: {numeroPagina}");
    Console.WriteLine("Deslocamento de página: " + deslocamentoPagina);
    Console.WriteLine("Endereço de Memória:  " + enderecoMemoria);
    Console.WriteLine("Linha em que o endereço pode ser encontrado: " + numeroLinha);
    Console.WriteLine("===================================================");

    // Ler arquivo e criar simulação do TLB
    List<List<string>> LerArquivo16b(string pCaminhoArquivo)
    {
        var paginasTlb = new List<List<string>> {
            new()
        };

        try
        {
            using StreamReader streamReader = new StreamReader(pCaminhoArquivo);
            while (streamReader.Peek() >= 0)
            {
                paginasTlb[^1].Add(streamReader.ReadLine() ?? throw new InvalidOperationException("Caminho não encontrado"));

                if (paginasTlb[^1].Count % tamanhoDeslocamentoPaginas == 0 && paginasTlb[^1].Count != 0)
                {
                    paginasTlb.Add(new List<string>());
                }
            }
        }
        catch (Exception xException)
        {
            Console.WriteLine("Erro: {0}", xException.Message);
        }

        return paginasTlb;
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

