namespace BTL.Models.RoomModels
{
	public class GetRoom
	{
        public string MaLp { get; set; } = null!;
        public DateTime? NgayDen { get; set; }

		public DateTime? NgayDi { get; set; }

		public GetRoom()
		{
			this.MaLp = null;
			this.NgayDen = null!;
			this.NgayDi = null!;
		}
    }
}
