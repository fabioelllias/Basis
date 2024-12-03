using Flunt.Validations;

namespace Desafio.Infrastructure
{
    public static class ContractHelper
    {
        public static Contract<T> Create<T>(Action<Contract<T>> setup) where T : class
        {
            var contract = new Contract<T>().Requires();
            setup(contract);
            return contract;
        }
    }
}