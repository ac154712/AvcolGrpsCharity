SELECT 
	DonorID, 
	Donor_name,
	SUM(DonationAmount) 
	AS TotalDonations
FROM 
	Donations
GROUP BY 
	DonorID;