namespace FitnessClub.UnitTests
{
    public class DataServiceTest
    {
        [Fact]
        public void GetClubByIndex_DoesNotThrowException_ForInvalidIndex()
        {
            var dataService = new DataService();
            var clubCount = dataService.GetClubs().Count;
            var exception = Record.Exception(() => dataService.GetClubByIndex(clubCount + 1));
            Assert.Null(exception);
        }

        [Fact]
        public void GetMemberByIndex_DoesNotThrowException_ForInvalidIndex()
        {
            var dataService = new DataService();
            var memberId = 9999;
            var exception = Record.Exception(() => dataService.GetMemberById(memberId));
            Assert.Null(exception);
        }
    }
}