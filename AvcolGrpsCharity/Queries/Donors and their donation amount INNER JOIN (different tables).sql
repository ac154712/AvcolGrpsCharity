SELECT 
	Donors.Donor_name, 
	Donations.DonationAmount
FROM 
	Donors
	INNER JOIN Donations ON Donors.DonorID = Donations.DonorID;