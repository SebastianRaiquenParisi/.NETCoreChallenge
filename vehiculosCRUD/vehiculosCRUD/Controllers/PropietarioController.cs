using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using vehiculosCRUD.Models;
using Newtonsoft.Json;
using Microsoft.Data.SqlClient;
using System.Configuration;

namespace vehiculosCRUD.Controllers
{
    public class PropietarioController : Controller
    {

        
        public static async void SubirJSONaBaseDeDatos()
        {
            List<Propietario> propietarios = await GetPropietariosJSON3PrimerasPaginas();
            foreach (Propietario prop in propietarios)
            {
                if (!CheckIfPropietarioExists(prop)) { 
                    InsertPropietarioIntoPropietarios(prop);
                }
            }
        }

        private static void InsertPropietarioIntoPropietarios(Propietario propietario) {
            var conn = new SqlConnection("Server=DESKTOP-GE740DJ; Database=db_vehiculos; Trusted_Connection=True");
            SqlCommand insert_into_propietarios = new SqlCommand("INSERT into Propietarios (nombre,apellido) VALUES (@nombre,@apellido)");
            conn.Open();
            insert_into_propietarios.Connection = conn;
            insert_into_propietarios.Parameters.AddWithValue("@nombre", propietario.Nombre);
            insert_into_propietarios.Parameters.AddWithValue("@apellido", propietario.Apellido);

            insert_into_propietarios.ExecuteNonQuery();
            conn.Close();
        }

        private static bool CheckIfPropietarioExists(Propietario propietario) {
            bool propietarioExists = false;
            var conn = new SqlConnection("Server=DESKTOP-GE740DJ; Database=db_vehiculos; Trusted_Connection=True");
            conn.Open();
            SqlCommand check_propietario = new SqlCommand("SELECT COUNT(*) FROM [Propietarios] WHERE ([nombre] = @nombre) AND ([apellido] = @apellido)");
            check_propietario.Connection = conn;
            check_propietario.Parameters.AddWithValue("@nombre", propietario.Nombre);
            check_propietario.Parameters.AddWithValue("@apellido", propietario.Apellido);
            int UserExist = (int)check_propietario.ExecuteScalar();

            if (UserExist > 0)
            {
                propietarioExists = true;
            }
            conn.Close();
            return propietarioExists;
        }

        private static async Task<List<Propietario>> GetPropietariosJSON3PrimerasPaginas()
        {
            List<Propietario> propietarios = new List<Propietario>();
            propietarios = await GetPropietariosJSONPaginaNumero(1, propietarios);
            propietarios = await GetPropietariosJSONPaginaNumero(2, propietarios);
            propietarios = await GetPropietariosJSONPaginaNumero(3, propietarios);
            return propietarios;
        }

        private static async Task<List<Propietario>> GetPropietariosJSONPaginaNumero(int i, List<Propietario> propietarios)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("https://reqres.in/api/users?page=" + i.ToString());
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            if (!string.IsNullOrEmpty(responseBody))
            {
                propietarios = ParseJSONListarPropietarios(responseBody, propietarios);
            }
            return propietarios;
        }
        
        private static List<Propietario> ParseJSONListarPropietarios(string json, List<Propietario> propietarios)
        {
            var _propietariosDAO = Newtonsoft.Json.JsonConvert.DeserializeObject<PropietarioDAO>(json);
            if(_propietariosDAO!=null) {
                _propietario[] _propietarios = _propietariosDAO.data;
                
                foreach (_propietario _prop in _propietarios)
                {
                    Propietario prop = new Propietario();
                    prop.Id = _prop.id;
                    prop.Nombre = _prop.first_name;
                    prop.Apellido = _prop.last_name;
                    propietarios.Add(prop);
                }
            }
            return propietarios;
        }



        //findPropietario(id)
        //
        //
        //
        //
        //
        //}
        //
        //
        //subirPaginaNumero(num){
        //  url += num;
        //  url into json
        //  decryptPropietarios(JSON) => propietarios
        //      foreach(propietario : propietarios){
        //           subirPropietarioDatabase()
        //      }
        //  }
        //  
        //}
        //
        //
        //subir3PrimerasPaginasBD(){
        //  subirPaginaNumero(1)
        //  subirPaginaNumero(2)
        //  subirPaginaNumero(3)
        //}


    }
}
