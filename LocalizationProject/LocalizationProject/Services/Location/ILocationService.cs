using System.Threading.Tasks;

namespace LocalizationProject.Services.Location
{
    public interface ILocationService
    {
        Task<Xamarin.Essentials.Location> GetCurrentLocationCoordinates();
    }
}