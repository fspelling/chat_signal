using FluentValidation;

namespace POC.ChatSignal.Domain.Extensions
{
    public static class ValidatorExtension
    {
        public static async Task ValidarRequestException<TRequest, TException>(this IValidator<TRequest> validator, TRequest request)
            where TException : Exception
        {
            var validacaoRequest = await validator.ValidateAsync(request);

            if (!validacaoRequest.IsValid)
                throw (TException)Activator.CreateInstance(typeof(TException), [validacaoRequest.Errors.First().ErrorMessage])!;
        }
    }
}
