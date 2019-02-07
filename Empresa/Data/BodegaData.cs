using System;
using System.Data;
using System.Data.SqlClient;
using Empresa.Models;

namespace Empresa.Data
{
    public class BodegaData
    {

        public static DataTable SelectAll()
        {
            SqlConnection connection = empresaData.GetConnection();
            string selectStatement
                = "SELECT "  
                + "     [Bodega].[BodegaID] "
                + "    ,[Bodega].[ProductosID] "
                + "    ,[Bodega].[Observacion] "
                + "FROM " 
                + "     [Bodega] " 
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
                + "     [Bodega].[BodegaID] "
                + "    ,[Bodega].[ProductosID] "
                + "    ,[Bodega].[Observacion] "
                + "FROM " 
                + "     [Bodega] " 
                    + "WHERE " 
                    + "     (@BodegaID IS NULL OR @BodegaID = '' OR [Bodega].[BodegaID] LIKE '%' + LTRIM(RTRIM(@BodegaID)) + '%') " 
                    + "AND   (@ProductosID IS NULL OR @ProductosID = '' OR [Bodega].[ProductosID] LIKE '%' + LTRIM(RTRIM(@ProductosID)) + '%') " 
                    + "AND   (@Observacion IS NULL OR @Observacion = '' OR [Bodega].[Observacion] LIKE '%' + LTRIM(RTRIM(@Observacion)) + '%') " 
                    + "";
            } else if (sCondition == "Equals") {
                selectStatement
                    = "SELECT "
                + "     [Bodega].[BodegaID] "
                + "    ,[Bodega].[ProductosID] "
                + "    ,[Bodega].[Observacion] "
                + "FROM " 
                + "     [Bodega] " 
                    + "WHERE " 
                    + "     (@BodegaID IS NULL OR @BodegaID = '' OR [Bodega].[BodegaID] = LTRIM(RTRIM(@BodegaID))) " 
                    + "AND   (@ProductosID IS NULL OR @ProductosID = '' OR [Bodega].[ProductosID] = LTRIM(RTRIM(@ProductosID))) " 
                    + "AND   (@Observacion IS NULL OR @Observacion = '' OR [Bodega].[Observacion] = LTRIM(RTRIM(@Observacion))) " 
                    + "";
            } else if  (sCondition == "Starts with...") {
                selectStatement
                    = "SELECT "
                + "     [Bodega].[BodegaID] "
                + "    ,[Bodega].[ProductosID] "
                + "    ,[Bodega].[Observacion] "
                + "FROM " 
                + "     [Bodega] " 
                    + "WHERE " 
                    + "     (@BodegaID IS NULL OR @BodegaID = '' OR [Bodega].[BodegaID] LIKE LTRIM(RTRIM(@BodegaID)) + '%') " 
                    + "AND   (@ProductosID IS NULL OR @ProductosID = '' OR [Bodega].[ProductosID] LIKE LTRIM(RTRIM(@ProductosID)) + '%') " 
                    + "AND   (@Observacion IS NULL OR @Observacion = '' OR [Bodega].[Observacion] LIKE LTRIM(RTRIM(@Observacion)) + '%') " 
                    + "";
            } else if  (sCondition == "More than...") {
                selectStatement
                    = "SELECT "
                + "     [Bodega].[BodegaID] "
                + "    ,[Bodega].[ProductosID] "
                + "    ,[Bodega].[Observacion] "
                + "FROM " 
                + "     [Bodega] " 
                    + "WHERE " 
                    + "     (@BodegaID IS NULL OR @BodegaID = '' OR [Bodega].[BodegaID] > LTRIM(RTRIM(@BodegaID))) " 
                    + "AND   (@ProductosID IS NULL OR @ProductosID = '' OR [Bodega].[ProductosID] > LTRIM(RTRIM(@ProductosID))) " 
                    + "AND   (@Observacion IS NULL OR @Observacion = '' OR [Bodega].[Observacion] > LTRIM(RTRIM(@Observacion))) " 
                    + "";
            } else if  (sCondition == "Less than...") {
                selectStatement
                    = "SELECT " 
                + "     [Bodega].[BodegaID] "
                + "    ,[Bodega].[ProductosID] "
                + "    ,[Bodega].[Observacion] "
                + "FROM " 
                + "     [Bodega] " 
                    + "WHERE " 
                    + "     (@BodegaID IS NULL OR @BodegaID = '' OR [Bodega].[BodegaID] < LTRIM(RTRIM(@BodegaID))) " 
                    + "AND   (@ProductosID IS NULL OR @ProductosID = '' OR [Bodega].[ProductosID] < LTRIM(RTRIM(@ProductosID))) " 
                    + "AND   (@Observacion IS NULL OR @Observacion = '' OR [Bodega].[Observacion] < LTRIM(RTRIM(@Observacion))) " 
                    + "";
            } else if  (sCondition == "Equal or more than...") {
                selectStatement
                    = "SELECT "
                + "     [Bodega].[BodegaID] "
                + "    ,[Bodega].[ProductosID] "
                + "    ,[Bodega].[Observacion] "
                + "FROM " 
                + "     [Bodega] " 
                    + "WHERE " 
                    + "     (@BodegaID IS NULL OR @BodegaID = '' OR [Bodega].[BodegaID] >= LTRIM(RTRIM(@BodegaID))) " 
                    + "AND   (@ProductosID IS NULL OR @ProductosID = '' OR [Bodega].[ProductosID] >= LTRIM(RTRIM(@ProductosID))) " 
                    + "AND   (@Observacion IS NULL OR @Observacion = '' OR [Bodega].[Observacion] >= LTRIM(RTRIM(@Observacion))) " 
                    + "";
            } else if (sCondition == "Equal or less than...") {
                selectStatement 
                    = "SELECT "
                + "     [Bodega].[BodegaID] "
                + "    ,[Bodega].[ProductosID] "
                + "    ,[Bodega].[Observacion] "
                + "FROM " 
                + "     [Bodega] " 
                    + "WHERE " 
                    + "     (@BodegaID IS NULL OR @BodegaID = '' OR [Bodega].[BodegaID] <= LTRIM(RTRIM(@BodegaID))) " 
                    + "AND   (@ProductosID IS NULL OR @ProductosID = '' OR [Bodega].[ProductosID] <= LTRIM(RTRIM(@ProductosID))) " 
                    + "AND   (@Observacion IS NULL OR @Observacion = '' OR [Bodega].[Observacion] <= LTRIM(RTRIM(@Observacion))) " 
                    + "";
            }
            SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
            selectCommand.CommandType = CommandType.Text;
            if (sField == "Bodega I D") {
                selectCommand.Parameters.AddWithValue("@BodegaID", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@BodegaID", DBNull.Value); }
            if (sField == "Productos I D") {
                selectCommand.Parameters.AddWithValue("@ProductosID", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@ProductosID", DBNull.Value); }
            if (sField == "Observacion") {
                selectCommand.Parameters.AddWithValue("@Observacion", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@Observacion", DBNull.Value); }
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

        public static Bodega Select_Record(Bodega BodegaPara)
        {
        Bodega Bodega = new Bodega();
            SqlConnection connection = empresaData.GetConnection();
            string selectStatement
            = "SELECT " 
                + "     [BodegaID] "
                + "    ,[ProductosID] "
                + "    ,[Observacion] "
                + "FROM "
                + "     [Bodega] "
                + "WHERE "
                + "     [BodegaID] = @BodegaID "
                + "";
            SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
            selectCommand.CommandType = CommandType.Text;
            selectCommand.Parameters.AddWithValue("@BodegaID", BodegaPara.BodegaID);
            try
            {
                connection.Open();
                SqlDataReader reader
                    = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    Bodega.BodegaID = System.Convert.ToString(reader["BodegaID"]);
                    Bodega.ProductosID = System.Convert.ToString(reader["ProductosID"]);
                    Bodega.Observacion = System.Convert.ToString(reader["Observacion"]);
                }
                else
                {
                    Bodega = null;
                }
                reader.Close();
            }
            catch (SqlException)
            {
                return Bodega;
            }
            finally
            {
                connection.Close();
            }
            return Bodega;
        }

        public static bool Add(Bodega Bodega)
        {
            SqlConnection connection = empresaData.GetConnection();
            string insertStatement
                = "INSERT " 
                + "     [Bodega] "
                + "     ( "
                + "     [BodegaID] "
                + "    ,[ProductosID] "
                + "    ,[Observacion] "
                + "     ) "
                + "VALUES " 
                + "     ( "
                + "     @BodegaID "
                + "    ,@ProductosID "
                + "    ,@Observacion "
                + "     ) "
                + "";
            SqlCommand insertCommand = new SqlCommand(insertStatement, connection);
            insertCommand.CommandType = CommandType.Text;
                insertCommand.Parameters.AddWithValue("@BodegaID", Bodega.BodegaID);
                insertCommand.Parameters.AddWithValue("@ProductosID", Bodega.ProductosID);
                insertCommand.Parameters.AddWithValue("@Observacion", Bodega.Observacion);
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

        public static bool Update(Bodega oldBodega, 
               Bodega newBodega)
        {
            SqlConnection connection = empresaData.GetConnection();
            string updateStatement
                = "UPDATE "  
                + "     [Bodega] "
                + "SET "
                + "     [BodegaID] = @NewBodegaID "
                + "    ,[ProductosID] = @NewProductosID "
                + "    ,[Observacion] = @NewObservacion "
                + "WHERE "
                + "     [BodegaID] = @OldBodegaID " 
                + " AND [ProductosID] = @OldProductosID " 
                + " AND [Observacion] = @OldObservacion " 
                + "";
            SqlCommand updateCommand = new SqlCommand(updateStatement, connection);
            updateCommand.CommandType = CommandType.Text;
            updateCommand.Parameters.AddWithValue("@NewBodegaID", newBodega.BodegaID);
            updateCommand.Parameters.AddWithValue("@NewProductosID", newBodega.ProductosID);
            updateCommand.Parameters.AddWithValue("@NewObservacion", newBodega.Observacion);
            updateCommand.Parameters.AddWithValue("@OldBodegaID", oldBodega.BodegaID);
            updateCommand.Parameters.AddWithValue("@OldProductosID", oldBodega.ProductosID);
            updateCommand.Parameters.AddWithValue("@OldObservacion", oldBodega.Observacion);
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

        public static bool Delete(Bodega Bodega)
        {
            SqlConnection connection = empresaData.GetConnection();
            string deleteStatement
                = "DELETE FROM "  
                + "     [Bodega] "
                + "WHERE " 
                + "     [BodegaID] = @OldBodegaID " 
                + " AND [ProductosID] = @OldProductosID " 
                + " AND [Observacion] = @OldObservacion " 
                + "";
            SqlCommand deleteCommand = new SqlCommand(deleteStatement, connection);
            deleteCommand.CommandType = CommandType.Text;
            deleteCommand.Parameters.AddWithValue("@OldBodegaID", Bodega.BodegaID);
            deleteCommand.Parameters.AddWithValue("@OldProductosID", Bodega.ProductosID);
            deleteCommand.Parameters.AddWithValue("@OldObservacion", Bodega.Observacion);
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
 
