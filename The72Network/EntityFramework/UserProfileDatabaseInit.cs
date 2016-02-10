using System.Data.Entity;
using System.Data.Entity.Migrations;
using WebMatrix.WebData;

namespace The72Network.EntityFramework
{
	//public class UserProfileDatabaseInit : DbMigrationsConfiguration<UserProfileDatabaseContext>
	public class UserProfileDatabaseInit : DropCreateDatabaseAlways<UserProfileDatabaseContext>
	{
		//public UserProfileDatabaseInit()
		//{
		//	AutomaticMigrationsEnabled = true;
		//}

		protected override void Seed(UserProfileDatabaseContext context)
		{
			SeedMemberShip();
		}

		private static void SeedMemberShip()
		{
			WebSecurity.InitializeDatabaseConnection("DefaultConnection", "UserProfile", "Id", "UserName", autoCreateTables:true);
		}
	}
}