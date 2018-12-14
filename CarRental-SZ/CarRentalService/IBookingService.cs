using CarRental.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Net.Security;

namespace CarRentalService
{
    [ServiceContract(Namespace = "http://sofiazaid.net/carRental/2018/11", Name = "BookingService")]
    public interface IBookingService
    {
        /// <summary>
        /// Messages sent with this operation are signed, but not encrypted.
        /// With sign we assure authentication: message was created by a known sender,
        /// was not altered during transit and we can asure the message was sent
        /// by specific sender.
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        [OperationContract(ProtectionLevel = ProtectionLevel.Sign)]
        List<Car> GetListOfAvailableCars(DateTime from, DateTime to);

        /// <summary>
        /// Messages sent through the use of this operation are both encrypted and signed
        /// which means we are getting both a signature to show authentication of messages
        /// and the messages themselves (sent with this operation) are encrypted (i.e. when
        /// looking in the log we can't see what the actual sent message looks like).
        /// </summary>
        /// <param name="booking"></param>
        /// <returns></returns>
        [OperationContract(ProtectionLevel = ProtectionLevel.EncryptAndSign)]
        [FaultContract(typeof(InvalidDateException))]
        int AddBooking(Booking booking);

        [OperationContract(ProtectionLevel = ProtectionLevel.Sign)]
        [FaultContract(typeof(InvalidOperationFault))]
        [FaultContract(typeof(DbUpdateFault))]
        void DeleteBooking(int id);

        [OperationContract(ProtectionLevel = ProtectionLevel.EncryptAndSign)]
        [FaultContract(typeof(EntityNotFoundFault))]
        Booking GetBooking(int id);

        [OperationContract(ProtectionLevel = ProtectionLevel.EncryptAndSign)]
        List<Booking> FindBookingsByCustomerEmail(string emailAdress);
    }
}
