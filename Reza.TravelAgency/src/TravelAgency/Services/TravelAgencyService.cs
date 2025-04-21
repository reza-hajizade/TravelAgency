using TravelAgency.Infrastructure;

namespace TravelAgency.Services;

public sealed class TravelAgencyService(
    TravelAgencyDbContext context)
{

    public TravelAgencyDbContext Context { get; } = context;

}



