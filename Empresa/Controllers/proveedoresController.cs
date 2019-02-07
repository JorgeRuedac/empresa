using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;
using PagedList;
using PagedList.Mvc;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using Empresa.Models;
using Empresa.Data;

namespace Empresa.Controllers
{
    public class proveedoresController : Controller
    {

        DataTable dtproveedores = new DataTable();

        // GET: /proveedores/
        public ActionResult Index(string sortOrder,  
                                  String SearchField,
                                  String SearchCondition,
                                  String SearchText,
                                  String Export,
                                  int? PageSize,
                                  int? page, 
                                  string command)
        {

            if (command == "Show All") {
                SearchField = null;
                SearchCondition = null;
                SearchText = null;
                Session["SearchField"] = null;
                Session["SearchCondition"] = null;
                Session["SearchText"] = null; } 
            else if (command == "Add New Record") { return RedirectToAction("Create"); } 
            else if (command == "Export") { Session["Export"] = Export; } 
            else if (command == "Search" | command == "Page Size") {
                if (!string.IsNullOrEmpty(SearchText)) {
                    Session["SearchField"] = SearchField;
                    Session["SearchCondition"] = SearchCondition;
                    Session["SearchText"] = SearchText; }
                } 
            if (command == "Page Size") { Session["PageSize"] = PageSize; }

            ViewData["SearchFields"] = GetFields((Session["SearchField"] == null ? "Proveedores I D" : Convert.ToString(Session["SearchField"])));
            ViewData["SearchConditions"] = Library.GetConditions((Session["SearchCondition"] == null ? "Contains" : Convert.ToString(Session["SearchCondition"])));
            ViewData["SearchText"] = Session["SearchText"];
            ViewData["Exports"] = Library.GetExports((Session["Export"] == null ? "Pdf" : Convert.ToString(Session["Export"])));
            ViewData["PageSizes"] = Library.GetPageSizes();

            ViewData["CurrentSort"] = sortOrder;
            ViewData["ProveedoresIDSortParm"] = sortOrder == "ProveedoresID_asc" ? "ProveedoresID_desc" : "ProveedoresID_asc";
            ViewData["Nombre_empresaSortParm"] = sortOrder == "Nombre_empresa_asc" ? "Nombre_empresa_desc" : "Nombre_empresa_asc";
            ViewData["DireccionSortParm"] = sortOrder == "Direccion_asc" ? "Direccion_desc" : "Direccion_asc";
            ViewData["TelefonoSortParm"] = sortOrder == "Telefono_asc" ? "Telefono_desc" : "Telefono_asc";
            ViewData["Celular_contactoSortParm"] = sortOrder == "Celular_contacto_asc" ? "Celular_contacto_desc" : "Celular_contacto_asc";
            ViewData["Nombre_contactoSortParm"] = sortOrder == "Nombre_contacto_asc" ? "Nombre_contacto_desc" : "Nombre_contacto_asc";

            dtproveedores = proveedoresData.SelectAll();

            try
            {
                if (!string.IsNullOrEmpty(Convert.ToString(Session["SearchField"])) & !string.IsNullOrEmpty(Convert.ToString(Session["SearchCondition"])) & !string.IsNullOrEmpty(Convert.ToString(Session["SearchText"])))
                {
                    dtproveedores = proveedoresData.Search(Convert.ToString(Session["SearchField"]), Convert.ToString(Session["SearchCondition"]), Convert.ToString(Session["SearchText"]));
                }
            }
            catch { }

            var Query = from rowproveedores in dtproveedores.AsEnumerable()
                        select new proveedores() {
                            ProveedoresID = rowproveedores.Field<Int32>("ProveedoresID")
                           ,Nombre_empresa = rowproveedores.Field<String>("Nombre_empresa")
                           ,Direccion = rowproveedores.Field<String>("Direccion")
                           ,Telefono = rowproveedores.Field<String>("Telefono")
                           ,Celular_contacto = rowproveedores.Field<String>("Celular_contacto")
                           ,Nombre_contacto = rowproveedores.Field<String>("Nombre_contacto")
                        };

            switch (sortOrder)
            {
                case "ProveedoresID_desc":
                    Query = Query.OrderByDescending(s => s.ProveedoresID);
                    break;
                case "ProveedoresID_asc":
                    Query = Query.OrderBy(s => s.ProveedoresID);
                    break;
                case "Nombre_empresa_desc":
                    Query = Query.OrderByDescending(s => s.Nombre_empresa);
                    break;
                case "Nombre_empresa_asc":
                    Query = Query.OrderBy(s => s.Nombre_empresa);
                    break;
                case "Direccion_desc":
                    Query = Query.OrderByDescending(s => s.Direccion);
                    break;
                case "Direccion_asc":
                    Query = Query.OrderBy(s => s.Direccion);
                    break;
                case "Telefono_desc":
                    Query = Query.OrderByDescending(s => s.Telefono);
                    break;
                case "Telefono_asc":
                    Query = Query.OrderBy(s => s.Telefono);
                    break;
                case "Celular_contacto_desc":
                    Query = Query.OrderByDescending(s => s.Celular_contacto);
                    break;
                case "Celular_contacto_asc":
                    Query = Query.OrderBy(s => s.Celular_contacto);
                    break;
                case "Nombre_contacto_desc":
                    Query = Query.OrderByDescending(s => s.Nombre_contacto);
                    break;
                case "Nombre_contacto_asc":
                    Query = Query.OrderBy(s => s.Nombre_contacto);
                    break;
                default:  // Name ascending 
                    Query = Query.OrderBy(s => s.ProveedoresID);
                    break;
            }

            if (command == "Export") {
                GridView gv = new GridView();
                DataTable dt = new DataTable();
                dt.Columns.Add("Proveedores I D", typeof(string));
                dt.Columns.Add("Nombre Empresa", typeof(string));
                dt.Columns.Add("Direccion", typeof(string));
                dt.Columns.Add("Telefono", typeof(string));
                dt.Columns.Add("Celular Contacto", typeof(string));
                dt.Columns.Add("Nombre Contacto", typeof(string));
                foreach (var item in Query)
                {
                    dt.Rows.Add(
                        item.ProveedoresID
                       ,item.Nombre_empresa
                       ,item.Direccion
                       ,item.Telefono
                       ,item.Celular_contacto
                       ,item.Nombre_contacto
                    );
                }
                gv.DataSource = dt;
                gv.DataBind();
                ExportData(Export, gv, dt);
            }

            int pageNumber = (page ?? 1);
            int? pageSZ = (Convert.ToInt32(Session["PageSize"]) == 0 ? 5 : Convert.ToInt32(Session["PageSize"]));
            return View(Query.ToPagedList(pageNumber, (pageSZ ?? 5)));
        }

