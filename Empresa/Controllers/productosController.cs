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
    public class productosController : Controller
    {

        DataTable dtproductos = new DataTable();

        // GET: /productos/
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

            ViewData["SearchFields"] = GetFields((Session["SearchField"] == null ? "Productos I D" : Convert.ToString(Session["SearchField"])));
            ViewData["SearchConditions"] = Library.GetConditions((Session["SearchCondition"] == null ? "Contains" : Convert.ToString(Session["SearchCondition"])));
            ViewData["SearchText"] = Session["SearchText"];
            ViewData["Exports"] = Library.GetExports((Session["Export"] == null ? "Pdf" : Convert.ToString(Session["Export"])));
            ViewData["PageSizes"] = Library.GetPageSizes();

            ViewData["CurrentSort"] = sortOrder;
            ViewData["ProductosIDSortParm"] = sortOrder == "ProductosID_asc" ? "ProductosID_desc" : "ProductosID_asc";
            ViewData["Nombre_productoSortParm"] = sortOrder == "Nombre_producto_asc" ? "Nombre_producto_desc" : "Nombre_producto_asc";
            ViewData["Precio_unitarioSortParm"] = sortOrder == "Precio_unitario_asc" ? "Precio_unitario_desc" : "Precio_unitario_asc";
            ViewData["Precio_venta_publicoSortParm"] = sortOrder == "Precio_venta_publico_asc" ? "Precio_venta_publico_desc" : "Precio_venta_publico_asc";
            ViewData["Fecha_fabricacionSortParm"] = sortOrder == "Fecha_fabricacion_asc" ? "Fecha_fabricacion_desc" : "Fecha_fabricacion_asc";
            ViewData["Fecha_caducidadSortParm"] = sortOrder == "Fecha_caducidad_asc" ? "Fecha_caducidad_desc" : "Fecha_caducidad_asc";
            ViewData["IvaSortParm"] = sortOrder == "Iva_asc" ? "Iva_desc" : "Iva_asc";

            dtproductos = productosData.SelectAll();

            try
            {
                if (!string.IsNullOrEmpty(Convert.ToString(Session["SearchField"])) & !string.IsNullOrEmpty(Convert.ToString(Session["SearchCondition"])) & !string.IsNullOrEmpty(Convert.ToString(Session["SearchText"])))
                {
                    dtproductos = productosData.Search(Convert.ToString(Session["SearchField"]), Convert.ToString(Session["SearchCondition"]), Convert.ToString(Session["SearchText"]));
                }
            }
            catch { }

            var Query = from rowproductos in dtproductos.AsEnumerable()
                        select new productos() {
                            ProductosID = rowproductos.Field<Int32>("ProductosID")
                           ,Nombre_producto = rowproductos.Field<String>("Nombre_producto")
                           ,Precio_unitario = rowproductos.Field<String>("Precio_unitario")
                           ,Precio_venta_publico = rowproductos.Field<String>("Precio_venta_publico")
                           ,Fecha_fabricacion = rowproductos.Field<String>("Fecha_fabricacion")
                           ,Fecha_caducidad = rowproductos.Field<String>("Fecha_caducidad")
                           ,Iva = rowproductos.Field<String>("Iva")
                        };

            switch (sortOrder)
            {
                case "ProductosID_desc":
                    Query = Query.OrderByDescending(s => s.ProductosID);
                    break;
                case "ProductosID_asc":
                    Query = Query.OrderBy(s => s.ProductosID);
                    break;
                case "Nombre_producto_desc":
                    Query = Query.OrderByDescending(s => s.Nombre_producto);
                    break;
                case "Nombre_producto_asc":
                    Query = Query.OrderBy(s => s.Nombre_producto);
                    break;
                case "Precio_unitario_desc":
                    Query = Query.OrderByDescending(s => s.Precio_unitario);
                    break;
                case "Precio_unitario_asc":
                    Query = Query.OrderBy(s => s.Precio_unitario);
                    break;
                case "Precio_venta_publico_desc":
                    Query = Query.OrderByDescending(s => s.Precio_venta_publico);
                    break;
                case "Precio_venta_publico_asc":
                    Query = Query.OrderBy(s => s.Precio_venta_publico);
                    break;
                case "Fecha_fabricacion_desc":
                    Query = Query.OrderByDescending(s => s.Fecha_fabricacion);
                    break;
                case "Fecha_fabricacion_asc":
                    Query = Query.OrderBy(s => s.Fecha_fabricacion);
                    break;
                case "Fecha_caducidad_desc":
                    Query = Query.OrderByDescending(s => s.Fecha_caducidad);
                    break;
                case "Fecha_caducidad_asc":
                    Query = Query.OrderBy(s => s.Fecha_caducidad);
                    break;
                case "Iva_desc":
                    Query = Query.OrderByDescending(s => s.Iva);
                    break;
                case "Iva_asc":
                    Query = Query.OrderBy(s => s.Iva);
                    break;
                default:  // Name ascending 
                    Query = Query.OrderBy(s => s.ProductosID);
                    break;
            }

            if (command == "Export") {
                GridView gv = new GridView();
                DataTable dt = new DataTable();
                dt.Columns.Add("Productos I D", typeof(string));
                dt.Columns.Add("Nombre Producto", typeof(string));
                dt.Columns.Add("Precio Unitario", typeof(string));
                dt.Columns.Add("Precio Venta Publico", typeof(string));
                dt.Columns.Add("Fecha Fabricacion", typeof(string));
                dt.Columns.Add("Fecha Caducidad", typeof(string));
                dt.Columns.Add("Iva", typeof(string));
                foreach (var item in Query)
                {
                    dt.Rows.Add(
                        item.ProductosID
                       ,item.Nombre_producto
                       ,item.Precio_unitario
                       ,item.Precio_venta_publico
                       ,item.Fecha_fabricacion
                       ,item.Fecha_caducidad
                       ,item.Iva
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

        // GET: /productos/Details/<id>
        public ActionResult Details(
                                      Int32? ProductosID
                                   )
        {
            if (
                    ProductosID == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            productos productos = new productos();
            productos.ProductosID = System.Convert.ToInt32(ProductosID);
            productos = productosData.Select_Record(productos);

            if (productos == null)
            {
                return HttpNotFound();
            }
            return View(productos);
        }

        // GET: /productos/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: /productos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include=
				           "ProductosID"
				   + "," + "Nombre_producto"
				   + "," + "Precio_unitario"
				   + "," + "Precio_venta_publico"
				   + "," + "Fecha_fabricacion"
				   + "," + "Fecha_caducidad"
				   + "," + "Iva"
				  )] productos productos)
        {
            if (ModelState.IsValid)
            {
                bool bSucess = false;
                bSucess = productosData.Add(productos);
                if (bSucess == true)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Can Not Insert");
                }
            }

            return View(productos);
        }

        // GET: /productos/Edit/<id>
        public ActionResult Edit(
                                   Int32? ProductosID
                                )
        {
            if (
                    ProductosID == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            productos productos = new productos();
            productos.ProductosID = System.Convert.ToInt32(ProductosID);
            productos = productosData.Select_Record(productos);

            if (productos == null)
            {
                return HttpNotFound();
            }

            return View(productos);
        }

        // POST: /productos/Edit/<id>
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(productos productos)
        {

            productos oproductos = new productos();
            oproductos.ProductosID = System.Convert.ToInt32(productos.ProductosID);
            oproductos = productosData.Select_Record(productos);

            if (ModelState.IsValid)
            {
                bool bSucess = false;
                bSucess = productosData.Update(oproductos, productos);
                if (bSucess == true)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Can Not Update");
                }
            }

            return View(productos);
        }

        // GET: /productos/Delete/<id>
        public ActionResult Delete(
                                     Int32? ProductosID
                                  )
        {
            if (
                    ProductosID == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            productos productos = new productos();
            productos.ProductosID = System.Convert.ToInt32(ProductosID);
            productos = productosData.Select_Record(productos);

            if (productos == null)
            {
                return HttpNotFound();
            }
            return View(productos);
        }

        // POST: /productos/Delete/<id>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(
                                            Int32? ProductosID
                                            )
        {

            productos productos = new productos();
            productos.ProductosID = System.Convert.ToInt32(ProductosID);
            productos = productosData.Select_Record(productos);

            bool bSucess = false;
            bSucess = productosData.Delete(productos);
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
            SelectListItem Item1 = new SelectListItem { Text = "Productos I D", Value = "Productos I D" };
            SelectListItem Item2 = new SelectListItem { Text = "Nombre Producto", Value = "Nombre Producto" };
            SelectListItem Item3 = new SelectListItem { Text = "Precio Unitario", Value = "Precio Unitario" };
            SelectListItem Item4 = new SelectListItem { Text = "Precio Venta Publico", Value = "Precio Venta Publico" };
            SelectListItem Item5 = new SelectListItem { Text = "Fecha Fabricacion", Value = "Fecha Fabricacion" };
            SelectListItem Item6 = new SelectListItem { Text = "Fecha Caducidad", Value = "Fecha Caducidad" };
            SelectListItem Item7 = new SelectListItem { Text = "Iva", Value = "Iva" };

                 if (select == "Productos I D") { Item1.Selected = true; }
            else if (select == "Nombre Producto") { Item2.Selected = true; }
            else if (select == "Precio Unitario") { Item3.Selected = true; }
            else if (select == "Precio Venta Publico") { Item4.Selected = true; }
            else if (select == "Fecha Fabricacion") { Item5.Selected = true; }
            else if (select == "Fecha Caducidad") { Item6.Selected = true; }
            else if (select == "Iva") { Item7.Selected = true; }

            list.Add(Item1);
            list.Add(Item2);
            list.Add(Item3);
            list.Add(Item4);
            list.Add(Item5);
            list.Add(Item6);
            list.Add(Item7);

            return list.ToList();
        }

        private void ExportData(String Export, GridView gv, DataTable dt)
        {
            if (Export == "Pdf")
            {
                PDFform pdfForm = new PDFform(dt, "Dbo.Productos", "Many");
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
 
