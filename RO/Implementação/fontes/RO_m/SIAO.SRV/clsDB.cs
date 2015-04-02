using System;
using System.Data;
using Npgsql;

namespace SIAO.SRV
{
	public class clsDB
	{
		public clsDB ()
		{
		}

		// Abre o acesso ao banco
		public static bool openConnection(NpgsqlCommand cmm)
		{
			bool lbOk = false;
			try
			{
				cmm.Connection.Open();
				lbOk = true;
			}
			catch
			{
				lbOk = false;
			}

			return lbOk;
		}

		// Fecha o acesso ao banco
		public static void closeConnection(NpgsqlCommand cmm)
		{
			try
			{
				if (cmm.Connection.State == System.Data.ConnectionState.Open)
				{
					cmm.Connection.Close();
				}
			}
			catch
			{
			}
		}

		// Traz o retorno do banco
		public object Query(object retorno, ref NpgsqlCommand cmm)
		{
			DataSet ds = new DataSet();

			try
			{
				if (cmm.Connection.State == System.Data.ConnectionState.Closed)
				{
					openConnection(cmm);
				}
			}
			catch
			{
				openConnection(cmm);
			}

			try
			{
				NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmm);

				da.Fill(ds);
			}
			catch
			{

			}

			if (ds.Tables[0].Rows.Count > 0) { return defineTipoRetorno(retorno, ds); } else { return defineTipoRetorno(retorno, addLine(ref ds, 0)); }

		}

		// Difine o tipo de retorno do banco
		private object defineTipoRetorno(object retorno, DataSet ds)
		{
			switch (retorno.GetType().Name)
			{
			case "DataSet":
				return (DataSet)ds;
			case "DataRow":
				return (DataRow)ds.Tables[0].Rows[0];
			case "DataColumn":
				return (DataColumn)ds.Tables[0].Columns[0];
			default:
				return (object)ds.Tables[0].Rows[0][0];
			}
		}

		// Executa SQL
		public static void Execute(ref NpgsqlCommand cmm)
		{
			try
			{
				if (cmm.Connection.State == System.Data.ConnectionState.Closed)
				{
					openConnection(cmm);
				}
			}
			catch
			{
				openConnection(cmm);
			}

			cmm.ExecuteNonQuery();
		}

		// Traz o dataset do banco
		public static DataSet QueryDS(ref NpgsqlCommand cmm, ref DataSet ds, string nomeTabela)
		{
			try
			{
				if (cmm.Connection.State == System.Data.ConnectionState.Closed)
				{
					openConnection(cmm);
				}
			}
			catch
			{
				openConnection(cmm);
			}

			try
			{
				NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmm);

				da.Fill(ds, nomeTabela);
			}
			catch
			{
				DataTable dt = new DataTable();
				dt.TableName = nomeTabela;
				ds.Tables.Add(dt);
			}

			if (ds.Tables[nomeTabela].Rows.Count > 0) { return ds; } else { return addLine(ref ds, nomeTabela); }
		}

		internal static DataTable QueryDS(ref NpgsqlCommand cmm, ref DataTable dt, string nomeTabela)
		{
			DataSet ds = new DataSet();

			try
			{
				if (cmm.Connection.State == System.Data.ConnectionState.Closed)
				{
					openConnection(cmm);
				}
			}
			catch
			{
				openConnection(cmm);
			}

			try
			{
				NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmm);

				da.Fill(ds, nomeTabela);
			}
			catch
			{
				dt = new DataTable();
				dt.TableName = nomeTabela;
			}

			if (ds.Tables[nomeTabela].Rows.Count > 0) { dt = ds.Tables[0]; } else { dt = addLine(ref ds, nomeTabela).Tables[0]; }

			return dt;
		}

		// Acrescenta uma linha a tabela
		private static DataSet addLine(ref DataSet ds, object Tabela)
		{
			if (Tabela.GetType().Name == "String")
			{
				DataRow dr = ds.Tables[Tabela.ToString()].NewRow();

				if (ds.Tables[Tabela.ToString()].Columns.Count > 0)
				{
					for (int i = 0; i < ds.Tables[Tabela.ToString()].Columns.Count; i++) { dr[i] = DBNull.Value; }

					ds.Tables[Tabela.ToString()].Rows.Add(dr);
				}
			}
			else
			{
				DataRow dr = ds.Tables[(int)Tabela].NewRow();

				if (ds.Tables[(int)Tabela].Columns.Count > 0)
				{
					for (int i = 0; i < ds.Tables[(int)Tabela].Columns.Count; i++) { dr[i] = DBNull.Value; }

					ds.Tables[(int)Tabela].Rows.Add(dr);
				}
			}


			return ds;
		}
	}
}

