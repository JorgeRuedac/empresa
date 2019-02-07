using System;
using System.Data;
using System.Data.SqlClient;
using Empresa.Models;

namespace Empresa.Data
{
    public class productosData
    {

        public static DataTable SelectAll()
        {
            SqlConnection connection = empresaData.GetConnection();
            string selectStatement
                = "SELECT "  
                + "     [productos].[ProductosID] "
                + "    ,[productos].[Nombre_producto] "
                + "    ,[productos].[Precio_unitario] "
                + "    ,[productos].[Precio_venta_publico] "
                + "    ,[productos].[Fecha_fabricacion] "
                + "    ,[productos].[Fecha_caducidad] "
                + "    ,[productos].[Iva] "
                + "FROM " 
                + "     [productos] " 
                + "";
            SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
            selectCommand.CommandType = CommandType.Text;
            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                SqlDataReader reader = selectCommand.ExecuteReader();
                if (reader.HasRows) {
                    dt.Load(reader); }
                reader.Close();
            }
            catch (SqlException)
            {
                return dt;
            }
            finally
            {
                connection.Close();
            }
            return dt;
        }

        public static DataTable Search(string sField, string sCondition, string sValue)
        {
            SqlConnection connection = empresaData.GetConnection();
            string selectStatement = "";
            if (sCondition == "Contains") {
                selectStatement
                    = "SELECT "
                + "     [productos].[ProductosID] "
                + "    ,[productos].[Nombre_producto] "
                + "    ,[productos].[Precio_unitario] "
                + "    ,[productos].[Precio_venta_publico] "
                + "    ,[productos].[Fecha_fabricacion] "
                + "    ,[productos].[Fecha_caducidad] "
                + "    ,[productos].[Iva] "
                + "FROM " 
                + "     [productos] " 
                    + "WHERE " 
                    + "     (@ProductosID IS NULL OR @ProductosID = '' OR [productos].[ProductosID] LIKE '%' + LTRIM(RTRIM(@ProductosID)) + '%') " 
                    + "AND   (@Nombre_producto IS NULL OR @Nombre_producto = '' OR [productos].[Nombre_producto] LIKE '%' + LTRIM(RTRIM(@Nombre_producto)) + '%') " 
                    + "AND   (@Precio_unitario IS NULL OR @Precio_unitario = '' OR [productos].[Precio_unitario] LIKE '%' + LTRIM(RTRIM(@Precio_unitario)) + '%') " 
                    + "AND   (@Precio_venta_publico IS NULL OR @Precio_venta_publico = '' OR [productos].[Precio_venta_publico] LIKE '%' + LTRIM(RTRIM(@Precio_venta_publico)) + '%') " 
                    + "AND   (@Fecha_fabricacion IS NULL OR @Fecha_fabricacion = '' OR [productos].[Fecha_fabricacion] LIKE '%' + LTRIM(RTRIM(@Fecha_fabricacion)) + '%') " 
                    + "AND   (@Fecha_caducidad IS NULL OR @Fecha_caducidad = '' OR [productos].[Fecha_caducidad] LIKE '%' + LTRIM(RTRIM(@Fecha_caducidad)) + '%') " 
                    + "AND   (@Iva IS NULL OR @Iva = '' OR [productos].[Iva] LIKE '%' + LTRIM(RTRIM(@Iva)) + '%') " 
                    + "";
            } else if (sCondition == "Equals") {
                selectStatement
                    = "SELECT "
                + "     [productos].[ProductosID] "
                + "    ,[productos].[Nombre_producto] "
                + "    ,[productos].[Precio_unitario] "
                + "    ,[productos].[Precio_venta_publico] "
                + "    ,[productos].[Fecha_fabricacion] "
                + "    ,[productos].[Fecha_caducidad] "
                + "    ,[productos].[Iva] "
                + "FROM " 
                + "     [productos] " 
                    + "WHERE " 
                    + "     (@ProductosID IS NULL OR @ProductosID = '' OR [productos].[ProductosID] = LTRIM(RTRIM(@ProductosID))) " 
                    + "AND   (@Nombre_producto IS NULL OR @Nombre_producto = '' OR [productos].[Nombre_producto] = LTRIM(RTRIM(@Nombre_producto))) " 
                    + "AND   (@Precio_unitario IS NULL OR @Precio_unitario = '' OR [productos].[Precio_unitario] = LTRIM(RTRIM(@Precio_unitario))) " 
                    + "AND   (@Precio_venta_publico IS NULL OR @Precio_venta_publico = '' OR [productos].[Precio_venta_publico] = LTRIM(RTRIM(@Precio_venta_publico))) " 
                    + "AND   (@Fecha_fabricacion IS NULL OR @Fecha_fabricacion = '' OR [productos].[Fecha_fabricacion] = LTRIM(RTRIM(@Fecha_fabricacion))) " 
                    + "AND   (@Fecha_caducidad IS NULL OR @Fecha_caducidad = '' OR [productos].[Fecha_caducidad] = LTRIM(RTRIM(@Fecha_caducidad))) " 
                    + "AND   (@Iva IS NULL OR @Iva = '' OR [productos].[Iva] = LTRIM(RTRIM(@Iva))) " 
                    + "";
            } else if  (sCondition == "Starts with...") {
                selectStatement
                    = "SELECT "
                + "     [productos].[ProductosID] "
                + "    ,[productos].[Nombre_producto] "
                + "    ,[productos].[Precio_unitario] "
                + "    ,[productos].[Precio_venta_publico] "
                + "    ,[productos].[Fecha_fabricacion] "
                + "    ,[productos].[Fecha_caducidad] "
                + "    ,[productos].[Iva] "
                + "FROM " 
                + "     [productos] " 
                    + "WHERE " 
                    + "     (@ProductosID IS NULL OR @ProductosID = '' OR [productos].[ProductosID] LIKE LTRIM(RTRIM(@ProductosID)) + '%') " 
                    + "AND   (@Nombre_producto IS NULL OR @Nombre_producto = '' OR [productos].[Nombre_producto] LIKE LTRIM(RTRIM(@Nombre_producto)) + '%') " 
                    + "AND   (@Precio_unitario IS NULL OR @Precio_unitario = '' OR [productos].[Precio_unitario] LIKE LTRIM(RTRIM(@Precio_unitario)) + '%') " 
                    + "AND   (@Precio_venta_publico IS NULL OR @Precio_venta_publico = '' OR [productos].[Precio_venta_publico] LIKE LTRIM(RTRIM(@Precio_venta_publico)) + '%') " 
                    + "AND   (@Fecha_fabricacion IS NULL OR @Fecha_fabricacion = '' OR [productos].[Fecha_fabricacion] LIKE LTRIM(RTRIM(@Fecha_fabricacion)) + '%') " 
                    + "AND   (@Fecha_caducidad IS NULL OR @Fecha_caducidad = '' OR [productos].[Fecha_caducidad] LIKE LTRIM(RTRIM(@Fecha_caducidad)) + '%') " 
                    + "AND   (@Iva IS NULL OR @Iva = '' OR [productos].[Iva] LIKE LTRIM(RTRIM(@Iva)) + '%') " 
                    + "";
            } else if  (sCondition == "More than...") {
                selectStatement
                    = "SELECT "
                + "     [productos].[ProductosID] "
                + "    ,[productos].[Nombre_producto] "
                + "    ,[productos].[Precio_unitario] "
                + "    ,[productos].[Precio_venta_publico] "
                + "    ,[productos].[Fecha_fabricacion] "
                + "    ,[productos].[Fecha_caducidad] "
                + "    ,[productos].[Iva] "
                + "FROM " 
                + "     [productos] " 
                    + "WHERE " 
                    + "     (@ProductosID IS NULL OR @ProductosID = '' OR [productos].[ProductosID] > LTRIM(RTRIM(@ProductosID))) " 
                    + "AND   (@Nombre_producto IS NULL OR @Nombre_producto = '' OR [productos].[Nombre_producto] > LTRIM(RTRIM(@Nombre_producto))) " 
                    + "AND   (@Precio_unitario IS NULL OR @Precio_unitario = '' OR [productos].[Precio_unitario] > LTRIM(RTRIM(@Precio_unitario))) " 
                    + "AND   (@Precio_venta_publico IS NULL OR @Precio_venta_publico = '' OR [productos].[Precio_venta_publico] > LTRIM(RTRIM(@Precio_venta_publico))) " 
                    + "AND   (@Fecha_fabricacion IS NULL OR @Fecha_fabricacion = '' OR [productos].[Fecha_fabricacion] > LTRIM(RTRIM(@Fecha_fabricacion))) " 
                    + "AND   (@Fecha_caducidad IS NULL OR @Fecha_caducidad = '' OR [productos].[Fecha_caducidad] > LTRIM(RTRIM(@Fecha_caducidad))) " 
                    + "AND   (@Iva IS NULL OR @Iva = '' OR [productos].[Iva] > LTRIM(RTRIM(@Iva))) " 
                    + "";
            } else if  (sCondition == "Less than...") {
                selectStatement
                    = "SELECT " 
                + "     [productos].[ProductosID] "
                + "    ,[productos].[Nombre_producto] "
                + "    ,[productos].[Precio_unitario] "
                + "    ,[productos].[Precio_venta_publico] "
                + "    ,[productos].[Fecha_fabricacion] "
                + "    ,[productos].[Fecha_caducidad] "
                + "    ,[productos].[Iva] "
                + "FROM " 
                + "     [productos] " 
                    + "WHERE " 
                    + "     (@ProductosID IS NULL OR @ProductosID = '' OR [productos].[ProductosID] < LTRIM(RTRIM(@ProductosID))) " 
                    + "AND   (@Nombre_producto IS NULL OR @Nombre_producto = '' OR [productos].[Nombre_producto] < LTRIM(RTRIM(@Nombre_producto))) " 
                    + "AND   (@Precio_unitario IS NULL OR @Precio_unitario = '' OR [productos].[Precio_unitario] < LTRIM(RTRIM(@Precio_unitario))) " 
                    + "AND   (@Precio_venta_publico IS NULL OR @Precio_venta_publico = '' OR [productos].[Precio_venta_publico] < LTRIM(RTRIM(@Precio_venta_publico))) " 
                    + "AND   (@Fecha_fabricacion IS NULL OR @Fecha_fabricacion = '' OR [productos].[Fecha_fabricacion] < LTRIM(RTRIM(@Fecha_fabricacion))) " 
                    + "AND   (@Fecha_caducidad IS NULL OR @Fecha_caducidad = '' OR [productos].[Fecha_caducidad] < LTRIM(RTRIM(@Fecha_caducidad))) " 
                    + "AND   (@Iva IS NULL OR @Iva = '' OR [productos].[Iva] < LTRIM(RTRIM(@Iva))) " 
                    + "";
            } else if  (sCondition == "Equal or more than...") {
                selectStatement
                    = "SELECT "
                + "     [productos].[ProductosID] "
                + "    ,[productos].[Nombre_producto] "
                + "    ,[productos].[Precio_unitario] "
                + "    ,[productos].[Precio_venta_publico] "
                + "    ,[productos].[Fecha_fabricacion] "
                + "    ,[productos].[Fecha_caducidad] "
                + "    ,[productos].[Iva] "
                + "FROM " 
                + "     [productos] " 
                    + "WHERE " 
                    + "     (@ProductosID IS NULL OR @ProductosID = '' OR [productos].[ProductosID] >= LTRIM(RTRIM(@ProductosID))) " 
                    + "AND   (@Nombre_producto IS NULL OR @Nombre_producto = '' OR [productos].[Nombre_producto] >= LTRIM(RTRIM(@Nombre_producto))) " 
                    + "AND   (@Precio_unitario IS NULL OR @Precio_unitario = '' OR [productos].[Precio_unitario] >= LTRIM(RTRIM(@Precio_unitario))) " 
                    + "AND   (@Precio_venta_publico IS NULL OR @Precio_venta_publico = '' OR [productos].[Precio_venta_publico] >= LTRIM(RTRIM(@Precio_venta_publico))) " 
                    + "AND   (@Fecha_fabricacion IS NULL OR @Fecha_fabricacion = '' OR [productos].[Fecha_fabricacion] >= LTRIM(RTRIM(@Fecha_fabricacion))) " 
                    + "AND   (@Fecha_caducidad IS NULL OR @Fecha_caducidad = '' OR [productos].[Fecha_caducidad] >= LTRIM(RTRIM(@Fecha_caducidad))) " 
                    + "AND   (@Iva IS NULL OR @Iva = '' OR [productos].[Iva] >= LTRIM(RTRIM(@Iva))) " 
                    + "";
            } else if (sCondition == "Equal or less than...") {
                selectStatement 
                    = "SELECT "
                + "     [productos].[ProductosID] "
                + "    ,[productos].[Nombre_producto] "
                + "    ,[productos].[Precio_unitario] "
                + "    ,[productos].[Precio_venta_publico] "
                + "    ,[productos].[Fecha_fabricacion] "
                + "    ,[productos].[Fecha_caducidad] "
                + "    ,[productos].[Iva] "
                + "FROM " 
                + "     [productos] " 
                    + "WHERE " 
                    + "     (@ProductosID IS NULL OR @ProductosID = '' OR [productos].[ProductosID] <= LTRIM(RTRIM(@ProductosID))) " 
                    + "AND   (@Nombre_producto IS NULL OR @Nombre_producto = '' OR [productos].[Nombre_producto] <= LTRIM(RTRIM(@Nombre_producto))) " 
                    + "AND   (@Precio_unitario IS NULL OR @Precio_unitario = '' OR [productos].[Precio_unitario] <= LTRIM(RTRIM(@Precio_unitario))) " 
                    + "AND   (@Precio_venta_publico IS NULL OR @Precio_venta_publico = '' OR [productos].[Precio_venta_publico] <= LTRIM(RTRIM(@Precio_venta_publico))) " 
                    + "AND   (@Fecha_fabricacion IS NULL OR @Fecha_fabricacion = '' OR [productos].[Fecha_fabricacion] <= LTRIM(RTRIM(@Fecha_fabricacion))) " 
                    + "AND   (@Fecha_caducidad IS NULL OR @Fecha_caducidad = '' OR [productos].[Fecha_caducidad] <= LTRIM(RTRIM(@Fecha_caducidad))) " 
                    + "AND   (@Iva IS NULL OR @Iva = '' OR [productos].[Iva] <= LTRIM(RTRIM(@Iva))) " 
                    + "";
            }
            SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
            selectCommand.CommandType = CommandType.Text;
            if (sField == "Productos I D") {
                selectCommand.Parameters.AddWithValue("@ProductosID", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@ProductosID", DBNull.Value); }
            if (sField == "Nombre Producto") {
                selectCommand.Parameters.AddWithValue("@Nombre_producto", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@Nombre_producto", DBNull.Value); }
            if (sField == "Precio Unitario") {
                selectCommand.Parameters.AddWithValue("@Precio_unitario", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@Precio_unitario", DBNull.Value); }
            if (sField == "Precio Venta Publico") {
                selectCommand.Parameters.AddWithValue("@Precio_venta_publico", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@Precio_venta_publico", DBNull.Value); }
            if (sField == "Fecha Fabricacion") {
                selectCommand.Parameters.AddWithValue("@Fecha_fabricacion", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@Fecha_fabricacion", DBNull.Value); }
            if (sField == "Fecha Caducidad") {
                selectCommand.Parameters.AddWithValue("@Fecha_caducidad", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@Fecha_caducidad", DBNull.Value); }
            if (sField == "Iva") {
                selectCommand.Parameters.AddWithValue("@Iva", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@Iva", DBNull.Value); }
            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                SqlDataReader reader = selectCommand.ExecuteReader();
                if (reader.HasRows) {
                    dt.Load(reader); }
                reader.Close();
            }
            catch (SqlException)
            {
                return dt;
            }
            finally
            {
                connection.Close();
            }
            return dt;
        }

        public static productos Select_Record(productos productosPara)
        {
        productos productos = new productos();
            SqlConnection connection = empresaData.GetConnection();
            string selectStatement
            = "SELECT " 
                + "     [ProductosID] "
                + "    ,[Nombre_producto] "
                + "    ,[Precio_unitario] "
                + "    ,[Precio_venta_publico] "
                + "    ,[Fecha_fabricacion] "
                + "    ,[Fecha_caducidad] "
                + "    ,[Iva] "
                + "FROM "
                + "     [productos] "
                + "WHERE "
                + "     [ProductosID] = @ProductosID "
                + "";
            SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
            selectCommand.CommandType = CommandType.Text;
            selectCommand.Parameters.AddWithValue("@ProductosID", productosPara.ProductosID);
            try
            {
                connection.Open();
                SqlDataReader reader
                    = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    productos.ProductosID = System.Convert.ToInt32(reader["ProductosID"]);
                    productos.Nombre_producto = System.Convert.ToString(reader["Nombre_producto"]);
                    productos.Precio_unitario = System.Convert.ToString(reader["Precio_unitario"]);
                    productos.Precio_venta_publico = System.Convert.ToString(reader["Precio_venta_publico"]);
                    productos.Fecha_fabricacion = System.Convert.ToString(reader["Fecha_fabricacion"]);
                    productos.Fecha_caducidad = System.Convert.ToString(reader["Fecha_caducidad"]);
                    productos.Iva = System.Convert.ToString(reader["Iva"]);
                }
                else
                {
                    productos = null;
                }
                reader.Close();
            }
            catch (SqlException)
            {
                return productos;
            }
            finally
            {
                connection.Close();
            }
            return productos;
        }

        public static bool Add(productos productos)
        {
            SqlConnection connection = empresaData.GetConnection();
            string insertStatement
                = "INSERT " 
                + "     [productos] "
                + "     ( "
                + "     [ProductosID] "
                + "    ,[Nombre_producto] "
                + "    ,[Precio_unitario] "
                + "    ,[Precio_venta_publico] "
                + "    ,[Fecha_fabricacion] "
                + "    ,[Fecha_caducidad] "
                + "    ,[Iva] "
                + "     ) "
                + "VALUES " 
                + "     ( "
                + "     @ProductosID "
                + "    ,@Nombre_producto "
                + "    ,@Precio_unitario "
                + "    ,@Precio_venta_publico "
                + "    ,@Fecha_fabricacion "
                + "    ,@Fecha_caducidad "
                + "    ,@Iva "
                + "     ) "
                + "";
            SqlCommand insertCommand = new SqlCommand(insertStatement, connection);
            insertCommand.CommandType = CommandType.Text;
                insertCommand.Parameters.AddWithValue("@ProductosID", productos.ProductosID);
                insertCommand.Parameters.AddWithValue("@Nombre_producto", productos.Nombre_producto);
                insertCommand.Parameters.AddWithValue("@Precio_unitario", productos.Precio_unitario);
                insertCommand.Parameters.AddWithValue("@Precio_venta_publico", productos.Precio_venta_publico);
                insertCommand.Parameters.AddWithValue("@Fecha_fabricacion", productos.Fecha_fabricacion);
                insertCommand.Parameters.AddWithValue("@Fecha_caducidad", productos.Fecha_caducidad);
                insertCommand.Parameters.AddWithValue("@Iva", productos.Iva);
            try
            {
                connection.Open();
                int count = insertCommand.ExecuteNonQuery();
                if (count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (SqlException)
            {
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        public static bool Update(productos oldproductos, 
               productos newproductos)
        {
            SqlConnection connection = empresaData.GetConnection();
            string updateStatement
                = "UPDATE "  
                + "     [productos] "
                + "SET "
                + "     [ProductosID] = @NewProductosID "
                + "    ,[Nombre_producto] = @NewNombre_producto "
                + "    ,[Precio_unitario] = @NewPrecio_unitario "
                + "    ,[Precio_venta_publico] = @NewPrecio_venta_publico "
                + "    ,[Fecha_fabricacion] = @NewFecha_fabricacion "
                + "    ,[Fecha_caducidad] = @NewFecha_caducidad "
                + "    ,[Iva] = @NewIva "
                + "WHERE "
                + "     [ProductosID] = @OldProductosID " 
                + " AND [Nombre_producto] = @OldNombre_producto " 
                + " AND [Precio_unitario] = @OldPrecio_unitario " 
                + " AND [Precio_venta_publico] = @OldPrecio_venta_publico " 
                + " AND [Fecha_fabricacion] = @OldFecha_fabricacion " 
                + " AND [Fecha_caducidad] = @OldFecha_caducidad " 
                + " AND [Iva] = @OldIva " 
                + "";
            SqlCommand updateCommand = new SqlCommand(updateStatement, connection);
            updateCommand.CommandType = CommandType.Text;
            updateCommand.Parameters.AddWithValue("@NewProductosID", newproductos.ProductosID);
            updateCommand.Parameters.AddWithValue("@NewNombre_producto", newproductos.Nombre_producto);
            updateCommand.Parameters.AddWithValue("@NewPrecio_unitario", newproductos.Precio_unitario);
            updateCommand.Parameters.AddWithValue("@NewPrecio_venta_publico", newproductos.Precio_venta_publico);
            updateCommand.Parameters.AddWithValue("@NewFecha_fabricacion", newproductos.Fecha_fabricacion);
            updateCommand.Parameters.AddWithValue("@NewFecha_caducidad", newproductos.Fecha_caducidad);
            updateCommand.Parameters.AddWithValue("@NewIva", newproductos.Iva);
            updateCommand.Parameters.AddWithValue("@OldProductosID", oldproductos.ProductosID);
            updateCommand.Parameters.AddWithValue("@OldNombre_producto", oldproductos.Nombre_producto);
            updateCommand.Parameters.AddWithValue("@OldPrecio_unitario", oldproductos.Precio_unitario);
            updateCommand.Parameters.AddWithValue("@OldPrecio_venta_publico", oldproductos.Precio_venta_publico);
            updateCommand.Parameters.AddWithValue("@OldFecha_fabricacion", oldproductos.Fecha_fabricacion);
            updateCommand.Parameters.AddWithValue("@OldFecha_caducidad", oldproductos.Fecha_caducidad);
            updateCommand.Parameters.AddWithValue("@OldIva", oldproductos.Iva);
            try
            {
                connection.Open();
                int count = updateCommand.ExecuteNonQuery();
                if (count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (SqlException)
            {
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        public static bool Delete(productos productos)
        {
            SqlConnection connection = empresaData.GetConnection();
            string deleteStatement
                = "DELETE FROM "  
                + "     [productos] "
                + "WHERE " 
                + "     [ProductosID] = @OldProductosID " 
                + " AND [Nombre_producto] = @OldNombre_producto " 
                + " AND [Precio_unitario] = @OldPrecio_unitario " 
                + " AND [Precio_venta_publico] = @OldPrecio_venta_publico " 
                + " AND [Fecha_fabricacion] = @OldFecha_fabricacion " 
                + " AND [Fecha_caducidad] = @OldFecha_caducidad " 
                + " AND [Iva] = @OldIva " 
                + "";
            SqlCommand deleteCommand = new SqlCommand(deleteStatement, connection);
            deleteCommand.CommandType = CommandType.Text;
            deleteCommand.Parameters.AddWithValue("@OldProductosID", productos.ProductosID);
            deleteCommand.Parameters.AddWithValue("@OldNombre_producto", productos.Nombre_producto);
            deleteCommand.Parameters.AddWithValue("@OldPrecio_unitario", productos.Precio_unitario);
            deleteCommand.Parameters.AddWithValue("@OldPrecio_venta_publico", productos.Precio_venta_publico);
            deleteCommand.Parameters.AddWithValue("@OldFecha_fabricacion", productos.Fecha_fabricacion);
            deleteCommand.Parameters.AddWithValue("@OldFecha_caducidad", productos.Fecha_caducidad);
            deleteCommand.Parameters.AddWithValue("@OldIva", productos.Iva);
            try
            {
                connection.Open();
                int count = deleteCommand.ExecuteNonQuery();
                if (count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (SqlException)
            {
                return false;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
 
