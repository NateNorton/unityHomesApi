namespace HomesApi.Models;

public class FavouriteProperty
{
		public long Id { get; set; }
		public long UserId { get; set; }
		public User? User { get; set; }
		public long PropertyId { get; set; }
		public Property? Property { get; set; }
}
