SELECT 
*
FROM 
	Donations
WHERE 
	DonationDate BETWEEN '2023-01-01' AND '2023-12-31'
	AND DonationAmount BETWEEN 100 AND 500;