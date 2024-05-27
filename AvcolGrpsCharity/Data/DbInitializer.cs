using AvcolGrpsCharity.Areas.Identity.Data;
using AvcolGrpsCharity.Models;
using System;
using System.Diagnostics;
using System.Linq;

namespace AvcolGrpsCharity.Data
{
    public static class DbInitializer
    {
        /*public static void Initialize(AvcolGrpsCharityDbContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.SignedCharityGrps.Any())
            {
                return;   // DB has been seeded
            }

            var SignedCharityGrps = new SignedCharityGrps[]
            {
            new SignedCharityGrps{CharityGrpID=1,ChartyGrp_Name="Save Trees",CharityGrp_description="A charity group to help preserve the greenery of New Zealand.", CharityGrp_email="savetrees@gmail.com", CharityGrp_phone="+64019242111",},
            new SignedCharityGrps{CharityGrpID=2,ChartyGrp_Name="Hunger Games",CharityGrp_description="A charity group to help people in needs of food.", CharityGrp_email="hungergames@gmail.com", CharityGrp_phone="+64190329294",},
            new SignedCharityGrps{CharityGrpID=3,ChartyGrp_Name="No To Littering",CharityGrp_description="A charity group funding cleaning services.", CharityGrp_email="notolittering@gmail.com", CharityGrp_phone="+641928581292",},
            new SignedCharityGrps{CharityGrpID=4,ChartyGrp_Name="Give Blood",CharityGrp_description="A charity group that help fund blood donation services.", CharityGrp_email="giveblood@gmail.com", CharityGrp_phone="+64931282932",},
            new SignedCharityGrps{CharityGrpID=5,ChartyGrp_Name="Pet Shelter",CharityGrp_description="A charity group that helps homeless animals.", CharityGrp_email="petshelter@gmail.com", CharityGrp_phone="+64912823232",},
            new SignedCharityGrps{CharityGrpID = 6, ChartyGrp_Name = "Clean Oceans", CharityGrp_description = "A charity group dedicated to preserving marine life and cleaning up ocean pollution.", CharityGrp_email = "cleanoceans@gmail.com", CharityGrp_phone = "+64128394485" },
            new SignedCharityGrps{CharityGrpID = 7, ChartyGrp_Name = "Education for All", CharityGrp_description = "A charity group providing educational resources and opportunities to underprivileged children.", CharityGrp_email = "eduforall@gmail.com", CharityGrp_phone = "+64938192733" },
            new SignedCharityGrps{CharityGrpID = 8, ChartyGrp_Name = "Healthcare Access", CharityGrp_description = "A charity group working to improve access to healthcare in remote and underserved areas.", CharityGrp_email = "healthaccess@gmail.com", CharityGrp_phone = "+64019284739" },
            new SignedCharityGrps{CharityGrpID = 9, ChartyGrp_Name = "Elderly Care Support", CharityGrp_description = "A charity group providing support and assistance to elderly individuals living alone.", CharityGrp_email = "eldercare@gmail.com", CharityGrp_phone = "+64927489213" },
            new SignedCharityGrps{CharityGrpID = 10, ChartyGrp_Name = "Disaster Relief Aid", CharityGrp_description = "A charity group specializing in providing aid and support to areas affected by natural disasters.", CharityGrp_email = "disasterrelief@gmail.com", CharityGrp_phone = "+64190238471" }
            };
            foreach (SignedCharityGrps s in SignedCharityGrps)
            {
                 context.SignedCharityGrps.Add(s);
            }
            context.SaveChanges();

            var Donors = new Donors[]
            {
            new Donors{DonorID=1,Donor_name="Baldo Christian",Donor_email= "baldochristian@gmail.com",SignedCharityGrpId= 1},
            new Donors{DonorID=2,Donor_name="Kaito Tik",Donor_email= "kaitotik@gmail.com",SignedCharityGrpId= 2},
            new Donors { DonorID = 3, Donor_name = "Maria Lopez", Donor_email = "marialopez@gmail.com", SignedCharityGrpId = 3 },
            new Donors { DonorID = 4, Donor_name = "John Smith", Donor_email = "johnsmith@yahoo.com", SignedCharityGrpId = 4 },
            new Donors { DonorID = 5, Donor_name = "Emily Davis", Donor_email = "emilydavis@hotmail.com", SignedCharityGrpId = 5 },
            new Donors { DonorID = 6, Donor_name = "Michael Johnson", Donor_email = "michaeljohnson@gmail.com", SignedCharityGrpId = 6 },
            new Donors { DonorID = 7, Donor_name = "Linda Martinez", Donor_email = "lindamartinez@yahoo.com", SignedCharityGrpId = 7 },
            new Donors { DonorID = 8, Donor_name = "Robert Brown", Donor_email = "robertbrown@gmail.com", SignedCharityGrpId = 8 },
            new Donors { DonorID = 9, Donor_name = "Amanda Lee", Donor_email = "amandalee@hotmail.com", SignedCharityGrpId = 9 },
            new Donors { DonorID = 10, Donor_name = "Daniel Jackson", Donor_email = "danieljackson@gmail.com", SignedCharityGrpId = 10 }
            };
            foreach (Course c in courses)
            {
                context.Courses.Add(c);
            }
            context.SaveChanges();

            var enrollments = new Enrollment[]
            {
            new Enrollment{StudentID=1,CourseID=1050,Grade=Grade.A},
            new Enrollment{StudentID=1,CourseID=4022,Grade=Grade.C},
            new Enrollment{StudentID=1,CourseID=4041,Grade=Grade.B},
            new Enrollment{StudentID=2,CourseID=1045,Grade=Grade.B},
            new Enrollment{StudentID=2,CourseID=3141,Grade=Grade.F},
            new Enrollment{StudentID=2,CourseID=2021,Grade=Grade.F},
            new Enrollment{StudentID=3,CourseID=1050},
            new Enrollment{StudentID=4,CourseID=1050},
            new Enrollment{StudentID=4,CourseID=4022,Grade=Grade.F},
            new Enrollment{StudentID=5,CourseID=4041,Grade=Grade.C},
            new Enrollment{StudentID=6,CourseID=1045},
            new Enrollment{StudentID=7,CourseID=3141,Grade=Grade.A},
            };
            foreach (Enrollment e in enrollments)
            {
                context.Enrollments.Add(e);
            }
            context.SaveChanges();
        }*/
    }
}