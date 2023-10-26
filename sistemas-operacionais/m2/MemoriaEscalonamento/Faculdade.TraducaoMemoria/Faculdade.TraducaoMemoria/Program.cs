Console.WriteLine("Teste");

var quantidadeBits = 16;
var deslocamentoDePagina = 256;
const uint input = 260;

var teste = input;
Console.WriteLine($"Valor: {teste}");

var linhas = LerArquivo("addresses_16.txt");
var numeroDaPagina = ObterNumeroDaPagina((int)Math.Log2(deslocamentoDePagina), input);
var deslocamentoDaPagina = ObterDeslocamento((int)Math.Log2(deslocamentoDePagina), input);

Console.WriteLine("Página: " + numeroDaPagina);
Console.WriteLine("Deslocamento de página: " + deslocamentoDaPagina);

uint ObterDeslocamento(int pQuantidadeBits, uint pInput)
{
    //Shift uint.MaxValue left by x, the least significant bits will be 1
    var mascara = uint.MaxValue << pQuantidadeBits;
    //Apply AND operator to reversed mask to get least significant bits of the input up
    var retorno = pInput & (~mascara);
    return retorno;
}

Console.WriteLine(numeroDaPagina);

uint ObterNumeroDaPagina(int pQuantidadeBits, uint pInput)
{
    //Shift uint.MaxValue left by x, the most significant bits will be 1
    var mascara = uint.MaxValue << pQuantidadeBits;
    //Apply AND operator to get most significant bits of the input up
    var bitsMaisSignificativos = mascara & pInput;
    //Shift most significant bits right, the most significant bits will become the page number 
    var retorno = bitsMaisSignificativos >> pQuantidadeBits;
    return retorno;
}

IEnumerable<string?> LerArquivo(string s)
{
    var xLinhas = new List<string?>(); 
    try
    {
        using (StreamReader streamReader = new StreamReader(s))
        {
            while (streamReader.Peek() >= 0)
            {
                xLinhas.Add(streamReader.ReadLine());
            }
        }
    }
    catch (Exception e)
    {
        Console.WriteLine("The process failed: {0}", e);
    }

    return xLinhas;
}