
namespace DataAccess
{
	public class Seed
	{
		public static async Task SeedData(DataContext context)
		{
			// Check that there are not users already in the database
			/*
			if (!userManager.Users.Any())
			{
				var users = new List<AppUser>
				{
					new AppUser {
						DisplayName = "Bob",
						UserName = "bob",
						Email = "bob@test.com"
					}
				};

				foreach (AppUser user in users)
				{
					// Below method will create the user in the db directly… no need
					// …to saveChangesAsync()
					await userManager.CreateAsync(user, "Pa$$w0rd!");
				}
			}
			*/

			/*
			if (context.Accounts.Any()) return;

			var seedData = new List<Account>
			{

			};

			await context.Accounts.AddRangeAsync(seedData);

			await context.SaveChangesAsync();
			*/
		}
	}

}
