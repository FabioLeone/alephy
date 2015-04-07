using System;
using System.Xml;
using System.Data;
using Npgsql;
using NpgsqlTypes;
using System.Configuration;
using System.Collections.Generic;
using System.Text;
using SIAO.SRV.TO;
using System.Data.SqlClient;
using System.Linq;
using SIAO.SRV.DAL;

namespace SIAO.SRV
{
	public class clsControl
	{
		public clsControl ()
		{
		}

		#region .:Variables:.
		clsDB clsDB = new clsDB();
		NpgsqlCommand cmm = new NpgsqlCommand();
		clsFuncs o = new clsFuncs();
		#endregion

		#region .:Search:.

		public static DataSet GetRedes()
		{
			DataSet ds = new DataSet();
			NpgsqlCommand cmm = new NpgsqlCommand();
			NpgsqlConnection cnn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString);
			clsDB clsDB = new clsDB();

			cmm.Connection = cnn;

			cmm.CommandText = "select id,name from t_networks order by name";

			if (clsDB.openConnection(cmm))
			{
				ds = clsDB.QueryDS(ref cmm, ref ds, "Redes");
			}
			clsDB.closeConnection(cmm);

			return ds;
		}

		public static Rede GetRedeByUserId(int intUserId)
		{
			DataSet ds = new DataSet();
			NpgsqlCommand cmm = new NpgsqlCommand();
			NpgsqlConnection cnn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString);
			Rede r = new Rede();

			cmm.Connection = cnn;
			cmm.CommandText = @"select n.id,n.name,s.user_id
				from t_networks n 
					inner join t_store_group s on n.id = s.network_id
				where s.user_id = @usuarioId";

			cmm.Parameters.Add("@usuarioId", NpgsqlDbType.Integer).Value = intUserId;

			if (clsDB.openConnection(cmm))
			{
				ds = clsDB.QueryDS(ref cmm, ref ds, "Rede");
			}
			clsDB.closeConnection(cmm);

			if (ds.Tables.Count > 0)
			{
				if (ds.Tables[0].Rows[0]["Id"] != DBNull.Value)
				{
					r.RedeId = Convert.ToInt16(ds.Tables[0].Rows[0]["Id"].ToString());
					r.RedeName = ds.Tables[0].Rows[0]["name"].ToString();
					r.UserId = Convert.ToInt16(ds.Tables[0].Rows[0]["user_id"].ToString() == "" ? 0 : ds.Tables[0].Rows[0]["user_id"]);
				}
			}

