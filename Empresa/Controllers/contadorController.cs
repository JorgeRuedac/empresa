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
    public class contadorController : Controller
    {

        DataTable dtcontador = new DataTable();

        // GET: /contador/
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

            ViewData["SearchFields"] = GetFields((Session["SearchField"] == null ? "Contador Id" : Convert.ToString(Session["SearchField"])));
            ViewData["SearchConditions"] = Library.GetConditions((Session["SearchCondition"] == null ? "Contains" : Convert.ToString(Session["SearchCondition"])));
            ViewData["SearchText"] = Session["SearchText"];
            ViewData["Exports"] = Library.GetExports((Session["Export"] == null ? "Pdf" : Convert.ToString(Session["Export"])));
            ViewData["PageSizes"] = Library.GetPageSizes();

            ViewData["CurrentSort"] = sortOrder;
            ViewData["contador_idSortParm"] = sortOrder == "contador_id_asc" ? "contador_id_desc" : "contador_id_asc";
            ViewData["nombreSortParm"] = sortOrder == "nombre_asc" ? "nombre_desc" : "nombre_asc";
            ViewData["apellidoSortParm"] = sortOrder == "apellido_asc" ? "apellido_desc" : "apellido_asc";
            ViewData["direccionSortParm"] = sortOrder == "direccion_asc" ? "direccion_desc" : "direccion_asc";
            ViewData["contactoSortParm"] = sortOrder == "contacto_asc" ? "contacto_desc" : "contacto_asc";

            dtcontador = contadorData.SelectAll();

            try
            {
                if (!string.IsNullOrEmpty(Convert.ToString(Session["SearchField"])) & !string.IsNullOrEmpty(Convert.ToString(Session["SearchCondition"])) & !string.IsNullOrEmpty(Convert.ToString(Session["SearchText"])))
                {
                    dtcontador = contadorData.Search(Convert.ToString(Session["SearchField"]), Convert.ToString(Session["SearchCondition"]), Convert.ToString(Session["SearchText"]));
                }
            }
            catch { }

            var Query = from rowcontador in dtcontador.AsEnumerable()
                        select new contador() {
                            contador_id = rowcontador.Field<Int32>("contador_id")
                           ,nombre = rowcontador.Field<String>("nombre")
                           ,apellido = rowcontador.Field<String>("apellido")
                           ,direccion = rowcontador.Field<String>("direccion")
                           ,contacto = rowcontador.Field<String>("contacto")
                        };

            switch (sortOrder)
            {
                case "contador_id_desc":
                    Query = Query.OrderByDescending(s => s.contador_id);
                    break;
                case "contador_id_asc":
                    Query = Query.OrderBy(s => s.contador_id);
                    break;
                case "nombre_desc":
                    Query = Query.OrderByDescending(s => s.nombre);
                    break;
                case "nombre_asc":
                    Query = Query.OrderBy(s => s.nombre);
                    break;
                case "apellido_desc":
                    Query = Query.OrderByDescending(s => s.apellido);
                    break;
                case "apellido_asc":
                    Query = Query.OrderBy(s => s.apellido);
                    break;
                case "direccion_desc":
                    Query = Query.OrderByDescending(s => s.direccion);
                    break;
                case "direccion_asc":
                    Query = Query.OrderBy(s => s.direccion);
                    break;
                case "contacto_desc":
                    Query = Query.OrderByDescending(s => s.contacto);
                    break;
                case "contacto_asc":
                    Query = Query.OrderBy(s => s.contacto);
                    break;
                default:  // Name ascending 
                    Query = Query.OrderBy(s => s.contador_id);
                    break;
            }

            if (command == "Export") {
                GridView gv = new GridView();
                DataTable dt = new DataTable();
                dt.Columns.Add("Contador Id", typeof(string));
                dt.Columns.Add("Nombre", typeof(string));
                dt.Columns.Add("Apellido", typeof(string));
                dt.Columns.Add("Direccion", typeof(string));
                dt.Columns.Add("Contacto", typeof(string));
                foreach (var item in Query)
                {
                    dt.Rows.Add(
                        item.contador_id
                       ,item.nombre
                       ,item.apellido
                       ,item.direccion
                       ,item.contacto
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

        // GET: /contador/Details/<id>
        public ActionResult Details(
                                      Int32? contador_id
                                   )
        {
            if (
                    contador_id == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            contador contador = new contador();
            contador.contador_id = System.Convert.ToInt32(contador_id);
            contador = contadorData.Select_Record(contador);

            if (contador == null)
            {
                return HttpNotFound();
            }
            return View(contador);
        }

        // GET: /contador/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: /contador/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include=
				           "contador_id"
				   + "," + "nombre"
				   + "," + "apellido"
				   + "," + "direccion"
				   + "," + "contacto"
				  )] contador contador)
        {
            if (ModelState.IsValid)
            {
                bool bSucess = false;
                bSucess = contadorData.Add(contador);
                if (bSucess == true)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Can Not Insert");
                }
            }

            return View(contador);
        }

        // GET: /contador/Edit/<id>
        public ActionResult Edit(
                                   Int32? contador_id
                                )
        {
            if (
                    contador_id == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            contador contador = new contador();
            contador.contador_id = System.Convert.ToInt32(contador_id);
            contador = contadorData.Select_Record(contador);

            if (contador == null)
            {
                return HttpNotFound();
            }

            return View(contador);
        }

        // POST: /contador/Edit/<id>
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(contador contador)
        {

            contador ocontador = new contador();
            ocontador.contador_id = System.Convert.ToInt32(contador.contador_id);
            ocontador = contadorData.Select_Record(contador);

            if (ModelState.IsValid)
            {
                bool bSucess = false;
                bSucess = contadorData.Update(ocontador, contador);
                if (bSucess == true)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Can Not Update");
                }
            }

            return View(contador);
        }

        // GET: /contador/Delete/<id>
        public ActionResult Delete(
                                     Int32? contador_id
                                  )
        {
            if (
                    contador_id == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            contador contador = new contador();
            contador.contador_id = System.Convert.ToInt32(contador_id);
            contador = contadorData.Select_Record(contador);

            if (contador == null)
            {
                return HttpNotFound();
            }
            return View(contador);
        }

        // POST: /contador/Delete/<id>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(
                                            Int32? contador_id
                                            )
        {

            contador contador = new contador();
            contador.contador_id = System.Convert.ToInt32(contador_id);
            contador = contadorData.Select_Record(contador);

            bool bSucess = false;
            bSucess = contadorData.Delete(contador);
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
            SelectListItem Item1 = new SelectListItem { Text = "Contador Id", Value = "Contador Id" };
            SelectListItem Item2 = new SelectListItem { Text = "Nombre", Value = "Nombre" };
            SelectListItem Item3 = new SelectListItem { Text = "Apellido", Value = "Apellido" };
            SelectListItem Item4 = new SelectListItem { Text = "Direccion", Value = "Direccion" };
            SelectListItem Item5 = new SelectListItem { Text = "Contacto", Value = "Contacto" };

                 if (select == "Contador Id") { Item1.Selected = true; }
            else if (select == "Nombre") { Item2.Selected = true; }
            else if (select == "Apellido") { Item3.Selected = true; }
            else if (select == "Direccion") { Item4.Selected = true; }
            else if (select == "Contacto") { Item5.Selected = true; }

            list.Add(Item1);
            list.Add(Item2);
            list.Add(Item3);
            list.Add(Item4);
            list.Add(Item5);

            return list.ToList();
        }

        private void ExportData(String Export, GridView gv, DataTable dt)
        {
            if (Export == "Pdf")
            {
                PDFform pdfForm = new PDFform(dt, "Dbo.Contador", "Many");
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
 
