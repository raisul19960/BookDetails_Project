using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Hospital_Management_System.Models
{
    public enum Gender { Male = 1, Female }
    public class Patient
    {
        public int PatientId { get; set; }
        [Required, StringLength(50)]
        public string PatientName { get; set; }
        [Required, Column(TypeName = "date")]
        public DateTime AdmissionDate { get; set; }
        [Required, Column(TypeName = "varchar")]
        public string Mobile { get; set; }
        [Required, EnumDataType(typeof(Gender))]
        public Gender Gender { get; set; }
        [Required, StringLength(50)]
        public string Picture { get; set; }

        //nav
        public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
    public class Appointment
    {
        public int Id { get; set; }
        [Required, StringLength(50)]
        public string DoctorName { get; set; }

        [Required, Column(TypeName = "date")]
        public DateTime AppointmentDate { get; set; }

        [Required, ForeignKey("Patient")]
        public int PatientId { get; set; }
        public virtual Patient Patient { get; set; }

    }
   public class PatientDbContext : DbContext
    {
        public PatientDbContext()
        {
            Database.SetInitializer(new DbInitializer());
        }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
    }

    public class DbInitializer : DropCreateDatabaseIfModelChanges<PatientDbContext>
    {

        protected override void Seed(PatientDbContext context)
        {
            Patient p1 = new Patient { PatientName = "Abdulla", AdmissionDate = DateTime.Parse("2023-02-02"), Mobile = "01309059311", Gender = Gender.Male, Picture = "1.jpg", };
            p1.Appointments.Add(new Appointment { DoctorName = "Nishat Rahman", AppointmentDate = DateTime.Parse("2023-7-2"), PatientId = 1 });
            p1.Appointments.Add(new Appointment { DoctorName = "Matiur Rahman", AppointmentDate = DateTime.Parse("2023-7-8"), PatientId = 1 });
            context.Patients.Add(p1);
            context.SaveChanges();
        }
    }
}