using My_Resturant.DTOs.Resevation;
using My_Resturant.Entities;
namespace My_Resturant.Interfaces
{
    public interface IReservationServices
    {
        Task CreateReservation(CreateReservationDTOs createReservationDTOs);
        Task DeleteRerservation(int  reservationId);
        Task UpdateReservation(UpdateReservationDTOs updateReservationDTOs);
        Task<GetReservationDTOs> GetMyReservation(int customerId);
        Task<List<GetReservationDTOs>> GetAllReservation();
    }
}
