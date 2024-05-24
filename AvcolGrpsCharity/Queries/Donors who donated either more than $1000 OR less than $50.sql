SELECT 
*
FROM 
    Donors
WHERE 
    DonorID IN (
    SELECT DonorID FROM Donations WHERE DonationAmount > 1000
    UNION
    SELECT DonorID FROM Donations WHERE DonationAmount < 50
);