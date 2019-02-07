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
    public class BodegaController : Controller
    {

        DataTable dtBodega = new DataTable();

        // GET: /Bodega/
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

            ViewData["SearchFields"] = GetFields((Session["SearchField"] == null ? "Bodega I D" : Convert.ToString(Session["SearchField"])));
            ViewData["SearchConditions"] = Library.GetConditions((Session["SearchCondition"] == null ? "Contains" : Convert.ToString(Session["SearchCondition"])));
            ViewData["SearchText"] = Session["SearchText"];
            ViewData["Exports"] = Library.GetExports((Session["Export"] == null ? "Pdf" : Convert.ToString(Session["Export"])));
            ViewData["PageSizes"] = Library.GetPageSizes();

            ViewData["CurrentSort"] = sortOrder;
            ViewData["BodegaIDSortParm"] = sortOrder == "BodegaID_asc" ? "BodegaID_desc" : "BodegaID_asc";
            ViewData["ProductosIDSortParm"] = sortOrder == "ProductosID_asc" ? "ProductosID_desc" : "ProductosID_asc";
            ViewData["ObservacionSortParm"] = sortOrder == "Observacion_asc" ? "Observacion_desc" : "Observacion_asc";

            dtBodega = BodegaData.SelectAll();

            try
            {
                if (!string.IsNullOrEmpty(Convert.ToString(Session["SearchField"])) & !string.IsNullOrEmpty(Convert.ToString(Session["SearchCondition"])) & !string.IsNullOrEmpty(Convert.ToString(Session["SearchText"])))
                {
                    dtBodega = BodegaData.Search(Convert.ToString(Session["SearchField"]), Convert.ToString(Session["SearchCondition"]), Convert.ToString(Session["SearchText"]));
                }
            }
            catch { }

            var Query = from rowBodega in dtBodega.AsEnumerable()
                        select new Bodega() {
                            BodegaID = rowBodega.Field<String>("BodegaID")
                           ,ProductosID = rowBodega.Field<String>("ProductosID")
                           ,Observacion = rowBodega.Field<String>("Observacion")
                        };

            switch (sortOrder)
            {
                case "BodegaID_desc":
                    Query = Query.OrderByDescending(s => s.BodegaID);
                    break;
                case "BodegaID_asc":
                    Query = Query.OrderBy(s => s.BodegaID);
                    break;
                case "ProductosID_desc":
                    Query = Query.OrderByDescending(s => s.ProductosID);
                    break;
                case "ProductosID_asc":
                    Query = Query.OrderBy(s => s.ProductosID);
                    break;
                case "Observacion_desc":
                    Query = Query.OrderByDescending(s => s.Observacion);
                    break;
                case "Observacion_asc":
                    Query = Query.OrderBy(s => s.Observacion);
                    break;
                default:  // Name ascending 
                    Query = Query.OrderBy(s => s.BodegaID);
                    break;
            }

            if (command == "Export") {
                GridView gv = new GridView();
                DataTable dt = new DataTable();
                dt.Columns.Add("Bodega I D", typeof(string));
                dt.Columns.Add("Productos I D", typeof(string));
                dt.Columns.Add("Observacion", typeof(string));
                foreach (var item in Query)
                {
                    dt.Rows.Add(
                        item.BodegaID
                       ,item.ProductosID
                       ,item.Observacion
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

        // GET: /Bodega/Details/<id>
        public ActionResult Details(
                                      String BodegaID
                                   )
        {
            if (
                    BodegaID == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            Bodega Bodega = new Bodega();
            Bodega.BodegaID = System.Convert.ToString(BodegaID);
            Bodega = BodegaData.Select_Record(Bodega);

            if (Bodega == null)
            {
                return HttpNotFound();
            }
            return View(Bodega);
        }

        // GET: /Bodega/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: /Bodega/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include=
				           "BodegaID"
				   + "," + "ProductosID"
				   + "," + "Observacion"
				  )] Bodega Bodega)
        {
            if (ModelState.IsValid)
            {
                bool bSucess = false;
                bSucess = BodegaData.Add(Bodega);
                if (bSucess == true)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Can Not Insert");
                }
            }

            return View(Bodega);
        }

        // GET: /Bodega/Edit/<id>
        public ActionResult Edit(
                                   String BodegaID
                                )
        {
            if (
                    BodegaID == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Bodega Bodega = new Bodega();
            Bodega.BodegaID = System.Convert.ToString(BodegaID);
            Bodega = BodegaData.Select_Record(Bodega);

            if (Bodega == null)
            {
                return HttpNotFound();
            }

            return View(Bodega);
        }

        // POST: /Bodega/Edit/<id>
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Bodega Bodega)
        {

            Bodega oBodega = new Bodega();
            oBodega.BodegaID = System.Convert.ToString(Bodega.BodegaID);
            oBodega = BodegaData.Select_Record(Bodega);

            if (ModelState.IsValid)
            {
                bool bSucess = false;
                bSucess = BodegaData.Update(oBodega, Bodega);
                if (bSucess == true)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Can Not Update");
                }
            }

            return View(Bodega);
        }

        // GET: /Bodega/Delete/<id>
        public ActionResult Delete(
                                     String BodegaID
                                  )
        {
            if (
                    BodegaID == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            Bodega Bodega = new Bodega();
            Bodega.BodegaID = System.Convert.ToString(BodegaID);
            Bodega = BodegaData.Select_Record(Bodega);

            if (Bodega == null)
            {
                return HttpNotFound();
            }
            return View(Bodega);
        }

        // POST: /Bodega/Delete/<id>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(
                                            String BodegaID
                                            )
        {

            Bodega Bodega = new Bodega();
            Bodega.BodegaID = System.Convert.ToString(BodegaID);
            Bodega = BodegaData.Select_Record(Bodega);

            bool bSucess = false;
            bSucess = BodegaData.Delete(Bodega);
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
            SelectListItem Item1 = new SelectListItem { Text = "Bodega I D", Value = "Bodega I D" };
            SelectListItem Item2 = new SelectListItem { Text = "Productos I D", Value = "Productos I D" };
            SelectListItem Item3 = new SelectListItem { Text = "Observacion", Value = "Observacion" };

                 if (select == "Bodega I D") { Item1.Selected = true; }
            else if (select == "Productos I D") { Item2.Selected = true; }
            else if (select == "Observacion") { Item3.Selected = true; }

            list.Add(Item1);
            list.Add(Item2);
            list.Add(Item3);

            return list.ToList();
        }

        private void ExportData(String Export, GridView gv, DataTable dt)
        {
            if (Export == "Pdf")
            {
                PDFform pdfForm = new PDFform(dt, "Dbo. Bodega", "Many");
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
 
