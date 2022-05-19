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
        /// En metode der tæller alle booking objekter fra databasen, hvor arrangement_id matcher id'et fra parameteren.
        /// </summary>
        /// <param name="id">Arrangement id</param>
        /// <returns>Retunere optællingen som en integer</returns>
        public int CountBookings(int? id);
      
        /// <summary>
        /// En metode der tæller alle booking objekter fra databasen,
        /// hvor arrangement_id matcher id'et fra parameteren & hvor parking er sat til True
        /// </summary>
        /// <param name="id">Arrangement id</param>
        /// <returns>Retunere optællingen som en integer</returns>
        public int CountParkings(int? id);

        /// <summary>
        /// En metode der søger efter et event objekt fra databasen, hvor arrangement_id, lokation (kun Tribunen og/eller Musikteltet)
        /// samt hvor start og slut tidsrammen matcher med Event objektet fra parameteren. 
        /// </summary>
        /// <param name="newEvent">Indsæt det nye Event objekt du vil oprette, hvor de ønskede værdier er sat inden metoden kaldes</param>
        /// <returns>Retunere et Event objekt, hvis et matchende findes i databasen. Hvis der ikke findes et matchende går den i null</returns>
        public Event CheckForDuplicateEvent(Event newEvent);



    }
}
