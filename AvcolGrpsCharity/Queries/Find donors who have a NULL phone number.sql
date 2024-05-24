SELECT 
*
FROM
    Donors
WHERE 
    Donor_email IS NOT NULL AND DonorID NOT IN (
    SELECT 
        DonorID 
    FROM 
        Donations);