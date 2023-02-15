using BikeForSell.Domain.Models;

namespace BikeForSell.Domain.Interfaces
{
    public interface IBikeRepository
    {
        IQueryable<Bike> GetAllActiveBikes();
        int Add(Bike bike);
        Bike GetBikeDetails(int id);
        IQueryable GetYourBikesList(string id);
        void ChangeStatus(int id);
        Bike GetBikeForEdit(int id);
        void EditBike(Bike bike);
        void DeleteBike(int id);
        void BuyBike(Transaction transaction, int id);
    }
}
