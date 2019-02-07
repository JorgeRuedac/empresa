using System;
using System.Data;
using System.Data.SqlClient;
using Empresa.Models;

namespace Empresa.Data
{
    public class contadorData
    {

        public static DataTable SelectAll()
        {
            SqlConnection connection = empresaData.GetConnection();
            string selectStatement
                = "SELECT "  
                + "     [contador].[contador_id] "
                + "    ,[contador].[nombre] "
                + "    ,[contador].[apellido] "
                + "    ,[contador].[direccion] "
                + "    ,[contador].[contacto] "
                + "FROM " 
                + "     [contador] " 
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
                + "     [contador].[contador_id] "
                + "    ,[contador].[nombre] "
                + "    ,[contador].[apellido] "
                + "    ,[contador].[direccion] "
                + "    ,[contador].[contacto] "
                + "FROM " 
                + "     [contador] " 
                    + "WHERE " 
                    + "     (@contador_id IS NULL OR @contador_id = '' OR [contador].[contador_id] LIKE '%' + LTRIM(RTRIM(@contador_id)) + '%') " 
                    + "AND   (@nombre IS NULL OR @nombre = '' OR [contador].[nombre] LIKE '%' + LTRIM(RTRIM(@nombre)) + '%') " 
                    + "AND   (@apellido IS NULL OR @apellido = '' OR [contador].[apellido] LIKE '%' + LTRIM(RTRIM(@apellido)) + '%') " 
                    + "AND   (@direccion IS NULL OR @direccion = '' OR [contador].[direccion] LIKE '%' + LTRIM(RTRIM(@direccion)) + '%') " 
                    + "AND   (@contacto IS NULL OR @contacto = '' OR [contador].[contacto] LIKE '%' + LTRIM(RTRIM(@contacto)) + '%') " 
                    + "";
            } else if (sCondition == "Equals") {
                selectStatement
                    = "SELECT "
                + "     [contador].[contador_id] "
                + "    ,[contador].[nombre] "
                + "    ,[contador].[apellido] "
                + "    ,[contador].[direccion] "
                + "    ,[contador].[contacto] "
                + "FROM " 
                + "     [contador] " 
                    + "WHERE " 
                    + "     (@contador_id IS NULL OR @contador_id = '' OR [contador].[contador_id] = LTRIM(RTRIM(@contador_id))) " 
                    + "AND   (@nombre IS NULL OR @nombre = '' OR [contador].[nombre] = LTRIM(RTRIM(@nombre))) " 
                    + "AND   (@apellido IS NULL OR @apellido = '' OR [contador].[apellido] = LTRIM(RTRIM(@apellido))) " 
                    + "AND   (@direccion IS NULL OR @direccion = '' OR [contador].[direccion] = LTRIM(RTRIM(@direccion))) " 
                    + "AND   (@contacto IS NULL OR @contacto = '' OR [contador].[contacto] = LTRIM(RTRIM(@contacto))) " 
                    + "";
            } else if  (sCondition == "Starts with...") {
                selectStatement
                    = "SELECT "
                + "     [contador].[contador_id] "
                + "    ,[contador].[nombre] "
                + "    ,[contador].[apellido] "
                + "    ,[contador].[direccion] "
                + "    ,[contador].[contacto] "
                + "FROM " 
                + "     [contador] " 
                    + "WHERE " 
                    + "     (@contador_id IS NULL OR @contador_id = '' OR [contador].[contador_id] LIKE LTRIM(RTRIM(@contador_id)) + '%') " 
                    + "AND   (@nombre IS NULL OR @nombre = '' OR [contador].[nombre] LIKE LTRIM(RTRIM(@nombre)) + '%') " 
                    + "AND   (@apellido IS NULL OR @apellido = '' OR [contador].[apellido] LIKE LTRIM(RTRIM(@apellido)) + '%') " 
                    + "AND   (@direccion IS NULL OR @direccion = '' OR [contador].[direccion] LIKE LTRIM(RTRIM(@direccion)) + '%') " 
                    + "AND   (@contacto IS NULL OR @contacto = '' OR [contador].[contacto] LIKE LTRIM(RTRIM(@contacto)) + '%') " 
                    + "";
            } else if  (sCondition == "More than...") {
                selectStatement
                    = "SELECT "
                + "     [contador].[contador_id] "
                + "    ,[contador].[nombre] "
                + "    ,[contador].[apellido] "
                + "    ,[contador].[direccion] "
                + "    ,[contador].[contacto] "
                + "FROM " 
                + "     [contador] " 
                    + "WHERE " 
                    + "     (@contador_id IS NULL OR @contador_id = '' OR [contador].[contador_id] > LTRIM(RTRIM(@contador_id))) " 
                    + "AND   (@nombre IS NULL OR @nombre = '' OR [contador].[nombre] > LTRIM(RTRIM(@nombre))) " 
                    + "AND   (@apellido IS NULL OR @apellido = '' OR [contador].[apellido] > LTRIM(RTRIM(@apellido))) " 
                    + "AND   (@direccion IS NULL OR @direccion = '' OR [contador].[direccion] > LTRIM(RTRIM(@direccion))) " 
                    + "AND   (@contacto IS NULL OR @contacto = '' OR [contador].[contacto] > LTRIM(RTRIM(@contacto))) " 
                    + "";
            } else if  (sCondition == "Less than...") {
                selectStatement
                    = "SELECT " 
                + "     [contador].[contador_id] "
                + "    ,[contador].[nombre] "
                + "    ,[contador].[apellido] "
                + "    ,[contador].[direccion] "
                + "    ,[contador].[contacto] "
                + "FROM " 
                + "     [contador] " 
                    + "WHERE " 
                    + "     (@contador_id IS NULL OR @contador_id = '' OR [contador].[contador_id] < LTRIM(RTRIM(@contador_id))) " 
                    + "AND   (@nombre IS NULL OR @nombre = '' OR [contador].[nombre] < LTRIM(RTRIM(@nombre))) " 
                    + "AND   (@apellido IS NULL OR @apellido = '' OR [contador].[apellido] < LTRIM(RTRIM(@apellido))) " 
                    + "AND   (@direccion IS NULL OR @direccion = '' OR [contador].[direccion] < LTRIM(RTRIM(@direccion))) " 
                    + "AND   (@contacto IS NULL OR @contacto = '' OR [contador].[contacto] < LTRIM(RTRIM(@contacto))) " 
                    + "";
            } else if  (sCondition == "Equal or more than...") {
                selectStatement
                    = "SELECT "
                + "     [contador].[contador_id] "
                + "    ,[contador].[nombre] "
                + "    ,[contador].[apellido] "
                + "    ,[contador].[direccion] "
                + "    ,[contador].[contacto] "
                + "FROM " 
                + "     [contador] " 
                    + "WHERE " 
                    + "     (@contador_id IS NULL OR @contador_id = '' OR [contador].[contador_id] >= LTRIM(RTRIM(@contador_id))) " 
                    + "AND   (@nombre IS NULL OR @nombre = '' OR [contador].[nombre] >= LTRIM(RTRIM(@nombre))) " 
                    + "AND   (@apellido IS NULL OR @apellido = '' OR [contador].[apellido] >= LTRIM(RTRIM(@apellido))) " 
                    + "AND   (@direccion IS NULL OR @direccion = '' OR [contador].[direccion] >= LTRIM(RTRIM(@direccion))) " 
                    + "AND   (@contacto IS NULL OR @contacto = '' OR [contador].[contacto] >= LTRIM(RTRIM(@contacto))) " 
                    + "";
            } else if (sCondition == "Equal or less than...") {
                selectStatement 
                    = "SELECT "
                + "     [contador].[contador_id] "
                + "    ,[contador].[nombre] "
                + "    ,[contador].[apellido] "
                + "    ,[contador].[direccion] "
                + "    ,[contador].[contacto] "
                + "FROM " 
                + "     [contador] " 
                    + "WHERE " 
                    + "     (@contador_id IS NULL OR @contador_id = '' OR [contador].[contador_id] <= LTRIM(RTRIM(@contador_id))) " 
                    + "AND   (@nombre IS NULL OR @nombre = '' OR [contador].[nombre] <= LTRIM(RTRIM(@nombre))) " 
                    + "AND   (@apellido IS NULL OR @apellido = '' OR [contador].[apellido] <= LTRIM(RTRIM(@apellido))) " 
                    + "AND   (@direccion IS NULL OR @direccion = '' OR [contador].[direccion] <= LTRIM(RTRIM(@direccion))) " 
                    + "AND   (@contacto IS NULL OR @contacto = '' OR [contador].[contacto] <= LTRIM(RTRIM(@contacto))) " 
                    + "";
            }
            SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
            selectCommand.CommandType = CommandType.Text;
            if (sField == "Contador Id") {
                selectCommand.Parameters.AddWithValue("@contador_id", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@contador_id", DBNull.Value); }
            if (sField == "Nombre") {
                selectCommand.Parameters.AddWithValue("@nombre", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@nombre", DBNull.Value); }
            if (sField == "Apellido") {
                selectCommand.Parameters.AddWithValue("@apellido", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@apellido", DBNull.Value); }
            if (sField == "Direccion") {
                selectCommand.Parameters.AddWithValue("@direccion", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@direccion", DBNull.Value); }
            if (sField == "Contacto") {
                selectCommand.Parameters.AddWithValue("@contacto", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@contacto", DBNull.Value); }
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

        public static contador Select_Record(contador contadorPara)
        {
        contador contador = new contador();
            SqlConnection connection = empresaData.GetConnection();
            string selectStatement
            = "SELECT " 
                + "     [contador_id] "
                + "    ,[nombre] "
                + "    ,[apellido] "
                + "    ,[direccion] "
                + "    ,[contacto] "
                + "FROM "
                + "     [contador] "
                + "WHERE "
                + "     [contador_id] = @contador_id "
                + "";
            SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
            selectCommand.CommandType = CommandType.Text;
            selectCommand.Parameters.AddWithValue("@contador_id", contadorPara.contador_id);
            try
            {
                connection.Open();
                SqlDataReader reader
                    = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    contador.contador_id = System.Convert.ToInt32(reader["contador_id"]);
                    contador.nombre = System.Convert.ToString(reader["nombre"]);
                    contador.apellido = System.Convert.ToString(reader["apellido"]);
                    contador.direccion = System.Convert.ToString(reader["direccion"]);
                    contador.contacto = System.Convert.ToString(reader["contacto"]);
                }
                else
                {
                    contador = null;
                }
                reader.Close();
            }
            catch (SqlException)
            {
                return contador;
            }
            finally
            {
                connection.Close();
            }
            return contador;
        }

        public static bool Add(contador contador)
        {
            SqlConnection connection = empresaData.GetConnection();
            string insertStatement
                = "INSERT " 
                + "     [contador] "
                + "     ( "
                + "     [contador_id] "
                + "    ,[nombre] "
                + "    ,[apellido] "
                + "    ,[direccion] "
                + "    ,[contacto] "
                + "     ) "
                + "VALUES " 
                + "     ( "
                + "     @contador_id "
                + "    ,@nombre "
                + "    ,@apellido "
                + "    ,@direccion "
                + "    ,@contacto "
                + "     ) "
                + "";
            SqlCommand insertCommand = new SqlCommand(insertStatement, connection);
            insertCommand.CommandType = CommandType.Text;
                insertCommand.Parameters.AddWithValue("@contador_id", contador.contador_id);
                insertCommand.Parameters.AddWithValue("@nombre", contador.nombre);
                insertCommand.Parameters.AddWithValue("@apellido", contador.apellido);
                insertCommand.Parameters.AddWithValue("@direccion", contador.direccion);
                insertCommand.Parameters.AddWithValue("@contacto", contador.contacto);
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

        public static bool Update(contador oldcontador, 
               contador newcontador)
        {
            SqlConnection connection = empresaData.GetConnection();
            string updateStatement
                = "UPDATE "  
                + "     [contador] "
                + "SET "
                + "     [contador_id] = @Newcontador_id "
                + "    ,[nombre] = @Newnombre "
                + "    ,[apellido] = @Newapellido "
                + "    ,[direccion] = @Newdireccion "
                + "    ,[contacto] = @Newcontacto "
                + "WHERE "
                + "     [contador_id] = @Oldcontador_id " 
                + " AND [nombre] = @Oldnombre " 
                + " AND [apellido] = @Oldapellido " 
                + " AND [direccion] = @Olddireccion " 
                + " AND [contacto] = @Oldcontacto " 
                + "";
            SqlCommand updateCommand = new SqlCommand(updateStatement, connection);
            updateCommand.CommandType = CommandType.Text;
            updateCommand.Parameters.AddWithValue("@Newcontador_id", newcontador.contador_id);
            updateCommand.Parameters.AddWithValue("@Newnombre", newcontador.nombre);
            updateCommand.Parameters.AddWithValue("@Newapellido", newcontador.apellido);
            updateCommand.Parameters.AddWithValue("@Newdireccion", newcontador.direccion);
            updateCommand.Parameters.AddWithValue("@Newcontacto", newcontador.contacto);
            updateCommand.Parameters.AddWithValue("@Oldcontador_id", oldcontador.contador_id);
            updateCommand.Parameters.AddWithValue("@Oldnombre", oldcontador.nombre);
            updateCommand.Parameters.AddWithValue("@Oldapellido", oldcontador.apellido);
            updateCommand.Parameters.AddWithValue("@Olddireccion", oldcontador.direccion);
            updateCommand.Parameters.AddWithValue("@Oldcontacto", oldcontador.contacto);
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

        public static bool Delete(contador contador)
        {
            SqlConnection connection = empresaData.GetConnection();
            string deleteStatement
                = "DELETE FROM "  
                + "     [contador] "
                + "WHERE " 
                + "     [contador_id] = @Oldcontador_id " 
                + " AND [nombre] = @Oldnombre " 
                + " AND [apellido] = @Oldapellido " 
                + " AND [direccion] = @Olddireccion " 
                + " AND [contacto] = @Oldcontacto " 
                + "";
            SqlCommand deleteCommand = new SqlCommand(deleteStatement, connection);
            deleteCommand.CommandType = CommandType.Text;
            deleteCommand.Parameters.AddWithValue("@Oldcontador_id", contador.contador_id);
            deleteCommand.Parameters.AddWithValue("@Oldnombre", contador.nombre);
            deleteCommand.Parameters.AddWithValue("@Oldapellido", contador.apellido);
            deleteCommand.Parameters.AddWithValue("@Olddireccion", contador.direccion);
            deleteCommand.Parameters.AddWithValue("@Oldcontacto", contador.contacto);
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
 
