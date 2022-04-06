using DevelSystem.Data;
using DevelSystem.Entities;
using DevelSystem.Helpers;
using DevelSystem.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace DevelSystem.Services
{
    public interface IEncuestaService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);

        string NuevaEncuesta(Encuesta encuesta);
        string EditarEncuesta(Encuesta encuesta);
        string EliminarEncuesta(int idEncuesta);        
        User GetById(int id);
    }
    public class EncuestaService : IEncuestaService 
    {
        private readonly AppSettings _appSettings;
        private readonly ApplicationDbContext _db;

        public EncuestaService(IOptions<AppSettings> appSettings, ApplicationDbContext db)
        {
            _appSettings = appSettings.Value;
            _db = db;
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            User user = _db.Users.Where(x => x.Username == model.Username && x.Password == model.Password).FirstOrDefault();

            if (user == null) return null;

            var token = generateJwtToken(user);

            return new AuthenticateResponse(user, token);
        }

        public string NuevaEncuesta(Encuesta encuesta)
        {
            try
            {
                EncuestaCab oEntidad = new EncuestaCab();
                oEntidad.NombreEncuesta = encuesta.NombreEncuesta;
                oEntidad.DescripcionEncuesta = encuesta.DescripcionEncuesta;
                oEntidad.Estado = true;
                _db.EncuestaCabs.Add(oEntidad);
                _db.SaveChanges();

                EncuestaDet oEntidadDet;
                for (int i = 0; i < encuesta.Detalle.Count; i++)
                {
                    oEntidadDet = new EncuestaDet();
                    oEntidadDet.IdEncuesta = oEntidad.IdEncuesta;
                    oEntidadDet.NombreCampo = encuesta.Detalle[i].NombreCampo;
                    oEntidadDet.TituloCampo = encuesta.Detalle[i].TituloCampo;
                    oEntidadDet.EsRequerido = encuesta.Detalle[i].EsRequerido;
                    oEntidadDet.TipoCampo = encuesta.Detalle[i].TipoCampo;
                    _db.EncuestaDets.Add(oEntidadDet);
                    _db.SaveChanges();
                }
                return "Registrado Correctamente";
            }
            catch (Exception ex)
            {
                return "Error al registrar, Errror : " + ex.Message;
            }
        }

        public User GetById(int id)
        {
            return _db.Users.Where(x => x.Id == id).FirstOrDefault();
        }

        private string generateJwtToken(User user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public string EditarEncuesta(Encuesta encuesta)
        {
            try
            {
                EncuestaCab objCab = _db.EncuestaCabs.Where(x => x.IdEncuesta == encuesta.IdEncuesta).SingleOrDefault();
                List<EncuestaDet> objDet = _db.EncuestaDets.Where(x => x.IdEncuesta == encuesta.IdEncuesta).ToList();

                //Edtar Cabecera
                objCab.NombreEncuesta = encuesta.NombreEncuesta;
                objCab.DescripcionEncuesta = encuesta.DescripcionEncuesta;
                _db.SaveChanges();


                //Editar Detalle
                
                List<int> idLst1 = new List<int>(); //Id's obtenidos en BD
                List<int> idLst2 = new List<int>(); //Id's en Request

                //Validar Id's
                for (int i = 0; i < objDet.Count; i++)
                {
                    idLst1.Add(objDet[i].IdEncuestaDetalle);
                }

                for (int i = 0; i < encuesta.Detalle.Count; i++)
                {
                    idLst2.Add(encuesta.Detalle[i].IdEncuestaDetalle);
                }

                if (idLst1.Count() > 0 || idLst2.Count() > 0)
                {
                    var detallesEliminados = idLst1.Except(idLst2);
                    var detallesAgregados = idLst2.Except(idLst1);

                    foreach (var item in detallesEliminados)
                    {
                        int id = item;
                        EncuestaDet entidadDet = _db.EncuestaDets.Where(x => x.IdEncuestaDetalle == id).SingleOrDefault();
                        _db.EncuestaDets.Remove(entidadDet);
                        _db.SaveChanges();
                    }

                    foreach (var item in detallesAgregados)
                    {
                        int id = item;

                        DetalleEncuesta oEntidad = encuesta.Detalle.Where(x => x.IdEncuestaDetalle == id).SingleOrDefault();

                        EncuestaDet oEntidadDet = new EncuestaDet();
                        oEntidadDet.IdEncuesta = oEntidad.IdEncuesta;
                        oEntidadDet.NombreCampo = oEntidad.NombreCampo;
                        oEntidadDet.TituloCampo = oEntidad.TituloCampo;
                        oEntidadDet.EsRequerido = oEntidad.EsRequerido;
                        oEntidadDet.TipoCampo = oEntidad.TipoCampo;
                        _db.EncuestaDets.Add(oEntidadDet);
                        _db.SaveChanges();
                    }

                }

                for (int i = 0; i < objDet.Count; i++)
                {
                    for (int x = 0; x < encuesta.Detalle.Count(); x++)
                    {
                        EncuestaDet entidadDetalle = _db.EncuestaDets.Where(y => y.IdEncuestaDetalle == encuesta.Detalle[x].IdEncuestaDetalle).SingleOrDefault();
                        entidadDetalle.IdEncuesta = encuesta.Detalle[x].IdEncuesta;
                        entidadDetalle.NombreCampo = encuesta.Detalle[x].NombreCampo;
                        entidadDetalle.TituloCampo = encuesta.Detalle[x].TituloCampo;
                        entidadDetalle.EsRequerido = encuesta.Detalle[x].EsRequerido;
                        entidadDetalle.TipoCampo = encuesta.Detalle[x].TipoCampo;
                        _db.SaveChanges();
                    }
                }

                return "Actualizado Correctamente";
            }
            catch (Exception ex)
            {
                return "Error al actualizar los registros, Errror : " + ex.Message;
            }

        }

        public string EliminarEncuesta(int idEncuesta)
        {
            try
            {
                EncuestaCab objCab = _db.EncuestaCabs.Where(x => x.IdEncuesta == idEncuesta).SingleOrDefault();
                objCab.Estado = false;
                _db.SaveChanges();

                return "Eliminado Correctamente";
            }
            catch (Exception ex)
            {
                return "Error al eliminar el registro, Errror : " + ex.Message;
            }
            
        }
    }
}