			return r;
		}

		internal static Rede GetRedeByLojaId(int loja_id)
		{
			DataSet ds = new DataSet();
			NpgsqlCommand cmm = new NpgsqlCommand();
			NpgsqlConnection cnn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString);
			Rede r = new Rede();

			cmm.Connection = cnn;
			cmm.CommandText = @"select n.id,n.name
				from t_networks n
					inner join t_stores s on n.id = s.network_id
				where s.id = @loja_id
				group by n.id
				order by n.name";

			cmm.Parameters.Add("@loja_id", NpgsqlDbType.Integer).Value = loja_id;

			if (clsDB.openConnection(cmm))
			{
				ds = clsDB.QueryDS(ref cmm, ref ds, "Rede");
			}
			clsDB.closeConnection(cmm);

			if (ds.Tables.Count > 0)
			{
				if (ds.Tables[0].Rows[0]["Id"] != DBNull.Value)
				{
					r.RedeId = Convert.ToInt16(ds.Tables[0].Rows[0]["id"].ToString());
					r.RedeName = ds.Tables[0].Rows[0]["name"].ToString();
				}
			}

			return r;
		}

		public static DataSet GetLojaByUserId(int intUserId)
		{
			DataSet ds = new DataSet();
			NpgsqlConnection cnn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString);
			NpgsqlCommand cmm = new NpgsqlCommand();

			cmm.Connection = cnn;
			cmm.CommandText = @"select distinct id,cnpj,fantasyname
				from (
					select s.id,s.cnpj,s.fantasyname
					from t_stores s
						inner join t_store_group sg on s.id = sg.store_id
					where sg.user_id = @usuarioid
					union
					select s.id,s.cnpj,s.fantasyname
					from t_stores s
						inner join t_store_group sg on s.network_id = sg.network_id
					where sg.user_id = @usuarioid
						and s.network_id > 0
				) as v
				order by fantasyname";

			cmm.Parameters.Clear();
			cmm.Parameters.Add("@usuarioid", NpgsqlDbType.Integer).Value = intUserId;

			if (clsDB.openConnection(cmm))
			{
				ds = clsDB.QueryDS(ref cmm, ref ds, "Farmacias");
			}
			clsDB.closeConnection(cmm);

			return ds;
		}

		internal static Loja GetLojaById(int intLoja)
		{
			DataSet ds = new DataSet();
			NpgsqlConnection cnn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString);
			NpgsqlCommand cmm = new NpgsqlCommand();
			Loja objLoja = new Loja();

			cmm.Connection = cnn;
			cmm.CommandText = @"select id,cnpj,fantasyname,network_id
				from t_stores
				where id = @id";

			cmm.Parameters.Clear();
			cmm.Parameters.Add("@id", NpgsqlDbType.Integer).Value = intLoja;

			if (clsDB.openConnection(cmm))
			{
				ds = clsDB.QueryDS(ref cmm, ref ds, "Farmacias");
			}
			clsDB.closeConnection(cmm);

			if (ds.Tables["Farmacias"].Rows.Count > 0)
			{
				objLoja.Cnpj = ds.Tables["Farmacias"].Rows[0]["cnpj"].ToString();
				objLoja.Id = Convert.ToInt32(ds.Tables["Farmacias"].Rows[0]["id"].ToString());
				objLoja.idRede = Convert.ToInt32(ds.Tables["Farmacias"].Rows[0]["network_id"].ToString());
				objLoja.NomeFantasia = ds.Tables["Farmacias"].Rows[0]["fantasyname"].ToString();
			}

			return objLoja;
		}

		internal static Rede GetRedeById(int intId)
		{
			DataSet ds = new DataSet();
			NpgsqlCommand cmm = new NpgsqlCommand();
			NpgsqlConnection cnn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString);
			Rede r = new Rede();

			cmm.Connection = cnn;
			cmm.CommandText = @"select id,name
				from t_networks 
				where id = @Id";

			cmm.Parameters.Add("@Id", NpgsqlDbType.Integer).Value = intId;

			if (clsDB.openConnection(cmm))
			{
				ds = clsDB.QueryDS(ref cmm, ref ds, "Rede");
			}
			clsDB.closeConnection(cmm);

			if (ds.Tables.Count > 0)
			{
				if (ds.Tables[0].Rows[0]["Id"] != DBNull.Value)
				{
					r.RedeId = Convert.ToInt16(ds.Tables[0].Rows[0]["id"].ToString());
					r.RedeName = ds.Tables[0].Rows[0]["name"].ToString();
				}
			}

			return r;
		}

		public static DataSet GetLojaByRedeId(int intRedeId)
		{
			DataSet ds = new DataSet();
			NpgsqlConnection cnn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString);
			NpgsqlCommand cmm = new NpgsqlCommand();

			cmm.Connection = cnn;
			cmm.CommandText = @"select id,cnpj,fantasyname,network_id
				from t_stores
				where network_id = @idRede
				order by fantasyname";

			cmm.Parameters.Clear();
			cmm.Parameters.Add("@idRede", NpgsqlDbType.Integer).Value = intRedeId;

			if (clsDB.openConnection(cmm))
			{
				ds = clsDB.QueryDS(ref cmm, ref ds, "Farmacias");
			}
			clsDB.closeConnection(cmm);

			return ds;
		}

		internal static DataSet GetLojaByRedeIdAndCity(int intRedeId, string city)
		{
			DataSet ds = new DataSet();
			NpgsqlConnection cnn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString);
			NpgsqlCommand cmm = new NpgsqlCommand();

			cmm.Connection = cnn;
			cmm.CommandText = @"select id,cnpj,fantasyname,network_id
				from t_stores
				where network_id = @idRede
					and city = @city
				order by fantasyname";

			cmm.Parameters.Clear();
			cmm.Parameters.Add("@idRede", NpgsqlDbType.Integer).Value = intRedeId;
			cmm.Parameters.Add("@city", NpgsqlDbType.Varchar).Value = city;

			if (clsDB.openConnection(cmm))
			{
				ds = clsDB.QueryDS(ref cmm, ref ds, "Farmacias");
			}
			clsDB.closeConnection(cmm);

			return ds;
		}

		internal static object GetLojaByRedeIdAndUf(int intRedeId, int ufId)
		{
			DataSet ds = new DataSet();
			NpgsqlConnection cnn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString);
			NpgsqlCommand cmm = new NpgsqlCommand();

			cmm.Connection = cnn;
			cmm.CommandText = @"select id,cnpj,fantasyname,network_id
				from t_stores
				where network_id = @idRede
					and state_id = @uf
				order by fantasyname";

			cmm.Parameters.Clear();
			cmm.Parameters.Add("@idRede", NpgsqlDbType.Integer).Value = intRedeId;
			cmm.Parameters.Add("@uf", NpgsqlDbType.Integer).Value = ufId;

			if (clsDB.openConnection(cmm))
			{
				ds = clsDB.QueryDS(ref cmm, ref ds, "Farmacias");
			}
			clsDB.closeConnection(cmm);

			return ds;
		}

		public static Loja GetLojaByCNPJ(string strCNPJ)
		{
			DataSet ds = new DataSet();
			NpgsqlConnection cnn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString);
			Loja clsLoja = new Loja();

			NpgsqlCommand cmm = new NpgsqlCommand();
			clsDB clsDB = new clsDB();

			cmm.Connection = cnn;
			StringBuilder strSQL = new StringBuilder();
			strSQL.Append(@"select id,owner,manager,emails,fantasyname,corporatename,cnpj,address,number,neighborhood,complement,city,state_id,phones,skype,active,network_id,zipcode
				from t_stores
				where cnpj = @Cnpj");

			string scnpj = strCNPJ.Replace(".", "");
			scnpj = scnpj.Replace("/", "");
			scnpj = scnpj.Replace("-", "");

			cmm.CommandText = strSQL.ToString();
			cmm.Parameters.Clear();
			cmm.Parameters.Add("@Cnpj", NpgsqlDbType.Varchar).Value = scnpj;

			if (clsDB.openConnection(cmm))
			{
				ds = clsDB.QueryDS(ref cmm, ref ds, "Loja");
			}
			clsDB.closeConnection(cmm);

			if (ds.Tables.Count > 0)
			{
				if (ds.Tables["Loja"].Rows[0]["Id"] != DBNull.Value)
				{
					clsLoja.Id = Convert.ToInt16(ds.Tables["Loja"].Rows[0]["id"].ToString());
					clsLoja.Proprietario = ds.Tables["Loja"].Rows[0]["owner"].ToString() == "" ? "" : ds.Tables["Loja"].Rows[0]["owner"].ToString();
					clsLoja.Gerente = ds.Tables["Loja"].Rows[0]["manager"].ToString() == "" ? "" : ds.Tables["Loja"].Rows[0]["manager"].ToString();
					string[] email = ds.Tables["Loja"].Rows[0]["emails"].ToString().Split(',');
					clsLoja.Email = email[0];
					if(email.Length > 1)
						clsLoja.Email2 = email[1];

					clsLoja.NomeFantasia = ds.Tables["Loja"].Rows[0]["fantasyname"].ToString();
					clsLoja.Razao = ds.Tables["Loja"].Rows[0]["corporatename"].ToString();
					clsLoja.Cnpj = ds.Tables["Loja"].Rows[0]["cnpj"].ToString();
					clsLoja.Endereco = ds.Tables["Loja"].Rows[0]["address"].ToString();
					clsLoja.EndNumero = ds.Tables["Loja"].Rows[0]["number"].ToString();
					clsLoja.Bairro = ds.Tables["Loja"].Rows[0]["neighborhood"].ToString();
					clsLoja.Complemento = ds.Tables["Loja"].Rows[0]["complement"].ToString();
					clsLoja.Cidade = ds.Tables["Loja"].Rows[0]["city"].ToString();
					clsLoja.Uf = ds.Tables["Loja"].Rows[0]["state_id"].ToString();
					string[] fone = ds.Tables["Loja"].Rows[0]["phones"].ToString().Split(',');
					clsLoja.Fone = fone[0];
					if(fone.Length > 1)
						clsLoja.Fone2 = fone[1];

					if(fone.Length > 2)
						clsLoja.Celular = fone[2];
						
					clsLoja.Skype = ds.Tables["Loja"].Rows[0]["skype"].ToString();
					clsLoja.Ativo = (ds.Tables["Loja"].Rows[0]["active"].ToString() == "1" ? true : false);
					clsLoja.idRede = Convert.ToInt32(ds.Tables["Loja"].Rows[0]["network_id"].ToString());
					clsLoja.CEP = ds.Tables["Loja"].Rows[0]["zipcode"].ToString();
				}
			}

			return clsLoja;
		}
		#endregion

		#region .:Methods:.
		public static void GetOption(System.Web.UI.HtmlControls.HtmlGenericControl ulConf, UsersTO ouser)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append(@"
            <li>
                <a href='wfmUsers.aspx' title=''>
                    <div class='imgCon'>
                        <p>Cadastro de usuários</p>
                    </div>
                </a>
            </li>");

			if (ouser.Nivel.Equals(0))
				sb.Append(@"
                <li>
                    <a href='wfmCadastroRede.aspx' title=''>
                        <div class='imgCon'>
                            <p>Cadastro de redes</p>
                        </div>
                    </a>
                </li>");

			if (!ouser.Nivel.Equals(2))
				sb.Append(@"
                <li>
                    <a href='wfmCadastroLojas.aspx' title=''>
                        <div class='imgCon'>
                            <p>Cadastro de lojas</p>
                        </div>
                    </a>
                </li>");

			sb.Append(@"
            <li>
                <a href='wfmVinculos.aspx' title=''>
                    <div class='imgCon'>
                        <p>Vincular usuário</p>
                    </div>
                </a>
            </li>");

			if (ouser.Nivel.Equals(0))
				sb.Append(@"
                <li>
                    <a href='wfmFiles.aspx' title=''>
                        <div class='imgCon'>
                            <p>Histórico de uploads</p>
                        </div>
                    </a>    
                </li>
                <li>
                    <a href='wfmBanco.aspx' title=''>
                            <div class='imgCon'>
                            <p>Gerênciamento do banco</p>
                        </div>
                    </a>
                </li>");

			ulConf.InnerHtml = sb.ToString();
		}
		#endregion

		#region .:Persistence:.

		internal static string AddUser(UsersTO clsUser)
		{
			NpgsqlCommand cmm = new NpgsqlCommand();
			clsDB clsDB = new clsDB();
			string msg = "";

			try
			{
				string pass = CDM.Cript(clsUser.Password);
				string nme = clsUser.UserName;

				NpgsqlConnection cnn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString);
				cmm.Connection = cnn;

				if (clsDB.openConnection(cmm))
				{
					cmm.CommandText = "select id from t_users where username = @UserName";
					cmm.Parameters.Clear();
					cmm.Parameters.Add("@UserName", NpgsqlDbType.Varchar).Value = clsUser.UserName;

					int id = 0;
					if (clsDB.Query(id, ref cmm) == DBNull.Value)
					{
						cmm.CommandText = @"insert into t_users (name,username,owner) values (@Name, @UserName, @owner)";
						cmm.Parameters.Clear();
						cmm.Parameters.Add("@Name", NpgsqlDbType.Varchar).Value = clsUser.Name;
						cmm.Parameters.Add("@UserName", NpgsqlDbType.Varchar).Value = clsUser.UserName;
						cmm.Parameters.Add("@owner", NpgsqlDbType.Integer).Value = clsUser.owner;

						clsDB.Execute(ref cmm);

						cmm.CommandText = "select id from t_users where username = @UserName";
						cmm.Parameters.Clear();
						cmm.Parameters.Add("@UserName", NpgsqlDbType.Varchar).Value = clsUser.UserName;

						id = (int)clsDB.Query(id, ref cmm);

						string cDate = clsUser.CreateDate.Year + "-" + clsUser.CreateDate.Month + "-" + clsUser.CreateDate.Day + " 00:00:00";
						string eDate = clsUser.ExpirationDate.Year + "-" + clsUser.ExpirationDate.Month + "-" + clsUser.ExpirationDate.Day + " 00:00:00";

						cmm.CommandText = @"insert into t_memberships (user_id,usertype_id,userlevel,username,password,email,createdate,expirationdate,lastactivitydate,active)
							values (" + id + ",@TipoId, @nivel, '" + nme + "', '" + pass + "', '" + clsUser.Email + "', '" + cDate + "', '" + eDate + "', @LastActivityDate, '" + (clsUser.Inactive == false ? 1 : 0) + "')";
						cmm.Parameters.Clear();
						cmm.Parameters.Add("@LastActivityDate", NpgsqlDbType.Date).Value = DateTime.Now.Date;
						cmm.Parameters.Add("@TipoId", NpgsqlDbType.Integer).Value = clsUser.TipoId;
						cmm.Parameters.Add("@nivel", NpgsqlDbType.Integer).Value = clsUser.Nivel;

						clsDB.Execute(ref cmm);
					}
					else { msg = "Usuário já cadastrado."; }
				}
			}
			catch (Exception ex)
			{
				msg = ex.Message;
			}

			return msg;
		}

		public string AddRede(string scn, Rede rede)
		{
			string msg = "";
			cmm.CommandText = "insert into t_networks (name,cnpj) values ('" + rede.RedeName + "','" + rede.CNPJ + "')";

			NpgsqlConnection cnn = new NpgsqlConnection(scn);
			cmm.Connection = cnn;

			try
			{
				if (clsDB.openConnection(cmm))
				{
					clsDB.Execute(ref cmm);
				}
			}
			catch (Exception ex)
			{
				msg = ex.Message;
			}
			clsDB.closeConnection(cmm);

			return msg;
		}

		public string UpdateRede(string scn, Rede rede)
		{
			string msg = "";
			cmm.CommandText = "update t_networks set name = '" + rede.RedeName + "', cnpj = '" + rede.CNPJ + "' where id = " + rede.RedeId;

			NpgsqlConnection cnn = new NpgsqlConnection(scn);
			cmm.Connection = cnn;

			try
			{
				if (clsDB.openConnection(cmm))
				{
					clsDB.Execute(ref cmm);
				}
			}
			catch (Exception ex)
			{
				msg = ex.Message;
			}
			clsDB.closeConnection(cmm);

			return msg;
		}

		public string AddLoja(string scn, Loja ol)
		{
			string msg = "";

			NpgsqlConnection cnn = new NpgsqlConnection(scn);
			cmm.Connection = cnn;

			string scnpj = ol.Cnpj.Replace(".", "");
			scnpj = scnpj.Replace("/", "");
			scnpj = scnpj.Replace("-", "");

			cmm.CommandText = @"insert into t_stores (owner,manager,emails,fantasyname,corporatename,cnpj,address,number,
				neighborhood,complement,city,state_id,phones,skype,active,network_id,zipcode) VALUES ('" + ol.Proprietario + "', '" + ol.Gerente + "', '" + ol.Email + "," 
				+ ol.Email2 + "', '" + ol.NomeFantasia + "', '" + ol.Razao + "', '" + scnpj + "', '" + ol.Endereco + "', '" + ol.EndNumero + "', '" + ol.Bairro + "', '"
				+ ol.Complemento + "', '" + ol.Cidade + "', '" + ol.Uf + "', '" + ol.Fone + "," + ol.Fone2 + "," + ol.Celular + "', '" + ol.Skype + "', '"
				+ (ol.Ativo == true ? 1 : 0) + "', " + ol.idRede + ",'" + ol.CEP + "')";

			try
			{
				if (clsDB.openConnection(cmm))
				{
					clsDB.Execute(ref cmm);
				}
			}
			catch (Exception ex)
			{
				msg = ex.Message;
			}
			clsDB.closeConnection(cmm);

			return msg;
		}

		public string UpdateLoja(string scn, Loja clsLoja)
		{
			string msg = "";

			NpgsqlConnection cnn = new NpgsqlConnection(scn);
			cmm.Connection = cnn;

			string scnpj = clsLoja.Cnpj.Replace(".", "");
			scnpj = scnpj.Replace("/", "");
			scnpj = scnpj.Replace("-", "");

			cmm.CommandText = "update t_stores set owner = '" + clsLoja.Proprietario + "', manager = '" + clsLoja.Gerente
				+ "', emails = '" + clsLoja.Email + "," + clsLoja.Email2 + "', fantasyname = '" + clsLoja.NomeFantasia
				+ "', corporatename = '" + clsLoja.Razao + "', cnpj = '" + scnpj + "', address = '" + clsLoja.Endereco + "', number = '"
				+ clsLoja.EndNumero + "', neighborhood = '" + clsLoja.Bairro + "', complement = '" + clsLoja.Complemento + "', city = '"
				+ clsLoja.Cidade + "', state_id = '" + clsLoja.Uf + "', phones = '" + clsLoja.Fone + "," + clsLoja.Fone2 + ", "
				+ clsLoja.Celular + "', skype = '" + clsLoja.Skype + "', active = '" + (clsLoja.Ativo == true ? 1 : 0) + "', network_id = " + clsLoja.idRede + ",zipcode = '" 
				+ clsLoja.CEP + "' WHERE id = " + clsLoja.Id;

			try
			{
				if (clsDB.openConnection(cmm))
				{
					clsDB.Execute(ref cmm);
				}
			}
			catch (Exception ex)
			{
				msg = ex.Message;
			}
			clsDB.closeConnection(cmm);

			return msg;
		}

		public string AddDbf(string scn, DataTable dt)
		{
			string msg = "";
			NpgsqlConnection msc = new NpgsqlConnection(scn);
			DataSet ds = new DataSet();

			cmm.Connection = msc;

			if (dt.Rows.Count > 0)
			{
				try
				{
					if (clsDB.openConnection(cmm))
					{
						cmm.CommandText = @"select p.barcode,p.codprod,p.nameprod,p.features,p.codlab,p.namelab,p.codpat,p.namepat,g.name as group_name,s.name as subgroup,p.sub_division,p.ncm,p.namepat_ncm,p.description_ncm,p.list,p.aliquot,p.st
							from t_products_base p
								left join t_group_products g on p.group_id = g.id
								left join t_subgroup_products s on p.subgroup_id = s.id
							where p.barcode in (";

						for (int i = 0; i < dt.Rows.Count; i++)
						{
							if (i > 0) { cmm.CommandText += ","; }
							cmm.CommandText += dt.Rows[i][16];
						}
						cmm.CommandText += ")";

						ds = clsDB.QueryDS(ref cmm, ref ds, "produtos_base");

						if (ds.Tables.Count > 0)
						{
							for (int i = 0; i < dt.Rows.Count; i++)
							{
								if (ds.Tables[0].Select("barcode = '" + dt.Rows[i][16] + "'").Length == 0)
								{
									cmm.CommandText = @"insert into t_products_base (barcode,codprod,nameprod,features,codlab,group_id,namepat_ncm,aliquot,st)
										values ('" + dt.Rows[i][16] + "', " + dt.Rows[i][0] + ", '" + dt.Rows[i][1] + "', '" + dt.Rows[i][35] + "', '" + dt.Rows[i][11] 
										+ "', (select id from t_group_products where lower(name) = lower(" + " '" + dt.Rows[i][2] + "')), '" + dt.Rows[i][41] + "', " + dt.Rows[i][54] 
										+ ", '" + dt.Rows[i][59] + "')";

									clsDB.Execute(ref cmm);
								}
								else
								{
									cmm.CommandText = "update t_products_base SET codprod = " + dt.Rows[i][0] + ", nameprod = '" + dt.Rows[i][1] + "', features = '"
										+ dt.Rows[i][35] + "', codlab = '" + dt.Rows[i][11] + "', group_id = (select id from t_group_products where lower(name) = lower('"
										+ dt.Rows[i][2] + "')), namepat_ncm = '" + dt.Rows[i][41]
										+ "', aliquot = " + dt.Rows[i][54] + ", st = '" + dt.Rows[i][59] + "'"
										+ " where barcode = " + dt.Rows[i][16];

									clsDB.Execute(ref cmm);
								}
							}
						}
						else
						{
							for (int i = 0; i < dt.Rows.Count; i++)
							{
								cmm.CommandText = @"insert into t_products_base (barcode,codprod,nameprod,features,codlab,group_id,namepat_ncm,aliquot,st)
									values ('" + dt.Rows[i][16] + "', " + dt.Rows[i][0] + ", '" + dt.Rows[i][1] + "', '" + dt.Rows[i][35] + "', '" + dt.Rows[i][11] 
									+ "', (select id from t_group_products where lower(name) = lower(" + " '" + dt.Rows[i][2] + "')), '" + dt.Rows[i][41] + "', " + dt.Rows[i][54] 
									+ ", '" + dt.Rows[i][59] + "')";

								clsDB.Execute(ref cmm);
							}
						}
					}
					clsDB.closeConnection(cmm);
				}
				catch (Exception)
				{

				}
			}
			else { msg = "Erro na gravação dos dados"; }
			return msg;
		}

		internal static string UpdateUser(UsersTO clsUser)
		{
			NpgsqlCommand cmm = new NpgsqlCommand();
			clsDB clsDB = new clsDB();
			string msg = "";

			try
			{
				string pass = CDM.Cript(clsUser.Password);
				string nme = clsUser.UserName;

				NpgsqlConnection cnn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString);
				cmm.Connection = cnn;

				if (clsDB.openConnection(cmm))
				{
					string lDate = DateTime.Today.Year + "-" + DateTime.Today.Month + "-" + DateTime.Today.Day + " 00:00:00";
					string stbSQL = "update t_users set username = '" + clsUser.UserName + "' where id = " + clsUser.UserId;

					cmm.CommandText = stbSQL;

					clsDB.Execute(ref cmm);

					string eDate = clsUser.ExpirationDate.Year + "-" + clsUser.ExpirationDate.Month + "-" + clsUser.ExpirationDate.Day + " 00:00:00";

					cmm.CommandText = @"update t_memberships set password = '" + pass + "', email = '"
						+ clsUser.Email + "', active = '" + (clsUser.Inactive == false ? 1 : 0)
						+ "', expirationdate = '" + eDate + "', username = '" + nme + "'";

					if (clsUser.TipoId > 0) cmm.CommandText += ",usertype_id = @TipoId";

					cmm.CommandText += ",userlevel = @nivel where user_id = " + clsUser.UserId;

					cmm.Parameters.Clear();
					cmm.Parameters.Add("@TipoId", NpgsqlDbType.Integer).Value = clsUser.TipoId;
					cmm.Parameters.Add("@nivel", NpgsqlDbType.Integer).Value = clsUser.Nivel;

					clsDB.Execute(ref cmm);
				}
			}
			catch (Exception ex)
			{
				msg = ex.Message;
			}

			return msg;
		}

		#endregion
	}
}

