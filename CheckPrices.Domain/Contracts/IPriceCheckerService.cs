namespace CheckPrices.Domain.Contracts
{
    public interface IPriceCheckerService
    {
        Task<decimal?> GetPriceFromUrlAsync(string url);
    }
}
