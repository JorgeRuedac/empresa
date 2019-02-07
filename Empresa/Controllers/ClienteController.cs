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
    public class ClienteController : Controller
    {

        DataTable dtCliente = new DataTable();

        // GET: /Cliente/
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

            ViewData["SearchFields"] = GetFields((Session["SearchField"] == null ? "Cliente I D" : Convert.ToString(Session["SearchField"])));
            ViewData["SearchConditions"] = Library.GetConditions((Session["SearchCondition"] == null ? "Contains" : Convert.ToString(Session["SearchCondition"])));
            ViewData["SearchText"] = Session["SearchText"];
            ViewData["Exports"] = Library.GetExports((Session["Export"] == null ? "Pdf" : Convert.ToString(Session["Export"])));
            ViewData["PageSizes"] = Library.GetPageSizes();

            ViewData["CurrentSort"] = sortOrder;
            ViewData["Cliente_IDSortParm"] = sortOrder == "Cliente_ID_asc" ? "Cliente_ID_desc" : "Cliente_ID_asc";
            ViewData["nombreSortParm"] = sortOrder == "nombre_asc" ? "nombre_desc" : "nombre_asc";
            ViewData["ApellidoSortParm"] = sortOrder == "Apellido_asc" ? "Apellido_desc" : "Apellido_asc";
            ViewData["DireccionSortParm"] = sortOrder == "Direccion_asc" ? "Direccion_desc" : "Direccion_asc";
            ViewData["TelefonoSortParm"] = sortOrder == "Telefono_asc" ? "Telefono_desc" : "Telefono_asc";
            ViewData["CorreoSortParm"] = sortOrder == "Correo_asc" ? "Correo_desc" : "Correo_asc";

            dtCliente = ClienteData.SelectAll();

            try
            {
                if (!string.IsNullOrEmpty(Convert.ToString(Session["SearchField"])) & !string.IsNullOrEmpty(Convert.ToString(Session["SearchCondition"])) & !string.IsNullOrEmpty(Convert.ToString(Session["SearchText"])))
                {
                    dtCliente = ClienteData.Search(Convert.ToString(Session["SearchField"]), Convert.ToString(Session["SearchCondition"]), Convert.ToString(Session["SearchText"]));
                }
            }
            catch { }

            var Query = from rowCliente in dtCliente.AsEnumerable()
                        select new Cliente() {
                            Cliente_ID = rowCliente.Field<Int32>("Cliente_ID")
                           ,nombre = rowCliente.Field<String>("nombre")
                           ,Apellido = rowCliente.Field<String>("Apellido")
                           ,Direccion = rowCliente.Field<String>("Direccion")
                           ,Telefono = rowCliente.Field<String>("Telefono")
                           ,Correo = rowCliente.Field<String>("Correo")
                        };

            switch (sortOrder)
            {
                case "Cliente_ID_desc":
                    Query = Query.OrderByDescending(s => s.Cliente_ID);
                    break;
                case "Cliente_ID_asc":
                    Query = Query.OrderBy(s => s.Cliente_ID);
                    break;
                case "nombre_desc":
                    Query = Query.OrderByDescending(s => s.nombre);
                    break;
                case "nombre_asc":
                    Query = Query.OrderBy(s => s.nombre);
                    break;
                case "Apellido_desc":
                    Query = Query.OrderByDescending(s => s.Apellido);
                    break;
                case "Apellido_asc":
                    Query = Query.OrderBy(s => s.Apellido);
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
                case "Correo_desc":
                    Query = Query.OrderByDescending(s => s.Correo);
                    break;
                case "Correo_asc":
                    Query = Query.OrderBy(s => s.Correo);
                    break;
                default:  // Name ascending 
                    Query = Query.OrderBy(s => s.Cliente_ID);
                    break;
            }

            if (command == "Export") {
                GridView gv = new GridView();
                DataTable dt = new DataTable();
                dt.Columns.Add("Cliente I D", typeof(string));
                dt.Columns.Add("Nombre", typeof(string));
                dt.Columns.Add("Apellido", typeof(string));
                dt.Columns.Add("Direccion", typeof(string));
                dt.Columns.Add("Telefono", typeof(string));
                dt.Columns.Add("Correo", typeof(string));
                foreach (var item in Query)
                {
                    dt.Rows.Add(
                        item.Cliente_ID
                       ,item.nombre
                       ,item.Apellido
                       ,item.Direccion
                       ,item.Telefono
                       ,item.Correo
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

        // GET: /Cliente/Details/<id>
        public ActionResult Details(
                                      Int32? Cliente_ID
                                   )
        {
            if (
                    Cliente_ID == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            Cliente Cliente = new Cliente();
            Cliente.Cliente_ID = System.Convert.ToInt32(Cliente_ID);
            Cliente = ClienteData.Select_Record(Cliente);

            if (Cliente == null)
            {
                return HttpNotFound();
            }
            return View(Cliente);
        }

        // GET: /Cliente/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: /Cliente/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include=
				           "Cliente_ID"
				   + "," + "nombre"
				   + "," + "Apellido"
				   + "," + "Direccion"
				   + "," + "Telefono"
				   + "," + "Correo"
				  )] Cliente Cliente)
        {
            if (ModelState.IsValid)
            {
                bool bSucess = false;
                bSucess = ClienteData.Add(Cliente);
                if (bSucess == true)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Can Not Insert");
                }
            }

            return View(Cliente);
        }

        // GET: /Cliente/Edit/<id>
        public ActionResult Edit(
                                   Int32? Cliente_ID
                                )
        {
            if (
                    Cliente_ID == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Cliente Cliente = new Cliente();
            Cliente.Cliente_ID = System.Convert.ToInt32(Cliente_ID);
            Cliente = ClienteData.Select_Record(Cliente);

            if (Cliente == null)
            {
                return HttpNotFound();
            }

            return View(Cliente);
        }

        // POST: /Cliente/Edit/<id>
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Cliente Cliente)
        {

            Cliente oCliente = new Cliente();
            oCliente.Cliente_ID = System.Convert.ToInt32(Cliente.Cliente_ID);
            oCliente = ClienteData.Select_Record(Cliente);

            if (ModelState.IsValid)
            {
                bool bSucess = false;
                bSucess = ClienteData.Update(oCliente, Cliente);
                if (bSucess == true)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Can Not Update");
                }
            }

            return View(Cliente);
        }

        // GET: /Cliente/Delete/<id>
        public ActionResult Delete(
                                     Int32? Cliente_ID
                                  )
        {
            if (
                    Cliente_ID == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            Cliente Cliente = new Cliente();
            Cliente.Cliente_ID = System.Convert.ToInt32(Cliente_ID);
            Cliente = ClienteData.Select_Record(Cliente);

            if (Cliente == null)
            {
                return HttpNotFound();
            }
            return View(Cliente);
        }

        // POST: /Cliente/Delete/<id>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(
                                            Int32? Cliente_ID
                                            )
        {

            Cliente Cliente = new Cliente();
            Cliente.Cliente_ID = System.Convert.ToInt32(Cliente_ID);
            Cliente = ClienteData.Select_Record(Cliente);

            bool bSucess = false;
            bSucess = ClienteData.Delete(Cliente);
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
            SelectListItem Item1 = new SelectListItem { Text = "Cliente I D", Value = "Cliente I D" };
            SelectListItem Item2 = new SelectListItem { Text = "Nombre", Value = "Nombre" };
            SelectListItem Item3 = new SelectListItem { Text = "Apellido", Value = "Apellido" };
            SelectListItem Item4 = new SelectListItem { Text = "Direccion", Value = "Direccion" };
            SelectListItem Item5 = new SelectListItem { Text = "Telefono", Value = "Telefono" };
            SelectListItem Item6 = new SelectListItem { Text = "Correo", Value = "Correo" };

                 if (select == "Cliente I D") { Item1.Selected = true; }
            else if (select == "Nombre") { Item2.Selected = true; }
            else if (select == "Apellido") { Item3.Selected = true; }
            else if (select == "Direccion") { Item4.Selected = true; }
            else if (select == "Telefono") { Item5.Selected = true; }
            else if (select == "Correo") { Item6.Selected = true; }

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
                PDFform pdfForm = new PDFform(dt, "Dbo. Cliente", "Many");
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
 
