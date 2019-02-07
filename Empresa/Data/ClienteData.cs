using System;
using System.Data;
using System.Data.SqlClient;
using Empresa.Models;

namespace Empresa.Data
{
    public class ClienteData
    {

        public static DataTable SelectAll()
        {
            SqlConnection connection = empresaData.GetConnection();
            string selectStatement
                = "SELECT "  
                + "     [Cliente].[Cliente_ID] "
                + "    ,[Cliente].[nombre] "
                + "    ,[Cliente].[Apellido] "
                + "    ,[Cliente].[Direccion] "
                + "    ,[Cliente].[Telefono] "
                + "    ,[Cliente].[Correo] "
                + "FROM " 
                + "     [Cliente] " 
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
                + "     [Cliente].[Cliente_ID] "
                + "    ,[Cliente].[nombre] "
                + "    ,[Cliente].[Apellido] "
                + "    ,[Cliente].[Direccion] "
                + "    ,[Cliente].[Telefono] "
                + "    ,[Cliente].[Correo] "
                + "FROM " 
                + "     [Cliente] " 
                    + "WHERE " 
                    + "     (@Cliente_ID IS NULL OR @Cliente_ID = '' OR [Cliente].[Cliente_ID] LIKE '%' + LTRIM(RTRIM(@Cliente_ID)) + '%') " 
                    + "AND   (@nombre IS NULL OR @nombre = '' OR [Cliente].[nombre] LIKE '%' + LTRIM(RTRIM(@nombre)) + '%') " 
                    + "AND   (@Apellido IS NULL OR @Apellido = '' OR [Cliente].[Apellido] LIKE '%' + LTRIM(RTRIM(@Apellido)) + '%') " 
                    + "AND   (@Direccion IS NULL OR @Direccion = '' OR [Cliente].[Direccion] LIKE '%' + LTRIM(RTRIM(@Direccion)) + '%') " 
                    + "AND   (@Telefono IS NULL OR @Telefono = '' OR [Cliente].[Telefono] LIKE '%' + LTRIM(RTRIM(@Telefono)) + '%') " 
                    + "AND   (@Correo IS NULL OR @Correo = '' OR [Cliente].[Correo] LIKE '%' + LTRIM(RTRIM(@Correo)) + '%') " 
                    + "";
            } else if (sCondition == "Equals") {
                selectStatement
                    = "SELECT "
                + "     [Cliente].[Cliente_ID] "
                + "    ,[Cliente].[nombre] "
                + "    ,[Cliente].[Apellido] "
                + "    ,[Cliente].[Direccion] "
                + "    ,[Cliente].[Telefono] "
                + "    ,[Cliente].[Correo] "
                + "FROM " 
                + "     [Cliente] " 
                    + "WHERE " 
                    + "     (@Cliente_ID IS NULL OR @Cliente_ID = '' OR [Cliente].[Cliente_ID] = LTRIM(RTRIM(@Cliente_ID))) " 
                    + "AND   (@nombre IS NULL OR @nombre = '' OR [Cliente].[nombre] = LTRIM(RTRIM(@nombre))) " 
                    + "AND   (@Apellido IS NULL OR @Apellido = '' OR [Cliente].[Apellido] = LTRIM(RTRIM(@Apellido))) " 
                    + "AND   (@Direccion IS NULL OR @Direccion = '' OR [Cliente].[Direccion] = LTRIM(RTRIM(@Direccion))) " 
                    + "AND   (@Telefono IS NULL OR @Telefono = '' OR [Cliente].[Telefono] = LTRIM(RTRIM(@Telefono))) " 
                    + "AND   (@Correo IS NULL OR @Correo = '' OR [Cliente].[Correo] = LTRIM(RTRIM(@Correo))) " 
                    + "";
            } else if  (sCondition == "Starts with...") {
                selectStatement
                    = "SELECT "
                + "     [Cliente].[Cliente_ID] "
                + "    ,[Cliente].[nombre] "
                + "    ,[Cliente].[Apellido] "
                + "    ,[Cliente].[Direccion] "
                + "    ,[Cliente].[Telefono] "
                + "    ,[Cliente].[Correo] "
                + "FROM " 
                + "     [Cliente] " 
                    + "WHERE " 
                    + "     (@Cliente_ID IS NULL OR @Cliente_ID = '' OR [Cliente].[Cliente_ID] LIKE LTRIM(RTRIM(@Cliente_ID)) + '%') " 
                    + "AND   (@nombre IS NULL OR @nombre = '' OR [Cliente].[nombre] LIKE LTRIM(RTRIM(@nombre)) + '%') " 
                    + "AND   (@Apellido IS NULL OR @Apellido = '' OR [Cliente].[Apellido] LIKE LTRIM(RTRIM(@Apellido)) + '%') " 
                    + "AND   (@Direccion IS NULL OR @Direccion = '' OR [Cliente].[Direccion] LIKE LTRIM(RTRIM(@Direccion)) + '%') " 
                    + "AND   (@Telefono IS NULL OR @Telefono = '' OR [Cliente].[Telefono] LIKE LTRIM(RTRIM(@Telefono)) + '%') " 
                    + "AND   (@Correo IS NULL OR @Correo = '' OR [Cliente].[Correo] LIKE LTRIM(RTRIM(@Correo)) + '%') " 
                    + "";
            } else if  (sCondition == "More than...") {
                selectStatement
                    = "SELECT "
                + "     [Cliente].[Cliente_ID] "
                + "    ,[Cliente].[nombre] "
                + "    ,[Cliente].[Apellido] "
                + "    ,[Cliente].[Direccion] "
                + "    ,[Cliente].[Telefono] "
                + "    ,[Cliente].[Correo] "
                + "FROM " 
                + "     [Cliente] " 
                    + "WHERE " 
                    + "     (@Cliente_ID IS NULL OR @Cliente_ID = '' OR [Cliente].[Cliente_ID] > LTRIM(RTRIM(@Cliente_ID))) " 
                    + "AND   (@nombre IS NULL OR @nombre = '' OR [Cliente].[nombre] > LTRIM(RTRIM(@nombre))) " 
                    + "AND   (@Apellido IS NULL OR @Apellido = '' OR [Cliente].[Apellido] > LTRIM(RTRIM(@Apellido))) " 
                    + "AND   (@Direccion IS NULL OR @Direccion = '' OR [Cliente].[Direccion] > LTRIM(RTRIM(@Direccion))) " 
                    + "AND   (@Telefono IS NULL OR @Telefono = '' OR [Cliente].[Telefono] > LTRIM(RTRIM(@Telefono))) " 
                    + "AND   (@Correo IS NULL OR @Correo = '' OR [Cliente].[Correo] > LTRIM(RTRIM(@Correo))) " 
                    + "";
            } else if  (sCondition == "Less than...") {
                selectStatement
                    = "SELECT " 
                + "     [Cliente].[Cliente_ID] "
                + "    ,[Cliente].[nombre] "
                + "    ,[Cliente].[Apellido] "
                + "    ,[Cliente].[Direccion] "
                + "    ,[Cliente].[Telefono] "
                + "    ,[Cliente].[Correo] "
                + "FROM " 
                + "     [Cliente] " 
                    + "WHERE " 
                    + "     (@Cliente_ID IS NULL OR @Cliente_ID = '' OR [Cliente].[Cliente_ID] < LTRIM(RTRIM(@Cliente_ID))) " 
                    + "AND   (@nombre IS NULL OR @nombre = '' OR [Cliente].[nombre] < LTRIM(RTRIM(@nombre))) " 
                    + "AND   (@Apellido IS NULL OR @Apellido = '' OR [Cliente].[Apellido] < LTRIM(RTRIM(@Apellido))) " 
                    + "AND   (@Direccion IS NULL OR @Direccion = '' OR [Cliente].[Direccion] < LTRIM(RTRIM(@Direccion))) " 
                    + "AND   (@Telefono IS NULL OR @Telefono = '' OR [Cliente].[Telefono] < LTRIM(RTRIM(@Telefono))) " 
                    + "AND   (@Correo IS NULL OR @Correo = '' OR [Cliente].[Correo] < LTRIM(RTRIM(@Correo))) " 
                    + "";
            } else if  (sCondition == "Equal or more than...") {
                selectStatement
                    = "SELECT "
                + "     [Cliente].[Cliente_ID] "
                + "    ,[Cliente].[nombre] "
                + "    ,[Cliente].[Apellido] "
                + "    ,[Cliente].[Direccion] "
                + "    ,[Cliente].[Telefono] "
                + "    ,[Cliente].[Correo] "
                + "FROM " 
                + "     [Cliente] " 
                    + "WHERE " 
                    + "     (@Cliente_ID IS NULL OR @Cliente_ID = '' OR [Cliente].[Cliente_ID] >= LTRIM(RTRIM(@Cliente_ID))) " 
                    + "AND   (@nombre IS NULL OR @nombre = '' OR [Cliente].[nombre] >= LTRIM(RTRIM(@nombre))) " 
                    + "AND   (@Apellido IS NULL OR @Apellido = '' OR [Cliente].[Apellido] >= LTRIM(RTRIM(@Apellido))) " 
                    + "AND   (@Direccion IS NULL OR @Direccion = '' OR [Cliente].[Direccion] >= LTRIM(RTRIM(@Direccion))) " 
                    + "AND   (@Telefono IS NULL OR @Telefono = '' OR [Cliente].[Telefono] >= LTRIM(RTRIM(@Telefono))) " 
                    + "AND   (@Correo IS NULL OR @Correo = '' OR [Cliente].[Correo] >= LTRIM(RTRIM(@Correo))) " 
                    + "";
            } else if (sCondition == "Equal or less than...") {
                selectStatement 
                    = "SELECT "
                + "     [Cliente].[Cliente_ID] "
                + "    ,[Cliente].[nombre] "
                + "    ,[Cliente].[Apellido] "
                + "    ,[Cliente].[Direccion] "
                + "    ,[Cliente].[Telefono] "
                + "    ,[Cliente].[Correo] "
                + "FROM " 
                + "     [Cliente] " 
                    + "WHERE " 
                    + "     (@Cliente_ID IS NULL OR @Cliente_ID = '' OR [Cliente].[Cliente_ID] <= LTRIM(RTRIM(@Cliente_ID))) " 
                    + "AND   (@nombre IS NULL OR @nombre = '' OR [Cliente].[nombre] <= LTRIM(RTRIM(@nombre))) " 
                    + "AND   (@Apellido IS NULL OR @Apellido = '' OR [Cliente].[Apellido] <= LTRIM(RTRIM(@Apellido))) " 
                    + "AND   (@Direccion IS NULL OR @Direccion = '' OR [Cliente].[Direccion] <= LTRIM(RTRIM(@Direccion))) " 
                    + "AND   (@Telefono IS NULL OR @Telefono = '' OR [Cliente].[Telefono] <= LTRIM(RTRIM(@Telefono))) " 
                    + "AND   (@Correo IS NULL OR @Correo = '' OR [Cliente].[Correo] <= LTRIM(RTRIM(@Correo))) " 
                    + "";
            }
            SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
            selectCommand.CommandType = CommandType.Text;
            if (sField == "Cliente I D") {
                selectCommand.Parameters.AddWithValue("@Cliente_ID", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@Cliente_ID", DBNull.Value); }
            if (sField == "Nombre") {
                selectCommand.Parameters.AddWithValue("@nombre", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@nombre", DBNull.Value); }
            if (sField == "Apellido") {
                selectCommand.Parameters.AddWithValue("@Apellido", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@Apellido", DBNull.Value); }
            if (sField == "Direccion") {
                selectCommand.Parameters.AddWithValue("@Direccion", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@Direccion", DBNull.Value); }
            if (sField == "Telefono") {
                selectCommand.Parameters.AddWithValue("@Telefono", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@Telefono", DBNull.Value); }
            if (sField == "Correo") {
                selectCommand.Parameters.AddWithValue("@Correo", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@Correo", DBNull.Value); }
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

        public static Cliente Select_Record(Cliente ClientePara)
        {
        Cliente Cliente = new Cliente();
            SqlConnection connection = empresaData.GetConnection();
            string selectStatement
            = "SELECT " 
                + "     [Cliente_ID] "
                + "    ,[nombre] "
                + "    ,[Apellido] "
                + "    ,[Direccion] "
                + "    ,[Telefono] "
                + "    ,[Correo] "
                + "FROM "
                + "     [Cliente] "
                + "WHERE "
                + "     [Cliente_ID] = @Cliente_ID "
                + "";
            SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
            selectCommand.CommandType = CommandType.Text;
            selectCommand.Parameters.AddWithValue("@Cliente_ID", ClientePara.Cliente_ID);
            try
            {
                connection.Open();
                SqlDataReader reader
                    = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    Cliente.Cliente_ID = System.Convert.ToInt32(reader["Cliente_ID"]);
                    Cliente.nombre = reader["nombre"] is DBNull ? null : reader["nombre"].ToString();
                    Cliente.Apellido = reader["Apellido"] is DBNull ? null : reader["Apellido"].ToString();
                    Cliente.Direccion = reader["Direccion"] is DBNull ? null : reader["Direccion"].ToString();
                    Cliente.Telefono = reader["Telefono"] is DBNull ? null : reader["Telefono"].ToString();
                    Cliente.Correo = reader["Correo"] is DBNull ? null : reader["Correo"].ToString();
                }
                else
                {
                    Cliente = null;
                }
                reader.Close();
            }
            catch (SqlException)
            {
                return Cliente;
            }
            finally
            {
                connection.Close();
            }
            return Cliente;
        }

        public static bool Add(Cliente Cliente)
        {
            SqlConnection connection = empresaData.GetConnection();
            string insertStatement
                = "INSERT " 
                + "     [Cliente] "
                + "     ( "
                + "     [Cliente_ID] "
                + "    ,[nombre] "
                + "    ,[Apellido] "
                + "    ,[Direccion] "
                + "    ,[Telefono] "
                + "    ,[Correo] "
                + "     ) "
                + "VALUES " 
                + "     ( "
                + "     @Cliente_ID "
                + "    ,@nombre "
                + "    ,@Apellido "
                + "    ,@Direccion "
                + "    ,@Telefono "
                + "    ,@Correo "
                + "     ) "
                + "";
            SqlCommand insertCommand = new SqlCommand(insertStatement, connection);
            insertCommand.CommandType = CommandType.Text;
                insertCommand.Parameters.AddWithValue("@Cliente_ID", Cliente.Cliente_ID);
            if (Cliente.nombre != null) {
                insertCommand.Parameters.AddWithValue("@nombre", Cliente.nombre);
            } else {
                insertCommand.Parameters.AddWithValue("@nombre", DBNull.Value); }
            if (Cliente.Apellido != null) {
                insertCommand.Parameters.AddWithValue("@Apellido", Cliente.Apellido);
            } else {
                insertCommand.Parameters.AddWithValue("@Apellido", DBNull.Value); }
            if (Cliente.Direccion != null) {
                insertCommand.Parameters.AddWithValue("@Direccion", Cliente.Direccion);
            } else {
                insertCommand.Parameters.AddWithValue("@Direccion", DBNull.Value); }
            if (Cliente.Telefono != null) {
                insertCommand.Parameters.AddWithValue("@Telefono", Cliente.Telefono);
            } else {
                insertCommand.Parameters.AddWithValue("@Telefono", DBNull.Value); }
            if (Cliente.Correo != null) {
                insertCommand.Parameters.AddWithValue("@Correo", Cliente.Correo);
            } else {
                insertCommand.Parameters.AddWithValue("@Correo", DBNull.Value); }
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

        public static bool Update(Cliente oldCliente, 
               Cliente newCliente)
        {
            SqlConnection connection = empresaData.GetConnection();
            string updateStatement
                = "UPDATE "  
                + "     [Cliente] "
                + "SET "
                + "     [Cliente_ID] = @NewCliente_ID "
                + "    ,[nombre] = @Newnombre "
                + "    ,[Apellido] = @NewApellido "
                + "    ,[Direccion] = @NewDireccion "
                + "    ,[Telefono] = @NewTelefono "
                + "    ,[Correo] = @NewCorreo "
                + "WHERE "
                + "     [Cliente_ID] = @OldCliente_ID " 
                + " AND ((@Oldnombre IS NULL AND [nombre] IS NULL) OR [nombre] = @Oldnombre) " 
                + " AND ((@OldApellido IS NULL AND [Apellido] IS NULL) OR [Apellido] = @OldApellido) " 
                + " AND ((@OldDireccion IS NULL AND [Direccion] IS NULL) OR [Direccion] = @OldDireccion) " 
                + " AND ((@OldTelefono IS NULL AND [Telefono] IS NULL) OR [Telefono] = @OldTelefono) " 
                + " AND ((@OldCorreo IS NULL AND [Correo] IS NULL) OR [Correo] = @OldCorreo) " 
                + "";
            SqlCommand updateCommand = new SqlCommand(updateStatement, connection);
            updateCommand.CommandType = CommandType.Text;
            updateCommand.Parameters.AddWithValue("@NewCliente_ID", newCliente.Cliente_ID);
            if (newCliente.nombre != null) {
                updateCommand.Parameters.AddWithValue("@Newnombre", newCliente.nombre);
            } else {
                updateCommand.Parameters.AddWithValue("@Newnombre", DBNull.Value); }
            if (newCliente.Apellido != null) {
                updateCommand.Parameters.AddWithValue("@NewApellido", newCliente.Apellido);
            } else {
                updateCommand.Parameters.AddWithValue("@NewApellido", DBNull.Value); }
            if (newCliente.Direccion != null) {
                updateCommand.Parameters.AddWithValue("@NewDireccion", newCliente.Direccion);
            } else {
                updateCommand.Parameters.AddWithValue("@NewDireccion", DBNull.Value); }
            if (newCliente.Telefono != null) {
                updateCommand.Parameters.AddWithValue("@NewTelefono", newCliente.Telefono);
            } else {
                updateCommand.Parameters.AddWithValue("@NewTelefono", DBNull.Value); }
            if (newCliente.Correo != null) {
                updateCommand.Parameters.AddWithValue("@NewCorreo", newCliente.Correo);
            } else {
                updateCommand.Parameters.AddWithValue("@NewCorreo", DBNull.Value); }
            updateCommand.Parameters.AddWithValue("@OldCliente_ID", oldCliente.Cliente_ID);
            if (oldCliente.nombre != null) {
                updateCommand.Parameters.AddWithValue("@Oldnombre", oldCliente.nombre);
            } else {
                updateCommand.Parameters.AddWithValue("@Oldnombre", DBNull.Value); }
            if (oldCliente.Apellido != null) {
                updateCommand.Parameters.AddWithValue("@OldApellido", oldCliente.Apellido);
            } else {
                updateCommand.Parameters.AddWithValue("@OldApellido", DBNull.Value); }
            if (oldCliente.Direccion != null) {
                updateCommand.Parameters.AddWithValue("@OldDireccion", oldCliente.Direccion);
            } else {
                updateCommand.Parameters.AddWithValue("@OldDireccion", DBNull.Value); }
            if (oldCliente.Telefono != null) {
                updateCommand.Parameters.AddWithValue("@OldTelefono", oldCliente.Telefono);
            } else {
                updateCommand.Parameters.AddWithValue("@OldTelefono", DBNull.Value); }
            if (oldCliente.Correo != null) {
                updateCommand.Parameters.AddWithValue("@OldCorreo", oldCliente.Correo);
            } else {
                updateCommand.Parameters.AddWithValue("@OldCorreo", DBNull.Value); }
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

        public static bool Delete(Cliente Cliente)
        {
            SqlConnection connection = empresaData.GetConnection();
            string deleteStatement
                = "DELETE FROM "  
                + "     [Cliente] "
                + "WHERE " 
                + "     [Cliente_ID] = @OldCliente_ID " 
                + " AND ((@Oldnombre IS NULL AND [nombre] IS NULL) OR [nombre] = @Oldnombre) " 
                + " AND ((@OldApellido IS NULL AND [Apellido] IS NULL) OR [Apellido] = @OldApellido) " 
                + " AND ((@OldDireccion IS NULL AND [Direccion] IS NULL) OR [Direccion] = @OldDireccion) " 
                + " AND ((@OldTelefono IS NULL AND [Telefono] IS NULL) OR [Telefono] = @OldTelefono) " 
                + " AND ((@OldCorreo IS NULL AND [Correo] IS NULL) OR [Correo] = @OldCorreo) " 
                + "";
            SqlCommand deleteCommand = new SqlCommand(deleteStatement, connection);
            deleteCommand.CommandType = CommandType.Text;
            deleteCommand.Parameters.AddWithValue("@OldCliente_ID", Cliente.Cliente_ID);
            if (Cliente.nombre != null) {
                deleteCommand.Parameters.AddWithValue("@Oldnombre", Cliente.nombre);
            } else {
                deleteCommand.Parameters.AddWithValue("@Oldnombre", DBNull.Value); }
            if (Cliente.Apellido != null) {
                deleteCommand.Parameters.AddWithValue("@OldApellido", Cliente.Apellido);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldApellido", DBNull.Value); }
            if (Cliente.Direccion != null) {
                deleteCommand.Parameters.AddWithValue("@OldDireccion", Cliente.Direccion);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldDireccion", DBNull.Value); }
            if (Cliente.Telefono != null) {
                deleteCommand.Parameters.AddWithValue("@OldTelefono", Cliente.Telefono);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldTelefono", DBNull.Value); }
            if (Cliente.Correo != null) {
                deleteCommand.Parameters.AddWithValue("@OldCorreo", Cliente.Correo);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldCorreo", DBNull.Value); }
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
 
