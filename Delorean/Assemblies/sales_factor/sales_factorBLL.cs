using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assemblies
{
    public class sales_factorBLL
    {
        #region .:Searches:.
		public static List<sales_factorTO> getAll()
        {
            List<sales_factorTO> lst;
            lst = helpers.GetFromCache<List<sales_factorTO>>("factors" + helpers.GetSession().UserId,false);
            
            if (lst == null)
            {
                lst = sales_factorDAL.getAll();
                helpers.SetCache("factors" + helpers.GetSession().UserId, lst, false);
            }

            return lst;
        }

        public static List<sales_factorTO> getByFilter(string p1, string p2)
        {
            List<sales_factorTO> lst;
            lst = helpers.GetFromCache<List<sales_factorTO>>("factors" + helpers.GetSession().UserId, false);

            if (lst == null)
            {
                lst = sales_factorDAL.getByFilter(p1, p2);
                helpers.SetCache("factors" + helpers.GetSession().UserId, lst, false);
            }

            return lst;
        }
        #endregion

        #region .:Persistences:.
        public static List<sales_factorTO> upCond(int id, string p)
        {
            List<sales_factorTO> lst = new List<sales_factorTO>();
            sales_factorTO o;

            lst = helpers.GetFromCache<List<sales_factorTO>>("factors" + helpers.GetSession().UserId, false);

            int iCond = 0;
            if (int.TryParse(p, out iCond))
            {
                o = sales_factorDAL.upCond(id, iCond);
                lst[lst.FindIndex(i => i.id == o.id)] = o;
            }

            helpers.SetCache("factors" + helpers.GetSession().UserId, lst, false);

            return lst;
        }

        public static List<sales_factorTO> upMargin(int id, string p)
        {
            List<sales_factorTO> lst = new List<sales_factorTO>();
            sales_factorTO o;

            lst = helpers.GetFromCache<List<sales_factorTO>>("factors" + helpers.GetSession().UserId, false);

            decimal dMargin = 0;
            if (decimal.TryParse(p, out dMargin))
            {
                o = sales_factorDAL.upMargin(id, dMargin);
                lst[lst.FindIndex(i => i.id == o.id)] = o;
            }

            helpers.SetCache("factors" + helpers.GetSession().UserId, lst, false);

            return lst;
        }

        public static List<sales_factorTO> upDesc(int id, string p)
        {
            List<sales_factorTO> lst = new List<sales_factorTO>();
            sales_factorTO o;

            lst = helpers.GetFromCache<List<sales_factorTO>>("factors" + helpers.GetSession().UserId, false);

            decimal dDesc = 0;
            if (decimal.TryParse(p, out dDesc))
            {
                o = sales_factorDAL.upDesc(id, dDesc);
                lst[lst.FindIndex(i => i.id == o.id)] = o;
            }

            helpers.SetCache("factors" + helpers.GetSession().UserId, lst, false);

            return lst;
        }
        #endregion

        #region .:Methods:.
        public static List<sales_factorTO> setCond(int id, string p, string s)
        {
            List<sales_factorTO> lst = new List<sales_factorTO>();

            if (id > 0)
            {
                lst = upCond(id, p);
            }
            else
            {
                sales_factorTO o;
                int iCond = 0;

                lst = helpers.GetFromCache<List<sales_factorTO>>("factors" + helpers.GetSession().UserId, false);

                if (lst.FindAll(t => t.id == id && t.barras == s).Count > 0)
                {
                    if (int.TryParse(p, out iCond))
                    {
                        o = lst.Find(i => i.barras == s);
                        o.cont_bxs = iCond;
                        sales_factorDAL.insert(o);
                        lst[lst.FindIndex(i => i.barras == s)] = o;
                    }
                }

                helpers.SetCache("factors" + helpers.GetSession().UserId, lst, false);
            }

            return lst;
        }

        public static List<sales_factorTO> setMargin(int id, string p, string s)
        {
            List<sales_factorTO> lst = new List<sales_factorTO>();

            if (id > 0)
            {
                lst = upMargin(id, p);
            }
            else
            {
                sales_factorTO o;

                lst = helpers.GetFromCache<List<sales_factorTO>>("factors" + helpers.GetSession().UserId, false);

                if (lst.FindAll(t => t.id == id && t.barras == s).Count > 0)
                {
                    int iMarg = 0;
                    if (int.TryParse(p, out iMarg))
                    {
                        o = lst.Find(i => i.barras == s);
                        o.cont_bxs = iMarg;
                        sales_factorDAL.insert(o);
                        lst[lst.FindIndex(i => i.barras == s)] = o;
                    }
                }

                helpers.SetCache("factors" + helpers.GetSession().UserId, lst, false);
            }

            return lst;
        }

        public static List<sales_factorTO> setDesc(int id, string p, string s)
        {
            List<sales_factorTO> lst = new List<sales_factorTO>();

            if (id > 0)
            {
                lst = upDesc(id, p);
            }
            else
            {
                sales_factorTO o;

                lst = helpers.GetFromCache<List<sales_factorTO>>("factors" + helpers.GetSession().UserId, false);

                if (lst.FindAll(t => t.id == id && t.barras == s).Count > 0)
                {
                    int iDesc = 0;
                    if (int.TryParse(p, out iDesc))
                    {
                        o = lst.Find(i => i.barras == s);
                        o.cont_bxs = iDesc;
                        sales_factorDAL.insert(o);
                        lst[lst.FindIndex(i => i.barras == s)] = o;
                    }
                }

                helpers.SetCache("factors" + helpers.GetSession().UserId, lst, false);
            }

            return lst;
        }
        #endregion
    }
}
