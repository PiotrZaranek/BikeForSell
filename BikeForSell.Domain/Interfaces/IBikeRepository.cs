using BikeForSell.Domain.Models;

namespace BikeForSell.Domain.Interfaces
{
    public interface IBikeRepository
    {
        IQueryable<Bike> GetAllActiveBikes();
        int Add(Bike bike);
        Bike GetBikeDetails(int id);
        IQueryable GetYourBikesList(int id);
        void ChangeStatus(int id);
        Bike GetBikeforEdit(int id);
        void EditBike(Bike bike);
        void DeleteBike(int id);
        void BuyBike(Transaction transaction);
    }
}
