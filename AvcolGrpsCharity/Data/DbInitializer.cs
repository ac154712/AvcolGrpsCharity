using AvcolGrpsCharity.Areas.Identity.Data;
using AvcolGrpsCharity.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace AvcolGrpsCharity.Data
{
    public static class DbInitializer
    {
        public static void Initialize(AvcolGrpsCharityDbContext context)
        {
            context.Database.EnsureCreated();

            // seeding Charity Groups, if there is any data then it will skip/return back
            if (context.SignedCharityGrps.Any())
            {
                return;
            }

            var charityGroups = new SignedCharityGrps[]
            {
                new SignedCharityGrps{ChartyGrp_Name="Save Trees",CharityGrp_description="A charity group to help preserve the greenery of New Zealand.", CharityGrp_email="savetrees@gmail.com", CharityGrp_phone="+64019242111"},
                new SignedCharityGrps{ChartyGrp_Name="Hunger Games",CharityGrp_description="A charity group to help people in need of food.", CharityGrp_email="hungergames@gmail.com", CharityGrp_phone="+64190329294"},
                new SignedCharityGrps{ChartyGrp_Name="No To Littering",CharityGrp_description="A charity group funding cleaning services.", CharityGrp_email="notolittering@gmail.com", CharityGrp_phone="+641928581292"},
                new SignedCharityGrps{ChartyGrp_Name="Give Blood",CharityGrp_description="A charity group that helps fund blood donation services.", CharityGrp_email="giveblood@gmail.com", CharityGrp_phone="+64931282932"},
                new SignedCharityGrps{ChartyGrp_Name="Pet Shelter",CharityGrp_description="A charity group that helps homeless animals.", CharityGrp_email="petshelter@gmail.com", CharityGrp_phone="+64912823232"},
                new SignedCharityGrps{ChartyGrp_Name = "Clean Oceans", CharityGrp_description = "A charity group dedicated to preserving marine life and cleaning up ocean pollution.", CharityGrp_email = "cleanoceans@gmail.com", CharityGrp_phone = "+64128394485"},
                new SignedCharityGrps{ChartyGrp_Name = "Education for All", CharityGrp_description = "A charity group providing educational resources and opportunities to underprivileged children.", CharityGrp_email = "eduforall@gmail.com", CharityGrp_phone = "+64938192733"},
                new SignedCharityGrps{ChartyGrp_Name = "Healthcare Access", CharityGrp_description = "A charity group working to improve access to healthcare in remote and underserved areas.", CharityGrp_email = "healthaccess@gmail.com", CharityGrp_phone = "+64019284739"},
                new SignedCharityGrps{ChartyGrp_Name = "Elderly Care Support", CharityGrp_description = "A charity group providing support and assistance to elderly individuals living alone.", CharityGrp_email = "eldercare@gmail.com", CharityGrp_phone = "+64927489213"},
                new SignedCharityGrps{ChartyGrp_Name = "Disaster Relief Aid", CharityGrp_description = "A charity group specializing in providing aid and support to areas affected by natural disasters.", CharityGrp_email = "disasterrelief@gmail.com", CharityGrp_phone = "+64190238471"}
            };
            foreach (SignedCharityGrps s in charityGroups)
            {
                context.SignedCharityGrps.Add(s);
            }
            context.SaveChanges();

            // fetching the primary IDs of the seeded charity groups PK and make a variable for the key to be put
            var saveTreesId = context.SignedCharityGrps.Single(s => s.ChartyGrp_Name == "Save Trees").CharityGrpID;
            var hungerGamesId = context.SignedCharityGrps.Single(s => s.ChartyGrp_Name == "Hunger Games").CharityGrpID;
            var noToLitteringId = context.SignedCharityGrps.Single(s => s.ChartyGrp_Name == "No To Littering").CharityGrpID;
            var giveBloodId = context.SignedCharityGrps.Single(s => s.ChartyGrp_Name == "Give Blood").CharityGrpID;
            var petShelterId = context.SignedCharityGrps.Single(s => s.ChartyGrp_Name == "Pet Shelter").CharityGrpID;
            var cleanOceansId = context.SignedCharityGrps.Single(s => s.ChartyGrp_Name == "Clean Oceans").CharityGrpID;
            var educationForAllId = context.SignedCharityGrps.Single(s => s.ChartyGrp_Name == "Education for All").CharityGrpID;
            var healthcareAccessId = context.SignedCharityGrps.Single(s => s.ChartyGrp_Name == "Healthcare Access").CharityGrpID;
            var elderlyCareSupportId = context.SignedCharityGrps.Single(s => s.ChartyGrp_Name == "Elderly Care Support").CharityGrpID;
            var disasterReliefAidId = context.SignedCharityGrps.Single(s => s.ChartyGrp_Name == "Disaster Relief Aid").CharityGrpID; //i did this so i don't have to repeat myself in foreign key seeding to other tables

            // seeding Donors if theres no data
            if (context.Donors.Any()) return;

            var donors = new Donors[]
            {
                new Donors{Donor_name="Baldo Christian",Donor_email= "baldochristian@gmail.com",SignedCharityGrpId= saveTreesId},
                new Donors{Donor_name="Kaito Tik",Donor_email= "kaitotik@gmail.com",SignedCharityGrpId= hungerGamesId},
                new Donors { Donor_name = "Maria Lopez", Donor_email = "marialopez@gmail.com", SignedCharityGrpId = noToLitteringId },
                new Donors { Donor_name = "John Smith", Donor_email = "johnsmith@yahoo.com", SignedCharityGrpId = giveBloodId },
                new Donors { Donor_name = "Emily Davis", Donor_email = "emilydavis@hotmail.com", SignedCharityGrpId = petShelterId },
                new Donors { Donor_name = "Michael Johnson", Donor_email = "michaeljohnson@gmail.com", SignedCharityGrpId = cleanOceansId },
                new Donors { Donor_name = "Linda Martinez", Donor_email = "lindamartinez@yahoo.com", SignedCharityGrpId = educationForAllId },
                new Donors { Donor_name = "Robert Brown", Donor_email = "robertbrown@gmail.com", SignedCharityGrpId = healthcareAccessId },
                new Donors { Donor_name = "Amanda Lee", Donor_email = "amandalee@hotmail.com", SignedCharityGrpId = elderlyCareSupportId },
                new Donors { Donor_name = "Daniel Jackson", Donor_email = "danieljackson@gmail.com", SignedCharityGrpId = disasterReliefAidId }
            };
            foreach (Donors o in donors)
            {
                context.Donors.Add(o);
            }
            context.SaveChanges(); ;

            // looking for any donations data
            if (context.Donations.Any()) return;

            var donations = new Donations[]
            {
                new Donations{DonationAmount = 150.00m, DonationMessage = "Supporting the cause!", DonationDate = DateTime.Now, DonorID = context.Donors.Single(d => d.Donor_name == "Kaito Tik").DonorID},
                new Donations{DonationAmount = 200.00m, DonationMessage = "Keep up the good work!", DonationDate = DateTime.Now, DonorID = context.Donors.Single(d => d.Donor_name == "Maria Lopez").DonorID},
                new Donations{DonationAmount = 300.00m, DonationMessage = "Every little bit helps.", DonationDate = DateTime.Now, DonorID = context.Donors.Single(d => d.Donor_name == "John Smith").DonorID},
                new Donations{DonationAmount = 250.00m, DonationMessage = "Proud to support this charity.", DonationDate = DateTime.Now, DonorID = context.Donors.Single(d => d.Donor_name == "Emily Davis").DonorID},
                new Donations{DonationAmount = 400.00m, DonationMessage = "For a better future.", DonationDate = DateTime.Now, DonorID = context.Donors.Single(d => d.Donor_name == "Michael Johnson").DonorID},
                new Donations{DonationAmount = 100.00m, DonationMessage = "Hope everyone who is sick in the words feels better!", DonationDate = DateTime.Now, DonorID = context.Donors.Single(d => d.Donor_name == "Baldo Christian").DonorID},
                new Donations{DonationAmount = 175.00m, DonationMessage = "Giving back to the community.", DonationDate = DateTime.Now, DonorID = context.Donors.Single(d => d.Donor_name == "Daniel Jackson").DonorID},
                new Donations{DonationAmount = 220.00m, DonationMessage = "In memory of a loved one.", DonationDate = DateTime.Now, DonorID = context.Donors.Single(d => d.Donor_name == "Linda Martinez").DonorID},
                new Donations{DonationAmount = 300.00m, DonationMessage = "Supporting education for all.", DonationDate = DateTime.Now, DonorID = context.Donors.Single(d => d.Donor_name == "Robert Brown").DonorID},
                new Donations{DonationAmount = 150.00m, DonationMessage = "Helping those in need.", DonationDate = DateTime.Now, DonorID = context.Donors.Single(d => d.Donor_name == "Amanda Lee").DonorID}
            };
            foreach (Donations d in donations)
            {
                context.Donations.Add(d);
            }
            context.SaveChanges();

            // seeding CharityGrpStaff
            if (context.CharityGrpStaff.Any()) return;

            var staff = new CharityGrpStaff[]
            {
                new CharityGrpStaff { StaffMember_name = "Ramee", StaffMember_email = "ramee@gmail.com", StaffMember_phonenum = "+64938595793", SignedCharityGrpId = saveTreesId },
                new CharityGrpStaff { StaffMember_name = "Lang Buddha", StaffMember_email = "langbuddha@gmail.com", StaffMember_phonenum = "+64938595794", SignedCharityGrpId = petShelterId },
                new CharityGrpStaff { StaffMember_name = "Mr. K", StaffMember_email = "mrk@gmail.com", StaffMember_phonenum = "+64938595795", SignedCharityGrpId = cleanOceansId },
                new CharityGrpStaff { StaffMember_name = "Tony Corleone", StaffMember_email = "tonycorleone@gmail.com", StaffMember_phonenum = "+64938595796", SignedCharityGrpId = hungerGamesId },
                new CharityGrpStaff { StaffMember_name = "Harry Brown", StaffMember_email = "harrybrown@gmail.com", StaffMember_phonenum = "+64938595797", SignedCharityGrpId = educationForAllId },
                new CharityGrpStaff { StaffMember_name = "Uchiha Jones", StaffMember_email = "uchihajones@gmail.com", StaffMember_phonenum = "+64938595798", SignedCharityGrpId = healthcareAccessId },
                new CharityGrpStaff { StaffMember_name = "Raymond Romanov", StaffMember_email = "raymondromanov@gmail.com", StaffMember_phonenum = "+64938595799", SignedCharityGrpId = elderlyCareSupportId },
                new CharityGrpStaff { StaffMember_name = "Taco Prince", StaffMember_email = "tacoprince@gmail.com", StaffMember_phonenum = "+64938595800", SignedCharityGrpId = giveBloodId },
                new CharityGrpStaff { StaffMember_name = "Randy Bullet", StaffMember_email = "randybullet@gmail.com", StaffMember_phonenum = "+64938595801", SignedCharityGrpId = noToLitteringId },
                new CharityGrpStaff { StaffMember_name = "Chawa Nut", StaffMember_email = "chawanut@gmail.com", StaffMember_phonenum = "+64938595802", SignedCharityGrpId = disasterReliefAidId }
            };
            foreach (CharityGrpStaff m in staff)
            {
                context.CharityGrpStaff.Add(m);
            }
            context.SaveChanges();

            // Seeding Categories model
            if (context.CharityCategory.Any()) return;

            var categories = new CharityCategory[]
            {
                new CharityCategory{SignedCharityGrpId= saveTreesId, Category_name = "Environment"},
                new CharityCategory{SignedCharityGrpId= hungerGamesId, Category_name = "Poverty"},
                new CharityCategory{SignedCharityGrpId= noToLitteringId, Category_name = "Community"},
                new CharityCategory{SignedCharityGrpId= giveBloodId, Category_name = "Health"},
                new CharityCategory{SignedCharityGrpId= petShelterId, Category_name = "Animal Welfare"},
                new CharityCategory{SignedCharityGrpId = cleanOceansId, Category_name = "Environment"},
                new CharityCategory{SignedCharityGrpId = educationForAllId, Category_name = "Education"},
                new CharityCategory{SignedCharityGrpId = healthcareAccessId, Category_name = "Health"},
                new CharityCategory{SignedCharityGrpId = elderlyCareSupportId, Category_name = "Elderly Care"},
                new CharityCategory{SignedCharityGrpId = disasterReliefAidId, Category_name = "Disaster Relief"}
            };
            foreach (CharityCategory c in categories)
            {
                context.CharityCategory.Add(c);
            }
            context.SaveChanges();
        }
    }
}
