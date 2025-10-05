using Microsoft.EntityFrameworkCore;
using My_Resturant.Context;
using My_Resturant.DTOs.Resevation;
using My_Resturant.Entities;
using My_Resturant.Interfaces;

namespace My_Resturant.Implementations
{
    public class ReservationServices : IReservationServices
    {
        private readonly RestDbContext _context;

        public ReservationServices(RestDbContext context)
        {
            _context = context;
        }

        public async Task CreateReservation(CreateReservationDTOs createReservationDTOs)
        {
            var confirmedStatus =(await _context.LookupItems.FirstOrDefaultAsync(li => li.name == "Confirmed")).id;
            int customerId = createReservationDTOs.customerId;
            if(createReservationDTOs.customerFirstName != null && createReservationDTOs.customerLastName !=null)
            {
                customerId = (from person in _context.People where person.firstName == 
                             createReservationDTOs.customerFirstName && person.lastName == createReservationDTOs.customerLastName select person.id).Single();
           
            }
            var reservation = new Reservation
            {
                reservationTime = createReservationDTOs.reservationTime,
                numberOfPeople = createReservationDTOs.numberOfPeople,
                status = confirmedStatus,
                customerId = createReservationDTOs.customerId,
                specialRequests = createReservationDTOs.specialRequests,
                ReservationDate= createReservationDTOs.reservationDate,
                isActive=true
            };

            await _context.Reservations.AddAsync(reservation);
            await _context.SaveChangesAsync();
        }



        public async Task DeleteRerservation(int reservationId)
        {
            var reservation = await _context.Reservations
                .FirstOrDefaultAsync(r => r.id == reservationId);

            if (reservation == null)
            {
                throw new Exception("Reservation not found");
            }

            reservation.status = (await _context.LookupItems.FirstOrDefaultAsync(li => li.name == "Cancelled")).id;
            await _context.SaveChangesAsync();
        }


        public async Task<List<GetReservationDTOs>> GetAllReservation()
        {
            var reservations = await _context.Reservations.ToListAsync();
            if (reservations == null || reservations.Count == 0)
            {
                return null;
            }

            var confirmedStatusId = await _context.LookupItems
                .Where(li => li.name == "Confirmed")
                .Select(li => li.id)
                .FirstOrDefaultAsync();

            var completedStatusId = await _context.LookupItems
                .Where(li => li.name == "Completed")
                .Select(li => li.id)
                .FirstOrDefaultAsync();


            foreach (var r in reservations)
            {
                if (r.ReservationDate <= DateOnly.FromDateTime(DateTime.Today) && TimeSpan.ParseExact(r.reservationTime, "hh\\:mm", null) <= DateTime.Now.TimeOfDay && r.status == confirmedStatusId )           
                {
                    r.status = completedStatusId;
                }
            }

            await _context.SaveChangesAsync();

            var lookupItems = await _context.LookupItems.ToListAsync();
            var people = await _context.People.ToListAsync();

            var getReservationDTOs = reservations.Select(r => new GetReservationDTOs
            {
                reservationId = r.id,
                reservationTime = r.reservationTime,
                numberOfPeople = r.numberOfPeople,
                status = lookupItems.FirstOrDefault(li => li.id == r.status)?.name,
                customerId = r.customerId,
                customerFirstName = people.FirstOrDefault(p => p.id == r.customerId)!.firstName,
                customerLastName = people.FirstOrDefault(p => p.id== r.customerId)!.lastName,
                specialRequests = r.specialRequests,
                reservationDate = r.ReservationDate
            }).ToList();

            return getReservationDTOs;
        }


        public async Task<GetReservationDTOs> GetMyReservation(int customerId)
        {
            var confirmedStatus = await _context.LookupItems
                .Where(li => li.name == "Confirmed")
                .Select(li => li.id) //by using this the DB just return the id but with just using firstordefualt it would return the whole obj
                .FirstOrDefaultAsync();
            var completedStatus = await _context.LookupItems
                .Where(li => li.name == "Completed")
                .Select(li => li.id)
                .FirstOrDefaultAsync();
            var customerFirstName = (await _context.People.SingleAsync(p => p.id == customerId)).firstName;
            var customerLastName = (await _context.People.SingleAsync(p => p.id == customerId)).lastName;

            var reservation = await _context.Reservations
                .FirstOrDefaultAsync(r => r.customerId == customerId && r.status ==confirmedStatus);

            if (reservation == null) {
                return null;
            }
            if (reservation.ReservationDate<=DateOnly.FromDateTime(DateTime.Today))
            {
                reservation.status = completedStatus;
                await _context.SaveChangesAsync();
                return null;
            }
            GetReservationDTOs getReservationDTOs = new GetReservationDTOs
            {
                reservationId = reservation.id,
                creationDate = reservation.creationDate,
                customerId = customerId,
                customerFirstName= customerFirstName,
                customerLastName= customerLastName,
                numberOfPeople = reservation.numberOfPeople,
                reservationTime = reservation.reservationTime,
                reservationDate=reservation.ReservationDate,
                specialRequests = reservation.specialRequests,
                status = "Confirmed"
            };
            return getReservationDTOs;
        }

        public async Task UpdateReservation(UpdateReservationDTOs updateReservationDTOs)
        {
            var reservation = await _context.Reservations
                .FirstOrDefaultAsync(r => r.id == updateReservationDTOs.reservationId);

            if (reservation == null)
            {
                throw new Exception("Reservation not found");
            }
            
            if (updateReservationDTOs.reservationTime!=null)
                reservation.reservationTime = updateReservationDTOs.reservationTime;
                reservation.modificationDate=DateTime.Now;

            if (updateReservationDTOs.numberOfPeople.HasValue)
                reservation.numberOfPeople = updateReservationDTOs.numberOfPeople.Value;
                reservation.modificationDate = DateTime.Now;

            if (updateReservationDTOs.reservationDate.HasValue)
                reservation.ReservationDate = updateReservationDTOs.reservationDate.Value;
                reservation.modificationDate = DateTime.Now;

            if (updateReservationDTOs.specialRequests != null)
            {
                reservation.specialRequests = updateReservationDTOs.specialRequests;
                reservation.modificationDate = DateTime.Now;
            }
            await _context.SaveChangesAsync();
        }
    }
}
