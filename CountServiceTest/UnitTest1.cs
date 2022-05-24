using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZealandEvent.Services;
using ZealandEventLib.Data;
using ZealandEventLib.Models;

namespace CountServiceTest
{
    [TestClass]
    public class UnitTest1
    {
        private CountService countService;

        [TestInitialize]
        public void BeforeEachTestMethod()
        {
            countService = new CountService();
        }

        [TestMethod]
        public void TestCountBookingsMethod()
        {
            // TESTER OM DET RIGTIGE ANTAL AF BOOKINGER TIL ET SPECIFIKT ARRANGEMENT
            // PASSER MED EXPECTED VALUE SOM MANUELT KAN FINDES I DATABASE TABELLEN

            // Arrange
            int expectedValue = 99;
            int arrangementId = 7;
            // Act
            int actualValue = countService.CountBookings(arrangementId);
            // Assert
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void TestCheckDuplicateUsernameMethod()
        {
            // TESTER OM METODEN FINDER DEN EKSISTERENDE BRUGER I USER TABELLEN MED USERNAME == "Admin"

            // Arrange
            bool existingUsernameFound = false;
            User existingUser = new User();
            existingUser.UserName = "Admin";
            // Act
            if (countService.CheckDuplicateUsername(existingUser) != null)
            {
                existingUsernameFound = true;
            }
            // Assert
            Assert.IsTrue(existingUsernameFound);
        }
    }
}
