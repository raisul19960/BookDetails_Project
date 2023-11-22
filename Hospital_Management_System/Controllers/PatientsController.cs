using Hospital_Management_System.Models;
using Hospital_Management_System.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using X.PagedList;

namespace Hospital_Management_System.Controllers
{
    [Authorize]
    public class PatientsController : Controller
    {
        private readonly PatientDbContext db = new PatientDbContext();
        // GET: Patients
        [AllowAnonymous]
        public async Task<ActionResult> Index(int pg = 1)
        {
            var data = await db.Patients.OrderBy(a => a.PatientId).ToPagedListAsync(pg, 5);
            return View(data);
        }
        public ActionResult Create()
        {

            PatientInputModel db = new PatientInputModel();
            db.Appointments.Add(new Appointment { });
            return View(db);
        }
        [HttpPost]
        public ActionResult Create(PatientInputModel data, string act = "")
        {
            if (act == "add")
            {
                data.Appointments.Add(new Appointment { });

                foreach (var item in ModelState.Values)
                {
                    item.Errors.Clear();
                }
            }
            if (act.StartsWith("remove"))
            {
                int index = int.Parse(act.Substring(act.IndexOf("_") + 1));
                data.Appointments.RemoveAt(index);
                foreach (var item in ModelState.Values)
                {
                    item.Errors.Clear();
                }
            }
            if (act == "insert")
            {
                if (ModelState.IsValid)
                {
                    var b = new Patient
                    {
                        PatientName = data.PatientName,
                        AdmissionDate = data.AdmissionDate,
                        Mobile = data.Mobile,
                        Gender = data.Gender,
                    };
                    string ext = Path.GetExtension(data.Picture.FileName);
                    string fileName = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + ext;
                    string savePath = Server.MapPath("~/Pictures/") + fileName;
                    data.Picture.SaveAs(savePath);
                    b.Picture = fileName;
                    foreach (var l in data.Appointments)
                    {

                        b.Appointments.Add(l);
                    }
                    db.Patients.Add(b);
                    db.SaveChanges();
                }
            }
            ViewBag.Act = act;
            return PartialView("_CreatePartial", data);
        }
        public ActionResult Edit(int id)
        {
            var a = db.Patients
              .Select(x => new PatientEditModel
              {
                  PatientId = x.PatientId,
                  PatientName = x.PatientName,
                  AdmissionDate = x.AdmissionDate,
                  Mobile = x.Mobile,
                  Gender = x.Gender,
                  Appointments = x.Appointments.ToList()
              })
              .FirstOrDefault(x => x.PatientId == id);

            ViewBag.CurrentPic = db.Patients.First(x => x.PatientId == id).Picture;

            return View(a);
        }

        [HttpPost]
        public ActionResult Edit(PatientEditModel data, string act = "")
        {
            if (act == "add")
            {

                data.Appointments.Add(new Appointment { });
            }

            if (act.StartsWith("remove"))
            {

                int index = int.Parse(act.Substring(act.IndexOf("_") + 1));

                data.Appointments.RemoveAt(index);
            }

            if (act == "update")
            {

                if (ModelState.IsValid)
                {

                    var a = db.Patients.First(x => x.PatientId == data.PatientId);

                    a.PatientName = data.PatientName;
                    a.AdmissionDate = data.AdmissionDate;
                    a.Mobile = data.Mobile;
                    a.Gender = data.Gender;
                    if (data.Picture != null)
                    {

                        string ext = Path.GetExtension(data.Picture.FileName);
                        string fileName = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + ext;
                        string savePath = Server.MapPath("~/Pictures/") + fileName;
                        data.Picture.SaveAs(savePath);
                        a.Picture = fileName;
                    }

                    db.Appointments.RemoveRange(db.Appointments.Where(x => x.PatientId == data.PatientId).ToList());

                    foreach (var item in data.Appointments)
                    {
                        a.Appointments.Add(new Appointment
                        {
                            PatientId = item.PatientId,
                            DoctorName = item.DoctorName,
                            AppointmentDate = item.AppointmentDate
                        });
                    }
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            return View(data);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var patient = new Patient { PatientId = id };
            db.Entry(patient).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            return Json(new { success = true, deleted = id });
        }
    }
}