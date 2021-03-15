using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Crudoperation.Models;
using Crudoperation.Controllers;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using Newtonsoft.Json;

namespace Crudoperation.Controllers
{
    public class OfficeinfoController : Controller
    {
        // GET: Officeinfo
        Office_InfoEntities1 ob = new Office_InfoEntities1();
       //
        public ActionResult Index()
        {
            var Q = ob.Office_Information.ToList();
            ViewBag.List = Q;
            return View();
        }
        public JsonResult GetUserDetails(string Empid)
        {
            int Id = Convert.ToInt32(Empid);
            var Q = ob.Office_Information.Where(M => M.Id == Id).FirstOrDefault();
            return Json(Q, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateDetails(  string modelParameter, HttpPostedFileBase ProfileImage)
        {
            string filename = ""; string sPath = "";
            Office_Information oboffice = new Office_Information();
            oboffice = JsonConvert.DeserializeObject<Office_Information>(modelParameter);
            if (ProfileImage != null)
            {
                if (ProfileImage.ContentLength > 0)
                {
                    
                    filename = Path.GetFileName(ProfileImage.FileName);
                    oboffice.File = filename;
                    sPath = Convert.ToString(HttpContext.Server.MapPath("/Content/Image/"+filename));
                    ProfileImage.SaveAs(sPath);
                }
            }
            //  int ide = Convert.ToInt32(id);
            //  var Q = ob.Office_Information.Where(M => M.Id == ide && M.File==file12).FirstOrDefault();

             var Q = ob.Office_Information.Where(M => M.Id == oboffice.Id).FirstOrDefault();
            if (Q != null)
            {




                Q.File = oboffice.File;
                // Q.Id = ide;
                Q.Name = oboffice.Name;
                Q.City = oboffice.City;
                Q.Gender = oboffice.Gender;
                Q.Pin = oboffice.Pin;
                //R.File = file12;
               
              //  ob.Office_Information.Add(new Office_Information { File = Path });
                ob.SaveChanges();
            }
           // ob.Office_Information.Add(oboffice);
            ViewBag.Msg = "Successfull";
            return Json("Success", JsonRequestBehavior.AllowGet);
        }
        public JsonResult DelDetails(string id1)
        {
            int _id1 = Convert.ToInt32(id1);
            //Office_Information ob = new Office_Information();
            //Office_InfoEntities1 db = new Office_InfoEntities1();
            //db.Office_Information.Remove(ob);
            //ob.Id = id1;
            var Q = ob.Office_Information.Where(M => M.Id == _id1).FirstOrDefault();
            ob.Office_Information.Remove(Q);
            ob.SaveChanges();
            return Json("success", JsonRequestBehavior.AllowGet);
        }
      
        public JsonResult InsertValue(string modelParameter1, HttpPostedFileBase ProfileImage1)
        {
            string filename1 = ""; string sPath1 = "";
            Office_Information db = new Office_Information();
            db= JsonConvert.DeserializeObject<Office_Information>(modelParameter1);
            if (ProfileImage1!= null)
            {
                if (ProfileImage1.ContentLength > 0)
                {

                    filename1 = Path.GetFileName(ProfileImage1.FileName);
                    db.File = filename1;
                    sPath1 = Convert.ToString(HttpContext.Server.MapPath("/Content/Image/" + filename1));
                    ProfileImage1.SaveAs(sPath1);
                    
                }
            }
            var Q = ob.Office_Information.Add(db);
            if (Q != null)
            {




                Q.File = db.File;
                // Q.Id = ide;
                Q.Name = db.Name;
                Q.City = db.City;
                Q.Gender = db.Gender;
                Q.Pin = db.Pin;
                Q.File = db.File;

                //  ob.Office_Information.Add(new Office_Information { File = Path });
                ob.SaveChanges();
            }

            //Office_Information ob = new Office_Information();
            //Office_InfoEntities1 db = new Office_InfoEntities1();
            //db.Office_Information.Add(ob);
            //ob.Name = nm5;
            //ob.City = city5;
            //ob.Gender = gen5;
            //ob.Pin = pin5;
            //ob.File = file;

            //ProfileImage.SaveAs(Server.MapPath("~\\Pic\\" + ProfileImage.FileName));
            //string Path = "~\\Pic\\" + ProfileImage.FileName;
            //db.Office_Information.Add(new Office_Information { File = Path });
            //db.SaveChanges();

            ViewBag.Msg = "Successfull";
            return Json("Successful", JsonRequestBehavior.AllowGet);
        }
    }
}