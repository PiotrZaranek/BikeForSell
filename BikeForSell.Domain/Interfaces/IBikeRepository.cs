using BikeForSell.Domain.Models;

namespace BikeForSell.Domain.Interfaces
{
    public interface IBikeRepository
    {
        IQueryable<Bike> GetAllActiveBikes();
        int Add(Bike newBike);
        Bike GetBikeForDetailsOrEdit(int bikeId);
        IQueryable GetYourBikesList(string userId);
        void ChangeStatus(int bikeId);
        void EditBike(Bike bike);
        void DeleteBike(int bikeId);
        void BuyBike(Transaction transaction, int bikeId);
    }
}
