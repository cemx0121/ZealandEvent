using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZealandEventLib.Models;

namespace ZealandEvent.Services
{
    public interface ICountService
    {
        /// <summary>
        /// Tæller alle booking objekter fra databasen, hvor arrangement_id matcher id'et fra parameteren.
        /// </summary>
        /// <param name="id">Arrangement id</param>
        /// <returns>Retunere optællingen som en integer</returns>
        int CountBookings(int? id);
      
        /// <summary>
        /// Tæller alle booking objekter fra databasen,
        /// hvor arrangement_id matcher id'et fra parameteren & hvor parking er sat til True
        /// </summary>
        /// <param name="id">Arrangement id</param>
        /// <returns>Retunere optællingen som en integer</returns>
        int CountParkings(int? id);

        /// <summary>
        /// Søger efter et event objekt fra databasen, hvor arrangement_id, lokation (kun Tribunen og/eller Musikteltet)
        /// samt hvor start og slut tidsrammen matcher med Event objektet fra parameteren. 
        /// </summary>
        /// <param name="newEvent">Indsæt det nye Event objekt du vil oprette, hvor de ønskede værdier er sat inden metoden kaldes</param>
        /// <returns>Retunere et Event objekt, hvis et matchende findes i databasen. Hvis der ikke findes et matchende går den i null</returns>
        Event CheckForDuplicateEvent(Event newEvent);

        /// <summary>
        /// Finder alle event objekter fra databasen, hvor arrangement_id matcher med id'et fra parameteren. 
        /// </summary>
        /// <param name="id">Arrangement id</param>
        /// <returns>Retunere en liste med alle de event objekter der matcher id'et</returns>
        List<Event> FindEventsToArrangement(int? id);

        /// <summary>
        /// Finder alle booking objekter fra databasen, hvor arrangement_id matcher med id'et fra parameteren.
        /// </summary>
        /// <param name="id">Arrangement id</param>
        /// <returns>Retunere en liste med alle de booking objekter der matcher id'et</returns>
        List<Booking> FindBookingsToArrangement(int? id);

        /// <summary>
        /// Søger efter et User objekt fra databasen, hvor UserName matcher med User objektet fra parameteren.
        /// </summary>
        /// <param name="newUser">Indsæt det nye User objekt du vil oprette, hvor de ønskede værdier er sat inden metoden kaldes</param>
        /// <returns>Retunere et User objekt, hvis et matchende findes i databasen. Hvis det ikke findes går den i null</returns>
        User CheckDuplicateUsername(User newUser);



    }
}
