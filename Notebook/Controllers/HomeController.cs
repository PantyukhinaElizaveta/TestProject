using System;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Web.Mvc;

namespace Notebook.Controllers
{
    public class HomeController : Controller
    {
        private NotebookDBEntities3 entities = new NotebookDBEntities3();
        const string Month = "month";
        const string Day = "day";
        const string Year = "year";
        const string Week = "week";

        public ActionResult Index(string search)
        {
            var element = from p in entities.Note
                    select p;

            if (!String.IsNullOrEmpty(search))
            {
                if (!ModelState.IsValid)
                    return View(entities.Note.ToList());

                try
                {
                    DateTime date = Convert.ToDateTime(search);
                    element = entities.Note.Where(p => SqlFunctions.DatePart(Day, p.StartTime) == SqlFunctions.DatePart(Day, date))
                    .Where(p => SqlFunctions.DatePart(Month, p.StartTime) == SqlFunctions.DatePart(Month, date))
                    .Where(p => SqlFunctions.DatePart(Year, p.StartTime) == SqlFunctions.DatePart(Year, date));

                    return View(element.ToList());
                }
                catch
                {
                    return View(entities.Note.ToList());
                }
                
            }
            return View(element.ToList());
        }
        
        public ActionResult TypeFilter(string type)
        {
            return View(entities.Note.Where(p => p.Type == type).ToList()); 
            
        }
        
        public ActionResult ShowModeForADay(DateTime date)
        {
            var element = entities.Note.Where(p => SqlFunctions.DatePart(Day, p.StartTime) == SqlFunctions.DatePart(Day, date))
                .Where(p => SqlFunctions.DatePart(Month, p.StartTime) == SqlFunctions.DatePart(Month, date))
                .Where(p => SqlFunctions.DatePart(Year, p.StartTime) == SqlFunctions.DatePart(Year, date));
                
            return View(element.ToList());
        }
        
        public ActionResult ShowModeForAWeek(DateTime date)
        {
            var element = entities.Note.Where(p => SqlFunctions.DatePart(Week, p.StartTime) == SqlFunctions.DatePart(Week, date))
                .Where(p => SqlFunctions.DatePart(Year, p.StartTime) == SqlFunctions.DatePart(Year, date));
            
            return View(element.ToList());
        }
        
        public ActionResult ShowModeForAMonth(DateTime date)
        {
            var element = entities.Note.Where(p => SqlFunctions.DatePart(Month, p.StartTime) == SqlFunctions.DatePart(Month, date))
                .Where(p => SqlFunctions.DatePart(Year, p.StartTime) == SqlFunctions.DatePart(Year, date));
            
            return View(element.ToList());
        }
        
        public ActionResult CreateMeet()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CreateMeet(NoteModel note)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                entities.Note.Add(note);
                entities.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        public ActionResult CreateBusiness()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CreateBusiness([Bind(Exclude = "Id")] NoteModel note)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                entities.Note.Add(note);
                entities.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        public ActionResult CreateJetting()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CreateJetting([Bind(Exclude = "Id")] NoteModel note)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                entities.Note.Add(note);
                entities.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        public ActionResult Edit(int id)
        {
            NoteModel note = entities.Note.Find(id);
            return View(note);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(NoteModel note)
        {
            if (!ModelState.IsValid)
                return View();
            try
            {
                entities.Entry(note).State = EntityState.Modified;
                entities.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        public ActionResult Delete(int id)
        {
            NoteModel note = entities.Note.Find(id);
            return View(note);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Delete(NoteModel note)
        {
            try
            { 
                var element = (from p in entities.Note
                      where p.Id == note.Id
                      select p).FirstOrDefault();

                entities.Note.Remove(element);
                entities.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}