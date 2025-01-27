﻿using CapaDatos.Contracts;
using CapaDatos.Entities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace CapaDatos.Repositories
{
    public class DoctorRepository:MasterRepository,IDoctorRepository
    {
        private string insert;
        private string update;
        private string delete;
        private string selectAll;
        private string selectByName;

        public DoctorRepository()
        {
            selectAll = "select * from doctores";
            selectByName = "select id_doctor, nombre from doctores";
            insert = "insert into doctores values(default,@idArea,@cedula,@nombre,@apellido,@telefono,@diasLaborales,@disponibilidad,@activo)";
            update = "update doctores set id_area=@idArea, cedula=@cedula, nombre=@nombre, apellido=@apellido, telefono=@telefono, diaslaborales=@diasLaborales, disponibilidad=@disponibilidad, activo=@activo where id_doctor=@id";
            delete = "delete from doctores where id_doctor=@id";

            
        }
        public int Create(Doctor Entity)
        {
            parametros = new List<MySqlParameter>();
            parametros.Add(new MySqlParameter("@idArea", Entity.idArea));
            parametros.Add(new MySqlParameter("@cedula",Entity.cedula));
            parametros.Add(new MySqlParameter("@nombre", Entity.nombre));
            parametros.Add(new MySqlParameter("@apellido", Entity.apellidos));
            parametros.Add(new MySqlParameter("@telefono", Entity.telefono));
            parametros.Add(new MySqlParameter("@diasLaborales", Entity.diasLaborales));
            parametros.Add(new MySqlParameter("@disponibilidad", Entity.disponibilidad));
            parametros.Add(new MySqlParameter("@activo", Entity.activo));
            return ExecuteNonQuery(insert);
        }

        public int Delete(int EntityID)
        {
            parametros = new List<MySqlParameter>();
            parametros.Add(new MySqlParameter("@id", EntityID));
            return ExecuteNonQuery(delete);
        }

        public IEnumerable<Doctor> Read()
        {
            DataTable resultadoTabla = ExecuteReader(selectAll);
            var listaDoctor = new List<Doctor>();
            foreach (DataRow item in resultadoTabla.Rows)
            {
                listaDoctor.Add(new Doctor
                {
                    idDoctor = Convert.ToInt32(item[0]),
                    idArea = Convert.ToInt32(item[1]),
                    cedula = item[2].ToString(),
                    nombre = item[3].ToString(),
                    apellidos = item[4].ToString(),
                    telefono = item[5].ToString(),
                    diasLaborales = item[6].ToString(),
                    disponibilidad = item[7].ToString(),
                    activo = item[8].ToString()

                });
            }
            return listaDoctor;
        }

        public int Update(Doctor Entity)
        {
            parametros = new List<MySqlParameter>();
            parametros.Add(new MySqlParameter("@id", Entity.idDoctor));
            parametros.Add(new MySqlParameter("@idArea", Entity.idArea));
            parametros.Add(new MySqlParameter("@cedula", Entity.cedula));
            parametros.Add(new MySqlParameter("@nombre", Entity.nombre));
            parametros.Add(new MySqlParameter("@apellido", Entity.apellidos));
            parametros.Add(new MySqlParameter("@telefono", Entity.telefono));
            parametros.Add(new MySqlParameter("@diasLaborales", Entity.diasLaborales));
            parametros.Add(new MySqlParameter("@disponibilidad", Entity.disponibilidad));
            parametros.Add(new MySqlParameter("@activo", Entity.activo));
            return ExecuteNonQuery(update);
        }

        public IEnumerable<Doctor> GetNames()
        {
            DataTable resultadoTabla = ExecuteReader(selectByName);
            var listaDoctor = new List<Doctor>();
            foreach (DataRow item in resultadoTabla.Rows)
            {
                listaDoctor.Add(new Doctor
                {
                    idDoctor = Convert.ToInt32(item[0].ToString()),
                    nombre = item[1].ToString(),
                });
            }
            return listaDoctor;
        }
    }
}
