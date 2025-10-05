namespace My_Resturant.DTOs.Resevation
{
    public class UpdateReservationDTOs
    {
        public int reservationId { get; set; }
        public string? reservationTime { get; set; }
        public int? numberOfPeople { get; set; }
        public string? status { get; set; }
        public string? specialRequests { get; set; }
        public DateOnly? reservationDate { get; set; }

    }
}
