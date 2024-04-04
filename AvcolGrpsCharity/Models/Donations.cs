namespace AvcolGrpsCharity.Models
{
    public class Donations
    {
        public int DonationID { get; set; }
        public int DonationAmount { get; set; }
        public string DonationMessage { get; set; }
        public DateTime DonationDate { get; set; }

        public ICollection<Donors> DonorsId { get; set; }
    }
}
