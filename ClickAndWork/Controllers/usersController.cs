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
    public class usersController : Controller
    {
        private modelEntities1 db = new modelEntities1();
        
        // GET: users
        public ActionResult Index()
        {
            return View(db.users.ToList());
        }
    
        public ActionResult Login()
        {
            Session["message"] = "";
            Session["messagee"] = "";
            return View();
        }
        [HttpPost]
        public ActionResult Login(user u)
        {
            if (ModelState.IsValid) // this is check validity
            {
                using (modelEntities1 dc = new modelEntities1())
                {
                    var v = dc.users.Where(a => a.mailid.Equals(u.mailid) && a.pwd.Equals(u.pwd)).FirstOrDefault();
                    if (v != null)
                    {
                        Session["LogedUserID"] = v.fname.ToString();
                        Session["LogedUserFullname"] = v.lname.ToString();
               
                        return RedirectToAction("WorkerPage");
                    }
                }
            }
            return View(u);
        }

        // GET: users/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user user = db.users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: users/Create
        public ActionResult Create()
        {
           
            return View();
        }
        // POST: users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "fname,lname,phone,mailid,pwd")] user user)
        {
           
            if (ModelState.IsValid)
            {
                db.users.Add(user);
                db.SaveChanges();
                Session["message"] = user.fname+" "+user.lname+" "+"Succesfully Registered ";
                return RedirectToAction("Create");
                
            }
            return View();
        }
        [HttpGet]
        public ActionResult WorkerPage()
        {
            Session["messageee"] = "";
            var jobposts = db.Jobposts.ToList();
            var user = new user() { fname = Session["LogedUserID"].ToString() };
            var viewModel = new WorkerHirer()
            {
                jobposts = jobposts,
                user = user
            };
            return View(viewModel);
        }
        public ActionResult Apply(string title, string id, string name)
        {
           
            var apply = new Jobapply() { jobtitle = title, workername = id, jobname = name};

            db.Jobapplies.Add(apply);
            try
            {
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            Session["messagee"] = "Job Successfully Applied For " + apply.jobtitle;
            return RedirectToAction("WorkerPage");

        }
        public ActionResult Applied(string user)
        {
            
            var apply = db.Jobapplies.Where(a => a.workername.Equals(user)).ToList();
            var viewModel = new WorkerHirer() { applies = apply};
            return View(viewModel);
        }
        
        public ActionResult CreateProfile()
        {
       
            return View();
        }
        [HttpPost]
        public ActionResult CreateProfile([Bind(Include = "firstname,lastname,dob,mailid,phone,technologies,experience,previouscompany,hobbies,Acheivements")] Profile Profile)
        {
            
            if(ModelState.IsValid)
            {
                db.Profiles.Add(Profile);
                try
                {
                    db.SaveChanges();
                }
                catch(Exception e)
                {
                    Console.WriteLine(e);
                }
                Session["messageee"] = Profile.firstname + " " + Profile.lastname + " " + "Profile Created ";
                return RedirectToAction("CreateProfile");
            }
           
            return View(Profile);
        }
        // GET: users/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user user = db.users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "fname,lname,phone,mailid,pwd")] user user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: users/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user user = db.users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            user user = db.users.Find(id);
            db.users.Remove(user);
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
