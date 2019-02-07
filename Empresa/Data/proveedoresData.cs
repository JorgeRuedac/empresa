using System;
using System.Data;
using System.Data.SqlClient;
using Empresa.Models;

namespace Empresa.Data
{
    public class proveedoresData
    {

        public static DataTable SelectAll()
        {
            SqlConnection connection = empresaData.GetConnection();
            string selectStatement
                = "SELECT "  
                + "     [proveedores].[ProveedoresID] "
                + "    ,[proveedores].[Nombre_empresa] "
                + "    ,[proveedores].[Direccion] "
                + "    ,[proveedores].[Telefono] "
                + "    ,[proveedores].[Celular_contacto] "
                + "    ,[proveedores].[Nombre_contacto] "
                + "FROM " 
                + "     [proveedores] " 
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
                + "     [proveedores].[ProveedoresID] "
                + "    ,[proveedores].[Nombre_empresa] "
                + "    ,[proveedores].[Direccion] "
                + "    ,[proveedores].[Telefono] "
                + "    ,[proveedores].[Celular_contacto] "
                + "    ,[proveedores].[Nombre_contacto] "
                + "FROM " 
                + "     [proveedores] " 
                    + "WHERE " 
                    + "     (@ProveedoresID IS NULL OR @ProveedoresID = '' OR [proveedores].[ProveedoresID] LIKE '%' + LTRIM(RTRIM(@ProveedoresID)) + '%') " 
                    + "AND   (@Nombre_empresa IS NULL OR @Nombre_empresa = '' OR [proveedores].[Nombre_empresa] LIKE '%' + LTRIM(RTRIM(@Nombre_empresa)) + '%') " 
                    + "AND   (@Direccion IS NULL OR @Direccion = '' OR [proveedores].[Direccion] LIKE '%' + LTRIM(RTRIM(@Direccion)) + '%') " 
                    + "AND   (@Telefono IS NULL OR @Telefono = '' OR [proveedores].[Telefono] LIKE '%' + LTRIM(RTRIM(@Telefono)) + '%') " 
                    + "AND   (@Celular_contacto IS NULL OR @Celular_contacto = '' OR [proveedores].[Celular_contacto] LIKE '%' + LTRIM(RTRIM(@Celular_contacto)) + '%') " 
                    + "AND   (@Nombre_contacto IS NULL OR @Nombre_contacto = '' OR [proveedores].[Nombre_contacto] LIKE '%' + LTRIM(RTRIM(@Nombre_contacto)) + '%') " 
                    + "";
            } else if (sCondition == "Equals") {
                selectStatement
                    = "SELECT "
                + "     [proveedores].[ProveedoresID] "
                + "    ,[proveedores].[Nombre_empresa] "
                + "    ,[proveedores].[Direccion] "
                + "    ,[proveedores].[Telefono] "
                + "    ,[proveedores].[Celular_contacto] "
                + "    ,[proveedores].[Nombre_contacto] "
                + "FROM " 
                + "     [proveedores] " 
                    + "WHERE " 
                    + "     (@ProveedoresID IS NULL OR @ProveedoresID = '' OR [proveedores].[ProveedoresID] = LTRIM(RTRIM(@ProveedoresID))) " 
                    + "AND   (@Nombre_empresa IS NULL OR @Nombre_empresa = '' OR [proveedores].[Nombre_empresa] = LTRIM(RTRIM(@Nombre_empresa))) " 
                    + "AND   (@Direccion IS NULL OR @Direccion = '' OR [proveedores].[Direccion] = LTRIM(RTRIM(@Direccion))) " 
                    + "AND   (@Telefono IS NULL OR @Telefono = '' OR [proveedores].[Telefono] = LTRIM(RTRIM(@Telefono))) " 
                    + "AND   (@Celular_contacto IS NULL OR @Celular_contacto = '' OR [proveedores].[Celular_contacto] = LTRIM(RTRIM(@Celular_contacto))) " 
                    + "AND   (@Nombre_contacto IS NULL OR @Nombre_contacto = '' OR [proveedores].[Nombre_contacto] = LTRIM(RTRIM(@Nombre_contacto))) " 
                    + "";
            } else if  (sCondition == "Starts with...") {
                selectStatement
                    = "SELECT "
                + "     [proveedores].[ProveedoresID] "
                + "    ,[proveedores].[Nombre_empresa] "
                + "    ,[proveedores].[Direccion] "
                + "    ,[proveedores].[Telefono] "
                + "    ,[proveedores].[Celular_contacto] "
                + "    ,[proveedores].[Nombre_contacto] "
                + "FROM " 
                + "     [proveedores] " 
                    + "WHERE " 
                    + "     (@ProveedoresID IS NULL OR @ProveedoresID = '' OR [proveedores].[ProveedoresID] LIKE LTRIM(RTRIM(@ProveedoresID)) + '%') " 
                    + "AND   (@Nombre_empresa IS NULL OR @Nombre_empresa = '' OR [proveedores].[Nombre_empresa] LIKE LTRIM(RTRIM(@Nombre_empresa)) + '%') " 
                    + "AND   (@Direccion IS NULL OR @Direccion = '' OR [proveedores].[Direccion] LIKE LTRIM(RTRIM(@Direccion)) + '%') " 
                    + "AND   (@Telefono IS NULL OR @Telefono = '' OR [proveedores].[Telefono] LIKE LTRIM(RTRIM(@Telefono)) + '%') " 
                    + "AND   (@Celular_contacto IS NULL OR @Celular_contacto = '' OR [proveedores].[Celular_contacto] LIKE LTRIM(RTRIM(@Celular_contacto)) + '%') " 
                    + "AND   (@Nombre_contacto IS NULL OR @Nombre_contacto = '' OR [proveedores].[Nombre_contacto] LIKE LTRIM(RTRIM(@Nombre_contacto)) + '%') " 
                    + "";
            } else if  (sCondition == "More than...") {
                selectStatement
                    = "SELECT "
                + "     [proveedores].[ProveedoresID] "
                + "    ,[proveedores].[Nombre_empresa] "
                + "    ,[proveedores].[Direccion] "
                + "    ,[proveedores].[Telefono] "
                + "    ,[proveedores].[Celular_contacto] "
                + "    ,[proveedores].[Nombre_contacto] "
                + "FROM " 
                + "     [proveedores] " 
                    + "WHERE " 
                    + "     (@ProveedoresID IS NULL OR @ProveedoresID = '' OR [proveedores].[ProveedoresID] > LTRIM(RTRIM(@ProveedoresID))) " 
                    + "AND   (@Nombre_empresa IS NULL OR @Nombre_empresa = '' OR [proveedores].[Nombre_empresa] > LTRIM(RTRIM(@Nombre_empresa))) " 
                    + "AND   (@Direccion IS NULL OR @Direccion = '' OR [proveedores].[Direccion] > LTRIM(RTRIM(@Direccion))) " 
                    + "AND   (@Telefono IS NULL OR @Telefono = '' OR [proveedores].[Telefono] > LTRIM(RTRIM(@Telefono))) " 
                    + "AND   (@Celular_contacto IS NULL OR @Celular_contacto = '' OR [proveedores].[Celular_contacto] > LTRIM(RTRIM(@Celular_contacto))) " 
                    + "AND   (@Nombre_contacto IS NULL OR @Nombre_contacto = '' OR [proveedores].[Nombre_contacto] > LTRIM(RTRIM(@Nombre_contacto))) " 
                    + "";
            } else if  (sCondition == "Less than...") {
                selectStatement
                    = "SELECT " 
                + "     [proveedores].[ProveedoresID] "
                + "    ,[proveedores].[Nombre_empresa] "
                + "    ,[proveedores].[Direccion] "
                + "    ,[proveedores].[Telefono] "
                + "    ,[proveedores].[Celular_contacto] "
                + "    ,[proveedores].[Nombre_contacto] "
                + "FROM " 
                + "     [proveedores] " 
                    + "WHERE " 
                    + "     (@ProveedoresID IS NULL OR @ProveedoresID = '' OR [proveedores].[ProveedoresID] < LTRIM(RTRIM(@ProveedoresID))) " 
                    + "AND   (@Nombre_empresa IS NULL OR @Nombre_empresa = '' OR [proveedores].[Nombre_empresa] < LTRIM(RTRIM(@Nombre_empresa))) " 
                    + "AND   (@Direccion IS NULL OR @Direccion = '' OR [proveedores].[Direccion] < LTRIM(RTRIM(@Direccion))) " 
                    + "AND   (@Telefono IS NULL OR @Telefono = '' OR [proveedores].[Telefono] < LTRIM(RTRIM(@Telefono))) " 
                    + "AND   (@Celular_contacto IS NULL OR @Celular_contacto = '' OR [proveedores].[Celular_contacto] < LTRIM(RTRIM(@Celular_contacto))) " 
                    + "AND   (@Nombre_contacto IS NULL OR @Nombre_contacto = '' OR [proveedores].[Nombre_contacto] < LTRIM(RTRIM(@Nombre_contacto))) " 
                    + "";
            } else if  (sCondition == "Equal or more than...") {
                selectStatement
                    = "SELECT "
                + "     [proveedores].[ProveedoresID] "
                + "    ,[proveedores].[Nombre_empresa] "
                + "    ,[proveedores].[Direccion] "
                + "    ,[proveedores].[Telefono] "
                + "    ,[proveedores].[Celular_contacto] "
                + "    ,[proveedores].[Nombre_contacto] "
                + "FROM " 
                + "     [proveedores] " 
                    + "WHERE " 
                    + "     (@ProveedoresID IS NULL OR @ProveedoresID = '' OR [proveedores].[ProveedoresID] >= LTRIM(RTRIM(@ProveedoresID))) " 
                    + "AND   (@Nombre_empresa IS NULL OR @Nombre_empresa = '' OR [proveedores].[Nombre_empresa] >= LTRIM(RTRIM(@Nombre_empresa))) " 
                    + "AND   (@Direccion IS NULL OR @Direccion = '' OR [proveedores].[Direccion] >= LTRIM(RTRIM(@Direccion))) " 
                    + "AND   (@Telefono IS NULL OR @Telefono = '' OR [proveedores].[Telefono] >= LTRIM(RTRIM(@Telefono))) " 
                    + "AND   (@Celular_contacto IS NULL OR @Celular_contacto = '' OR [proveedores].[Celular_contacto] >= LTRIM(RTRIM(@Celular_contacto))) " 
                    + "AND   (@Nombre_contacto IS NULL OR @Nombre_contacto = '' OR [proveedores].[Nombre_contacto] >= LTRIM(RTRIM(@Nombre_contacto))) " 
                    + "";
            } else if (sCondition == "Equal or less than...") {
                selectStatement 
                    = "SELECT "
                + "     [proveedores].[ProveedoresID] "
                + "    ,[proveedores].[Nombre_empresa] "
                + "    ,[proveedores].[Direccion] "
                + "    ,[proveedores].[Telefono] "
                + "    ,[proveedores].[Celular_contacto] "
                + "    ,[proveedores].[Nombre_contacto] "
                + "FROM " 
                + "     [proveedores] " 
                    + "WHERE " 
                    + "     (@ProveedoresID IS NULL OR @ProveedoresID = '' OR [proveedores].[ProveedoresID] <= LTRIM(RTRIM(@ProveedoresID))) " 
                    + "AND   (@Nombre_empresa IS NULL OR @Nombre_empresa = '' OR [proveedores].[Nombre_empresa] <= LTRIM(RTRIM(@Nombre_empresa))) " 
                    + "AND   (@Direccion IS NULL OR @Direccion = '' OR [proveedores].[Direccion] <= LTRIM(RTRIM(@Direccion))) " 
                    + "AND   (@Telefono IS NULL OR @Telefono = '' OR [proveedores].[Telefono] <= LTRIM(RTRIM(@Telefono))) " 
                    + "AND   (@Celular_contacto IS NULL OR @Celular_contacto = '' OR [proveedores].[Celular_contacto] <= LTRIM(RTRIM(@Celular_contacto))) " 
                    + "AND   (@Nombre_contacto IS NULL OR @Nombre_contacto = '' OR [proveedores].[Nombre_contacto] <= LTRIM(RTRIM(@Nombre_contacto))) " 
                    + "";
            }
            SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
            selectCommand.CommandType = CommandType.Text;
            if (sField == "Proveedores I D") {
                selectCommand.Parameters.AddWithValue("@ProveedoresID", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@ProveedoresID", DBNull.Value); }
            if (sField == "Nombre Empresa") {
                selectCommand.Parameters.AddWithValue("@Nombre_empresa", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@Nombre_empresa", DBNull.Value); }
            if (sField == "Direccion") {
                selectCommand.Parameters.AddWithValue("@Direccion", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@Direccion", DBNull.Value); }
            if (sField == "Telefono") {
                selectCommand.Parameters.AddWithValue("@Telefono", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@Telefono", DBNull.Value); }
            if (sField == "Celular Contacto") {
                selectCommand.Parameters.AddWithValue("@Celular_contacto", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@Celular_contacto", DBNull.Value); }
            if (sField == "Nombre Contacto") {
                selectCommand.Parameters.AddWithValue("@Nombre_contacto", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@Nombre_contacto", DBNull.Value); }
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

        public static proveedores Select_Record(proveedores proveedoresPara)
        {
        proveedores proveedores = new proveedores();
            SqlConnection connection = empresaData.GetConnection();
            string selectStatement
            = "SELECT " 
                + "     [ProveedoresID] "
                + "    ,[Nombre_empresa] "
                + "    ,[Direccion] "
                + "    ,[Telefono] "
                + "    ,[Celular_contacto] "
                + "    ,[Nombre_contacto] "
                + "FROM "
                + "     [proveedores] "
                + "WHERE "
                + "     [ProveedoresID] = @ProveedoresID "
                + "";
            SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
            selectCommand.CommandType = CommandType.Text;
            selectCommand.Parameters.AddWithValue("@ProveedoresID", proveedoresPara.ProveedoresID);
            try
            {
                connection.Open();
                SqlDataReader reader
                    = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    proveedores.ProveedoresID = System.Convert.ToInt32(reader["ProveedoresID"]);
                    proveedores.Nombre_empresa = System.Convert.ToString(reader["Nombre_empresa"]);
                    proveedores.Direccion = System.Convert.ToString(reader["Direccion"]);
                    proveedores.Telefono = System.Convert.ToString(reader["Telefono"]);
                    proveedores.Celular_contacto = System.Convert.ToString(reader["Celular_contacto"]);
                    proveedores.Nombre_contacto = System.Convert.ToString(reader["Nombre_contacto"]);
                }
                else
                {
                    proveedores = null;
                }
                reader.Close();
            }
            catch (SqlException)
            {
                return proveedores;
            }
            finally
            {
                connection.Close();
            }
            return proveedores;
        }

        public static bool Add(proveedores proveedores)
        {
            SqlConnection connection = empresaData.GetConnection();
            string insertStatement
                = "INSERT " 
                + "     [proveedores] "
                + "     ( "
                + "     [ProveedoresID] "
                + "    ,[Nombre_empresa] "
                + "    ,[Direccion] "
                + "    ,[Telefono] "
                + "    ,[Celular_contacto] "
                + "    ,[Nombre_contacto] "
                + "     ) "
                + "VALUES " 
                + "     ( "
                + "     @ProveedoresID "
                + "    ,@Nombre_empresa "
                + "    ,@Direccion "
                + "    ,@Telefono "
                + "    ,@Celular_contacto "
                + "    ,@Nombre_contacto "
                + "     ) "
                + "";
            SqlCommand insertCommand = new SqlCommand(insertStatement, connection);
            insertCommand.CommandType = CommandType.Text;
                insertCommand.Parameters.AddWithValue("@ProveedoresID", proveedores.ProveedoresID);
                insertCommand.Parameters.AddWithValue("@Nombre_empresa", proveedores.Nombre_empresa);
                insertCommand.Parameters.AddWithValue("@Direccion", proveedores.Direccion);
                insertCommand.Parameters.AddWithValue("@Telefono", proveedores.Telefono);
                insertCommand.Parameters.AddWithValue("@Celular_contacto", proveedores.Celular_contacto);
                insertCommand.Parameters.AddWithValue("@Nombre_contacto", proveedores.Nombre_contacto);
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

        public static bool Update(proveedores oldproveedores, 
               proveedores newproveedores)
        {
            SqlConnection connection = empresaData.GetConnection();
            string updateStatement
                = "UPDATE "  
                + "     [proveedores] "
                + "SET "
                + "     [ProveedoresID] = @NewProveedoresID "
                + "    ,[Nombre_empresa] = @NewNombre_empresa "
                + "    ,[Direccion] = @NewDireccion "
                + "    ,[Telefono] = @NewTelefono "
                + "    ,[Celular_contacto] = @NewCelular_contacto "
                + "    ,[Nombre_contacto] = @NewNombre_contacto "
                + "WHERE "
                + "     [ProveedoresID] = @OldProveedoresID " 
                + " AND [Nombre_empresa] = @OldNombre_empresa " 
                + " AND [Direccion] = @OldDireccion " 
                + " AND [Telefono] = @OldTelefono " 
                + " AND [Celular_contacto] = @OldCelular_contacto " 
                + " AND [Nombre_contacto] = @OldNombre_contacto " 
                + "";
            SqlCommand updateCommand = new SqlCommand(updateStatement, connection);
            updateCommand.CommandType = CommandType.Text;
            updateCommand.Parameters.AddWithValue("@NewProveedoresID", newproveedores.ProveedoresID);
            updateCommand.Parameters.AddWithValue("@NewNombre_empresa", newproveedores.Nombre_empresa);
            updateCommand.Parameters.AddWithValue("@NewDireccion", newproveedores.Direccion);
            updateCommand.Parameters.AddWithValue("@NewTelefono", newproveedores.Telefono);
            updateCommand.Parameters.AddWithValue("@NewCelular_contacto", newproveedores.Celular_contacto);
            updateCommand.Parameters.AddWithValue("@NewNombre_contacto", newproveedores.Nombre_contacto);
            updateCommand.Parameters.AddWithValue("@OldProveedoresID", oldproveedores.ProveedoresID);
            updateCommand.Parameters.AddWithValue("@OldNombre_empresa", oldproveedores.Nombre_empresa);
            updateCommand.Parameters.AddWithValue("@OldDireccion", oldproveedores.Direccion);
            updateCommand.Parameters.AddWithValue("@OldTelefono", oldproveedores.Telefono);
            updateCommand.Parameters.AddWithValue("@OldCelular_contacto", oldproveedores.Celular_contacto);
            updateCommand.Parameters.AddWithValue("@OldNombre_contacto", oldproveedores.Nombre_contacto);
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

        public static bool Delete(proveedores proveedores)
        {
            SqlConnection connection = empresaData.GetConnection();
            string deleteStatement
                = "DELETE FROM "  
                + "     [proveedores] "
                + "WHERE " 
                + "     [ProveedoresID] = @OldProveedoresID " 
                + " AND [Nombre_empresa] = @OldNombre_empresa " 
                + " AND [Direccion] = @OldDireccion " 
                + " AND [Telefono] = @OldTelefono " 
                + " AND [Celular_contacto] = @OldCelular_contacto " 
                + " AND [Nombre_contacto] = @OldNombre_contacto " 
                + "";
            SqlCommand deleteCommand = new SqlCommand(deleteStatement, connection);
            deleteCommand.CommandType = CommandType.Text;
            deleteCommand.Parameters.AddWithValue("@OldProveedoresID", proveedores.ProveedoresID);
            deleteCommand.Parameters.AddWithValue("@OldNombre_empresa", proveedores.Nombre_empresa);
            deleteCommand.Parameters.AddWithValue("@OldDireccion", proveedores.Direccion);
            deleteCommand.Parameters.AddWithValue("@OldTelefono", proveedores.Telefono);
            deleteCommand.Parameters.AddWithValue("@OldCelular_contacto", proveedores.Celular_contacto);
            deleteCommand.Parameters.AddWithValue("@OldNombre_contacto", proveedores.Nombre_contacto);
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
 
