﻿using CapaDatos.Contracts;
using CapaDatos.Entities;
using CapaDatos.Repositories;
using CapaLogica.ValueObjects;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CapaLogica.Models
{
    public class UserModel
    {
        private int idUsuario;
        private string nombre;
        private string password;

        private IUsuarioRepository usuarioRepositorio;
        public EntityState estadoEntidad { private get; set; }

        public int IdUsuario { get => idUsuario; set => idUsuario = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Password { private get => password; set => password = value; }

        public UserModel()
        {
            usuarioRepositorio = new UsuarioRepository();
        }

        /// <summary>
        /// Observa cual es el <see cref="EntityState"/> de UserModel, luego se encarga de ejecutar los metodos insert, update o delete segun corresponda.
        /// </summary>
        /// <returns>Mensaja con respuesta</returns>
        public async Task<string> ejecutarAccion()
        {
            return await Task.Run(() =>
            {
                string message = null;
                try
                {
                    var usuarioDataModel = new Usuario();
                    usuarioDataModel.idUsuario = idUsuario;
                    usuarioDataModel.nombre = nombre;
                    usuarioDataModel.passUser = password;

                    switch (estadoEntidad)
                    {
                        case EntityState.Added:
                            usuarioRepositorio.Create(usuarioDataModel);
                            message = $"Se ha registrado {usuarioDataModel.nombre}, como un nuevo usuario.";
                            break;

                            /*case EntityState.Modified:
                                usuarioRepositorio.Update(areaDataModel);
                                message = $"El area con ID:{areaDataModel.idArea}, se ha modificado";
                                break;

                            case EntityState.Deleted:
                                usuarioRepositorio.Delete(idUsuario);
                                message = "Se ha elimiando correctamente";
                                break;
                            */
                    }

                }
                catch (Exception e)
                {
                    message = $"Un error ha ocurrido\n\nError:\n{e}";
                }
                return message;
            });
        }

        /// <summary>
        /// Comprobacion de datos para iniciar sesion en la aplicacion
        /// </summary>
        /// <returns></returns>
        public bool validarUsuario()
        {
            var usuarioDataModel = new Usuario();
            usuarioDataModel.nombre = nombre;
            usuarioDataModel.passUser = password;

            if (usuarioRepositorio.Login(usuarioDataModel))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Cifra la contraseña del usuario por metodo <see cref="SHA512"/>
        /// </summary>
        /// <param name="pwrd">Contraseña en texto plano</param>
        /// <returns>Contraseña en base64</returns>
        public string Encrypt(string pwrd)
        {
            SHA512 hashSvc = SHA512.Create();
            byte[] hash = hashSvc.ComputeHash(Encoding.UTF8.GetBytes(pwrd));
            return Convert.ToBase64String(hash).ToUpper();
        }
    }
}
