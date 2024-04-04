namespace AvcolGrpsCharity.Models
{
    public class Donors
    {
        public int DonorID { get; set; }
        public string Donor_name { get; set; }
        public string Donor_email { get; set; }
        public Donations DonationId { get; set; } //referening this line of code in Donations model: public ICollection<Enrollment> Enrollments { get; set; }
    }
}
