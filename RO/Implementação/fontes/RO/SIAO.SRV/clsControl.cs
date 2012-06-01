using System;
using System.Data;
using System.Xml;
using System.Collections.Generic;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Text;

namespace SIAO.SRV
{
    public class clsControl
    {
        clsDB oDB = new clsDB();
        MySqlCommand cmm = new MySqlCommand();
        clsFuncs o = new clsFuncs();

        public string AddUser(Usuario u, string scn) {
            string msg = "";

            try
            {
                string pass = o.encr(u.Password);
                string acs = o.encr(u.Access);
                string nme = o.encr(u.AcsName.ToUpper());

                MySqlConnection cnn = new MySqlConnection(scn);
                cmm.Connection = cnn;

                if (oDB.openConnection(cmm))
                {
                    cmm.CommandText = "SELECT UserId FROM  users WHERE UserName = '" + u.Name + "'";

                    int id = 0;
                    if (oDB.Query(id, ref cmm) == DBNull.Value)
                    {
                        string lDate = DateTime.Today.Year + "-" + DateTime.Today.Month + "-" + DateTime.Today.Day + " 00:00:00";
                        
                        cmm.CommandText = "INSERT INTO users"
                            + " (UserName, LastActivityDate)"
                            + " VALUES ('" + u.Name + "', '" + lDate + "')";

                        oDB.Execute(ref cmm);

                        cmm.CommandText = "SELECT UserId FROM  users WHERE UserName = '" + u.Name + "'";

                        id = (int)oDB.Query(id, ref cmm);

                        string cDate = u.CreateDate.Year + "-" + u.CreateDate.Month + "-" + u.CreateDate.Day + " 00:00:00";
                        string eDate = u.ExpirationDate.Year + "-" + u.ExpirationDate.Month + "-" + u.ExpirationDate.Day + " 00:00:00";

                        cmm.CommandText = "INSERT INTO memberships"
                            + " (UserId, Password, Email, Inactive, CreateDate, ExpirationDate, Access, Name)"
                            + " VALUES (" + id + ", '" + pass + "', '" + u.Email + "', "
                            + (u.Inactive == false ? 0 : 1) + ", '" + cDate + "', '" + eDate + "', '" 
                            + acs + "','"+ nme +"')";

                        oDB.Execute(ref cmm);

                        cmm.CommandText = "INSERT INTO usuarios_farmacias"
                            +" (UserId, FarmaciaId)"
                            +" VALUES ("+ id +", "+ u.FarmaciaId +")";

                        oDB.Execute(ref cmm);
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

        public System.Data.DataSet GetUser(string scn, string p)
        {
            string s = o.encr(p);
            DataSet ds = new DataSet();

            cmm.CommandText = "SELECT users.UserName, memberships.UserId"
                +" FROM  users INNER JOIN"
                +" memberships ON users.UserId = memberships.UserId"
                +" WHERE (memberships.Inactive = 0) AND (memberships.Access = '"+ s +"')"
                +" AND (memberships.ExpirationDate > SYSDATE())";

            MySqlConnection cnn = new MySqlConnection(scn);
            cmm.Connection = cnn;

            if (oDB.openConnection(cmm)) {
                ds = oDB.QueryDS(ref cmm, ref ds, "Gerentes");
            }
            oDB.closeConnection(cmm);
            
            return ds;
        }

        public string AddRede(string scn, Rede rede)
        {
            string msg = "";
            cmm.CommandText = "INSERT INTO redesfarmaceuticas (Descricao, UserId)"
                +" VALUES ('"+ rede.RedeName +"', '"+ rede.UserId +"')";

            MySqlConnection cnn = new MySqlConnection(scn);
            cmm.Connection = cnn;

            try
            {
                if (oDB.openConnection(cmm))
                {
                    oDB.Execute(ref cmm);
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            oDB.closeConnection(cmm);

            return msg;
        }

        public string UpdateRede(string scn, Rede rede)
        {
            string msg = "";
            cmm.CommandText = "UPDATE redesfarmaceuticas SET Descricao = '" + rede.RedeName
                + "', UserId = '" + rede.UserId + "' WHERE Id = "+ rede.RedeId;

            MySqlConnection cnn = new MySqlConnection(scn);
            cmm.Connection = cnn;

            try
            {
                if (oDB.openConnection(cmm))
                {
                    oDB.Execute(ref cmm);
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            oDB.closeConnection(cmm);

            return msg;
        }

        public DataSet GetRedes(string scn)
        {
            DataSet ds = new DataSet();

            MySqlConnection cnn = new MySqlConnection(scn);
            cmm.Connection = cnn;

            cmm.CommandText = "SELECT Id, Descricao FROM redesfarmaceuticas";

            if (oDB.openConnection(cmm)) {
                ds = oDB.QueryDS(ref cmm, ref ds, "Redes");
            }
            oDB.closeConnection(cmm);

            return ds;
        }

        public DataSet GetUf(string scn)
        {
            DataSet ds = new DataSet();

            MySqlConnection cnn = new MySqlConnection(scn);
            cmm.Connection = cnn;

            cmm.CommandText = "SELECT * FROM uf";

            if (oDB.openConnection(cmm))
            {
                ds = oDB.QueryDS(ref cmm, ref ds, "UF");
            }
            oDB.closeConnection(cmm);

            return ds;
        }

        public string AddLoja(string scn, Loja ol)
        {
            string msg = "";

            MySqlConnection cnn = new MySqlConnection(scn);
            cmm.Connection = cnn;

            string scnpj = ol.Cnpj.Replace(".", "");
            scnpj = scnpj.Replace("/", "");
            scnpj = scnpj.Replace("-", "");

            cmm.CommandText = "INSERT INTO farmacias (Proprietario, Gerente, Email, Email2, NomeFantasia, RazaoSocial, Cnpj, Endereco, Numero, Bairro, Complemento, Cidade, UF, Tel1, Tel2, Celular, Site, Skype, "
                +" Msn, Ativo, idRede)"
                +" VALUES ('"+ ol.Proprietario +"', '"+ ol.Gerente +"', '"+ ol.Email +"', '"+ ol.Email2 +"', '"
                + ol.NomeFantasia +"', '"+ ol.Razao +"', '"+ scnpj +"', '"+ ol.Endereco +"', '"+ ol.EndNumero
                +"', '"+ ol.Bairro +"', '"+ ol.Complemento +"', '"+ ol.Cidade +"', '"+ ol.Uf +"', '"+ ol.Fone 
                +"', '"+ ol.Fone2 +"', '"+ ol.Celular +"', '"+ ol.Site +"', '"+ ol.Skype +"', "
                +" '"+ ol.Msn +"', "+ (ol.Ativo == true ? 1 : 0) +", "+ ol.idRede +")";

            try
            {
                if (oDB.openConnection(cmm)) {
                    oDB.Execute(ref cmm);
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            oDB.closeConnection(cmm);

            return msg;
        }

        public string UpdateLoja(string scn, Loja clsLoja)
        {
            string msg = "";

            MySqlConnection cnn = new MySqlConnection(scn);
            cmm.Connection = cnn;

            string scnpj = clsLoja.Cnpj.Replace(".", "");
            scnpj = scnpj.Replace("/", "");
            scnpj = scnpj.Replace("-", "");

            cmm.CommandText = "UPDATE farmacias SET Proprietario = '" + clsLoja.Proprietario + "', Gerente = '"
                + clsLoja.Gerente + "', Email = '" + clsLoja.Email + "', Email2 = '" + clsLoja.Email2
                + "', NomeFantasia = '" + clsLoja.NomeFantasia + "', RazaoSocial = '" + clsLoja.Razao
                + "', Cnpj = '" + scnpj + "', Endereco = '" + clsLoja.Endereco + "', Numero = '"
                + clsLoja.EndNumero + "', Bairro = '" + clsLoja.Bairro + "', Complemento = '"
                + clsLoja.Complemento + "', Cidade = '" + clsLoja.Cidade + "', UF = '" + clsLoja.Uf
                + "', Tel1 = '" + clsLoja.Fone + "', Tel2 = '" + clsLoja.Fone2 + "', Celular = '"
                + clsLoja.Celular + "', Site = '" + clsLoja.Site + "', Skype = '" + clsLoja.Skype + "', "
                + " Msn = " + " '" + clsLoja.Msn + "', Ativo = " + (clsLoja.Ativo == true ? 1 : 0)
                + ", idRede = " + clsLoja.idRede + " WHERE Id = " + clsLoja.Id;

            try
            {
                if (oDB.openConnection(cmm))
                {
                    oDB.Execute(ref cmm);
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            oDB.closeConnection(cmm);

            return msg;
        }

        public string AddXml(string scn, XmlDocument xd, Usuario u)
        {
            MySqlConnection cnn = new MySqlConnection(scn);
            cmm.Connection = cnn;

            DataSet ds = new DataSet();

            ds.ReadXml(new XmlNodeReader(xd));
            string msg = "";

            if (ds.Tables.Count > 0) {
                try
                {
                    if (oDB.openConnection(cmm))
                    {
                        cmm.CommandText = "SELECT arquivosenviados.id FROM arquivosenviados"
                            + " WHERE arquivosenviados.cnpj = '" + ds.Tables[0].Rows[0]["cnpj"].ToString()
                            + "' AND arquivosenviados.mes = " + ds.Tables[0].Rows[0]["mes"]
                            + " AND arquivosenviados.ano = " + ds.Tables[0].Rows[0]["ano"];

                        int id = 0;
                        if (oDB.Query(id, ref cmm) == DBNull.Value)
                        {
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                cmm.CommandText = "INSERT INTO base_clientes (Cnpj, Mes, Ano, Barras, Descricao, Fabricante, Quantidade, Valor_Bruto, Valor_Liquido, Valor_Desconto)"
                                    + " VALUES ('" + ds.Tables[0].Rows[i]["cnpj"].ToString() + "', "
                                    + ds.Tables[0].Rows[i]["mes"] + ", " + ds.Tables[0].Rows[i]["ano"] + ", '"
                                    + ds.Tables[0].Rows[i]["ean"].ToString() + "', '"
                                    + ds.Tables[0].Rows[i]["nprod"].ToString() + "', '"
                                    + ds.Tables[0].Rows[i]["fab"].ToString() + "', " + ds.Tables[0].Rows[i]["quant"]
                                    + ", " + ds.Tables[0].Rows[i]["vbruto"] + ", " + ds.Tables[0].Rows[i]["vliquido"]
                                    + ", " + ds.Tables[0].Rows[i]["desconto"] + ")";

                                oDB.Execute(ref cmm);
                            }
                        }
                        else
                        {
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                cmm.CommandText = "UPDATE base_clientes SET Barras = '" 
                                    + ds.Tables[0].Rows[i]["ean"].ToString() 
                                    + "', Descricao = '" + ds.Tables[0].Rows[i]["nprod"].ToString()
                                    + "', Fabricante = '" + ds.Tables[0].Rows[i]["fab"].ToString()
                                    + "', Quantidade = " + ds.Tables[0].Rows[i]["quant"] + ", Valor_Bruto = "
                                    + ds.Tables[0].Rows[i]["vbruto"] + ", Valor_Liquido = "
                                    + ds.Tables[0].Rows[i]["vliquido"] + ", Valor_Desconto = "
                                    + ds.Tables[0].Rows[i]["desconto"] + " WHERE Cnpj = '"
                                    + ds.Tables[0].Rows[i]["cnpj"].ToString() + "' AND Mes = "
                                    + ds.Tables[0].Rows[i]["mes"] + " AND Ano = " + ds.Tables[0].Rows[i]["ano"];

                                oDB.Execute(ref cmm);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    msg = ex.Message;
                }

                oDB.closeConnection(cmm);

                AddXmlData(scn, ds, u);

            } else { msg = "Erro ao converter o xml."; }

            return msg; 
        }

        private void AddXmlData(string scn, DataSet ds, Usuario u)
        {
            MySqlConnection cnn = new MySqlConnection(scn);
            cmm.Connection = cnn;

            if (ds.Tables.Count > 0)
            {
                try
                {
                    if (oDB.openConnection(cmm))
                    {
                        cmm.CommandText = "INSERT INTO arquivosenviados (UserId, cnpj, tipo, mes, ano)"
                            + " VALUES (" + u.UserId + ", '" + ds.Tables[0].Rows[0]["cnpj"].ToString()
                            + "', 'XML', " + ds.Tables[0].Rows[0]["mes"] + ", "
                            + ds.Tables[0].Rows[0]["ano"] + ")";

                        oDB.Execute(ref cmm);
                    }
                }
                catch (Exception)
                {
                }

                oDB.closeConnection(cmm);
            }
        }

        public DataSet GetFarmacias(string scn)
        {
            DataSet ds = new DataSet();
            MySqlConnection cnn = new MySqlConnection(scn);

            cmm.Connection = cnn;
            cmm.CommandText = "SELECT Id, NomeFantasia FROM farmacias";
            
            if (oDB.openConnection(cmm)) {
                ds = oDB.QueryDS(ref cmm, ref ds, "Farmacias");
            }
            oDB.closeConnection(cmm);
            
            return ds;
        }

        public string AddDbf(string scn, DataTable dt)
        {
            string msg = "";
            MySqlConnection msc = new MySqlConnection(scn);
            DataSet ds = new DataSet();

            cmm.Connection = msc;

            if (dt.Rows.Count > 0)
            {
                try
                {
                    if (oDB.openConnection(cmm))
                    {
                        cmm.CommandText = "SELECT * FROM produtos_base";
                        cmm.CommandText += " WHERE CodBarra IN (";
                        for (int i = 0; i < dt.Rows.Count; i++) {
                            if (i > 0) { cmm.CommandText += ","; }
                            cmm.CommandText += dt.Rows[i][16];
                        }
                        cmm.CommandText += ")";

                        ds = oDB.QueryDS(ref cmm, ref ds, "produtos_base");

                        if (ds.Tables.Count > 0) {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                if (ds.Tables[0].Select("CodBarra = '" + dt.Rows[i][16] +"'").Length == 0)
                                {
                                    cmm.CommandText = "INSERT INTO produtos_base (CodBarra, CodProd, NomeProd, Apresenta, CodLab,"
                                        + " Grupo, `NomePat-NCM`, ALiq, St)"
                                        + " VALUES ('" + dt.Rows[i][16] + "', " + dt.Rows[i][0] + ", '" + dt.Rows[i][1]
                                        + "', '" + dt.Rows[i][35] + "', '" + dt.Rows[i][11] + "',"
                                        + " '" + dt.Rows[i][2] + "', '" + dt.Rows[i][41] + "', " + dt.Rows[i][54] + ", '" + dt.Rows[i][59] + "')";

                                    oDB.Execute(ref cmm);
                                }
                                else {
                                    cmm.CommandText = "UPDATE produtos_base SET CodProd = " + dt.Rows[i][0] 
                                        + ", NomeProd = '" + dt.Rows[i][1] + "', Apresenta = '" 
                                        + dt.Rows[i][35] + "', CodLab = '" + dt.Rows[i][11] + "', Grupo = '" 
                                        + dt.Rows[i][2] + "', `NomePat-NCM` = '" + dt.Rows[i][41] 
                                        + "', ALiq = " + dt.Rows[i][54] + ", St = '" + dt.Rows[i][59] + "'"
                                        + " WHERE CodBarra = " + dt.Rows[i][16];

                                    oDB.Execute(ref cmm);
                                }
                            }
                        } else {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                cmm.CommandText = "INSERT INTO produtos_base (CodBarra, CodProd, NomeProd, Apresenta, CodLab,"
                                + " Grupo, `NomePat-NCM`, ALiq, St)"
                                + " VALUES ('" + dt.Rows[i][16] + "', " + dt.Rows[i][0] + ", '" + dt.Rows[i][1]
                                + "', '" + dt.Rows[i][35] + "', '" + dt.Rows[i][11] + "',"
                                + " '" + dt.Rows[i][2] + "', '" + dt.Rows[i][41] + "', " + dt.Rows[i][54] + ", '" + dt.Rows[i][59] + "')";

                                oDB.Execute(ref cmm);
                            }
                        }
                    }
                    oDB.closeConnection(cmm);
                }
                catch (Exception)
                {

                }
            }
            else { msg = "Erro na gravação dos dados"; }
            return msg;
        }

        public string AddTxt(string scn, DataTable dt, Usuario u)
        {
            MySqlConnection cnn = new MySqlConnection(scn);
            cmm.Connection = cnn;

            string msg = "";

            if (dt.Rows.Count > 0)
            {
                try
                {
                    if (oDB.openConnection(cmm))
                    {
                        cmm.CommandText = "SELECT arquivosenviados.id FROM arquivosenviados"
                            + " WHERE arquivosenviados.cnpj = '" + dt.Rows[0]["cnpj"].ToString()
                            + "' AND arquivosenviados.mes = " + dt.Rows[0][2]
                            + " AND arquivosenviados.ano = " + dt.Rows[0]["ano"];

                        int id = 0;
                        if (oDB.Query(id, ref cmm) == DBNull.Value)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                string svb = "", svl = "", svd = "";

                                svb = dt.Rows[i][10].ToString().Replace(".", "");
                                svb = svb.Replace(",", ".");

                                svl = dt.Rows[i][11].ToString().Replace(".", "");
                                svl = svl.Replace(",", ".");

                                svd = dt.Rows[i][12].ToString().Replace(".", "");
                                svd = svd.Replace(",", ".");

                                cmm.CommandText = "INSERT INTO base_clientes (Razao_Social, Cnpj, Mes, Ano, Barras, Descricao,"
                                    + " Fabricante, Quantidade, Valor_Bruto, Valor_Liquido, Valor_Desconto)"
                                    + " VALUES ('" + dt.Rows[i][0].ToString().Replace("'", "''") + "', '"
                                    + dt.Rows[i][1].ToString() + "', " + dt.Rows[i][2] + ", " + dt.Rows[i][3]
                                    + ", '" + dt.Rows[i][4].ToString() + "', '"
                                    + dt.Rows[i][5].ToString().Replace("'", "''") + "', '"
                                    + dt.Rows[i][6].ToString().Replace("'", "''") + "', " + dt.Rows[i][9] + ", "
                                    + svb + ", " + svl + ", " + svd + ")";

                                oDB.Execute(ref cmm);
                            }
                        }
                        else {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                string svb = "", svl = "", svd = "";

                                svb = dt.Rows[i][10].ToString().Replace(".", "");
                                svb = svb.Replace(",", ".");

                                svl = dt.Rows[i][11].ToString().Replace(".", "");
                                svl = svl.Replace(",", ".");

                                svd = dt.Rows[i][12].ToString().Replace(".", "");
                                svd = svd.Replace(",", ".");

                                cmm.CommandText = "UPDATE base_clientes SET Razao_Social = '"
                                    + dt.Rows[i][0].ToString().Replace("'", "''") + "', Barras = '" 
                                    + dt.Rows[i][4].ToString()  + "', Descricao = '" 
                                    + dt.Rows[i][5].ToString().Replace("'", "''") + "', Fabricante = '" 
                                    + dt.Rows[i][6].ToString().Replace("'", "''") + "', Quantidade = " 
                                    + dt.Rows[i][9] + ", Valor_Bruto = " + svb + ", Valor_Liquido = " + svl 
                                    + ", Valor_Desconto = " + svd + " WHERE Cnpj = '" 
                                    + dt.Rows[i][1].ToString() + "' AND Mes = " + dt.Rows[i][2] +" AND Ano = " 
                                    + dt.Rows[i][3];

                                oDB.Execute(ref cmm);
                            }
                        }
                        
                    }
                }
                catch (Exception ex)
                {
                    msg = ex.Message;
                }

                oDB.closeConnection(cmm);

                this.AddTxtData(scn, dt, u);

            }
            else { msg = "Erro ao converter o txt."; }

            return msg; 
        }

        private void AddTxtData(string scn, DataTable dt, Usuario u)
        {
            MySqlConnection cnn = new MySqlConnection(scn);
            cmm.Connection = cnn;

            if (dt.Rows.Count > 0)
            {
                try
                {
                    if (oDB.openConnection(cmm))
                    {
                        cmm.CommandText = "INSERT INTO arquivosenviados (UserId, cnpj, tipo, mes, ano)"
                            + " VALUES (" + u.UserId + ", '" + dt.Rows[0]["cnpj"].ToString() + "', 'TXT', " 
                            + dt.Rows[0][2] + ", " + dt.Rows[0]["ano"] + ")";

                        oDB.Execute(ref cmm);
                    }
                }
                catch (Exception)
                {
                }

                oDB.closeConnection(cmm);
            }
        }

        public List<clsRelat1> GetCross(string scn, Usuario ou, string ano)
        {
            List<clsRelat1> lr = new List<clsRelat1>();
            MySqlConnection cnn = new MySqlConnection(scn);
            DataSet ds = new DataSet();

            cmm.Connection = cnn;
            string SQL = "SELECT (CASE WHEN base_clientes.Razao_Social IS NULL THEN ("
                + " SELECT farmacias.RazaoSocial FROM farmacias WHERE farmacias.Cnpj = base_clientes.Cnpj"
                + " ) ELSE base_clientes.Razao_Social END) AS Razao_Social, base_clientes.Cnpj, "
                + " base_clientes.Mes, produtos_base.Sub_Consultoria, produtos_base.Grupo,"
                + " SUM(base_clientes.Quantidade) AS 'Soma De Quantidade',"
                + " SUM(base_clientes.Valor_Bruto) AS 'Soma De Valor bruto',"
                + " SUM(base_clientes.Valor_Liquido) AS 'Soma De Valor liquido',"
                + " SUM(base_clientes.Valor_Desconto) AS 'Soma De Valor desconto'"
                + " FROM"
                + " base_clientes"
                + " LEFT JOIN produtos_base ON base_clientes.Barras = produtos_base.CodBarra"
                + " WHERE Ano = ";

            if (ano == "") { SQL += DateTime.Now.Year; } else {
                SQL += ano;
            }


            if (ou.Access == "nvg")
            {
                cmm.CommandText += "SELECT"
                    + " farmacias.Cnpj"
                    + " FROM"
                    + " memberships"
                    + " INNER JOIN redesfarmaceuticas ON memberships.UserId = redesfarmaceuticas.UserId"
                    + " INNER JOIN farmacias ON redesfarmaceuticas.Id = farmacias.idRede"
                    + " WHERE memberships.UserId = " + ou.UserId;

                if (oDB.openConnection(cmm))
                {
                    ds = oDB.QueryDS(ref cmm, ref ds, "Cnpj");
                }
                oDB.closeConnection(cmm);

                if (ds.Tables.Count > 0)
                {
                    ou.Cnpj = new List<string>();
                    for (int i = 0; i < ds.Tables["Cnpj"].Rows.Count; i++)
                    {
                        ou.Cnpj.Add(ds.Tables["Cnpj"].Rows[i]["Cnpj"].ToString());
                    }
                }

                if (ou.Cnpj.Count > 0) {
                    SQL += " AND base_clientes.Cnpj IN ('";
                    for (int i = 0; i < ou.Cnpj.Count; i++)
                    {
                        if (i == 0) { SQL += ou.Cnpj[i]; } else { SQL += "', '" + ou.Cnpj[i]; }
                    }
                    SQL += "')";
                }
            }
            else if (ou.Access == "nvp")
            {
                cmm.CommandText += "SELECT"
                    + " farmacias.Cnpj"
                    + " FROM"
                    + " usuarios_farmacias"
                    + " INNER JOIN farmacias ON usuarios_farmacias.FarmaciaId = farmacias.Id"
                    + " INNER JOIN memberships ON usuarios_farmacias.UserId = memberships.UserId"
                    + " WHERE memberships.UserId = " + ou.UserId;

                if (oDB.openConnection(cmm))
                {
                    ds = oDB.QueryDS(ref cmm, ref ds, "Cnpj");
                }
                oDB.closeConnection(cmm);

                if (ds.Tables.Count > 0)
                {
                    ou.Cnpj = new List<string>();
                    for (int i = 0; i < ds.Tables["Cnpj"].Rows.Count; i++)
                    {
                        ou.Cnpj.Add(ds.Tables["Cnpj"].Rows[i]["Cnpj"].ToString());
                    }
                }

                if (ou.Cnpj.Count > 0)
                {
                    SQL += " AND base_clientes.Cnpj IN ('";
                    for (int i = 0; i < ou.Cnpj.Count; i++)
                    {
                        if (i == 0) { SQL += ou.Cnpj[i]; } else { SQL += "', '" + ou.Cnpj[i]; }
                    }
                    SQL += "')";
                }
            }

            SQL += " GROUP BY Razao_Social, base_clientes.Razao_Social, base_clientes.Cnpj, produtos_base.Sub_Consultoria, base_clientes.Mes, produtos_base.Grupo"
                + " ORDER BY Razao_Social, base_clientes.Mes, produtos_base.Sub_Consultoria, produtos_base.Grupo";

            cmm.CommandText = SQL;
            cmm.CommandTimeout = 9999;

            if (oDB.openConnection(cmm))
            {
                ds = oDB.QueryDS(ref cmm, ref ds, "CrossR1");
            }
            oDB.closeConnection(cmm);

            try
            {
                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables["CrossR1"].Rows.Count; i++)
                    {
                        clsRelat1 or = new clsRelat1();

                        or.Razao = ds.Tables["CrossR1"].Rows[i]["Razao_Social"].ToString();
                        or.Cnpj = MaskCnpj(ds.Tables["CrossR1"].Rows[i]["Cnpj"].ToString());
                        or.SubConsultoria = ds.Tables["CrossR1"].Rows[i]["Sub_Consultoria"].ToString();
                        or.Mes = (int)ds.Tables["CrossR1"].Rows[i]["Mes"];
                        or.Grupo = ds.Tables["CrossR1"].Rows[i]["Grupo"].ToString();
                        or.SomaDeQuantidade = Convert.ToDecimal(ds.Tables["CrossR1"].Rows[i]["Soma De Quantidade"].ToString());
                        or.SomaDeValorBruto = Convert.ToDecimal(ds.Tables["CrossR1"].Rows[i]["Soma De Valor bruto"].ToString());
                        or.SomaDeValorLiquido = Convert.ToDecimal(ds.Tables["CrossR1"].Rows[i]["Soma De Valor liquido"].ToString());
                        or.SomaDeValorDesconto = Convert.ToDecimal(ds.Tables["CrossR1"].Rows[i]["Soma De Valor desconto"].ToString());
                        if (or.SomaDeValorDesconto > 0)
                        {
                            if (or.SomaDeValorBruto > 0) { or.PercentualDesconto = Convert.ToDecimal(((or.SomaDeValorDesconto / or.SomaDeValorBruto) * 100).ToString("N2")); }
                        }
                        else { or.PercentualDesconto = 0; }

                        lr.Add(or);
                    }
                }
            }
            finally
            {

            }

            return lr;
        }

        private string MaskCnpj(string p)
        {
            string cnpj = "";
            if (string.IsNullOrEmpty(p))
                return cnpj;
            else {
                cnpj = p.Substring(0, 2) + ".";
                cnpj += p.Substring(2, 3) + ".";
                cnpj += p.Substring(5, 3) + "/";
                cnpj += p.Substring(8, 4) + "-";
                cnpj += p.Substring(12, 2);

                return cnpj;
            }
        }

        public DataSet GetUsers(string scn)
        {
            DataSet ds = new DataSet();
            MySqlConnection cnn = new MySqlConnection(scn);

            cmm.Connection = cnn;
            cmm.CommandText = "SELECT users.UserId, users.UserName"
                + " FROM  users INNER JOIN"
                + " memberships ON users.UserId = memberships.UserId"
                + " WHERE memberships.Inactive = 0";

            if (oDB.openConnection(cmm))
            {
                ds = oDB.QueryDS(ref cmm, ref ds, "Users");
            }
            oDB.closeConnection(cmm);

            return ds;
        }

        public Usuario GetUserEdit(string scn, string p)
        {
            DataSet ds = new DataSet();
            MySqlConnection cnn = new MySqlConnection(scn);
            Usuario u = new Usuario();

            cmm.Connection = cnn;
            cmm.CommandText = "SELECT users.UserId, users.UserName, memberships.Email,"
                +" memberships.Inactive, memberships.ExpirationDate, memberships.Access, "
                +" memberships.Name, usuarios_farmacias.FarmaciaId"
                +" FROM  users INNER JOIN"
                +" memberships ON users.UserId = memberships.UserId LEFT OUTER JOIN"
                +" usuarios_farmacias ON users.UserId = usuarios_farmacias.UserId"
                +" WHERE (memberships.Inactive = 0) AND (users.UserId = "+ p +")";

            if (oDB.openConnection(cmm))
            {
                ds = oDB.QueryDS(ref cmm, ref ds, "UserEd");
            }
            oDB.closeConnection(cmm);

            if (ds.Tables.Count > 0)
            {
                u.UserId = Convert.ToInt16(ds.Tables[0].Rows[0]["UserId"].ToString());
                u.Inactive = (bool)(ds.Tables[0].Rows[0]["Inactive"].ToString() == "0" ? false : true);
                u.FarmaciaId = Convert.ToInt16(ds.Tables[0].Rows[0]["FarmaciaId"].ToString() == "" ? 0 : ds.Tables[0].Rows[0]["FarmaciaId"]);
                u.AcsName = o.denc(ds.Tables[0].Rows[0]["Name"].ToString());
                u.Email = ds.Tables[0].Rows[0]["Email"].ToString();
                u.Name = ds.Tables[0].Rows[0]["UserName"].ToString();
                u.ExpirationDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["ExpirationDate"].ToString());
                u.Access = o.denc(ds.Tables[0].Rows[0]["Access"].ToString());
            }

            return u;
        }

        public string UpdateUser(Usuario u, string scn)
        {
            string msg = "";

            try
            {
                string pass = o.encr(u.Password);
                string acs = o.encr(u.Access);
                string nme = o.encr(u.AcsName.ToUpper());

                MySqlConnection cnn = new MySqlConnection(scn);
                cmm.Connection = cnn;

                if (oDB.openConnection(cmm))
                {
                    string lDate = DateTime.Today.Year + "-" + DateTime.Today.Month + "-" + DateTime.Today.Day + " 00:00:00";

                    cmm.CommandText = "UPDATE users SET UserName = '" + u.Name + "', LastActivityDate = '" 
                        + lDate + "' WHERE UserId = "+ u.UserId;

                        oDB.Execute(ref cmm);

                        string eDate = u.ExpirationDate.Year + "-" + u.ExpirationDate.Month + "-" + u.ExpirationDate.Day + " 00:00:00";

                        cmm.CommandText = "UPDATE memberships SET Password = '" + pass + "', Email = '" 
                            + u.Email + "', Inactive = " + (u.Inactive == false ? 0 : 1) 
                            + ", ExpirationDate = '" + eDate + "', Access = '" + acs + "', Name = '" + nme 
                            + "' WHERE UserId = "+ u.UserId;

                        oDB.Execute(ref cmm);

                        cmm.CommandText = "UPDATE usuarios_farmacias SET FarmaciaId = " + u.FarmaciaId 
                            + " WHERE UserId = "+ u.UserId;

                        oDB.Execute(ref cmm);
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

            return msg;
        }

        public Rede GetRedesEdit(string scn, string p)
        {
            DataSet ds = new DataSet();
            MySqlConnection cnn = new MySqlConnection(scn);
            Rede r = new Rede();

            cmm.Connection = cnn;
            cmm.CommandText = "SELECT * FROM  redesfarmaceuticas WHERE (redesfarmaceuticas.Id = " + p + ")";

            if (oDB.openConnection(cmm))
            {
                ds = oDB.QueryDS(ref cmm, ref ds, "RedeEd");
            }
            oDB.closeConnection(cmm);

            if (ds.Tables.Count > 0)
            {
                r.RedeId = Convert.ToInt16(ds.Tables[0].Rows[0]["Id"].ToString());
                r.RedeName = ds.Tables[0].Rows[0]["Descricao"].ToString();
                r.UserId = Convert.ToInt16(ds.Tables[0].Rows[0]["UserId"].ToString() == "" ? 0 : ds.Tables[0].Rows[0]["UserId"]);
            }

            return r;
        }


        public Loja GetLojaEdit(string scn, string p)
        {
            DataSet ds = new DataSet();
            MySqlConnection cnn = new MySqlConnection(scn);
            Loja clsLoja = new Loja();

            cmm.Connection = cnn;
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("SELECT farmacias.Id, farmacias.Proprietario, farmacias.Gerente, farmacias.Email,");
            strSQL.Append(" farmacias.Email2, farmacias.NomeFantasia, farmacias.RazaoSocial, farmacias.Cnpj,");
            strSQL.Append(" farmacias.Endereco, farmacias.Numero, farmacias.Bairro, farmacias.Complemento,");
            strSQL.Append(" farmacias.Cidade, farmacias.UF, farmacias.Tel1, farmacias.Tel2, farmacias.Celular,");
            strSQL.Append(" farmacias.Site, farmacias.Skype, farmacias.Msn, farmacias.Ativo, farmacias.idRede");
            strSQL.Append(" FROM farmacias WHERE (farmacias.Id = @Id)");

            cmm.CommandText = strSQL.ToString();
            cmm.Parameters.Clear();
            cmm.Parameters.Add("@Id", MySqlDbType.Int32).Value = p;

            if (oDB.openConnection(cmm))
            {
                ds = oDB.QueryDS(ref cmm, ref ds, "LojaEd");
            }
            oDB.closeConnection(cmm);

            if (ds.Tables.Count > 0)
            {
                clsLoja.Id = Convert.ToInt16(ds.Tables[0].Rows[0]["Id"].ToString());
                clsLoja.Proprietario = ds.Tables[0].Rows[0]["Proprietario"].ToString();
                clsLoja.Gerente = ds.Tables[0].Rows[0]["Gerente"].ToString();
                clsLoja.Email = ds.Tables[0].Rows[0]["Email"].ToString();
                clsLoja.Email2 = ds.Tables[0].Rows[0]["Email2"].ToString();
                clsLoja.NomeFantasia = ds.Tables[0].Rows[0]["NomeFantasia"].ToString();
                clsLoja.Razao = ds.Tables[0].Rows[0]["RazaoSocial"].ToString();
                clsLoja.Cnpj = ds.Tables[0].Rows[0]["Cnpj"].ToString();
                clsLoja.Endereco = ds.Tables[0].Rows[0]["Endereco"].ToString();
                clsLoja.EndNumero = Convert.ToInt32(ds.Tables[0].Rows[0]["Numero"].ToString());
                clsLoja.Bairro = ds.Tables[0].Rows[0]["Bairro"].ToString();
                clsLoja.Complemento = ds.Tables[0].Rows[0]["Complemento"].ToString();
                clsLoja.Cidade = ds.Tables[0].Rows[0]["Cidade"].ToString();
                clsLoja.Uf = ds.Tables[0].Rows[0]["UF"].ToString();
                clsLoja.Fone = ds.Tables[0].Rows[0]["Tel1"].ToString();
                clsLoja.Fone2 = ds.Tables[0].Rows[0]["Tel2"].ToString();
                clsLoja.Celular = ds.Tables[0].Rows[0]["Celular"].ToString();
                clsLoja.Site = ds.Tables[0].Rows[0]["Site"].ToString();
                clsLoja.Skype = ds.Tables[0].Rows[0]["Skype"].ToString();
                clsLoja.Msn = ds.Tables[0].Rows[0]["Msn"].ToString();
                clsLoja.Ativo = (ds.Tables[0].Rows[0]["Ativo"].ToString() == "1" ? true : false );
                clsLoja.idRede = Convert.ToInt32(ds.Tables[0].Rows[0]["idRede"].ToString());
            }

            return clsLoja;
        }

    }
}
