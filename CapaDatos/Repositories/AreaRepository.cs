﻿using CapaDatos.Contracts;
using CapaDatos.Entities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace CapaDatos.Repositories
{
    public class AreaRepository : MasterRepository, IAreaRepository
    {
        private string insert;
        private string update;
        private string delete;
        private string selectAll;
        private string selectByName;

        public AreaRepository()
        {
            selectAll = "select * from areas";
            selectByName = "select id_area, nombre from areas";
            insert = "insert into areas values(default,@nombre,@edificio,@habilitada)";
            update = "update areas set nombre=@nombre, edificio=@edificio, habilitada=@habilitada where id_area=@id";
            delete = "delete from areas where id_area=@id";
        }

        public int Create(Area Entity)
        {
            parametros = new List<MySqlParameter>();
            parametros.Add(new MySqlParameter("@nombre", Entity.nombre));
            parametros.Add(new MySqlParameter("@edificio", Entity.edificio));
            parametros.Add(new MySqlParameter("@habilitada", Entity.habilitada));
            return ExecuteNonQuery(insert);
        }

        public int Delete(int EntityID)
        {
            parametros = new List<MySqlParameter>();
            parametros.Add(new MySqlParameter("@id", EntityID));
            return ExecuteNonQuery(delete);
        }

        public IEnumerable<Area> Read()
        {
            DataTable resultadoTabla = ExecuteReader(selectAll);
            var listaAreas = new List<Area>();
            foreach (DataRow item in resultadoTabla.Rows)
            {
                listaAreas.Add(new Area
                {
                    idArea = Convert.ToInt32(item[0]),
                    nombre = item[1].ToString(),
                    edificio = item[2].ToString(),
                    habilitada = item[3].ToString()
                });
            }
            return listaAreas;
        }

        public int Update(Area Entity)
        {
            parametros = new List<MySqlParameter>();
            parametros.Add(new MySqlParameter("@id", Entity.idArea));
            parametros.Add(new MySqlParameter("@nombre", Entity.nombre));
            parametros.Add(new MySqlParameter("@edificio", Entity.edificio));
            parametros.Add(new MySqlParameter("@habilitada", Entity.habilitada));
            
            return ExecuteNonQuery(update);
           
        }

        public IEnumerable<Area> GetNames()
        {
            DataTable resultadoTabla = ExecuteReader(selectByName);
            var listaAreas = new List<Area>();
            foreach (DataRow item in resultadoTabla.Rows)
            {
                listaAreas.Add(new Area
                {
                    idArea = Convert.ToInt32(item[0]),
                    nombre = item[1].ToString(),
                });
            }
            return listaAreas;
        }
    }
}
