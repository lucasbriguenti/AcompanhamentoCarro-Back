namespace OnboardingSIGDB1.Domain.Utils
{
    public static class CpfCnpjExtensions
    {
        public static string LimpaMascaraCnpjCpf(this string cnpj)
        {
            return cnpj.Replace(".", "").Replace("-", "").Replace("/", "").Trim();
        }
    }
}
