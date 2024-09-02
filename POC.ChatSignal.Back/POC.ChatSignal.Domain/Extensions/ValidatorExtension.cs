namespace POC.ChatSignal.Domain.Extensions
{
    public static class ValidatorExtension
    {
        public static void ValidarCampoObrigatorioException<T>(this Dictionary<string, object> parameters) where T : Exception
        {
            foreach (var parameter in parameters)
            {
                if (parameter.Value is null)
                {
                    var messageError = $"O campo {parameter.Key} é obrigatório.";
                    throw (T)Activator.CreateInstance(typeof(T), [messageError]);
                }
            }
        }
    }
}
