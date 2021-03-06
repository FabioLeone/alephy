﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIAO.SRV.DAL;
using SIAO.SRV.TO;

namespace SIAO.SRV.BLL
{
    public class RolesBLL
    {
        #region .: Search :.
        public static List<TO.RolesTO> GetByRedeId(int intRedeId, string strConnection)
        {
            return RolesDAL.GetByRedeId(intRedeId, strConnection);
        }

        public static List<RelatoriosTO> GetRelatoriosByRedeId(int RedeId)
        {
            return RolesDAL.GetRelatoriosByRedeId(RedeId);
        }
        #endregion

        #region .: Persistence :.
        public static bool Insert(TO.RolesTO clsRole, string strConnection)
        {
            return RolesDAL.Insert(clsRole, strConnection);
        }
        /// <summary>
        /// Faz o insert/update das informações no banco
        /// </summary>
        /// <param name="lstRoles">Lista de Roles</param>
        /// <param name="lstRelatorios">Lista de Relatorios</param>
        /// <returns>True/False</returns>
        public static bool InsertUpdate(List<RolesTO> lstRoles, List<RelatoriosTO> lstRelatorios, string strConnection)
        {
            try
            {
                lstRoles.ForEach(delegate(RolesTO _role)
                {
                    if (_role.Add)
                    {
                        if (Insert(_role, strConnection))
                            Insert(lstRelatorios, strConnection);
                    }
                    else
                    {
                        Update(_role, strConnection);
                        Update(lstRelatorios, strConnection, _role.UserId);
                    }
                });
                return true;
            }
            catch
            {
                return false;
            }
        }
        private static void Insert(List<RelatoriosTO> lstRelatorios, string strConnection)
        {
            lstRelatorios.ForEach(delegate(RelatoriosTO _relatorio)
            {
                Insert(_relatorio, strConnection);
            });
        }

        private static void Insert(RelatoriosTO clsRelatorio, string strConnection)
        {
            RolesDAL.Insert(clsRelatorio, strConnection);
        }
        public static void Update(TO.RolesTO clsRole, string strConnection)
        {
            RolesDAL.Update(clsRole, strConnection);
        }

        private static void Update(List<RelatoriosTO> lstRelatorios, string strConnection, int intUserId)
        {
            Delete(lstRelatorios.Find(r=>r.Rede_Id == intUserId), strConnection);
            lstRelatorios.FindAll(r=>r.Rede_Id == intUserId).ForEach(delegate(RelatoriosTO _relatorio)
            {
                Insert(_relatorio, strConnection);
            });
        }

        private static void Delete(RelatoriosTO clsRelatorios, string strConnection)
        {
            if (clsRelatorios == null)
                return;
            else
                RolesDAL.Delete(clsRelatorios, strConnection);
        }
        #endregion

    }
}
