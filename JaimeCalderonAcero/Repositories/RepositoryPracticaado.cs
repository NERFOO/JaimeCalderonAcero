using JaimeCalderonAcero.Helpers;
using JaimeCalderonAcero.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region PROCEDURES

/*
 CREATE PROCEDURE SP_CLIENTES
AS
	SELECT * FROM clientes
GO

CREATE PROCEDURE SP_CLIENTES_CONTACTO (@CONTACTO NVARCHAR (50))
AS
	SELECT * FROM clientes WHERE Contacto = @CONTACTO
GO

ALTER PROCEDURE SP_PEDIDOS_CLIENTE (@CONTACTO NVARCHAR (50))
AS
	SELECT Empresa, contacto, Cargo, Ciudad, Telefono, CodigoPedido FROM clientes
	INNER JOIN pedidos
	ON clientes.CodigoCliente = pedidos.CodigoCliente
	WHERE Contacto = @CONTACTO
GO

ALTER PROCEDURE SP_PEDIDO_BUSQUEDA (@PEDIDO NVARCHAR (50))
AS
	SELECT CodigoPedido, FechaEntrega, FormaEnvio, Importe FROM pedidos WHERE CodigoPedido = @PEDIDO
GO

CREATE PROCEDURE SP_DELETE_PEDIDO (@PEDIDO NVARCHAR (50))
AS
	DELETE FROM pedidos WHERE CodigoPedido = @PEDIDO
GO

CREATE PROCEDURE SP_CREAR_PEDIDO (@CodPedido NVARCHAR (50), @CodCliente NVARCHAR (50), @FechEntrega DATETIME, @FormaEnvio NVARCHAR (50), @Importe INT) 
AS
	INSERT INTO pedidos VALUES (@CodPedido, @CodCliente, @FechEntrega, @FormaEnvio, @Importe)
GO
 */

#endregion


namespace JaimeCalderonAcero.Repositories
{
    public class RepositoryPracticaado
    {

        private SqlCommand command;
        private SqlConnection connection;
        private SqlDataReader reader;

        public RepositoryPracticaado()
        {
            string connectionString = HelperConfiguration.GetConnectionString();
            this.connection = new SqlConnection(connectionString);
            this.command = new SqlCommand();
            this.command.Connection = this.connection;
        }

        public List<string> GetClientes()
        {
            List<string> clientes = new List<string>();

            this.command.CommandType = CommandType.StoredProcedure;
            this.command.CommandText = "SP_CLIENTES";

            this.connection.Open();
            this.reader = this.command.ExecuteReader();

            while (this.reader.Read())
            {
                string cliente = this.reader["Contacto"].ToString();

                clientes.Add(cliente);
            }

            this.connection.Close();
            this.reader.Close();

            return clientes;
        }

        public List<Cliente> CargarClientes(string nombre)
        {
            SqlParameter paramNombre = new SqlParameter("@CONTACTO", nombre);
            this.command.Parameters.Add(paramNombre);

            List<Cliente> clientes = new List<Cliente>();

            this.command.CommandType = CommandType.StoredProcedure;
            this.command.CommandText = "SP_CLIENTES_CONTACTO";

            this.connection.Open();
            this.reader = this.command.ExecuteReader();

            while (this.reader.Read())
            {
                string empresa = this.reader["Empresa"].ToString();
                string contacto = this.reader["Contacto"].ToString();
                string cargo = this.reader["Cargo"].ToString();
                string ciudad = this.reader["Ciudad"].ToString();
                int tel = int.Parse(this.reader["Telefono"].ToString());

                Cliente cli = new Cliente();

                cli.Empresa = empresa;
                cli.Contacto = contacto;
                cli.Cargo = cargo;
                cli.Ciudad = ciudad;
                cli.Telefono = tel;

                clientes.Add(cli);
            }
            this.reader.Close();
            this.connection.Close();
            this.command.Parameters.Clear();

            return clientes;
        }

        public List<string> GetPedidos(string nombre)
        {
            SqlParameter paramNombre = new SqlParameter("@CONTACTO", nombre);
            this.command.Parameters.Add(paramNombre);

            List<string> pedidos = new List<string>();

            this.command.CommandType = CommandType.StoredProcedure;
            this.command.CommandText = "SP_PEDIDOS_CLIENTE";

            this.connection.Open();
            this.reader = this.command.ExecuteReader();

            while (this.reader.Read())
            {
                string pedido = this.reader["CodigoPedido"].ToString();

                pedidos.Add(pedido);
            }

            this.connection.Close();
            this.reader.Close();

            return pedidos;
        }

        public List<Pedido> CargarDatosPedidos(string pedido)
        {
            List<Pedido> pedidos = new List<Pedido>();

            SqlParameter paramPedido = new SqlParameter("@PEDIDO", pedido);
            this.command.Parameters.Add(paramPedido);

            this.command.CommandType = CommandType.StoredProcedure;
            this.command.CommandText = "SP_PEDIDO_BUSQUEDA";

            this.connection.Open();
            this.reader = this.command.ExecuteReader();

            while (this.reader.Read())
            {
                string codPedido = this.reader["CodigoPedido"].ToString();
                DateTime fhcEntrega = DateTime.Parse(this.reader["FechaEntrega"].ToString());
                string envio = this.reader["FormaEnvio"].ToString();
                int importe = int.Parse(this.reader["Importe"].ToString());

                Pedido ped = new Pedido();

                ped.CodPedido = codPedido;
                ped.FechEntrega = fhcEntrega;
                ped.FormaEnvio = envio;
                ped.Importe = importe;

                pedidos.Add(ped);
            }
            this.reader.Close();
            this.connection.Close();
            this.command.Parameters.Clear();

            return pedidos;
        }

        public int DeletePedido(string pedido)
        {
            SqlParameter paramPedido = new SqlParameter("@PEDIDO", pedido);
            this.command.Parameters.Add(paramPedido);

            this.command.CommandType = CommandType.StoredProcedure;
            this.command.CommandText = "SP_DELETE_PEDIDO";

            this.connection.Open();
            int eliminados = this.command.ExecuteNonQuery();

            this.connection.Close();
            this.command.Parameters.Clear();

            return eliminados;
        }
    }
}