        // GET: /proveedores/Details/<id>
        public ActionResult Details(
                                      Int32? ProveedoresID
                                   )
        {
            if (
                    ProveedoresID == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            proveedores proveedores = new proveedores();
            proveedores.ProveedoresID = System.Convert.ToInt32(ProveedoresID);
            proveedores = proveedoresData.Select_Record(proveedores);

            if (proveedores == null)
            {
                return HttpNotFound();
            }
            return View(proveedores);
        }

        // GET: /proveedores/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: /proveedores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include=
				           "ProveedoresID"
				   + "," + "Nombre_empresa"
				   + "," + "Direccion"
				   + "," + "Telefono"
				   + "," + "Celular_contacto"
				   + "," + "Nombre_contacto"
				  )] proveedores proveedores)
        {
            if (ModelState.IsValid)
            {
                bool bSucess = false;
                bSucess = proveedoresData.Add(proveedores);
                if (bSucess == true)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Can Not Insert");
                }
            }

            return View(proveedores);
        }

        // GET: /proveedores/Edit/<id>
        public ActionResult Edit(
                                   Int32? ProveedoresID
                                )
        {
            if (
                    ProveedoresID == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            proveedores proveedores = new proveedores();
            proveedores.ProveedoresID = System.Convert.ToInt32(ProveedoresID);
            proveedores = proveedoresData.Select_Record(proveedores);

            if (proveedores == null)
            {
                return HttpNotFound();
            }

            return View(proveedores);
        }

        // POST: /proveedores/Edit/<id>
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(proveedores proveedores)
        {

            proveedores oproveedores = new proveedores();
            oproveedores.ProveedoresID = System.Convert.ToInt32(proveedores.ProveedoresID);
            oproveedores = proveedoresData.Select_Record(proveedores);

            if (ModelState.IsValid)
            {
                bool bSucess = false;
                bSucess = proveedoresData.Update(oproveedores, proveedores);
                if (bSucess == true)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Can Not Update");
                }
            }

            return View(proveedores);
        }

        // GET: /proveedores/Delete/<id>
        public ActionResult Delete(
                                     Int32? ProveedoresID
                                  )
        {
            if (
                    ProveedoresID == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            proveedores proveedores = new proveedores();
            proveedores.ProveedoresID = System.Convert.ToInt32(ProveedoresID);
            proveedores = proveedoresData.Select_Record(proveedores);

            if (proveedores == null)
            {
                return HttpNotFound();
            }
            return View(proveedores);
        }

        // POST: /proveedores/Delete/<id>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(
                                            Int32? ProveedoresID
                                            )
        {

            proveedores proveedores = new proveedores();
            proveedores.ProveedoresID = System.Convert.ToInt32(ProveedoresID);
            proveedores = proveedoresData.Select_Record(proveedores);

            bool bSucess = false;
            bSucess = proveedoresData.Delete(proveedores);
            if (bSucess == true)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Can Not Delete");
            }
            return null;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        private static List<SelectListItem> GetFields(String select)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            SelectListItem Item1 = new SelectListItem { Text = "Proveedores I D", Value = "Proveedores I D" };
            SelectListItem Item2 = new SelectListItem { Text = "Nombre Empresa", Value = "Nombre Empresa" };
            SelectListItem Item3 = new SelectListItem { Text = "Direccion", Value = "Direccion" };
            SelectListItem Item4 = new SelectListItem { Text = "Telefono", Value = "Telefono" };
            SelectListItem Item5 = new SelectListItem { Text = "Celular Contacto", Value = "Celular Contacto" };
            SelectListItem Item6 = new SelectListItem { Text = "Nombre Contacto", Value = "Nombre Contacto" };

                 if (select == "Proveedores I D") { Item1.Selected = true; }
            else if (select == "Nombre Empresa") { Item2.Selected = true; }
            else if (select == "Direccion") { Item3.Selected = true; }
            else if (select == "Telefono") { Item4.Selected = true; }
            else if (select == "Celular Contacto") { Item5.Selected = true; }
            else if (select == "Nombre Contacto") { Item6.Selected = true; }

            list.Add(Item1);
            list.Add(Item2);
            list.Add(Item3);
            list.Add(Item4);
            list.Add(Item5);
            list.Add(Item6);

            return list.ToList();
        }

        private void ExportData(String Export, GridView gv, DataTable dt)
        {
            if (Export == "Pdf")
            {
                PDFform pdfForm = new PDFform(dt, "Dbo.Proveedores", "Many");
                Document document = pdfForm.CreateDocument();
                PdfDocumentRenderer renderer = new PdfDocumentRenderer(true);
                renderer.Document = document;
                renderer.RenderDocument();

                MemoryStream stream = new MemoryStream();
                renderer.PdfDocument.Save(stream, false);

                Response.Clear();
                Response.AddHeader("content-disposition", "attachment;filename=" + "Report.pdf");
                Response.ContentType = "application/Pdf.pdf";
                Response.BinaryWrite(stream.ToArray());
                Response.Flush();
                Response.End();
            }
            else
            {
                Response.ClearContent();
                Response.Buffer = true;
                if (Export == "Excel")
                {
                    Response.AddHeader("content-disposition", "attachment;filename=" + "Report.xls");
                    Response.ContentType = "application/Excel.xls";
                }
                else if (Export == "Word")
                {
                    Response.AddHeader("content-disposition", "attachment;filename=" + "Report.doc");
                    Response.ContentType = "application/Word.doc";
                }
                Response.Charset = "";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                gv.RenderControl(htw);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }

    }
}
 
