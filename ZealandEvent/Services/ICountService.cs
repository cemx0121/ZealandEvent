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

        /// <summary>
        /// Søger efter arrangementer som har værdien af parameteren i arrangement.Navn
        /// </summary>
        /// <param name="searchtext">Søgetekst på arrangement.Navn</param>
        /// <returns>En liste af arrangement hvor arrangement.Navn indeholder søgetekst fra parameteren</returns>
        List<Arrangement> SearchForArrangement(string searchtext);

        /// <summary>
        /// Finder alle booking objekter fra databasen, hvor user_id matcher med id'et fra parameteren.
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns>Retunere en liste med alle de booking objekter der matcher id'et</returns>
        List<Booking> FindBookingsToUser(int? id);

        /// <summary>
        /// Søger efter bookinger som har værdien af parameteren i booking.Firstname ELLER booking.Lastname
        /// </summary>
        /// <param name="searchText">Fornavn eller efternavn</param>
        /// <returns>En liste af bookinger hvor booking.firstname eller lastname indeholder søgetekst fra parameteren</returns>
        List<Booking> SearchForBookersName(string searchText, int? id);

        /// <summary>
        /// Søger efter users som har værdien af parameteren i user.Username
        /// </summary>
        /// <param name="searchText">Username</param>
        /// <returns>En liste af users hvor username indeholder søgetekst fra parameteren</returns>
        List<User> SearchForUserName(string searchText);


    }
}
