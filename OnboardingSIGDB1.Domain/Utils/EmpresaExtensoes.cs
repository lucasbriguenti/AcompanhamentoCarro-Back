namespace OnboardingSIGDB1.Domain.Utils
{
    public static class EmpresaExtensoes
    {
        public static string LimpaMascaraCnpj(this string cnpj)
        {
            return cnpj.Replace(".", "").Replace("-", "").Replace("/", "").Trim();
        }
    }
}
