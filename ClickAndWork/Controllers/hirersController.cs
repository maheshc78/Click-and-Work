using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClickAndWork.Models;
using ClickAndWork.ViewModels;

namespace ClickAndWork.Controllers
{
    public class hirersController : Controller
    {
        private modelEntities1 db = new modelEntities1();

        // GET: hirers
        public ActionResult Index()
        {
            return View(db.hirers.ToList());
        }
        public ActionResult Hirelogin()
        {
            Session["hmessage"] = "";
            Session["job"] = "";
            return View();
        }
        [HttpPost]
        public ActionResult Hirelogin(hirer h)
        {
            if (ModelState.IsValid) // this is check validity
            {
                using (modelEntities1 dc = new modelEntities1())
                {
                    var v = dc.hirers.Where(a => a.hmailid.Equals(h.hmailid) && a.hpwd.Equals(h.hpwd)).FirstOrDefault();
                    if (v != null)
                    {
                        Session["LogedUserID"] = v.hfname.ToString();
                        Session["LogedUserFullname"] = v.hlname.ToString();
                        return RedirectToAction("HirerPage");
                    }
                }
            }
            return View(h);
        }
        
        public ActionResult Search()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Search(string searchBy,string search)
        {
            if (searchBy == "Experience")
            {
                var profiles = db.Profiles.Where(x => x.experience == search).ToList();
                var viewModel = new WorkerHirer() { profiles = profiles };
                return View(viewModel);
            }
            else
            {
                var profiles = db.Profiles.Where(x => x.technologies.Contains(search)).ToList();
                var viewModel = new WorkerHirer() { profiles = profiles };
                return View(viewModel);
            }
        }
        public ActionResult HirerPage()
        {
            
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HirerPage([Bind(Include = "jobtitle,publishby,jobdesp,exp,tech,prevcom,date")]Jobpost obj)
        {
            if (ModelState.IsValid)
            {
                db.Jobposts.Add(obj);
                try
                {
                    db.SaveChanges();
                }
                catch(Exception e)
                {
                    Console.WriteLine(e);
                }
                Session["job"] = "job posted successfully";
                return RedirectToAction("HirerPage");
            }
            return View(obj);
        }
        // GET: hirers/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            hirer hirer = db.hirers.Find(id);
            if (hirer == null)
            {
                return HttpNotFound();
            }
            return View(hirer);
        }

        // GET: hirers/Create
        public ActionResult Create()
        {
            
            return View();
        }

        // POST: hirers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "hfname,hlname,hphone,hmailid,hpwd")] hirer hirer)
        {
            if (ModelState.IsValid)
            {
                db.hirers.Add(hirer);
                db.SaveChanges();
                Session["hmessage"] = hirer.hfname + " " + hirer.hlname + " " + "Succesfully Registered ";
                return RedirectToAction("Create");
            }

            return View(hirer);
        }

        // GET: hirers/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            hirer hirer = db.hirers.Find(id);
            if (hirer == null)
            {
                return HttpNotFound();
            }
            return View(hirer);
        }

        // POST: hirers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "hfname,hlname,hphone,hmailid,hpwd")] hirer hirer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hirer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hirer);
        }

        // GET: hirers/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            hirer hirer = db.hirers.Find(id);
            if (hirer == null)
            {
                return HttpNotFound();
            }
            return View(hirer);
        }

        // POST: hirers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            hirer hirer = db.hirers.Find(id);
            db.hirers.Remove(hirer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
